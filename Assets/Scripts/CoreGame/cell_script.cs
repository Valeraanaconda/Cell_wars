using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cell_script : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject WinSprite;
    public GameObject LoseSprite;

    public int score;
    public TextMeshProUGUI score_UI;

    ///
    public GameObject mCell;
    public GameObject mCell_enemy;

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
    [SerializeField] float time = 5; //boost
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
        Global_variables.score_level = 0;
        team = tag;
        if (Global_variables.boost_spawn_speed == 1) time = 4.75f;
        if (Global_variables.boost_spawn_speed == 2) time = 4.25f;
        if (Global_variables.boost_spawn_speed == 3) time = 4.0f;
        if (Global_variables.boost_spawn_speed == 4) time = 3.75f;
        if (Global_variables.boost_spawn_speed == 5) time = 2.75f;
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
                    if (hit.collider.tag == "inert" || hit.collider.tag == "Enemy");
                    else
                    {
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
                timeAi += 10;
            }
        }
    }

    public void AILogic()
    {
        GameObject obj_start = Point_controller.Instance.RandomEnemy();
        GameObject obj_end = Point_controller.Instance.RandomEndPos();
        countM_cell += number / 2;
        number = countM_cell;
        countM_cell = 0;
        createMcells(number, obj_end.transform.position, obj_start.transform.position, obj_end.transform);
    }



    //this metod performs m_cells
    public void createMcells(int count, Vector3 end_m_cell, Vector3 startPos, Transform end_lock)
    {
        if (tag == "friend")
        {
        GameObject obj = Instantiate(mCell, transform.position, Quaternion.identity);
        obj.GetComponent<M_Cell>().count = count;
        obj.GetComponent<M_Cell>().endPos = end_m_cell;
        obj.GetComponent<M_Cell>().team = this.tag;
        obj.GetComponent<M_Cell>().transform_end = end_lock;
        obj.GetComponent<M_Cell>().startPos = startPos;
        }
        if (tag == "Enemy")
        {
            GameObject obj = Instantiate(mCell_enemy, transform.position, Quaternion.identity);
            obj.GetComponent<M_Cell>().count = count;
            obj.GetComponent<M_Cell>().endPos = end_m_cell;
            obj.GetComponent<M_Cell>().team = this.tag;
            obj.GetComponent<M_Cell>().transform_end = end_lock;
            obj.GetComponent<M_Cell>().startPos = startPos;

        }
    }

    #region trigers
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        bool t = true; // this variable needed for correct scoring
        // inert point capture logic parent friend
        if (other.gameObject.tag == "friend" && tag == "inert")
        {
            int res = number - obj.GetComponent<M_Cell>().count;
            if (res <= 0)
            {
                number = (res * -1);
                tag = "friend";
                GameObject transf = Point_controller.Instance.AllCell.Find(x => x.gameObject.name.Equals(this.name));
                Point_controller.Instance.AddFriend(transf);
            }
            else number -= obj.GetComponent<M_Cell>().count;
            t = false;
        }
        // inert point capture logic parent enemy
        if (other.gameObject.tag == "Enemy" && tag == "inert")
        {
            int res = number - obj.GetComponent<M_Cell>().count;
            if (res <= 0)
            {
                number = (res * -1);
                tag = "Enemy";
                GameObject transf = Point_controller.Instance.AllCell.Find(x => x.gameObject.name.Equals(this.name));
                Point_controller.Instance.AddEnemy(transf);

            }
            else number -= obj.GetComponent<M_Cell>().count;
            t = false;
        }

        if (other.gameObject.tag == "Enemy" && tag == "friend")
        {
            int res = number - obj.GetComponent<M_Cell>().count;
            if (res <= 0)
            {
                Point_controller.Instance.Remfriend(name);
                number = (res * -1);
                tag = "Enemy";
                GameObject transf = Point_controller.Instance.AllCell.Find(x => x.gameObject.name.Equals(this.name));
                Point_controller.Instance.AddEnemy(transf);
                if (Point_controller.Instance.FriendList.Count == 0)
                {
                    //lose
                    Time.timeScale = 0;
                    LoseSprite.gameObject.SetActive(true);
                }
            }
            else number -= obj.GetComponent<M_Cell>().count;
        }

        if (other.gameObject.tag == "friend" && tag == "Enemy")
        {
            int res = number - obj.GetComponent<M_Cell>().count;
            if (res <= 0)
            {
                Point_controller.Instance.RemEnemy(name);
                number = (res * -1);
                tag = "friend";
                GameObject transf = Point_controller.Instance.AllCell.Find(x => x.gameObject.name.Equals(this.name));
                Point_controller.Instance.AddFriend(transf);
                if (Point_controller.Instance.EnemyList.Count == 0)
                {
                    //win
                    score_UI.text = $"{Global_variables.score_level}";
                    Time.timeScale = 0;
                    WinSprite.gameObject.SetActive(true);
                    score_UI.text = $"{score}";
                }

            }
            else number -= obj.GetComponent<M_Cell>().count;
        }

        if (other.gameObject.tag == "Enemy" && tag == "Enemy" && t == true) number += obj.GetComponent<M_Cell>().count;
        if (other.gameObject.tag == "friend" && tag == "friend" && t == true)
        {
            //score++;
            number += obj.GetComponent<M_Cell>().count;
        }

        score_UI.text = $"{Global_variables.score_level}";
        Destroy(other.gameObject);
    }
    #endregion

    #region IponterInterfaces
    public void OnPointerUp(PointerEventData eventData)
    {
        Global_variables.score_level++;
        test = false;
        if (hit.collider.tag == "inert" && number > 1 || hit.collider.tag == "Enemy" || hit.collider.tag == "friend")
        {
            test = false;
            end_m_cell = hit.transform.position;
            countM_cell += number / 2;
            number = countM_cell;
            createMcells(number, end_m_cell, this.transform.position,hit.transform);
            countM_cell = 0;
            Point_controller.Instance.ClearCell();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (tag == "friend")
        {
            test = true;
            Point_controller.Instance.AddCell(this);
        }
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if (tag == "friend")
    //    {
    //        test = true;
    //        Point_controller.Instance.AddCell(this);
    //    }


    //}

    #endregion
}


