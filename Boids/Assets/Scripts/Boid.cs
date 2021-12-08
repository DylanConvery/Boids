using UnityEngine;
using Vector3 = UnityEngine.Vector3;

//TODO: decouple Boid and Spawner logic

public class Boid : MonoBehaviour {
    private void Awake() {
        //grab rigidbody now to avoid having to getcomponent everywhere we need to access our rigidbody
        m_rigid_body = GetComponent<Rigidbody>();
        //grab neighbourhood now to avoid having to getcomponent everywhere we need to access our neighbourhood
        m_neighbourhood = GetComponent<Neighbourhood>();
        //grab our attractions transform now for later
        m_attraction = GameObject.FindGameObjectWithTag("track");
        //set the spawn position randomly within a 1.0f radius sphere * our spawn radius
        m_position = Random.insideUnitSphere * Spawner.m_boid_spawner.m_spawn_radius;
        //set the velocity of each boid to be random
        m_rigid_body.velocity = Random.onUnitSphere * Spawner.m_boid_spawner.m_velocity;
        LookAhead();
    }

    private void FixedUpdate() {
        //collision avoidance
        Vector3 velocity_avoid = Vector3.zero;
        Vector3 too_close_position = m_neighbourhood.m_average_close_position;
        if (too_close_position != Vector3.zero) {
            velocity_avoid = m_position - too_close_position;
            velocity_avoid.Normalize();
            velocity_avoid *= Spawner.m_boid_spawner.m_velocity;
        }

        //velocity matching
        Vector3 velocity_align = m_neighbourhood.m_average_velocity;
        if (velocity_align != Vector3.zero) {
            velocity_align.Normalize();
            velocity_align *= Spawner.m_boid_spawner.m_velocity;
        }

        //centering
        Vector3 velocity_center = m_neighbourhood.m_average_position;
        if (velocity_center != Vector3.zero) {
            velocity_center -= transform.position;
            velocity_center.Normalize();
            velocity_center *= Spawner.m_boid_spawner.m_velocity;
        }

        //get current distance from attraction to boid
        var delta = m_attraction.transform.position - m_position;
        if (velocity_avoid != Vector3.zero) {
            m_rigid_body.velocity = Vector3.Lerp(
                m_rigid_body.velocity,
                velocity_avoid,
                Spawner.m_boid_spawner.m_collision_avoid_distance * Time.fixedDeltaTime
            );
        } else {
            if (velocity_align != Vector3.zero) {
                m_rigid_body.velocity = Vector3.Lerp(
                    m_rigid_body.velocity,
                    velocity_align,
                    Spawner.m_boid_spawner.m_velocity_matching * Time.fixedDeltaTime
                );
            }

            if (velocity_center != Vector3.zero) {
                m_rigid_body.velocity = Vector3.Lerp(
                    m_rigid_body.velocity,
                    velocity_align,
                    Spawner.m_boid_spawner.m_centering * Time.fixedDeltaTime
                );
            }

            if (delta.normalized * Spawner.m_boid_spawner.m_velocity != Vector3.zero) {
                if (delta.magnitude > Spawner.m_boid_spawner.m_attraction_push_distance) {
                    m_rigid_body.velocity = Vector3.Lerp(
                        m_rigid_body.velocity,
                        delta.normalized * Spawner.m_boid_spawner.m_velocity,
                        Spawner.m_boid_spawner.m_attraction_pull * Time.fixedDeltaTime
                    );
                } else {
                    m_rigid_body.velocity = Vector3.Lerp(
                        m_rigid_body.velocity,
                        -delta.normalized * Spawner.m_boid_spawner.m_velocity,
                        Spawner.m_boid_spawner.m_attraction_push * Time.fixedDeltaTime
                    );
                }
            }
        }

        //normalize vector and set velocity
        m_rigid_body.velocity = m_rigid_body.velocity.normalized * Spawner.m_boid_spawner.m_velocity;
        LookAhead();
    }

    //set our rotation to be in the direction of our velocity
    private void LookAhead() { transform.LookAt(m_position + m_rigid_body.velocity); }

    private Rigidbody m_rigid_body;
    public GameObject m_attraction;
    private Neighbourhood m_neighbourhood;

    public Vector3 m_velocity => m_rigid_body.velocity;

    public Vector3 m_position {
        get => transform.position;
        set => transform.position = value;
    }
}