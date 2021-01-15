using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class cell_script : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public bool test = false;
    [SerializeField] public GameObject aim;
    [SerializeField] public GameObject cell;
    [SerializeField] public TextMeshProUGUI textMeshPro;
    [HideInInspector] int number;
    [SerializeField] float time = 3;
    //Drow Line variables
    Vector3 startPos;
    Vector3 endPos;
    Vector3 mousePos;
    Vector3 mouseDir;
    Camera cam;
    LineRenderer lr;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        cam = Camera.main;
    }

    void Update()
    {
        /// Timer and text
        textMeshPro.text = $"{number}";
        time -= Time.deltaTime;
        if (time < 0)
        {
            number++;
            time += 3;
        }
        ///

        /// selections and drow lines
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseDir = mousePos - gameObject.transform.position;
        mouseDir.z = 0;
        mouseDir = mouseDir.normalized;
        if (test == true)
        {
            aim.gameObject.SetActive(true);
            lr.enabled = true;
            startPos = gameObject.transform.position;
            startPos.z = 0;
            lr.SetPosition(0, startPos);
            endPos = mousePos;
            endPos.z = 0;
            lr.SetPosition(1, endPos);
        }
        else
        {
            lr.enabled = false;
            aim.gameObject.SetActive(false);
        }
    }


    //IpointerInterfaces
    public void OnPointerUp(PointerEventData eventData)
    {
        test = false;
        Point_controller.Instance.ClearCell();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        test = true;
        Point_controller.Instance.AddCell(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        test = true;
        Point_controller.Instance.AddCell(this);
    }
}


