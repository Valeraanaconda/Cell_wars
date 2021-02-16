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



    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<Collider>().enabled = false;
        if (Global_variables.boost_speed == 1) step = 0.001f;
        if (Global_variables.boost_speed == 2) step = 0.0015f;
        if (Global_variables.boost_speed == 3) step = 0.0016f;
        if (Global_variables.boost_speed == 4) step = 0.0017f;
        if (Global_variables.boost_speed == 5) step = 0.0018f;
        step = 0.0095f;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (team == "friend")
        {
            var particle = GetComponentInChildren<ParticleSystem>();
            particle.startColor = Color.green;
            particle.maxParticles = count;

        }
        if (team == "Enemy")
        {
            var particle = GetComponentInChildren<ParticleSystem>();
            particle.startColor = Color.red;
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
