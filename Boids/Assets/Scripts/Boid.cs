using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private void Awake()
    {
        //coupling boid to boidspawner for the time being, will fix later
        m_rigid_body = GetComponent<Rigidbody>();
        m_position = Random.insideUnitSphere * BoidSpawner.spawner.boid_spawn_radius;
        m_rigid_body.velocity = Random.onUnitSphere * BoidSpawner.spawner.boid_velocity;
        LookAhead();
    }

    private void LookAhead()
    {
        transform.LookAt(m_position + m_rigid_body.velocity);
    }

    private Rigidbody m_rigid_body;
    private Vector3 m_position
    {
        get => transform.position;
        set => transform.position = value;
    }
}
