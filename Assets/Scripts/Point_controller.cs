using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_controller : Singleton<Point_controller>
{
    public GameObject m_cell;

    /*[HideInInspector]*/ public List<cell_script> selectedCells = new List<cell_script>();
    public void AddCell(cell_script cell)
    {
        if(!selectedCells.Contains(cell)) selectedCells.Add(cell);
    }

  

    public void ClearCell() => selectedCells.Clear();
}
