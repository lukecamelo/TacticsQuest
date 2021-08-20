using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera[] m_cameras;
    public Transform m_tacticsCamera;
    Camera m_currentCamera;

    public bool useStaticBillboard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SetCurrentCamera();

        if (!useStaticBillboard) {
            transform.LookAt(m_currentCamera.transform);
        } else {
            transform.rotation = m_currentCamera.transform.rotation;
        }

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }

    void SetCurrentCamera() {
        m_tacticsCamera = Object.FindObjectOfType<TacticsCamera>().transform;
        m_cameras = m_tacticsCamera.GetComponentsInChildren<Camera>();
        foreach(Camera cam in m_cameras) {
            if(cam.isActiveAndEnabled) {
                m_currentCamera = cam;
            }
        }
    }
}
