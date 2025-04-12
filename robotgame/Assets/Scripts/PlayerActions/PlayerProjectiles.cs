using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectiles : MonoBehaviour
{

    public bool canFire;
    public Transform player;
    public Transform gun;

    public Material glow;
    public GameObject bullet;
    public float fireRate = 1f;

    public GameObject gunArm;
    public GameObject crosshair;

    public Vector3 worldPos;
    Vector3 mousePos;
    

    // Start is called before the first frame update
    void Start()
    {
        // canFire = false;
        glow.SetColor("_EmissionColor", Color.blue);
        mousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunArm.activeSelf) {
            crosshair.SetActive(true);
            canFire = true;
        }
        else {
            crosshair.SetActive(false);
            canFire = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && canFire) {
            shoot();
        }

        worldPos = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, GetComponent<Camera>().nearClipPlane));
        crosshair.transform.position = worldPos;
    }

    public void FixedUpdate() {
    }

    public void shoot() {
        canFire = false;
        // glow.SetColor("_EmissionColor", Color.red);
    
        GameObject bul = Instantiate(bullet, gun.position, player.rotation);
        bul.tag = "weapon";
        bul.GetComponent<Rigidbody>().velocity = gun.right * 20;
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
        glow.SetColor("_EmissionColor", Color.blue);
    }
}
