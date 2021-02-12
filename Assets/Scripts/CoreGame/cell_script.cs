using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cell_script : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public GameObject mCell;
    public Vector3 end_m_cell;
    private int countM_cell;
    //public GameObject spawnPoint;

    [HideInInspector] public string team;
    [HideInInspector] SpriteRenderer m_SpriteRenderer;
    RaycastHit hit;

    [HideInInspector] public bool test = true;
    [SerializeField] public GameObject aim;
    [SerializeField] public TextMeshProUGUI textMesh;
    /*[HideInInspector]*/
    public int number = 0;
    [SerializeField] float time = 3;
    //Drow Line variables
    [HideInInspector] Vector3 startPos;
    [HideInInspector] Vector3 endPos;
    [HideInInspector] Vector3 mousePos;
    [HideInInspector] Vector3 mouseDir;
    [HideInInspector] Camera cam;
    [HideInInspector] LineRenderer lr;


    #region Bot variables
    public bool AiCell = false;
    [SerializeField] float timeAi = 10;
    #endregion

    private void Awake()
    {
        team = tag;
    }
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        cam = Camera.main;
    }
    void Update()
    {
        if (Point_controller.Instance.EnemyList.Count == 0) SceneManager.LoadScene(1);


        textMesh.text = $"{number}";
        /// Timer and text
        if (tag == "friend" || tag == "Enemy")
        {
            textMesh.text = $"{number}";
            time -= Time.deltaTime;
            if (time < 0)
            {
                number++;
                time += 3;
            }
        }
        ///
        if (tag == "friend")
        {
            m_SpriteRenderer.color = Color.green;
            AiCell = false;
        }
        if (tag == "Enemy")
        {
            m_SpriteRenderer.color = Color.red;
            AiCell = true;
        }

        if (AiCell == false)
        {
            /// selections and drow lines
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseDir = mousePos - gameObject.transform.position;
            mouseDir.z = 0;
            mouseDir = mouseDir.normalized;
            ///
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

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "inert" || hit.collider.tag == "Enemy")
                    {
                        
                         Debug.Log("insude");
                        //createMcells(number, end_m_cell, this.transform.position);
                    }
                    else
                    {
                        countM_cell = 0;
                        GetComponent<Collider>().enabled = true;
                    }
                }
            }
            else
            {
                lr.enabled = false;
                aim.gameObject.SetActive(false);
            }
        }
        else if (AiCell == true)
        {
            textMesh.text = $"{number}";
            timeAi -= Time.deltaTime;
            if (timeAi < 0)
            {
                if (timeAi < 3)
                {
                    AILogic();
                }
                else
                {
                    GetComponent<Collider>().enabled = true;
                }

                timeAi += 10;
            }
        }
    }

    public void AILogic()
    {
        Point_controller.Instance.AddEnemy();
        GameObject obj_start = Point_controller.Instance.RandomEnemy();
        GameObject obj_end = Point_controller.Instance.RandomStartPos();
        createMcells(number, obj_end.transform.position, obj_start.transform.position);
    }

    IEnumerator pointGain()
    {
        Debug.Log("insude in corutine " + countM_cell);

        while (countM_cell != 0)
        {
            GetComponent<Collider>().enabled = false;
            countM_cell += number / 2;
            createMcells(number, end_m_cell, this.transform.position);
            countM_cell--;
            yield return new WaitForSeconds(1);
        }
        if (countM_cell == 0) StopCoroutine(pointGain());

    }
    //this metod performs m_cells
    public void createMcells(int count, Vector3 end_m_cell, Vector3 startPos)
    {
        GameObject obj = Instantiate(mCell, transform.position, Quaternion.identity);
        obj.GetComponent<M_Cell>().endPos = end_m_cell;
        obj.GetComponent<M_Cell>().team = this.tag;
        obj.GetComponent<M_Cell>().startPos = startPos;

        number -= count / 2;
        countM_cell += number;
    }

    #region triger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "friend" && tag == "inert")
        {
            number--;
        }
        if (number <= 0 || other.gameObject.tag == "friend")
        {
            tag = "friend";
            number++;
        }

        //if (other.gameObject.tag == "friend" && tag == "Enemy")
        //{
        //    number--;
        //}
        //if (number <= 0 && other.gameObject.tag == "friend")
        //{
        //    tag = "friend";
        //    Point_controller.Instance.RemEnemy(this.name);
        //    number++;
        //}
        /////
        //if (other.gameObject.tag == "Enemy" && tag == "friend")
        //{
        //    number--;
        //}
        //if (number <= 0 && other.gameObject.tag == "Enemy")
        //{
        //    tag = "Enemy";
        //    number++;
        //}

        //if (other.gameObject.tag == "Enemy" && tag == "inert")
        //{
        //    number--;
        //}
        //if (number <= 0 && other.gameObject.tag == "Enemy")
        //{
        //    tag = "Enemy";

        //    number++;
        //}



        Destroy(other.gameObject);
    }

    #endregion

    #region IponterInterfaces
    public void OnPointerUp(PointerEventData eventData)
    {
      
            test = false;
            if (hit.collider.tag == "inert")
            {
                end_m_cell = hit.transform.position;
                countM_cell += number / 2;
                StartCoroutine(pointGain());

            }
        Point_controller.Instance.ClearCell();

        test = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (tag == "friend")
        {
            test = true;
            Point_controller.Instance.AddCell(this);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tag == "friend")
        {
            test = true;
            Point_controller.Instance.AddCell(this);
        }


    }

    #endregion
}


