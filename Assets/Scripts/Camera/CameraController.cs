using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 10f;
    public float zoomSpeed = 1f;
    public float rotationSpeed = 50f;

    private Transform camTransform;

    private void Start()
    {
        camTransform = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            cameraSpeed *= 3;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            cameraSpeed /= 3;
        }

        // pan
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += cameraSpeed * Time.unscaledDeltaTime * transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += cameraSpeed * Time.unscaledDeltaTime * -transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += cameraSpeed * Time.unscaledDeltaTime * -transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += cameraSpeed * Time.unscaledDeltaTime * transform.right;
        }

        // rotation
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(rotationSpeed * Time.deltaTime * transform.up);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(rotationSpeed * Time.deltaTime * -transform.up);
        }

        // zoom
        transform.position -= (Vector3)Input.mouseScrollDelta;
    }
}
