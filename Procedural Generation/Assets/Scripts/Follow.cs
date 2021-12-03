using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    //stores the boid attraction transform
    private Transform m_attraction_transform;

    //grab transform of object tagged "Attraction"
    private void Start()
    {
        m_attraction_transform = GameObject.FindGameObjectWithTag("Attraction").transform;
    }

    //update camera position to follow boid attraction
    private void Update()
    {
        transform.LookAt(m_attraction_transform.position);
    }
}
