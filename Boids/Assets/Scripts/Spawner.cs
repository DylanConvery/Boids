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
        //start spawning boids
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
        boid.transform.SetParent(this.transform);
        //set the spawn position randomly within a 1.0f radius sphere * our spawn radius
        boid.m_position = transform.position + Random.insideUnitSphere * Spawner.m_boid_spawner.m_spawn_radius;
        m_boids.Add(boid);
    }

    //singleton
    private static Spawner _instance;

    //return singleton
    public static Spawner m_boid_spawner => _instance;

    //boid spawn values
    public int m_boid_target_amount = 100;
    public float m_spawn_radius = 100f;
    public float m_creation_delay = 0.05f;

    public GameObject m_boid_prefab;
    private List<Boid> m_boids = new List<Boid>();
}