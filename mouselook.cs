using UnityEngine;

public class mouselook : MonoBehaviour
{

    public float mousesentivity = 100f;

    float xrotation;
    float yrotation;
    float mousex;
    float mousey;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()

    {
        Cursor.lockState = CursorLockMode.Locked;

    }
    // Update is called once per frame
    void Update()
    {
            mousex = Input.GetAxis("Mouse X") * mousesentivity * Time.deltaTime;
            mousey = Input.GetAxis("Mouse Y") * mousesentivity * Time.deltaTime;

            xrotation -= mousey;

            xrotation = Mathf.Clamp(xrotation, -90f, 90f);

            yrotation += mousex;

            transform.localRotation = Quaternion.Euler(xrotation, yrotation, 0);
        

    }
}
