using System.Collections.Generic;
using UnityEngine;

public class Neighbourhood : MonoBehaviour {
    void Start() {
        m_neighbourhood = GetComponent<SphereCollider>();
        m_neighbourhood.radius = BoidSpawner.boid_spawner.boid_neighbour_distance / 2;
    }

    void FixedUpdate() {
        if (m_neighbourhood.radius != BoidSpawner.boid_spawner.boid_neighbour_distance / 2) {
            m_neighbourhood.radius = BoidSpawner.boid_spawner.boid_neighbour_distance / 2;
        }
    }

    void OnTriggerEnter(Collider neighbour) {
        //get access to neighbouring boid
        var boid = neighbour.GetComponent<Boid>();
        if (boid != null) {
            //check if we have a reference to it or not
            if (m_neighbours.IndexOf(boid) == -1) {
                //add boid reference to list
                m_neighbours.Add(boid);
            }
        }
    }

    void OnTriggerExit(Collider neighbour) {
        //get access to neighbouring boid
        var boid = neighbour.GetComponent<Boid>();
        if (boid != null) {
            //check if we have a reference to it or not
            if (m_neighbours.IndexOf(boid) == -1) {
                //remove boid reference from list
                m_neighbours.Remove(boid);
            }
        }
    }

    //stores boid neighbours
    private List<Boid> m_neighbours = new List<Boid>();

    //collider that represents the neighbourhood
    private SphereCollider m_neighbourhood;
}