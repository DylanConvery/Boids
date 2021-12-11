using UnityEngine;

public class Attraction : MonoBehaviour {
    private void Start() { fixed_position = transform.position; }

    private void FixedUpdate() {
        //update transform position to use cyclic movement
        Vector3 offset = new Vector3(
            Mathf.Sin(m_x_phase * Time.time) * m_radius * transform.localScale.x,
            Mathf.Sin(m_y_phase * Time.time) * m_radius * transform.localScale.y,
            Mathf.Sin(m_z_phase * Time.time) * m_radius * transform.localScale.z
        );

        transform.position = fixed_position + offset;
    }

    [SerializeField] private float m_radius = 10f;
    [SerializeField] private float m_x_phase = 0.5f;
    [SerializeField] private float m_y_phase = 0.4f;
    [SerializeField] private float m_z_phase = 0.1f;

    private Vector3 fixed_position;
}