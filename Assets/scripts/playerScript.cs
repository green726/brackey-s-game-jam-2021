using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
//TODO: when slide move the head position down, and back up after slide finish
public class playerScript : MonoBehaviour
{
    public float speedMult = 7;
    public float mouseXmult = 1;
    public float mouseYmult = 1;
    public GameObject cam;
    public CinemachineVirtualCamera virCam;
    public CinemachinePOV pov;
    public bool touchingGround;
    public Transform bottom;
    public LayerMask groundLMask;
    public float jumpSpeed = 6f;
    public float slideSpeed = 10f;
    public float stamina = 25f;
    public TextMeshProUGUI staminaDisplay;
    public bool sprinting = false;
    public bool sliding = false;
    public Rigidbody rb;
    public CapsuleCollider coll;
    public Transform head;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cam = GameObject.Find("playerCam");
        virCam = cam.GetComponent<CinemachineVirtualCamera>();
        pov = virCam.GetCinemachineComponent<CinemachinePOV>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
       
        //stamina
        if (stamina < 0f)
        {
            stamina = 0f;
        }

        if (stamina < 25f)
        {
            stamina += 1f * Time.deltaTime * 4;
        }

        staminaDisplay.text = "Stamina: " + Mathf.Round(stamina);

        //jumping
        if (Input.GetKeyDown("space") && touchingGround == true && stamina >= 10)
        {

            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            stamina -= 10f;
        }

        //sprinting
        if (Input.GetKey(KeyCode.LeftShift) && touchingGround == true && stamina >= 4)
        {
            sprinting = true;
            speedMult = 12f;
            stamina -= 4f * Time.deltaTime * 3;
        }

        if (sprinting == false)
        {
            speedMult = 7f;
        }

        //sliding
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Mouse4) && touchingGround == true && stamina >= 5)
        {
            playerSlide();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Mouse4))
        {
            playerEndSlide();
        }

        void playerSlide()
        {
         
            sliding = true;
            coll.height = 1;
            Vector3 headDown = new Vector3(0, -.7f, 0);
            head.Translate(headDown);
            rb.AddForce(transform.forward * slideSpeed, ForceMode.VelocityChange);
            stamina -= 5f;

        }
        void playerEndSlide()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            sliding = false;
            coll.height = 2;
            Vector3 headUp = new Vector3(0, .7f, 0);
            head.Translate(headUp);
        }



    }
  
  

    void FixedUpdate()
    {
        sprinting = false;

        //groundChecking
        touchingGround = Physics.CheckSphere(bottom.position, .2f, groundLMask);
        //mouse input and player rotation

        Quaternion rotateAmount = Quaternion.Euler(0, pov.m_HorizontalAxis.Value, 0);
        transform.rotation = rotateAmount;

        //player movement
        
                float xVal = Input.GetAxis("Horizontal");
                float zVal = Input.GetAxis("Vertical");

                Vector3 moveAmount = transform.right * xVal + transform.forward * zVal;

        if (sliding == false)
        {
            rb.MovePosition(transform.position + moveAmount * Time.deltaTime * speedMult);

        }
    }
}
