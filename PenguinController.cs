using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour {

    public float speed =  3.0f;
    public float obstacleRange = 9.0f;

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = speed * 0.75f;
    }

    public void ReactToHit()
    {

    }
	void Update ()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
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
