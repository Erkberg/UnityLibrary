using System.Collections;
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
        private int activeElevation;

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
                EditCell(hexGrid.GetCell(hit.point));
            }
        }

        public void SelectColor(int index)
        {
            activeColor = colors[index];
        }

        public void SetElevation(float elevation)
        {
            activeElevation = (int)elevation;
        }

        void EditCell(HexCell cell)
        {
            cell.color = activeColor;
            cell.Elevation = activeElevation;
            hexGrid.Refresh();
        }

        private void GenerateRandomMap()
        {
            foreach(HexCell cell in FindObjectsOfType<HexCell>())
            {
                cell.color = colors[Random.Range(0, colors.Length)];
                cell.Elevation = Random.Range(0, 4);
                cell.color = colors[cell.Elevation];
            }

            hexGrid.Refresh();
        }
    }
}