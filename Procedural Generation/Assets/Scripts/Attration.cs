using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Attration : MonoBehaviour
{
    public float radius = 5f;
    public float x_phase = 0.5f;
    public float y_phase = 0.4f;
    public float z_phase = 0.1f;

    private void FixedUpdate()
    {
        //update transform position to use cyclic movement
        transform.position = new Vector3(
            Mathf.Sin(x_phase * Time.time) * radius * transform.localScale.x,
            Mathf.Sin(y_phase * Time.time) * radius * transform.localScale.y,
            Mathf.Sin(z_phase * Time.time) * radius * transform.localScale.z
        );
    }
}
