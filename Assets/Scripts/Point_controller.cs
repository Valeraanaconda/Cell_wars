using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_controller : Singleton<Point_controller>
{
    public List<GameObject> AllCell = new List<GameObject>();
    /*[HideInInspector]*/
    public List<cell_script> selectedCells = new List<cell_script>();

    public List<GameObject> EnemyList = new List<GameObject>();

    public void AddEnemy()
    {
        for (int i = 0; i < AllCell.Count; i++)
        {
            if (AllCell[i].tag == "Enemy") EnemyList.Add(AllCell[i]);
        }
    }
    public GameObject RandomStartPos()
    {
        return AllCell[Random.Range(0, AllCell.Count)];
    }
    public GameObject RandomEnemy()
    {
        return EnemyList[Random.Range(0, EnemyList.Count)];
    }

    public void AddCell(cell_script cell)
    {
        if(!selectedCells.Contains(cell)) selectedCells.Add(cell);
    }
    public void ClearCell() => selectedCells.Clear();
}
