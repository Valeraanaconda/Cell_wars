using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu_UI : MonoBehaviour
{
    public TextMeshProUGUI money;
    // Update is called once per frame
    void Update()
    {
        money.text = $"money: {Global_variables.money}";
    }
}
