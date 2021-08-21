using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    float minFov = 15f;
    float maxFov = 90f;
    float sensitivity = 10f;
    float zoom;
    float zoomSpeed = 15f;
    Camera m_camera;

    void Start() {
        m_camera = Camera.main;
        zoom = m_camera.fieldOfView;
    }

    void Update() {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        zoom = Mathf.Clamp(zoom, minFov, maxFov);
    }

    void LateUpdate() {
        m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, zoom, Time.deltaTime * zoomSpeed);
    }
}
