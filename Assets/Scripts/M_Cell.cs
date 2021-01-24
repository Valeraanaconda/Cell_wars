using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Cell : MonoBehaviour
{
    public string team;
    public Vector3 startPos;
    public Vector3 endPos;
    public float step;
    public float progress;
    public SpriteRenderer m_SpriteRenderer;



    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
      
        step = 0.01f;
        if (Point_controller.Instance.selectedCells != null)
        {
            startPos = Point_controller.Instance.selectedCells[0].transform.position;
        }
        transform.position = startPos;
    } 


    // Update is called once per frame
    void FixedUpdate()
    {
        if (team == "friend")
        {
            m_SpriteRenderer.color = Color.green;
        }
        transform.position = Vector2.Lerp(startPos,endPos,progress);
        progress += step;
    }
}
 