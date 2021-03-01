using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class traning_level : MonoBehaviour
{
    public GameObject panel_1;
    [SerializeField] public TextMeshProUGUI text;
    public cell_script cell;
    public cell_script cell_1;
    private int count;

    private void Start()
    {
        count = 0;
    }

    void Update()
    {
        if (panel_1.gameObject.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else Time.timeScale = 1;

        if (cell.aim.gameObject.activeSelf == true && count == 0)
        {
            count++;
            text.text = "Отлично!!! теперь необходимо не отпуская палец перетащить палец на нейтральную планету чтобы захватить";
            panel_1.SetActive(true);
            Time.timeScale = 1;
        }
        
        if (cell_1.tag == "friend" && count == 1)
        {
            count++;
            text.text = "Для победы над противником необходимо завхватить все красные планеты. Освободи нажу глактику, вперёд!";
            panel_1.SetActive(true);

        }

    }
}
