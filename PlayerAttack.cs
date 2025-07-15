using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    public GameObject Bulletprefeb;
    public Transform firepoint;
    public float bulletspeed = 20f;
    public Camera playercamera;
    public float Range = 100f;
    public GameObject impactEffect;
    public Animator animator;
    public ParticleSystem Muzzel;
    public AudioSource source;
    public AudioClip clip;
    private GunReload reload;

    void Start()
    {
        reload = GetComponent<GunReload>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reload.IsReloading)
            return; // don't shoot while reloading

        if (Input.GetMouseButtonDown(0))
        {
            if (reload.TryUseAmmo())
            {
                Shot();
            }
            else
            {
                Debug.Log("Out of Ammo! Press R to reload.");
                // Optionally, you can play an empty click sound here
            }
        }
    }

    void Shot() {
        animator.SetTrigger("Shoot");
        Muzzel.Play();
        if (source && clip) {

            source.PlayOneShot(clip);
        }



        Ray ray = playercamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, Range))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(Range);
        }

        // Calculate direction from firepoint to target
        Vector3 direction = (targetPoint - firepoint.position).normalized;

        // Spawn bullet
        GameObject bullet = Instantiate(Bulletprefeb, firepoint.position, Quaternion.LookRotation(direction));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firepoint.forward * bulletspeed;



    }

}

