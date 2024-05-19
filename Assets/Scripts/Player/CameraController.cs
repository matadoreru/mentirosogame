using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float zoomSpeed = 10f;
    public float rotationSpeed = 100f;
    public float minZoomDistance = 2f;
    public float maxZoomDistance = 50f;
    public float yCoordinateLimit = 10f; // Limite de coordenada Y para el zoom

    private Vector3 movement;
    private float rotationX = 0f;
    private float rotationY = 0f;

    void Update()
    {
        HandleMovement();
        HandleZoom();
        HandleRotation();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calcula la dirección en el plano XZ
        Vector3 forward = transform.forward;
        forward.y = 0; // Ignora el componente Y
        forward.Normalize(); // Normaliza para mantener la magnitud correcta

        Vector3 right = transform.right;
        right.y = 0; // Ignora el componente Y
        right.Normalize(); // Normaliza para mantener la magnitud correcta

        // Calcula el movimiento basado en la dirección del plano XZ
        movement = (forward * moveZ + right * moveX) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoomDirection = transform.forward * scroll * zoomSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + zoomDirection;


        if (newPosition.y > 0 && newPosition.y < 35)
        {
            // Limita la coordenada Y
            newPosition.y = Mathf.Clamp(newPosition.y, -yCoordinateLimit, yCoordinateLimit);
            transform.position = newPosition;
        }
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(1)) // Botón derecho del ratón
        {
            rotationX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            rotationY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, -90, 90);

            transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);
        }
    }
}
