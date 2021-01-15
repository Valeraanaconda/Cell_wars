using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy_script : MonoBehaviour
{
    [SerializeField]
    public GameObject cell;
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
