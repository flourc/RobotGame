using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectiles : MonoBehaviour
{

    public bool canFire;
    public Transform player;
    public Transform gun;
    public Transform gunBody;

    public Material glow;
    public GameObject bullet;
    public float fireRate = 1f;


    // Start is called before the first frame update
    void Start()
    {
        canFire = true;
        glow.SetColor("_EmissionColor", Color.blue);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canFire) {
            shoot();
        }
    }

    public void FixedUpdate() {
    }

    public void shoot() {
        canFire = false;
        glow.SetColor("_EmissionColor", Color.red);
        GameObject bul = Instantiate(bullet, gun.position, transform.rotation);
        Vector3 shootDir = transform.forward + new Vector3(-100f, 0f, 0f);
        bul.GetComponent<Rigidbody>().velocity = gun.forward * 30;
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {

        yield return new WaitForSeconds(fireRate);
        canFire = true;
        glow.SetColor("_EmissionColor", Color.blue);
    }
}
