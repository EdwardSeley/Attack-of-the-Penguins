using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PenguinController : MonoBehaviour {

    public float speed =  3.0f;
    public float obstacleRange = 9.0f;
    public float attackRange = 3.0f;

    private Animator m_anim;
    private Rigidbody m_rigidBody;
    private NavMeshAgent m_navAgent;
    private GameObject player;
    private bool _alive;


    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_anim.SetFloat("speed", speed * 0.75f);
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.centerOfMass = new Vector3(-0.2f, 2.4f, 0.8f);
        m_navAgent = GetComponent<NavMeshAgent>();
        m_navAgent.speed = 5.0f;
        player = GameObject.Find("Player");
        _alive = true;

    }

    public void ReactToHit()
    {
        _alive = false;
    }

    void Update()
    {
        m_navAgent.SetDestination(player.transform.position);
        bool withinAttackDist = Vector3.Distance(transform.position, player.transform.position) < attackRange;
        if (withinAttackDist)
        {
            m_anim.SetBool("WithinAttackingDistance", withinAttackDist);
            Debug.Log(withinAttackDist);
        }
        // m_anim.SetBool("WithinAttackingDistance", withinAttackDist);
        //Debug.Log(withinAttackDist);
    }

    public void wanderAround()
    {
        m_rigidBody.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        Vector3 currentPosition = transform.position;
        currentPosition.y *= 0.5f;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 1f, out hit))
        {
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }
}
