using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Neighbourhood : MonoBehaviour {
    //check when GameObject enters collider
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

    //check when GameObject exits collider
    private void OnTriggerExit(Collider neighbour) {
        //get access to neighbouring boid
        var boid = neighbour.GetComponent<Boid>();
        //make sure we're interacting with a boid
        if (boid == null) return;
        //check if we have a reference to it or not
        if (m_neighbours.IndexOf(boid) != -1) {
            //remove boid reference from list
            m_neighbours.Remove(boid);
        }
    }

    //average position of neighbouring boids
    public Vector3 m_average_position {
        get {
            var average_position = Vector3.zero;
            //check if m_neighbours is empty
            if (!m_neighbours.Any()) return average_position;
            //sum of all neighbouring boid positions
            foreach (var boid in m_neighbours) {
                average_position += boid.m_position;
            }
            //average of all neighbouring boid positions
            average_position /= m_neighbours.Count;
            return average_position;
        }
    }

    //average velocity of neighbouring boids
    public Vector3 m_average_velocity {
        get {
            var average_velocity = Vector3.zero;
            //check if m_neighbours is empty
            if (!m_neighbours.Any()) return average_velocity;
            //sum of all neighbouring boid velocities
            foreach (var boid in m_neighbours) {
                average_velocity += boid.m_velocity;
            }
            //average of all neighbouring boid velocities 
            average_velocity /= m_neighbours.Count;
            return average_velocity;
        }
    }

    //average position of neighbouring boids that are too close
    public Vector3 m_average_close_position {
        get {
            var average_close_position = Vector3.zero;
            var near_count = 0;
            //check if m_neighbours is empty
            if (!m_neighbours.Any()) return average_close_position;
            foreach (var boid in m_neighbours) {
                var delta = boid.m_position - transform.position;
                //check if neighbouring boid position from our position is less than or equal to collision distance
                //if it is, add to sum
                if (delta.magnitude <= m_collision_distance) {
                    average_close_position += boid.m_position;
                    near_count++;
                }
            }
            //check if there are any near boids
            if (near_count == 0) return average_close_position;
            //average of all close boid positions
            average_close_position /= near_count;
            return average_close_position;
        }
    }

    //stores boid neighbours
    private List<Boid> m_neighbours = new List<Boid>();

    //collider that represents the neighbourhood
    private SphereCollider m_neighbourhood;

    //boid flocking values
    public float m_collision_distance = 4f;
    public float m_velocity_matching = 0.25f;
    public float m_centering = 0.2f;
    public float m_collision_avoid_distance = 2f;
    public float m_attraction_pull = 2f;
    public float m_attraction_push = 2f;
    public float m_attraction_push_distance = 5f;
}