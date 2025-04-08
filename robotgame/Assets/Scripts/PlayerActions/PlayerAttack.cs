
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 1;
    public float attackRange = 2f;
    public LayerMask enemyLayer;
    public Animator animator;
    public GameObject swordArm;

    private bool canFire = false;

    public GameObject gun;

    public Material glow;
    public GameObject bullet;
    public float fireRate = 1f;


    private bool isAttacking = false;

    void Start()
    {
        toggleArm();
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
        if (Input.GetMouseButtonDown(0) && swordArm.activeSelf && !isAttacking)
        {
            StartCoroutine(SlashAttack());
        }

        if (gun.activeSelf) {
            canFire = true;
        }
        else {
            canFire = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && canFire) {
            shoot();
        }
    }

    IEnumerator SlashAttack()
    {
        isAttacking = true;

        // Start attack animation by setting the slashing bool to true
        // animator.SetBool("slashing", true);
        animator.Play("slash");

        // Wait until the animation begins (OPTIONAL: short delay to simulate wind-up)
        
        // yield return new WaitForSeconds(0.2f);
        //NOTE i got rid of this because i feel like our attack is pretty fast
        
        // Wait until the animation is fully complete — adjust to match your animation length
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float attackDuration = stateInfo.length > 0 ? stateInfo.length : 0.7f;
        yield return new WaitForSeconds(attackDuration);

        // Now we check for enemies in range
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("enemy"))
            {
                EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }

        // End attack animation
        animator.SetBool("slashing", false);
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void shoot() {
        canFire = false;
        glow.SetColor("_EmissionColor", Color.red);
    
        GameObject bul = Instantiate(bullet, gun.GetComponent<Transform>().position, transform.rotation);
        bul.tag = "weapon";
        bul.GetComponent<Rigidbody>().velocity = gun.GetComponent<Transform>().right * 20;
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
        glow.SetColor("_EmissionColor", Color.blue);
    }
}
