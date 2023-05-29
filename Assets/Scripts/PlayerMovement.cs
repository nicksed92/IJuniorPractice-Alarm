using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 6.0f;
    [SerializeField] private float _mouseSensitivity = 300.0f;

    private CharacterController _controller;
    private float _xRotation;

    private float _mouseX;
    private float _mouseY;
    private float _horizontal;
    private float _vertical;
    private Vector3 _moveDirection;

    public static UnityEvent<float> SpeedChanged = new UnityEvent<float>();

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        SpeedChanged.Invoke(Mathf.Clamp(Mathf.Abs(_horizontal + _vertical), 0f, 1f));

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, 5f, 45f);

        transform.Rotate(Vector3.up * _mouseX);

        _moveDirection = transform.right * _horizontal + transform.forward * _vertical;
        _controller.Move(_speed * Time.deltaTime * _moveDirection.normalized);
    }
}