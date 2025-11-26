using UnityEngine;

public class FreeFlyCamera : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float boostMultiplier = 3f;

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 200f;

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 rot = transform.localRotation.eulerAngles;
        yRotation = rot.y;
        xRotation = rot.x;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    void HandleMovement()
    {
        float speed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed *= boostMultiplier;

        float x = Input.GetAxis("Horizontal");   // A, D
        float z = Input.GetAxis("Vertical");     // W, S
        float y = 0f;

        if (Input.GetKey(KeyCode.E)) y = 1f;     // Move up
        if (Input.GetKey(KeyCode.Q)) y = -1f;    // Move down

        Vector3 direction =
            transform.forward * z +
            transform.right * x +
            transform.up * y;

        transform.position += direction * speed * Time.deltaTime;
    }
}
