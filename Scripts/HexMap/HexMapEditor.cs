﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;

namespace ErksUnityLibrary.HexMap
{
    public class HexMapEditor : MonoBehaviour
    {
        private enum OptionalToggle
        {
            Ignore, Yes, No
        }

        public HexGrid hexGrid;

        private int activeElevation;
        private int activeWaterLevel;
        private bool applyElevation;
        private bool applyWaterLevel;
        private int activeUrbanLevel, activeFarmLevel, activePlantLevel, activeSpecialIndex;
        private bool applyUrbanLevel, applyFarmLevel, applyPlantLevel, applySpecialIndex;

        private int brushSize;
        private int activeTerrainTypeIndex;

        private OptionalToggle riverMode, roadMode, walledMode;

        private bool isDrag;
        private HexDirection dragDirection;
        private HexCell previousCell;

        private IEnumerator Start()
        {
            yield return null;
            GenerateRandomMap();
        }

        void Update()
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                HandleInput();
            }
            else
            {
                previousCell = null;
            }
        }

        void HandleInput()
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit))
            {
                HexCell currentCell = hexGrid.GetCell(hit.point);
                if (previousCell && previousCell != currentCell)
                {
                    ValidateDrag(currentCell);
                }
                else
                {
                    isDrag = false;
                }
                EditCells(currentCell);
                previousCell = currentCell;
            }
            else
            {
                previousCell = null;
            }
        }

        private void ValidateDrag(HexCell currentCell)
        {
            for (dragDirection = HexDirection.NE; dragDirection <= HexDirection.NW; dragDirection++)
            {
                if (previousCell.GetNeighbor(dragDirection) == currentCell)
                {
                    isDrag = true;
                    return;
                }
            }
            isDrag = false;
        }

        public void SetElevation(float elevation)
        {
            activeElevation = (int)elevation;
        }

        public void SetApplyElevation(bool toggle)
        {
            applyElevation = toggle;
        }

        public void SetBrushSize(float size)
        {
            brushSize = (int)size;
        }

        void EditCells(HexCell center)
        {
            int centerX = center.coordinates.X;
            int centerZ = center.coordinates.Z;

            for (int r = 0, z = centerZ - brushSize; z <= centerZ; z++, r++)
            {
                for (int x = centerX - r; x <= centerX + brushSize; x++)
                {
                    EditCell(hexGrid.GetCell(new HexCoordinates(x, z)));
                }
            }

            for (int r = 0, z = centerZ + brushSize; z > centerZ; z--, r++)
            {
                for (int x = centerX - brushSize; x <= centerX + r; x++)
                {
                    EditCell(hexGrid.GetCell(new HexCoordinates(x, z)));
                }
            }
        }

        void EditCell(HexCell cell)
        {
            if(cell)
            {
                if (applyElevation)
                {
                    cell.Elevation = activeElevation;
                }

                if (activeTerrainTypeIndex >= 0)
                {
                    cell.TerrainTypeIndex = activeTerrainTypeIndex;
                }
            }

            if (riverMode == OptionalToggle.No)
            {
                cell.RemoveRiver();
            }

            if (roadMode == OptionalToggle.No)
            {
                cell.RemoveRoads();
            }

            if (isDrag)
            {
                HexCell otherCell = cell.GetNeighbor(dragDirection.Opposite());
                if (otherCell)
                {
                    if (riverMode == OptionalToggle.Yes)
                    {
                        otherCell.SetOutgoingRiver(dragDirection);
                    }
                    if (roadMode == OptionalToggle.Yes)
                    {
                        otherCell.AddRoad(dragDirection);
                    }
                    if (walledMode != OptionalToggle.Ignore)
                    {
                        cell.Walled = walledMode == OptionalToggle.Yes;
                    }
                    if (applyWaterLevel)
                    {
                        cell.WaterLevel = activeWaterLevel;
                    }
                    if (applyUrbanLevel)
                    {
                        cell.UrbanLevel = activeUrbanLevel;
                    }
                    if (applyFarmLevel)
                    {
                        cell.FarmLevel = activeFarmLevel;
                    }
                    if (applyPlantLevel)
                    {
                        cell.PlantLevel = activePlantLevel;
                    }
                    if (riverMode == OptionalToggle.No)
                    {
                        cell.RemoveRiver();
                    }
                    if (roadMode == OptionalToggle.No)
                    {
                        cell.RemoveRoads();
                    }
                    if (applySpecialIndex)
                    {
                        cell.SpecialIndex = activeSpecialIndex;
                    }
                }
            }
        }

        public void ShowUI(bool visible)
        {
            hexGrid.ShowUI(visible);
        }

        public void SetRiverMode(int mode)
        {
            riverMode = (OptionalToggle)mode;
        }

        public void SetApplyWaterLevel(bool toggle)
        {
            applyWaterLevel = toggle;
        }

        public void SetWaterLevel(float level)
        {
            activeWaterLevel = (int)level;
        }

        public void SetRoadMode(int mode)
        {
            roadMode = (OptionalToggle)mode;
        }
	
	    public void SetApplyUrbanLevel(bool toggle)
        {
            applyUrbanLevel = toggle;
        }

        public void SetUrbanLevel(float level)
        {
            activeUrbanLevel = (int)level;
        }

        public void SetApplyFarmLevel(bool toggle)
        {
            applyFarmLevel = toggle;
        }

        public void SetFarmLevel(float level)
        {
            activeFarmLevel = (int)level;
        }

        public void SetApplyPlantLevel(bool toggle)
        {
            applyPlantLevel = toggle;
        }

        public void SetPlantLevel(float level)
        {
            activePlantLevel = (int)level;
        }

        public void SetWalledMode(int mode)
        {
            walledMode = (OptionalToggle)mode;
        }

        public void SetApplySpecialIndex(bool toggle)
        {
            applySpecialIndex = toggle;
        }

        public void SetSpecialIndex(float index)
        {
            activeSpecialIndex = (int)index;
        }

        private void GenerateRandomMap()
        {
            foreach(HexCell cell in FindObjectsOfType<HexCell>())
            {
                cell.Elevation = Random.Range(0, 4);
                cell.TerrainTypeIndex = cell.Elevation;
                cell.WaterLevel = 1;

                cell.UrbanLevel = Random.Range(-8, 4);
                cell.FarmLevel = Random.Range(-8, 4);
                cell.PlantLevel = Random.Range(-8, 4);
                cell.SpecialIndex = Random.Range(-16, 4);

                cell.Walled = Random.Range(0f, 1f) < 0.5f;
            }
        }

        public void SetTerrainTypeIndex(int index)
        {
            activeTerrainTypeIndex = index;
        }

        private HexDirection GetRandomDirection()
        {
            var values = HexDirection.GetValues(typeof(HexDirection));
            System.Random random = new System.Random();
            return (HexDirection)values.GetValue(random.Next(values.Length));
        }

        public void Save()
        {
            string path = Path.Combine(Application.persistentDataPath, "test.map");
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                writer.Write(0);
                hexGrid.Save(writer);
            }
        }

        public void Load()
        {
            string path = Path.Combine(Application.persistentDataPath, "test.map");
            using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
            {
                int header = reader.ReadInt32();
                if (header == 0)
                {
                    hexGrid.Load(reader);
                }
                else
                {
                    Debug.LogWarning("Unknown map format " + header);
                }
            }
        }
    }
}