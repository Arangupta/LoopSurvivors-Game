using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class playermovement : MonoBehaviour
{
    public float speed = 12f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    [Header("FootSound")]
    public AudioClip footstepClips;
    public AudioSource audioSource;
    public float stepDelay = 0.5f;
    private float stepTimer;

    private float defaultSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        defaultSpeed = speed;
      
        stepTimer = stepDelay;
    }

    void Update()
    {
        // Use built-in grounded check from CharacterController
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // small downward force to keep grounded
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded && (x != 0 || z != 0))
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                audioSource.PlayOneShot(footstepClips);
                stepTimer = stepDelay;
            }
        }
        else
        {
            stepTimer = stepDelay;
        }

    }

    public IEnumerator SpeedBoost(float boostAmount, float boostDuration)
    {
        speed *= boostAmount;
        yield return new WaitForSeconds(boostDuration);
        speed = defaultSpeed;
    }

}
