using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 10f;
    public float zoomSpeed = 1f;
    public float rotationSpeed = 40f;

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
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(cameraSpeed * Time.unscaledDeltaTime * transform.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(cameraSpeed * Time.unscaledDeltaTime * -transform.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(cameraSpeed * Time.unscaledDeltaTime * -transform.right);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(cameraSpeed * Time.unscaledDeltaTime * transform.right);
        }

        transform.position -= (Vector3)Input.mouseScrollDelta;
    }
}
