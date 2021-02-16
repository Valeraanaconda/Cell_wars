using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_controller : Singleton<Point_controller>
{
    public List<GameObject> AllCell = new List<GameObject>();
    /*[HideInInspector]*/
    public List<cell_script> selectedCells = new List<cell_script>();

    public List<GameObject> EnemyList = new List<GameObject>();
    public List<GameObject> FriendList = new List<GameObject>();



    public void AddFriend()
    {
        for (int i = 0; i < AllCell.Count; i++)
        {
            if (AllCell[i].tag == "friend")
            {
                FriendList.Add(AllCell[i]);
                break;
            }
        }
    }
    public void Remfriend(string cell_name)
    {
        for (int i = 0; i < FriendList.Count; i++)
        {
            if (FriendList[i].name == cell_name) FriendList.Remove(FriendList[i]);
        }
    }
    public void AddEnemy()
    {
        for (int i = 0; i < AllCell.Count; i++)
        {
            if (AllCell[i].tag == "Enemy")
            {
                EnemyList.Add(AllCell[i]);
                break;
            }
        }
    }

    public void RemEnemy(string cell_name)
    {
        for (int i = 0; i < EnemyList.Count; i++)
        {
            if (EnemyList[i].name == cell_name) EnemyList.Remove(EnemyList[i]);
        }
    }
    public GameObject RandomEndPos()
    {
        int count_iner=0;
        foreach (var i in AllCell)
        {
            if (i.tag == "inert") count_iner++;
        }
        if(count_iner == 0) return AllCell[Random.Range(0, FriendList.Count)];

        return AllCell[Random.Range(0, AllCell.Count)];
    }
    public GameObject RandomEnemy()
    {
        return EnemyList[Random.Range(0, EnemyList.Count)];
    }

    public void AddCell(cell_script cell)
    {
        if (!selectedCells.Contains(cell)) selectedCells.Add(cell);
    }
    public void ClearCell() => selectedCells.Clear();
}
