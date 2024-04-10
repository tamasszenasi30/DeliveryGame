using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    AudioSource audioS;

    //Old method to move object
    Rigidbody playerRB;

    // foodObject GameObject indicator
    public GameObject foodObject;

    // movement direction indicator
    public GameObject movementDirection;

    // player GameObject
    public GameObject player;

    // ratio for tuning the player's speed
   public float playerSpeedRatio = 5f;

    // Vector3 variable for storing the distance between two points in space
    Vector3 distance;

    // Vector3 for rotating right
    private static Vector3 rotationRight = new Vector3(0f, 120f, 0f);

    // Vector3 for rotating left
    private static Vector3 rotationLeft = new Vector3(0f, -120f, 0f);

    // multiplier for rotating
    private float rotationMultiplier = 1f;
    
    // BOOLEAN VARIABLES to determine the status of things during runtime
    
    // boolean to see when we are moving forward, set to false by default
    private bool movesForward = false;

    // boolean to see when we are moving backwards, set to false by default
    private bool movesBackwards = false;

    [HideInInspector]
    // boolean to see when the player is on the Restaurant Zone trigger
    public bool isRestaurantZoneTouched;

    [HideInInspector]
    // boolean to see if the customerzone is touched
    public bool isCustomerZoneTouched;

    [HideInInspector]
    // boolean to see when the food is picked up
    public bool foodPickedUp;

    [HideInInspector]
    public bool indicatorIsSpawned = false;

    public bool paused = false;

    // Cross References to get access to variables and methods from other scripts/

    Instructions instructionScript;

    UI uiScript;

    CountDown countDownRef;

    Slider sliderScript;

    // End of cross references 

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        // saying the editor that when we mention playerRb, we refer to the RigidBody component of the player
        playerRB = GetComponent<Rigidbody>();

        // foodObject is not active by default as we do not have food
        foodObject.SetActive(false);

        // when we mention uiScript reference variable we are mentioning the UI script
        uiScript = FindObjectOfType<UI>();

        // food is not picked up by default
        foodPickedUp = false;

        // when we mention countDownRef variable, we refer to the CountDown script
        countDownRef = FindObjectOfType<CountDown>();

        // when we mention instructionScript, we refer to the Instructions script
        instructionScript = FindObjectOfType<Instructions>();

        sliderScript = FindObjectOfType<Slider>();

        // this is to stop the player Rigidbody's rotation/shaking due to physics in the scene
        playerRB.freezeRotation = true;

        audioS = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isRestaurantZoneTouched || !isCustomerZoneTouched)
        {
            PlayerMove();
            // calling the player rotation method 
            PlayerRotation();
        }

        if (isRestaurantZoneTouched && countDownRef.countDownIsOn)
        {
            playerRB.MovePosition(transform.position);
        }

        if (isRestaurantZoneTouched && !countDownRef.countDownIsOn)
        {
            PlayerMove();
            // calling the player rotation method 
            PlayerRotation();

        }

        if(sliderScript.sliderImage.fillAmount == 0)
        {
            playerRB.MovePosition(transform.position);
        }

        GamePaused();
    }

    void PlayerMove()
    {

        // X Axis is A and D - Horizontal
        // Y Axis won't change - nothing
        // Z Axis is W and S - Vertical

        // Taking values of the horizontal Axis off as a temp variable
        //Vector3 horizontalInput = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        // Taking values of the vertical axis off as a temp variable
        //Vector3 verticalInput = new Vector3(0, 0, Input.GetAxis("Vertical"));

        // calculating the distance between the player's position and the movement direction indicator
        distance = player.transform.position - movementDirection.transform.position;

        // W key is held down
        if (Input.GetKey(KeyCode.W))
        {
            movesForward = true;
            // move rigidbody from actual position on the vertical Axis (Up and Down)
            // as the Time goes and with -player speed
            //Vector3 forwardVector = new Vector3(xMovement, 0, 0);

            playerRB.MovePosition(transform.position + -distance * playerSpeedRatio * Time.deltaTime);

            // moving the player forward with playerSpeed and as the time goes

            // use transform.translate to move gameobject
            //transform.Translate(forward * playerSpeed * Time.deltaTime);
        }

        else
        {
            movesForward = false;
        }

        // S key is held down
        if (Input.GetKey(KeyCode.S))
        {
            movesBackwards = true;
            // move rigidbody from actual position on the vertical Axis (Up and Down)
            // as the time goes and with player speed
            playerRB.MovePosition(transform.position + distance * playerSpeedRatio * Time.deltaTime);

            // moving the player backward with player speed as the time goes
            //transform.Translate(backward * playerSpeed * Time.deltaTime);
        }

        else
        {
            movesBackwards = false;
        }

    }

    void PlayerRotation()
    {
        // A key is held down
        if (Input.GetKey(KeyCode.A))
        {
            // move rigidbody from actual position on the -horizontal Axis (Left and right)
            // as the time goes and with player speed
            //playerRB.MovePosition(transform.position + -horizontalInput * Time.deltaTime * playerSpeed);

            // Rotation goes - on Y axis

            Quaternion deltaRotationLeft = Quaternion.Euler(rotationLeft * rotationMultiplier * Time.deltaTime);

            Quaternion deltaRotationRight = Quaternion.Euler(rotationRight * rotationMultiplier * Time.deltaTime);

            if (movesForward)
            {
                playerRB.MoveRotation(playerRB.rotation * deltaRotationLeft);
            }


            if (movesBackwards)
            {
                playerRB.MoveRotation(playerRB.rotation * deltaRotationRight);
            }

        }

        // D key is held down
        if (Input.GetKey(KeyCode.D))
        {
            // move rigidbody from actual position on the horizontal Axis (Left and right)
            // as the time goes and with -player speed
            //playerRB.MovePosition(transform.position + horizontalInput * Time.deltaTime * -playerSpeed);

            Quaternion deltaRotationLeft = Quaternion.Euler(rotationLeft * rotationMultiplier * Time.deltaTime);
            // rotation values of the player
            Quaternion deltaRotationRight = Quaternion.Euler(rotationRight * rotationMultiplier * Time.deltaTime);

            if (movesForward)
            {
                playerRB.MoveRotation(playerRB.rotation * deltaRotationRight);
            }

            if (movesBackwards)
            {
                playerRB.MoveRotation(playerRB.rotation * deltaRotationLeft);
            }
        }
    }

    // if player collides with another trigger
    public void OnTriggerEnter(Collider other)
    {
        // if the other gameobject has a tag on it "RestaurantZone"
        // and the food is not picked up yet
        if (other.gameObject.tag == "RestaurantZone" && !foodPickedUp)
        {
            // play pick up sound
            audioS.Play();

            // remove the spawnobject from screen
            Destroy(instructionScript.spawnObject);

            // set indicator spawned to false as the indicator is not spawned now
            indicatorIsSpawned = false;

            // the restaurant's pick up zone is touched
            isRestaurantZoneTouched = true;

            // set the remaining seconds to a random variable returning from the set rand time method
            countDownRef.remainingSeconds = countDownRef.SetRandTime();

            // start the countdown method
            countDownRef.StartCountDown();

            // start the pick up coroutine
            StartCoroutine(StartCountDownForPickUp());
        }

        // add score only if we are colliding with the customer zone
        // and we have food on the back
        if(other.gameObject.tag == "CustomerZone" && foodPickedUp)
        {
            Destroy(instructionScript.spawnObject);

            indicatorIsSpawned = false;

            isCustomerZoneTouched = true;
            // start the count down routine for unloading the food
            StartCoroutine(StartCountDownForUnload());

        }

        if(other.gameObject.tag== "CustomerZone" && !foodPickedUp)
        {
            isCustomerZoneTouched = true;
        }


        // if touch a trigger box labelled "Petrol"
        //if(other.gameObject.tag == "Petrol" && uiScript.score > 5)
        //{
            // fills up the petrol completely
            //sliderScript.sliderImage.fillAmount = 1f;

            // takes 5 from the score
            //uiScript.score -= 5;
        //}

        if (other.gameObject.tag == "Petrol" && uiScript.score == 0.0f)
        {
            countDownRef.countdownText.text = "You do not have enough money!";
            uiScript.score = 0;
        }
    }

    // when the player LEAVES a certain trigger area
    public void OnTriggerExit(Collider other)
    {
        // the trigger area named as " " and we do have an order
        if(other.gameObject.tag == "RestaurantZone" && foodPickedUp)
        {
            // we left the restaurant's pick up zone
            isRestaurantZoneTouched = false;

            // taking the random position for the customer indicator and set it to the
            // random customer index
            instructionScript.randomCustomerIndex = instructionScript.ChooseRandomCustomer();

            // spawning the indicator
            instructionScript.ShowCustomerIndicator();

            indicatorIsSpawned = true;

        }

        // If the player LEAVES the customer zone and we do NOT have an order
        if(other.gameObject.tag == "CustomerZone" && !foodPickedUp && !indicatorIsSpawned)
        {
            isCustomerZoneTouched = false;
            // taking the random position for the restaurant indicator and set it to the 
            // random restaurant index
            instructionScript.randomRestaurantIndex = instructionScript.ChooseRandomRestaurant();

            // spawning the indicator to the random position
            instructionScript.ShowRestaurantIndicator();

            indicatorIsSpawned = true;

            // show the Idle message in the UI
            countDownRef.ShowIdleMessage();
  
        }

        if(other.gameObject.tag == "CustomerZone" && !foodPickedUp && indicatorIsSpawned)
        {
            isCustomerZoneTouched = false;
            
        }
        
    }

    // Pickup coroutine
    IEnumerator StartCountDownForPickUp()
    {
        // wait for 6 seconds
        yield return new WaitForSeconds(countDownRef.remainingSeconds);
        // set the food indicator to active, the food appears on the back of the motorbike
        foodObject.SetActive(true);
        // set the boolean to true as we picked up the food
        foodPickedUp = true;
    }

    // Unload coroutine
    IEnumerator StartCountDownForUnload()
    {
        countDownRef.UnloadTextOnScreen();
        // wait for 1 second for unloading the food
        yield return new WaitForSeconds(1f);

        // Adding random money returning from the randomMoneyGenerator to the player's score 
        uiScript.score += uiScript.RandomMoneyGenerator();

        countDownRef.ShowIdleMessage();

        // set the food indicator to false as we unloaded the food
        foodObject.SetActive(false);
        // set the boolean to false as the food is delivered
        foodPickedUp = false;

    }

    void GamePaused()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            paused = true;
            SceneManager.LoadScene(0);
        }





    }   
}
