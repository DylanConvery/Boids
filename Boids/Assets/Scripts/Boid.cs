using UnityEngine;

public class Boid : MonoBehaviour {
    private void Awake() {
        //TODO: decouple Boid and BoidSpawner logic
        //grab rigidbody now to avoid having to getcomponent everywhere we need to access our rigidbody
        m_rigid_body = GetComponent<Rigidbody>();
        //grab our attractions transform now for later
        m_attraction_transform = GameObject.FindGameObjectWithTag("Attraction").transform;
        //set the spawn position randomly within a 1.0f radius sphere * our spawn radius
        m_position = Random.insideUnitSphere * BoidSpawner.boid_spawner.m_boid_spawn_radius;
        //set the velocity of each boid to be random
        m_rigid_body.velocity = Random.onUnitSphere * BoidSpawner.boid_spawner.m_boid_velocity;
        LookAhead();
    }

    private void FixedUpdate() {
        var velocity = m_rigid_body.velocity;
        //get current distance from attraction to boid
        var delta = m_attraction_transform.position - m_position;

        //check whether we need to move towards or away from the attraction
        if (delta.magnitude > BoidSpawner.boid_spawner.m_attraction_push_distance) {
            velocity = Vector3.Lerp(velocity, delta.normalized * BoidSpawner.boid_spawner.m_boid_velocity, BoidSpawner.boid_spawner.m_attraction_pull * Time.fixedDeltaTime);
        } else {
            velocity = Vector3.Lerp(velocity, -delta.normalized * BoidSpawner.boid_spawner.m_boid_velocity, BoidSpawner.boid_spawner.m_attraction_pull * Time.fixedDeltaTime);
        }

        //normalize vector and set velocity
        m_rigid_body.velocity = velocity.normalized * BoidSpawner.boid_spawner.m_boid_velocity;
        LookAhead();
    }

    //set our rotation to be in the direction of our velocity
    private void LookAhead() { transform.LookAt(m_position + m_rigid_body.velocity); }

    private Rigidbody m_rigid_body;
    private Transform m_attraction_transform;

    public Vector3 m_rigidbody_velocity => m_rigid_body.velocity;

    public Vector3 m_position {
        get => transform.position;
        set => transform.position = value;
    }
}