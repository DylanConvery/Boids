using UnityEngine;

public class LookAtAttraction : MonoBehaviour {
    //grab transform of object tagged "Attraction"
    private void Start() { m_attraction_transform = GameObject.FindGameObjectWithTag("Attraction").transform; }

    //update camera position to follow boid attraction
    private void Update() { transform.LookAt(m_attraction_transform.position); }

    //stores the boid attraction transform
    private Transform m_attraction_transform;
}