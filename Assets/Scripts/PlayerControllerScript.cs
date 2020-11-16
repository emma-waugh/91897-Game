using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    private Rigidbody playerRb;
    //Jump Force and Gravity
    public float jumpForce = 10.0f;
    public float gravityModifier;

    //Score - points
    public Text ScoreText;
    private int Score;
    private int playerLevel;
    //last level (SceneEnd) in the game
    private int playerLevelEnd;  

    //Prevent player from double-jumping
    public bool isOnGround = true;
    //Player speed
    public float horizontalInput;
    public float speed = 7f;

    //Keep Player Inbounds - unable to move off the screen
    public float xRangeLeft = 7f;
    public float xRangeRight = 65.8f;
    public float yRange = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Make player jump if spacebar pressed
        playerRb = GetComponent<Rigidbody>();
        //Jump Force and Gravity
        Physics.gravity = new Vector3(0, gravityModifier,0);
        //
        ScoreText.text = "Score = 0";
        //Loading saved variable values as the scene changes
        playerPrefsLoad();
        playerLevelEnd = 3;
        //delete all registry values if starting level 1 to make sure all values have been cleared
        if (playerLevel == 1) {playerPrefsDelete();} 
    }

    // Update is called once per frame
    void Update()
    {
        //Make player jump if spacebar pressed - Prevent player from double-jumping
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            //Make player jump if spacebar pressed
            playerRb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
            //Prevent player from double-jumping
            isOnGround = false;
        }

        //Move player left - right
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        
        //Keep Player Inbounds - unable to leave the screen
        if (transform.position.x < -xRangeLeft)
        {
            transform.position = new Vector3(-xRangeLeft, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRangeRight)
        {
            transform.position = new Vector3(xRangeRight, transform.position.y, transform.position.z);
        }

        //BOTTOM, If you fall through and hit nothing (or glitch out) you die/reset  
        if (transform.position.y < -yRange)
        {
            //Reloads the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }

    //Collision Detector
    private void OnCollisionEnter(Collision collision)
    {
        //Prevent Player from double-jumping
        isOnGround = true;

        //When the player collects a coin, a point is added to the score and the coin is removed/dissapears
        if (collision.gameObject.CompareTag("Coin"))
        {
            //Adds a point to the score
            Score = Score+1;
            ScoreText.text = "Score = "+Score.ToString();
            //Removes the coin / makes it dissapear
            Destroy(collision.gameObject);
        }

        //When player hits Spike or Water or Saw or Lava they return to start and redo level
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Water") || collision.gameObject.CompareTag("Saw") || collision.gameObject.CompareTag("Lava")) // The || means OR
        {
            //Reloads the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        //When player hits chest they will move onto next level
        if (collision.gameObject.CompareTag("ChestClosed"))
        {
            if (playerLevel < playerLevelEnd)
            {
                //Increasing the level before moving to the next scene
                playerLevel++;
                //Saving current values before moving to next scene
                playerPrefsSave();
                SceneManager.LoadScene("Scene" + playerLevel);
            }
            else
            {
                //Increasing the level to allow end game scene
                playerLevel++;
                //Saving current values before moving to next scene
                playerPrefsSave();
                //Loading final scene
                SceneManager.LoadScene("SceneEnd");
                //Showing final score
                ScoreText.text = "Score = " + Score.ToString();
            }
        }      
    }

    void OnGUI()
    {
        //This displays a Button on the screen at (x,y,w,h). The button’s text reads the last parameter. Press this for the SceneManager to load the Scene. 

        if (playerLevel <= playerLevelEnd)
        {
            //This is the restart button on scenes 1,2,3
            if (GUI.Button(new Rect(Screen.width - 167, 2, 30, 25), "<<"))
            {
                //This calls the routing to delete all player preferences before starting again
                playerPrefsDelete();
                //The SceneManager loads your Start Scene as a single Scene (not overlapping). This is Single mode
                SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
            }

            //This is the Retry button
            if (GUI.Button(new Rect(Screen.width - 135, 2, 30, 25), "<"))
            {
                //Reseting the current scene - Retry
                Resources.UnloadUnusedAssets();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            }

            GUI.color = Color.black;
            //Showing what level the Player is on
            GUI.Box(new Rect(Screen.width - 102, 2, 100, 25), "Level : " + playerLevel.ToString());
        }
        else
        {
            //This is the Restart button on the end scene
            if (GUI.Button(new Rect(Screen.width/2-40, Screen.height/2+100, 80, 30), "Restart"))
            {
                //This calls the routing to delete all player preferences before starting again
                playerPrefsDelete();
                //The SceneManager loads your Start Scene as a single Scene (not overlapping). This is Single mode
                SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
            }
        }
    }


    public void playerPrefsLoad()
    {
        //If the PlayerPrefs file currently has a value registered to the playerScoreKey,  
        if (PlayerPrefs.HasKey("scoreKey"))
        {
            //load playerScore from the PlayerPrefs file. 
            Score = PlayerPrefs.GetInt("scoreKey");
            ScoreText.text = "Score = " + Score.ToString();
        }

        else
        {
            //Otherwise the score will be set to 0
            ScoreText.text = "Score = 0";
            Score = 0;
        }

        if (PlayerPrefs.HasKey("playerLevel"))
        {
            //load playerLevel from the PlayerPrefs file. 
            playerLevel = PlayerPrefs.GetInt("playerLevel");
        }

        else
        {
            //Otherwise the level will be set to 1
            playerLevel = 1;
        }

    }

    public void playerPrefsSave()
    {
        //Set the values to the PlayerPrefs file using their corresponding keys
        PlayerPrefs.SetInt("scoreKey", Score);
        PlayerPrefs.SetInt("playerLevel", playerLevel);

        //Saving the PlayerPrefs file to disk/registry, so it is accessible in later routines e.g. scene change
        PlayerPrefs.Save();
    }

    public void playerPrefsDelete()
    {
        //Delete all values from the PlayerPrefs file
        PlayerPrefs.DeleteAll();
    }




}
