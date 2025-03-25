using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMover : MonoBehaviour
{
    public Animator anim;
    public float moveDirection;
    public Vector3 movement;
    public Transform cameraObject;
    public CharacterController control;
    public float speed;
    public bool shouldJump;
    public Camera cam1;
    public Camera cam2;
    public bool slashOrStab;
    public GameHandler gh;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        slashOrStab = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            anim.SetBool("walking", true);
        }
            
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || 
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            anim.SetBool("walking", true);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) ||
            Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) ||
            Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) {
            anim.SetBool("walking", false);
            anim.Play("rest");
        }

        if (Input.GetKeyDown(KeyCode.W) && (transform.rotation.y < 80f || transform.rotation.y > 100f)) {
            transform.eulerAngles = new Vector3(0f, 90f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.A) && (transform.rotation.y > -10f || transform.rotation.y < 10f)) {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.S) && (transform.rotation.y < 250f || transform.rotation.y > 280f)) {
            transform.eulerAngles = new Vector3(0f, 270f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.D) && (transform.rotation.y < 170f || transform.rotation.y > 190f)) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (transform.position.y < -20f) {
            // transform.position = new Vector3(2f, 10f, 11f);
            gh.MainMenu();
        } //why is this permanent.


        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            switchThirdPersonCam();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            switchOmniCam();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            print("hi");
            gh.levelTwoTemp();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (slashOrStab = true) {
                anim.Play("slash");
                slashOrStab = false;
            }
            else {
                anim.Play("stab");
                slashOrStab = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            anim.Play("grab");
        }
        
    }
    

    private void FixedUpdate(){
        float currentFallSpeed = control.velocity.y;
        currentFallSpeed -= (2f * Time.deltaTime);
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), currentFallSpeed, Input.GetAxisRaw("Vertical"));

        control.Move(movement*Time.deltaTime*speed);

    }

    public void switchThirdPersonCam() {
        cam1.enabled = true;
        cam2.enabled = false;
    }

    public void switchOmniCam() {
        cam1.enabled = false;
        cam2.enabled = true;
    }




}
