using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSway : MonoBehaviour
{

    float originalY;
    public float floatStrength = .01f;

    void Start() {
        transform.localPosition = new Vector3(0, 0.5f, 0);
        originalY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Mathf.Sin(Time.time) * floatStrength),
            transform.position.z);
    }
}
