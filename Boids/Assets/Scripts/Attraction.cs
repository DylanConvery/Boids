using UnityEngine;

public class Attraction : MonoBehaviour
{
    private void FixedUpdate()
    {
        //update transform position to use cyclic movement
        transform.position = new Vector3(
            Mathf.Sin(m_x_phase * Time.time) * m_radius * transform.localScale.x,
            Mathf.Sin(m_y_phase * Time.time) * m_radius * transform.localScale.y,
            Mathf.Sin(m_z_phase * Time.time) * m_radius * transform.localScale.z
        );
    }

    //serializable to change in the inspector. not const because of this.
    [SerializeField] private float m_radius = 5f;
    [SerializeField] private float m_x_phase = 0.5f;
    [SerializeField] private float m_y_phase = 0.4f;
    [SerializeField] private float m_z_phase = 0.1f;
}
