
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 1;
    public float attackRange = 3f;
    public LayerMask enemyLayer;
    public Animator animator;
    public GameObject swordArm;

    private bool canFire = false;

    public GameObject gun;
    public Transform bulletSpawn;
    public GameObject crosshair;
    public Vector3 mousePos;
    public Vector3 worldPos;

    public Material glow;
    public GameObject bullet;
    public float fireRate = 1f;

    private bool isAttacking = false;
    public Camera camera;
    public bool cameraSnap;

    public PauseMenu pm;
    public KeypadUI kp;

    public GameHandler handler;

    void Start()
    {
        // toggleArm(); //should this be in update?
        // gun.SetActive(true); //temp
        canFire = true;
        crosshair.SetActive(false);

    }

    public void toggleArm()
    {
        if (swordArm.activeSelf) {
            swordArm.SetActive(false);
            gun.SetActive(true);
        } else if (gun.activeSelf) {
            gun.SetActive(false);
            swordArm.SetActive(true);
        }
    }

    void Update()
    {


        // gun.SetActive(true);//temp i couldnt make it active for some reason
       
        if (Input.GetMouseButtonDown(0) && swordArm.activeSelf && !isAttacking)
        {
            // StartCoroutine(SlashAttack());
            slashTemp();
        }

        // crosshair.SetActive(false);
        // canFire = false;

        if (handler.UIActive) {
            if (crosshair.activeSelf) {
                crosshair.SetActive(false);
                canFire = false;
            }
        }
        else if (gun.activeSelf) {
            crosshair.SetActive(true);
            canFire = true;
        }
        else {
            crosshair.SetActive(false);
            canFire = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && canFire) {
            crosshair.GetComponent<Image>().color = Color.red;
            shoot();
        }
        // TEMPORARILY ERASED

        //TEMP REPLACEMENT BELOW

        Vector3 targetPosition = new Vector3(worldPos.x,
                                       worldPos.y, 
                                      worldPos.z);//? i kno this looks stupid but its so its easier to switch out any one of the variables lool

        Vector3 roboTarget = new Vector3(worldPos.x, 
                                       transform.position.y, 
                                       worldPos.z);



        if (Input.GetMouseButton(0) && canFire) {
            crosshair.GetComponent<Image>().color = Color.red;
            cameraSnap = true;
            transform.LookAt(roboTarget);
            gun.GetComponent<Transform>().LookAt(targetPosition);
            if (!animator.GetBool("Walking")){
                animator.Play("shoot");
            }
            // if (animator.GetBool("Walking")) {
            //     // animator.SetBool("aiming", true);
            // }
            // else {
            //     animator.Play("shoot");
            // }
        
        }
        if (canFire &&Input.GetMouseButtonUp(0))
        {
            crosshair.GetComponent<Image>().color = Color.white;
            shoot();
            cameraSnap = false;
        }
    //cursor stuff
        // Camera camera = SceneView.lastActiveSceneView.camera;
        mousePos = Input.mousePosition;
        worldPos = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, camera.nearClipPlane + 20));        
        crosshair.transform.position = mousePos;

    }

    public bool returnSnap() {
        return cameraSnap;
    }

    public void slashTemp() {
         animator.Play("slash");

        // Now we check for enemies in range
        Collider[] hits = Physics.OverlapSphere(transform.position + new Vector3(0f, 2.5f, 0f), attackRange, enemyLayer);
        foreach (Collider hit in hits)
        {
            print("collision");
            if (hit.CompareTag("enemy") && hit.gameObject != null)
            {
                print("hit");
                EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0f, 2.5f, 0f), attackRange);
    }

    public void shoot() {
        canFire = false;

        glow.SetColor("_EmissionColor", Color.red);

    
        GameObject bul = Instantiate(bullet, bulletSpawn.position, transform.rotation);
        // print(bulletSpawn.position);
        bul.tag = "weapon";

        bul.GetComponent<Rigidbody>().velocity = gun.GetComponent<Transform>().forward * 50;
    

        // Vector3 targetPosition = new Vector3(worldPos.x,
        //                                worldPos.y, 
        //                               worldPos.z);//? i kno this looks stupid but its so its easier to switch out any one of the variables lool

        // Vector3 roboTarget = new Vector3(worldPos.x, 
        //                                transform.position.y, 
        //                                worldPos.z);

        // transform.LookAt(roboTarget);
        // gun.GetComponent<Transform>().LookAt(targetPosition);
        // bul.transform.LookAt(targetPosition);
        
        RaycastHit hit;
        
        
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
        glow.SetColor("_EmissionColor", Color.blue);
    }
}
