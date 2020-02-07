using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker
{ 
    public Command _command;
    private float _startTime;
    private float commandExecutionTime;

    //function to use when recording to write execution time information into proper queue
    public void SetCommand(Command command, float startTime)
    {
        _command = command;
        _startTime = startTime;
        Debug.Log("Command: " + _command.name);
        
        CommandLog.commandStart.Enqueue(_startTime);
        CommandLog.log.Enqueue(_command);
    }
    // Overload of the function to be used purely at replay
    public void SetCommand(Command command)
    {
       // Debug.Log(_startTime);
        _command = command;
    }
    public void ExecuteCommand()
    {
        
        _command.Execute();
    }

    public void EndCommand(float EndTime)
    {
        commandExecutionTime = EndTime - _startTime;
        CommandLog.commandRunTime.Enqueue(commandExecutionTime);
        Debug.Log("Command executed at " + _startTime + " for " + commandExecutionTime + " seconds");
    }
}

