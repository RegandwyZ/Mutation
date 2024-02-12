using UnityEngine;

public class CameraController : MonoBehaviour
{
    private readonly float _zoomSpeed = 15.0f;
    private readonly float _movementSpeed = 15.0f;
    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontalInput, 0, verticalInput) * (_movementSpeed * Time.deltaTime);
        transform.Translate(movement, Space.World);
        
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * (scroll * _zoomSpeed), Space.Self);
    }

}
