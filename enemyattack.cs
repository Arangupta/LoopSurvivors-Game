using System.Collections;
using UnityEngine;

public class enemyattack : MonoBehaviour
{
    public Transform player;

    public GameObject bulletprefeb;
    public Transform firePoint;
    public float bulletspeed = 50f;
    public float shootrange = 15f;
    public float RateofFire = 5f;
    private float nextFireTime = 0f;
    public ParticleSystem MuzzelFlash;
    public AudioSource source;
    public AudioClip clip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(fireinterval());

    }
    IEnumerator fireinterval() {
        while (true)
        {
            yield return new WaitForSeconds(RateofFire);

        }
    
    
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= shootrange && Time.time >= nextFireTime)
        {
            Shootplayer();
            nextFireTime = Time.time + RateofFire;
        
        
        }
    }

    void Shootplayer() {
        Vector3 lookDirection = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookDirection);
        MuzzelFlash.Play();
        if (source && clip)
        {

            source.PlayOneShot(clip);
        }

        GameObject bullet = Instantiate(bulletprefeb, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = (player.position - firePoint.position).normalized * bulletspeed;
        Debug.Log("enemy shoot the player");
    
    }

}
