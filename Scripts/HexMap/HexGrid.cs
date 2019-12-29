﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace ErksUnityLibrary.HexMap
{
    public class HexGrid : MonoBehaviour
    {
        public int cellCountX = 20, cellCountZ = 15;
        private int chunkCountX, chunkCountZ;

        public HexGridChunk chunkPrefab;
        public HexCell cellPrefab;
        public Text cellLabelPrefab;

        public Texture2D noiseSource;

        public int seed = 1234;

        public HexUnit unitPrefab;

        private HexGridChunk[] chunks;
        private HexCell[] cells;

        private HexCellPriorityQueue searchFrontier;
        private int searchFrontierPhase;

        private HexCell currentPathFrom, currentPathTo;
        private bool currentPathExists;

        private List<HexUnit> units = new List<HexUnit>();

        private HexCellShaderData cellShaderData;

        void Awake()
        {
            HexMetrics.noiseSource = noiseSource;
            HexMetrics.InitializeHashGrid(seed);
            HexUnit.unitPrefab = unitPrefab;
            cellShaderData = gameObject.AddComponent<HexCellShaderData>();
            cellShaderData.Grid = this;
            CreateMap(cellCountX, cellCountZ);
        }

        void OnEnable()
        {
            if (!HexMetrics.noiseSource)
            {
                HexMetrics.noiseSource = noiseSource;
                HexMetrics.InitializeHashGrid(seed);
                HexUnit.unitPrefab = unitPrefab;
                ResetVisibility();
            }
        }

        public bool CreateMap(int x, int z)
        {
            if (x <= 0 || x % HexMetrics.chunkSizeX != 0 || z <= 0 || z % HexMetrics.chunkSizeZ != 0)
            {
                Debug.LogError("Unsupported map size.");
                return false;
            }

            ClearPath();
            ClearUnits();

            if (chunks != null)
            {
                for (int i = 0; i < chunks.Length; i++)
                {
                    Destroy(chunks[i].gameObject);
                }
            }

            cellCountX = x;
            cellCountZ = z;
            chunkCountX = cellCountX / HexMetrics.chunkSizeX;
            chunkCountZ = cellCountZ / HexMetrics.chunkSizeZ;

            cellShaderData.Initialize(cellCountX, cellCountZ);
            CreateChunks();
            CreateCells();

            return true;
        }

        public HexCell GetCell(HexCoordinates coordinates)
        {
            int z = coordinates.Z;
            if (z < 0 || z >= cellCountZ)
            {
                return null;
            }

            int x = coordinates.X + z / 2;
            if (x < 0 || x >= cellCountX)
            {
                return null;
            }

            return cells[x + z * cellCountX];
        }

        public void ShowUI(bool visible)
        {
            for (int i = 0; i < chunks.Length; i++)
            {
                chunks[i].ShowUI(visible);
            }
        }

        private void CreateChunks()
        {
            chunks = new HexGridChunk[chunkCountX * chunkCountZ];

            for (int z = 0, i = 0; z < chunkCountZ; z++)
            {
                for (int x = 0; x < chunkCountX; x++)
                {
                    HexGridChunk chunk = chunks[i++] = Instantiate(chunkPrefab);
                    chunk.transform.SetParent(transform);
                }
            }
        }

        void CreateCells()
        {
            cells = new HexCell[cellCountZ * cellCountX];

            for (int z = 0, i = 0; z < cellCountZ; z++)
            {
                for (int x = 0; x < cellCountX; x++)
                {
                    CreateCell(x, z, i++);
                }
            }
        }

        public void AddUnit(HexUnit unit, HexCell location, float orientation)
        {
            units.Add(unit);
            unit.Grid = this;
            unit.transform.SetParent(transform, false);
            unit.Location = location;
            unit.Orientation = orientation;
        }

        public void RemoveUnit(HexUnit unit)
        {
            units.Remove(unit);
            unit.Die();
        }

        private void ClearUnits()
        {
            for (int i = 0; i < units.Count; i++)
            {
                units[i].Die();
            }
            units.Clear();
        }

        public void FindPath(HexCell fromCell, HexCell toCell, HexUnit unit)
        {
            ClearPath();
            currentPathFrom = fromCell;
            currentPathTo = toCell;
            currentPathExists = Search(fromCell, toCell, unit);
            ShowPath(unit.Speed);
        }

        private void ShowPath(int speed)
        {
            if (currentPathExists)
            {
                HexCell current = currentPathTo;
                while (current != currentPathFrom)
                {
                    int turn = (current.Distance - 1) / speed;
                    current.SetLabel(turn.ToString());
                    current.EnableHighlight(Color.white);
                    current = current.PathFrom;
                }
            }
            currentPathFrom.EnableHighlight(Color.blue);
            currentPathTo.EnableHighlight(Color.red);
        }

        public List<HexCell> GetPath()
        {
            if (!currentPathExists)
            {
                return null;
            }
            List<HexCell> path = ListPool<HexCell>.Get();
            for (HexCell c = currentPathTo; c != currentPathFrom; c = c.PathFrom)
            {
                path.Add(c);
            }
            path.Add(currentPathFrom);
            path.Reverse();
            return path;
        }

        public bool HasPath
        {
            get
            {
                return currentPathExists;
            }
        }

        public void ClearPath()
        {
            if (currentPathExists)
            {
                HexCell current = currentPathTo;
                while (current != currentPathFrom)
                {
                    current.SetLabel(null);
                    current.DisableHighlight();
                    current = current.PathFrom;
                }
                current.DisableHighlight();
                currentPathExists = false;
            }
            else if (currentPathFrom)
            {
                currentPathFrom.DisableHighlight();
                currentPathTo.DisableHighlight();
            }
            currentPathFrom = currentPathTo = null;
        }

        private bool Search(HexCell fromCell, HexCell toCell, HexUnit unit)
        {
            searchFrontierPhase += 2;

            if (searchFrontier == null)
            {
                searchFrontier = new HexCellPriorityQueue();
            }
            else
            {
                searchFrontier.Clear();
            }

            fromCell.SearchPhase = searchFrontierPhase;
            fromCell.Distance = 0;
            searchFrontier.Enqueue(fromCell);

            while (searchFrontier.Count > 0)
            {
                HexCell current = searchFrontier.Dequeue();
                current.SearchPhase += 1;

                if (current == toCell)
                {
                    return true;
                }

                int currentTurn = (current.Distance - 1) / unit.Speed;

                for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
                {
                    HexCell neighbor = current.GetNeighbor(d);                    

                    // Continue conditions
                    if (neighbor == null || neighbor.SearchPhase > searchFrontierPhase)
                    {
                        continue;
                    }

                    // Move cost
                    if (!unit.IsValidDestination(neighbor))
                    {
                        continue;
                    }
                    int moveCost = unit.GetMoveCost(current, neighbor, d);
                    if (moveCost < 0)
                    {
                        continue;
                    }

                    // Turns
                    int distance = current.Distance + moveCost;
                    int turn = (distance - 1) / unit.Speed;
                    if (turn > currentTurn)
                    {
                        distance = turn * unit.Speed + moveCost;
                    }

                    // Update neighbor / frontier
                    if (neighbor.SearchPhase < searchFrontierPhase)
                    {
                        neighbor.SearchPhase = searchFrontierPhase;
                        neighbor.Distance = distance;
                        neighbor.PathFrom = current;
                        neighbor.SearchHeuristic = neighbor.coordinates.DistanceTo(toCell.coordinates);
                        searchFrontier.Enqueue(neighbor);
                    }
                    else if (distance < neighbor.Distance)
                    {
                        int oldPriority = neighbor.SearchPriority;
                        neighbor.Distance = distance;
                        neighbor.PathFrom = current;
                        searchFrontier.Change(neighbor, oldPriority);
                    }
                }
            }

            return false;
        }

        #region visibility
        private List<HexCell> GetVisibleCells(HexCell fromCell, int range)
        {
            List<HexCell> visibleCells = ListPool<HexCell>.Get();

            searchFrontierPhase += 2;
            if (searchFrontier == null)
            {
                searchFrontier = new HexCellPriorityQueue();
            }
            else
            {
                searchFrontier.Clear();
            }

            range += fromCell.ViewElevation;
            fromCell.SearchPhase = searchFrontierPhase;
            fromCell.Distance = 0;
            searchFrontier.Enqueue(fromCell);
            HexCoordinates fromCoordinates = fromCell.coordinates;
            while (searchFrontier.Count > 0)
            {
                HexCell current = searchFrontier.Dequeue();
                current.SearchPhase += 1;
                visibleCells.Add(current);

                for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
                {
                    HexCell neighbor = current.GetNeighbor(d);
                    if (neighbor == null || neighbor.SearchPhase > searchFrontierPhase || !neighbor.Explorable)
                    {
                        continue;
                    }

                    int distance = current.Distance + 1;
                    if (distance + neighbor.ViewElevation > range ||distance > fromCoordinates.DistanceTo(neighbor.coordinates))
                    {
                        continue;
                    }

                    if (neighbor.SearchPhase < searchFrontierPhase)
                    {
                        neighbor.SearchPhase = searchFrontierPhase;
                        neighbor.Distance = distance;
                        neighbor.SearchHeuristic = 0;
                        searchFrontier.Enqueue(neighbor);
                    }
                    else if (distance < neighbor.Distance)
                    {
                        int oldPriority = neighbor.SearchPriority;
                        neighbor.Distance = distance;
                        searchFrontier.Change(neighbor, oldPriority);
                    }
                }
            }
            return visibleCells;
        }

        public void IncreaseVisibility(HexCell fromCell, int range)
        {
            List<HexCell> cells = GetVisibleCells(fromCell, range);
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].IncreaseVisibility();
            }
            ListPool<HexCell>.Add(cells);
        }

        public void DecreaseVisibility(HexCell fromCell, int range)
        {
            List<HexCell> cells = GetVisibleCells(fromCell, range);
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].DecreaseVisibility();
            }
            ListPool<HexCell>.Add(cells);
        }

        public void ResetVisibility()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].ResetVisibility();
            }

            for (int i = 0; i < units.Count; i++)
            {
                HexUnit unit = units[i];
                IncreaseVisibility(unit.Location, unit.VisionRange);
            }
        }
        #endregion visibility

        private void CreateCell(int x, int z, int i)
        {
            // Position
            Vector3 position;
            position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
            position.y = 0f;
            position.z = z * (HexMetrics.outerRadius * 1.5f);

            // Cell
            HexCell cell = cells[i] = Instantiate(cellPrefab);
            cell.transform.localPosition = position;
            cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
            cell.Index = i;
            cell.ShaderData = cellShaderData;
            cell.Explorable = x > 0 && z > 0 && x < cellCountX - 1 && z < cellCountZ - 1;
            cell.TerrainTypeIndex = 0;
            cell.name = "HexCell - " + cell.coordinates.ToString();

            // Neighbors
            if (x > 0)
            {
                cell.SetNeighbor(HexDirection.W, cells[i - 1]);
            }

            if (z > 0)
            {
                if ((z & 1) == 0)
                {
                    cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX]);
                    if (x > 0)
                    {
                        cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX - 1]);
                    }
                }
                else
                {
                    cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX]);
                    if (x < cellCountX - 1)
                    {
                        cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX + 1]);
                    }
                }
            }

            // Coord UI
            Text label = Instantiate(cellLabelPrefab);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
            //label.text = cell.coordinates.ToStringOnSeparateLines();
            cell.uiRect = label.rectTransform;

            cell.Elevation = 0;

            AddCellToChunk(x, z, cell);
        }

        private void AddCellToChunk(int x, int z, HexCell cell)
        {
            int chunkX = x / HexMetrics.chunkSizeX;
            int chunkZ = z / HexMetrics.chunkSizeZ;
            HexGridChunk chunk = chunks[chunkX + chunkZ * chunkCountX];

            int localX = x - chunkX * HexMetrics.chunkSizeX;
            int localZ = z - chunkZ * HexMetrics.chunkSizeZ;
            chunk.AddCell(localX + localZ * HexMetrics.chunkSizeX, cell);
        }

        public HexCell GetCell(Vector3 position)
        {
            position = transform.InverseTransformPoint(position);
            HexCoordinates coordinates = HexCoordinates.FromPosition(position);
            int index = coordinates.X + coordinates.Z * cellCountX + coordinates.Z / 2;
            return cells[index];
        }

        public HexCell GetCell(Ray ray)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return GetCell(hit.point);
            }
            return null;
        }

        public HexCell GetCell(int xOffset, int zOffset)
        {
            return cells[xOffset + zOffset * cellCountX];
        }

        public HexCell GetCell(int cellIndex)
        {
            return cells[cellIndex];
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(cellCountX);
            writer.Write(cellCountZ);

            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].Save(writer);
            }

            writer.Write(units.Count);
            for (int i = 0; i < units.Count; i++)
            {
                units[i].Save(writer);
            }
        }

        public void Load(BinaryReader reader, int header)
        {
            ClearPath();
            ClearUnits();

            int x = 20, z = 15;
            if (header >= 1)
            {
                x = reader.ReadInt32();
                z = reader.ReadInt32();
            }

            if (x != cellCountX || z != cellCountZ)
            {
                if (!CreateMap(x, z))
                {
                    return;
                }
            }

            bool originalImmediateMode = cellShaderData.ImmediateMode;
            cellShaderData.ImmediateMode = true;

            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].Load(reader, header);
            }

            for (int i = 0; i < chunks.Length; i++)
            {
                chunks[i].Refresh();
            }

            if (header >= 2)
            {
                int unitCount = reader.ReadInt32();
                for (int i = 0; i < unitCount; i++)
                {
                    HexUnit.Load(reader, this);
                }
            }

            cellShaderData.ImmediateMode = originalImmediateMode;
        }
    }
}