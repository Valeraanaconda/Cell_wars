using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Cell : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;
    public float step;
    public float progress;

    void Start()
    {
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
