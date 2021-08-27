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
    //save game vars, 
    //Int = the integer value of player level, string = name of scene player is on, pUps = array of unlocked gadgets/power ups
    public int playerLevelInt;
    public string playerLevelString;
    public Scene currentScene;
    public string[] collectedPowerUps;
    //player look and movement
    public float baseSensMult = 100;
    public float speedMult = 7;
    public float mouseSens = 1;
    //some necessary objects
    public GameObject cam;
    public CinemachineVirtualCamera virCam;
    public CinemachinePOV pov;
    //is the player touching the ground?
    public bool touchingGround;
    //the transform of the bottom of the player
    public Transform bottom;
    //layer mask for ground
    public LayerMask groundLMask;
    //more movement
    public float jumpSpeed = 6f;
    public float wallJumpOffSpeed = 7f;
    public float wallJumpUpOffSpeed = 1f;
    public float slideSpeed = 10f;
    public float stamina = 25f;
    //UI
    public TextMeshProUGUI staminaDisplay;
    //more movement
    public bool sprinting = false;
    public bool sliding = false;
    //more necessary values, head = transform of top of player
    public Rigidbody rb;
    public CapsuleCollider coll;
    public Transform head;
    //wallrunning
    public LayerMask wallMask;
    public float wallRunForce = 6f;
    public float maxWallRunTime;
    public float maxWallRunSpeed = 7f;
    bool isWallLeft, isWallRight, isWallRunning;
    public float maxWallrunCamTilt, wallRunCamTilt;
    public Transform orient;
    //pausing
    public bool isPaused = false;
    //more UI
    public menuSystem menuScript;
    public GameObject escMenu;
    public GameObject optionsMenu;
    public GameObject sensSlider;
    public GameObject sensText;
 
    


    // Start is called before the first frame update
    void Start()
    {
        stamina = 100f;
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
        if (currentScene.name.Contains("game"))
        {
            playerLevelInt = int.Parse(currentScene.name.Replace("game", ""));
            saveGame.performSave(GetComponent<playerScript>());
        }
        else
        {
            playerLevelInt = 99;
        }
        
        Debug.Log(playerLevelInt);
        
    }

    // Update is called once per frame
    void Update()
    {

        sprinting = false;
        //groundChecking
        touchingGround = Physics.CheckSphere(bottom.position, .1f, groundLMask);

        //wall run:
        checkForWall();
        wallRunInput();
        //wall run cam tilt activate
        if (Mathf.Abs(wallRunCamTilt) < maxWallrunCamTilt && isWallRunning && isWallRight)
        {
            wallRunCamTilt += Time.deltaTime * maxWallrunCamTilt * 2;
        }
        if (Mathf.Abs(wallRunCamTilt) < maxWallrunCamTilt && isWallRunning && isWallLeft)
        {
            wallRunCamTilt -= Time.deltaTime * maxWallrunCamTilt * 2;
        }
        //tilt wall run cam back
        if (wallRunCamTilt > 0 && !isWallRight && !isWallLeft)
        {
            wallRunCamTilt -= Time.deltaTime * maxWallrunCamTilt * 2;
        }
        if (wallRunCamTilt < 0 && !isWallRight && !isWallLeft)
        {
            wallRunCamTilt += Time.deltaTime * maxWallrunCamTilt * 2;
        }



        //mouse input and player rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        virCam.m_Lens.Dutch = wallRunCamTilt;
        pov.m_HorizontalAxis.m_InputAxisValue = mouseX;
        pov.m_VerticalAxis.m_InputAxisValue = mouseY;
        
        Quaternion rotateAmount = Quaternion.Euler(0, pov.m_HorizontalAxis.Value, 0);
        transform.rotation = rotateAmount;


        
         
        //stamina
        if (stamina < 0f)
        {
            stamina = 0f;
        }

        if (stamina < 100f)
        {
            stamina += 2f * Time.deltaTime * 4;
        }

        staminaDisplay.text = "Stamina: " + Mathf.Round(stamina);

        //jumping
        if (Input.GetKeyDown("space") && stamina >= 10)
        {
            //grounded jump
            if (touchingGround == true && isWallRunning == false)
            {
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
                stamina -= 10f;
            }
            //wall jump
            if (isWallRunning)
            {
                //not holding down button to side jump, normal jump
                if (isWallLeft && !Input.GetKey(KeyCode.D) || isWallRight && !Input.GetKey(KeyCode.A))
                {
                    Debug.Log("normal jump");
                    if (isWallRight) rb.AddForce(-orient.right * 4f);
                    if (isWallLeft) rb.AddForce(orient.right * 4f);
                    rb.AddForce(Vector3.up * wallJumpUpOffSpeed);
                   
                }
                //if wall is right and holding left jump left
                if (isWallRight && Input.GetKey(KeyCode.A))
                {
                    Debug.Log("side jump");
                    rb.AddForce(orient.up * wallJumpUpOffSpeed);
                    rb.AddForce(-orient.right * wallJumpOffSpeed);
                }
                //if wall is left and holding right jump right
                if (isWallLeft && Input.GetKey(KeyCode.D))
                {
                    Debug.Log("side jump");
                    rb.AddForce(orient.up * wallJumpUpOffSpeed);
                    rb.AddForce(orient.right * wallJumpOffSpeed);
                }
            }
            //double jump goes beneath here
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
        if (touchingGround == true && stamina >= 5 && Input.GetKeyDown(KeyCode.LeftControl) || touchingGround == true && stamina >= 5 && Input.GetKeyDown(KeyCode.Mouse4))
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
  
  //function to get player input and call wall run start
    public void wallRunInput()
    {
        if (Input.GetKey(KeyCode.D) && isWallRight)
        {
            startWallRun();
        }
        if (Input.GetKey(KeyCode.A) && isWallLeft)
        {
            startWallRun();
        }
    }
    //function to start wall run
    public void startWallRun()
    {
        rb.useGravity = false;
        isWallRunning = true;

        if (rb.velocity.magnitude <= maxWallRunSpeed)
        {
            rb.AddForce(orient.forward * wallRunForce * Time.deltaTime);

            if (isWallRight)
            {
                rb.AddForce(orient.right * wallRunForce * Time.deltaTime);
            } else if (isWallLeft)
            {
                rb.AddForce(-orient.right * wallRunForce * Time.deltaTime);
            }
        }
    }
    //end wall run function
    public void stopWallRun()
    {
        rb.useGravity = true;
        isWallRunning = false;
    }
    //function to check right and left of player for wall
    public void checkForWall()
    {
        //wall bools
        isWallRight = Physics.Raycast(transform.position, orient.right, 1f, wallMask);
        isWallLeft = Physics.Raycast(transform.position, -orient.right, 1f, wallMask);
        //code to call leave wall run
        if (!isWallLeft && !isWallRight)
        {
            stopWallRun();
        }
        //code to reset double jump goes below this comment
    }


    void FixedUpdate()
    {
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
