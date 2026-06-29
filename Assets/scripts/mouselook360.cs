using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook360 : MonoBehaviour
{
    public float sensitivity = 2f;
    public Transform player;

    private float xRotation = 0f;

    void Update()
    {
        if (Mouse.current == null)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * sensitivity * Time.deltaTime * 100f;
        float mouseY = mouseDelta.y * sensitivity * Time.deltaTime * 100f;

        // Rotate the player horizontally
        player.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}