using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ErksUnityLibrary.HexMap
{
    public class HexGridChunk : MonoBehaviour
    {
        public HexMesh hexMesh;
        public Canvas gridCanvas;

        private HexCell[] cells;
        private bool refresh = true;

        void Awake()
        {
            cells = new HexCell[HexMetrics.chunkSizeX * HexMetrics.chunkSizeZ];
            ShowUI(false);
        }

        private void LateUpdate()
        {
            if(refresh)
            {
                hexMesh.Triangulate(cells);
                refresh = false;
            }
        }

        public void AddCell(int index, HexCell cell)
        {
            cells[index] = cell;
            cell.chunk = this;
            cell.transform.SetParent(transform, false);
            cell.uiRect.SetParent(gridCanvas.transform, false);
        }

        public void Refresh()
        {
            refresh = true;
        }

        public void ShowUI(bool visible)
        {
            gridCanvas.gameObject.SetActive(visible);
        }
    }
}