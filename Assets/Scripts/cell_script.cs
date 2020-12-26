using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cell_script : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI textMeshPro;
    public int number;
    private float time = 3;

    void Start()
    {
    }

    void Update()
    {
        textMeshPro.text = $"{number}";
        time -= Time.deltaTime;
        if (time < 0)
        {
            number++;
            time += 3;
        }
    }
   

}


