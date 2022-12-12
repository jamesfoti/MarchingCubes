using UnityEngine;

public class OrbitalCamera : MonoBehaviour
{
    public float MovementSpeed = 10f;
    public float RotationSpeed = 10f; 

    private Vector3 _movementDirection = Vector3.zero;
    private Vector3 _rotationDirection = Vector3.zero;

    private void Update()
    {
        ReadInput();
        ApplyInput();
    }

    private void ReadInput()
	{
        if (Input.GetMouseButton(0))
        {
            _movementDirection.x = -Input.GetAxis("Mouse X");
            _movementDirection.y = -Input.GetAxis("Mouse Y");
        }

        _movementDirection.z = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButton(1))
        {
            _rotationDirection.x += Input.GetAxis("Mouse X");
            _rotationDirection.y += Input.GetAxis("Mouse Y");
        }
    }

    private void ApplyInput()
	{
        if (_movementDirection.sqrMagnitude > 0)
        {
            transform.Translate(_movementDirection * MovementSpeed * Time.deltaTime);
        }

        if (_rotationDirection.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.Euler(-_rotationDirection.y * RotationSpeed, _rotationDirection.x * RotationSpeed, 0f);
        }
    }
}