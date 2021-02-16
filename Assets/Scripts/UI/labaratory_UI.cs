using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class labaratory_UI : MonoBehaviour
{
    public TextMeshProUGUI spawn_speed;
    public TextMeshProUGUI speed;


    // Update is called once per frame
    void Update()
    {
        spawn_speed.text = $"spawn speed: {Global_variables.boost_spawn_speed}";
        speed.text = $"speed: {Global_variables.boost_speed}";

    }
}
