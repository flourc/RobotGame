using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public Transform player;
    public Transform gun;
    public Transform gunBody;


    public bool hostile;
    public Material glow;
    public float distanceFromPlayer;
    public GameObject bullet;
    public float fireRate = 1f;


    // Start is called before the first frame update
    void Start()
    {
        hostile = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.position, transform.position);
        
        if (distanceFromPlayer < 15f && !hostile) {
            hostile = true;
            InvokeRepeating("shoot", .5f, .7f);
            glow.SetColor("_EmissionColor", Color.red);
            gunBody.Rotate(new Vector3(-10f, 0f, 0f));
        }
        else if (distanceFromPlayer >= 15f && hostile){
            CancelInvoke();
            gunBody.Rotate(new Vector3(10f, 0f, 0f));
            hostile = false;
        }

        if (hostile) {
            transform.LookAt(player);
            transform.Rotate(new Vector3(-90f, 0f, 0f));
        }
        else {
            glow.SetColor("_EmissionColor", Color.blue);
        }
    }

    // public void setHostile() {
    //     hostile = true;
    //     InvokeRepeating("shoot", 1f, .5f);
    //     glow.SetColor("_EmissionColor", Color.red);
    //     gunBody.Rotate(new Vector3(-10f, 0f, 0f));
    // }

    public void FixedUpdate() {
    }

    public void shoot() {
        GameObject bul = Instantiate(bullet, gun.position, transform.rotation);
        Vector3 shootDir = transform.forward + new Vector3(-100f, 0f, 0f);
        bul.GetComponent<Rigidbody>().velocity = gun.forward * 30;
    }
}
