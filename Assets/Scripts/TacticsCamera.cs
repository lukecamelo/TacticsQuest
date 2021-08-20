using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour
{
    Camera[] cameras;
    private int currentCameraIndex = 0;
    private Camera currentCamera;

    void Awake() {
        cameras = transform.GetComponentsInChildren<Camera>();

        for (int i = 0; i < cameras.Length; i++) {
            cameras[i].gameObject.SetActive(false);
        }

        if(cameras.Length > 0) {
            currentCamera = cameras[0];
            currentCamera.gameObject.SetActive(true);
        }
    }

    // TODO: reimplement rotating left and replace cameras with gameobjects
    public void RotateLeft() {
        currentCameraIndex--;

        if (currentCameraIndex < 0) currentCameraIndex = cameras.Length - 1;

        ChangeView();
    }

    public void RotateRight() {
        currentCameraIndex++;

        if(currentCameraIndex > cameras.Length - 1) currentCameraIndex = 0;

        ChangeView();
    }

    void ChangeView() {
        // currentCamera.gameObject.SetActive(false);
        // currentCamera = cameras[currentCameraIndex];
        StartCoroutine(MoveCameraToPosition(currentCamera.transform, cameras[currentCameraIndex].transform, 1f));
        // currentCamera.gameObject.SetActive(true);
    }

    public IEnumerator MoveCameraToPosition(Transform _cameraToMove, Transform _locationToMoveTo, float _timeToMove)
    {
        Vector3 currentPos = _cameraToMove.transform.position;
        Quaternion currentRotation = _cameraToMove.transform.rotation;

        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / _timeToMove;

            _cameraToMove.transform.position = Vector3.Lerp(currentPos, _locationToMoveTo.position, t);
            _cameraToMove.transform.rotation = Quaternion.Lerp(currentRotation, _locationToMoveTo.rotation, t);

            // if (t > 0.8f)
            //     currentlyFollowing = _locationToMoveTo;
            yield return null;
        }
    }
}
