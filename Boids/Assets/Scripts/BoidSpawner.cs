using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    void Awake()
    {
        //singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        SpawnBoids();
    }

    // until desired amount of boids have been instantiated
    private void SpawnBoids()
    {
        if (m_boids.Count < m_boid_target_amount)
        {
            CreateBoid();
            Invoke("SpawnBoids", m_creation_delay);
        }
    }

    //instantiates a boid and adds it to m_boids list
    private void CreateBoid()
    {
        var boid_gameobject = Instantiate(m_boid_prefab);
        var boid = boid_gameobject.GetComponent<Boid>();
        boid.transform.SetParent(m_boid_anchor.transform);
        m_boids.Add(boid);
    }

    private static BoidSpawner _instance;
    public static BoidSpawner boid_spawner => _instance;

    [SerializeField] private int m_boid_target_amount = 100;
    private const float m_creation_delay = 0.1f;

    //boid spawn fields
    public float boid_spawn_radius = 100f;
    public float boid_velocity = 30f;
    public float attract_push_distance = 5f;
    public float attraction_pull = 2f;

    [SerializeField] private GameObject m_boid_prefab = null;
    [SerializeField] private GameObject m_boid_anchor = null;
    private List<Boid> m_boids = new List<Boid>();

}
