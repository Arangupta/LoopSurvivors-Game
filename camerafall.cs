using UnityEngine;

public class CameraFall : MonoBehaviour
{
    public float fallDuration = 1.5f;
    public float fallAngle = 50f;

    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private bool isFalling = false;
    private float timer = 0f;

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = Quaternion.Euler(fallAngle, 0f, 70f); // Adjust this if needed
    }

    public void StartCameraFall()
    {
        isFalling = true;
        timer = 0f;
    }

    public void ResetFall()
    {
        isFalling = false;
        timer = 0f;
        transform.rotation = originalRotation;  // Reset the camera rotation
    }

    void Update()
    {
        if (isFalling)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / fallDuration);
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, t);
        }
    }
}
