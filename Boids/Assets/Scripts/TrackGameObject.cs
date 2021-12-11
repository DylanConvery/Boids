using UnityEngine;

public class TrackGameObject : MonoBehaviour {
    //grab object tagged "track"
    private void Start() { m_game_object = GameObject.FindGameObjectWithTag("track"); }

    //update camera position to track GameObject
    private void Update() { transform.LookAt(m_game_object.transform.position); }

    public GameObject m_game_object;
}