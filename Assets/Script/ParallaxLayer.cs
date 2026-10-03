using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Attach this to each parallax layer (Background, MidGround, Foreground).
/// Set parallaxMultiplier:
///   - Background  -> small value  (e.g. 0.02)
///   - Midground   -> medium value (e.g. 0.05)
///   - Foreground  -> large value  (e.g. 0.10) or 0 to keep static
/// </summary>
public class ParallaxLayer : MonoBehaviour
{
    [Header("Parallax Settings")]
    [Tooltip("How much this layer moves horizontally relative to the mouse. 0 = no movement.")]
    public float parallaxMultiplierX = 0.05f;

    [Tooltip("How much this layer moves vertically relative to the mouse. 0 = no movement.")]
    public float parallaxMultiplierY = 0.02f;

    [Tooltip("How smoothly the layer follows the mouse.")]
    public float smoothSpeed = 5f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        // Normalize mouse to -0.5 .. 0.5 range
        float normalizedX = (mousePos.x / Screen.width)  - 0.5f;
        float normalizedY = (mousePos.y / Screen.height) - 0.5f;

        Vector3 targetPosition = startPosition + new Vector3(
            normalizedX * parallaxMultiplierX,
            normalizedY * parallaxMultiplierY,
            0f
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * smoothSpeed
        );
    }
}
