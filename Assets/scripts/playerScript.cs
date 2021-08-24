using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//TODO: when slide move the head position down, and back up after slide finish
public class playerScript : MonoBehaviour
{
    public string playerLevelInt;
    public string playerLevelString;
    public Scene currentScene;
    public string[] collectedPowerUps;
    public float baseSensMult = 100;
    public float speedMult = 7;
    public float mouseSens = 1;
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
    public LayerMask wallMask;
    public float wallrunForce, maxWallRunTime, maxWallrunSpeed;
    bool isWallLeft, isWallRight, isWallRunning;
    public float maxWallrunCamTilt, wallRunCamTilt;
    public bool isPaused = false;
    public menuSystem menuScript;
    public GameObject escMenu;
    public GameObject optionsMenu;
    public GameObject sensSlider;
    public GameObject sensText;
    // Start is called before the first frame update
    void Start()
    {

        escMenu.SetActive(false);
        optionsMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cam = GameObject.Find("playerCam");
        virCam = cam.GetComponent<CinemachineVirtualCamera>();
        pov = virCam.GetCinemachineComponent<CinemachinePOV>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        currentScene = SceneManager.GetActiveScene();
        playerLevelString = currentScene.name;
        playerLevelInt = currentScene.name.Replace("game", "");
        Debug.Log(playerLevelInt);
    }

    // Update is called once per frame
    void Update()
    {
        //mouse input and player rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        pov.m_HorizontalAxis.m_InputAxisValue = mouseX;
        pov.m_VerticalAxis.m_InputAxisValue = mouseY;
        Quaternion rotateAmount = Quaternion.Euler(0, pov.m_HorizontalAxis.Value, 0);
        transform.rotation = rotateAmount;
        //transform.Rotate(0, mouseX, 0);
     


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
        //menus
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuScript.escMenuOpen == true)
            {
                menuScript.closeEsc();
            } else if (menuScript.escMenuOpen == false)
            {
                menuScript.openEsc();
            }
            
        }
        

    }
  
  

    void FixedUpdate()
    {
       
        sprinting = false;

        //groundChecking
        touchingGround = Physics.CheckSphere(bottom.position, .2f, groundLMask);
       

    

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
