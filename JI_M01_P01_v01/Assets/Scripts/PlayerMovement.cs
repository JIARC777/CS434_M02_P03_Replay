//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    // grab Rigidbody from Player Model for Physics
    public Rigidbody playerRB;
    // handle to control force used to move player forward
    public float zForcePerFrame = 1000f;
    // handle to control force applied to X+ and X- for strafing movement 
    public float xForcePerFrame = 100f;

    Invoker invoker;
    public bool replay = false;
    //Variable so that times to stop executing functions can be shared between two if statements in a function that runs per-frame 
    float executionEndTime = 0;

    void Start()
    {
        invoker = new Invoker();
        //check if replay is false to make sure that no more than two levels can be in replay
        if (replay == false && CommandLog.log.Count > 0)
            replay = true;
        else
            replay = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        // small variable to test against load time to see if commands might have originated from an earlier playthrough (replay)
        playerRB.AddForce(0, 0, zForcePerFrame * Time.deltaTime);
        if (replay)
        {
            Debug.Log("Still Replaying");
            Replay();
        }
            
        else
            EnterInput();

        if (playerRB.position.y < -1)
		{
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    void Replay()
    {
        if(CommandLog.commandStart.Count != 0)
        {
            float nextExecutionTime = CommandLog.commandStart.Peek();
            Command nextCommand = CommandLog.log.Peek();
            if (Time.timeSinceLevelLoad >= nextExecutionTime)
            {
                float executionTime = CommandLog.commandStart.Dequeue();
                executionEndTime = executionTime + CommandLog.commandRunTime.Dequeue();
                Command currentCommand = CommandLog.log.Dequeue();
                currentCommand._player = playerRB;
                invoker.SetCommand(currentCommand);
                Debug.Log("Replaying " + currentCommand.name + " at " + executionTime + " seconds" + " for " + (executionEndTime - executionTime) + " seconds");
            }
            if (Time.timeSinceLevelLoad < executionEndTime && executionEndTime > 0)
            {
               // Debug.Log("Executing Command");
                invoker.ExecuteCommand();
            }
        }
        
    }
    void EnterInput()
    {
        if (Input.GetKey("d"))
        {
            if (invoker._command != null)
                invoker.ExecuteCommand();
        }
        // if key "A" is pressed move left
        if (Input.GetKey("a"))
        {
            if (invoker._command != null)
                invoker.ExecuteCommand();
        }

        if (Input.GetKeyDown("d"))
        {
            Command moveRight = new MoveRight(playerRB, xForcePerFrame);
            invoker.SetCommand(moveRight, Time.timeSinceLevelLoad);
        }
        if (Input.GetKeyUp("d"))
        {
            invoker.EndCommand(Time.timeSinceLevelLoad);
        }
        if (Input.GetKeyDown("a"))
        {
            Command moveLeft = new MoveLeft(playerRB, xForcePerFrame);
            invoker.SetCommand(moveLeft, Time.timeSinceLevelLoad);
        }
        if (Input.GetKeyUp("a"))
        {
            invoker.EndCommand(Time.timeSinceLevelLoad);
        }
    }
}
 