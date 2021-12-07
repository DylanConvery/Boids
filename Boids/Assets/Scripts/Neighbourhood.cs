using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Neighbourhood : MonoBehaviour {
    private void Start() {
        m_neighbourhood = GetComponent<SphereCollider>();
        m_neighbourhood.radius = BoidSpawner.boid_spawner.m_boid_neighbour_distance / 2;
    }

    private void FixedUpdate() {
        if (m_neighbourhood.radius != BoidSpawner.boid_spawner.m_boid_neighbour_distance / 2) {
            m_neighbourhood.radius = BoidSpawner.boid_spawner.m_boid_neighbour_distance / 2;
        }
    }

    private void OnTriggerEnter(Collider neighbour) {
        //get access to neighbouring boid
        var boid = neighbour.GetComponent<Boid>();
        //make sure we're interacting with a boid
        if (boid == null) return;
        //check if we have a reference to it or not
        if (m_neighbours.IndexOf(boid) == -1) {
            //add boid reference to list
            m_neighbours.Add(boid);
        }
    }

    private void OnTriggerExit(Collider neighbour) {
        //get access to neighbouring boid
        var boid = neighbour.GetComponent<Boid>();
        //make sure we're interacting with a boid
        if (boid == null) return;
        //check if we have a reference to it or not
        if (m_neighbours.IndexOf(boid) == -1) {
            //remove boid reference from list
            m_neighbours.Remove(boid);
        }
    }

    public Vector3 m_average_position {
        get {
            var average_position = Vector3.zero;
            if (!m_neighbours.Any()) return average_position;
            foreach (var boid in m_neighbours) {
                average_position += boid.m_position;
            }

            average_position /= m_neighbours.Count;
            return average_position;
        }
    }

    public Vector3 m_average_velocity {
        get {
            var average_velocity = Vector3.zero;
            if (!m_neighbours.Any()) return average_velocity;
            foreach (var boid in m_neighbours) {
                average_velocity += boid.m_rigidbody_velocity;
            }

            average_velocity /= m_neighbours.Count;
            return average_velocity;
        }
    }

    public Vector3 m_average_close_position {
        get {
            var average_close_position = Vector3.zero;
            var near_count = 0;
            if (!m_neighbours.Any()) return average_close_position;
            foreach (var boid in m_neighbours) {
                var delta = boid.m_position - transform.position;
                if (delta.magnitude <= BoidSpawner.boid_spawner.m_boid_collider_distance) {
                    average_close_position += boid.m_position;
                    near_count++;
                }
            }

            if (near_count == 0) return average_close_position;
            average_close_position /= m_neighbours.Count;
            return average_close_position;
        }
    }

    //stores boid neighbours
    private List<Boid> m_neighbours = new List<Boid>();

    //collider that represents the neighbourhood
    private SphereCollider m_neighbourhood;
}