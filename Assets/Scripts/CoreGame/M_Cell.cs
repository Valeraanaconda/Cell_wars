using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Cell : MonoBehaviour
{
    public string team;
    public Vector3 startPos;
    public Vector3 endPos;
    public float step; // boost
    public float progress;
    public int count;
    public SpriteRenderer m_SpriteRenderer;

    public Transform transform_end;



    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<Collider>().enabled = false;
        step = 3f;
        if (Global_variables.boost_speed == 1) step = 3.3f;
        if (Global_variables.boost_speed == 2) step =3.7f;
        if (Global_variables.boost_speed == 3) step = 4;
        if (Global_variables.boost_speed == 4) step = 4.4f;
        if (Global_variables.boost_speed == 5) step = 4.9f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(transform_end);
        var particle = GetComponentInChildren<ParticleSystem>();
        particle.startRotation = Mathf.Abs(transform.rotation.x);
        if (team == "friend")
        {
            particle.maxParticles = count;
        }
        if (team == "Enemy")
        {
            particle.maxParticles = count;
        }
        tag = team;
        transform.position = Vector2.MoveTowards(startPos, endPos, Time.deltaTime* progress);
        progress += step;
        StartCoroutine(colideron());

    }
    IEnumerator colideron()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Collider>().enabled = true;
    }
}
