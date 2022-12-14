using UnityEngine;

public class OrbitalCamera : MonoBehaviour
{
    public float MovementSpeed = 10f;
    public float RotationSpeed = 50f; 

    private Vector3 _movementDirection = Vector3.zero;
    private Vector3 _rotationDirection = Vector3.zero;

	private void Update()
    {
        ReadInput();
        ApplyInput();
    }

    private void ReadInput()
	{
        if (Input.GetMouseButton(1))
        {
            _rotationDirection.x = Input.GetAxis("Mouse X");
            _rotationDirection.y = Input.GetAxis("Mouse Y");
        }

        if (Input.GetMouseButton(2))
        {
            _movementDirection.x = -Input.GetAxis("Mouse X");
            _movementDirection.y = -Input.GetAxis("Mouse Y");
        }

        _movementDirection.z = Input.GetAxis("Mouse ScrollWheel");
    }

    private void ApplyInput()
	{
        if (_movementDirection.sqrMagnitude > 0)
        {
            transform.Translate(_movementDirection * MovementSpeed * Time.deltaTime);
        }

        if (_rotationDirection.sqrMagnitude > 0)
        {
			transform.Rotate(0f, _rotationDirection.x * RotationSpeed * Time.deltaTime, 0f, Space.World);
			transform.Rotate(-_rotationDirection.y * RotationSpeed * Time.deltaTime, 0f, 0f, Space.Self);
        }
    }
}