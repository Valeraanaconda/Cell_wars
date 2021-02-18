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
        step = 0.0095f;
        if (Global_variables.boost_speed == 1) step = 0.001f;
        if (Global_variables.boost_speed == 2) step = 0.0015f;
        if (Global_variables.boost_speed == 3) step = 0.0016f;
        if (Global_variables.boost_speed == 4) step = 0.0017f;
        if (Global_variables.boost_speed == 5) step = 0.0018f;
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
        transform.position = Vector2.Lerp(startPos, endPos, progress);
        progress += step;
        StartCoroutine(colideron());

    }
    IEnumerator colideron()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Collider>().enabled = true;
    }
}
