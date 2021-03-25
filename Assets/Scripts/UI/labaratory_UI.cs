using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class labaratory_UI : MonoBehaviour
{
    public TextMeshProUGUI gl_money;

    public TextMeshProUGUI spawn_speed;
    public TextMeshProUGUI speed;

    public TextMeshProUGUI money_1;
    public TextMeshProUGUI money_2;


    // Update is called once per frame
    void Update()
    {
        gl_money.text = $"money: {Global_variables.money}";

        spawn_speed.text = $"spawn speed: {Global_variables.boost_spawn_speed}";
        speed.text = $"speed: {Global_variables.boost_speed}";
        switch (Global_variables.boost_speed)
        {
            case 1:
                money_1.text = "200";
                break;
            case 2:
                money_1.text = "400";
                break;
            case 3:
                money_1.text = "800";
                break;
            case 4:
                money_1.text = "1600";
                break;
            case 5:
                money_1.text = "MAX level";
                break;
        }
        switch (Global_variables.boost_spawn_speed)
        {
            case 1:
                money_2.text = "200";
                break;
            case 2:
                money_2.text = "400";
                break;
            case 3:
                money_2.text = "800";
                break;
            case 4:
                money_2.text = "1600";
                break;
            case 5:
                money_2.text = "MAX level";
                break;
        }


    }
}
