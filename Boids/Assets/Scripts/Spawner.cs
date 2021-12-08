using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private void Awake() {
        //set up singleton
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        SpawnBoids();
    }

    //keep recursively invoking until desired amount of boids have been instantiated
    private void SpawnBoids() {
        if (m_boids.Count >= m_boid_target_amount) return;
        CreateBoid();
        Invoke("SpawnBoids", m_creation_delay);
    }

    //instantiates a boid and adds it to m_boids list
    private void CreateBoid() {
        var boid_gameobject = Instantiate(m_boid_prefab);
        var boid = boid_gameobject.GetComponent<Boid>();
        boid.transform.SetParent(m_boid_anchor);
        m_boids.Add(boid);
    }

    //singleton
    private static Spawner _instance;
    //return singleton
    public static Spawner m_boid_spawner => _instance;

    //boid spawn values
    public int m_boid_target_amount = 100;
    public float m_spawn_radius = 100f;
    public float m_velocity = 30f;
    public float m_neighbour_distance = 30f;
    public float m_collision_distance = 4f;
    public float m_velocity_matching = 0.25f;
    public float m_centering = 0.2f;
    public float m_collision_avoid_distance = 2f;
    public float m_attraction_pull = 2f;
    public float m_attraction_push = 2f;
    public float m_attraction_push_distance = 5f;
    private const float m_creation_delay = 0.05f;

    public GameObject m_boid_prefab;
    public Transform m_boid_anchor;
    private List<Boid> m_boids = new List<Boid>();
}