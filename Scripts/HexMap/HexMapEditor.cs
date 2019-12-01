﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ErksUnityLibrary.HexMap
{
    public class HexMapEditor : MonoBehaviour
    {
        public Color[] colors;

        public HexGrid hexGrid;

        private Color activeColor;
        private bool applyColor;

        private int activeElevation;
        private bool applyElevation = true;

        private int brushSize;
        

        void Awake()
        {
            SelectColor(0);
        }

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
        }

        void HandleInput()
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit))
            {
                EditCells(hexGrid.GetCell(hit.point));
            }
        }

        public void SelectColor(int index)
        {
            applyColor = index >= 0;
            if (applyColor)
            {
                activeColor = colors[index];
            }
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
                if (applyColor)
                {
                    cell.Color = activeColor;
                }

                if (applyElevation)
                {
                    cell.Elevation = activeElevation;
                }
            }            
        }

        public void ShowUI(bool visible)
        {
            hexGrid.ShowUI(visible);
        }

        private void GenerateRandomMap()
        {
            foreach(HexCell cell in FindObjectsOfType<HexCell>())
            {
                cell.Color = colors[Random.Range(0, colors.Length)];
                cell.Elevation = Random.Range(0, 4);
                cell.Color = colors[cell.Elevation];
            }
        }
    }
}