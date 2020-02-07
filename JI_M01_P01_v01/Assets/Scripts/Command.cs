using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public Rigidbody _player;
    public string name;
    public abstract void Execute();
}

class MoveLeft: Command
{
    float _force;
    // pass any variables needed into constructor and store into private variables
    public MoveLeft(Rigidbody player, float force)
    {
        name = "Move Left";
        _player = player;
        _force = force;
    }
    public override void Execute()
    {
        // Same line of code with rigidbody  and adding force 
        _player.AddForce(-_force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}


class MoveRight: Command
{
    float _force;
    // pass any variables needed into constructor and store into private variables
    public MoveRight(Rigidbody player, float force)
    {
        name = "Move Right";
        _player = player;
        _force = force;
    }
    public override void Execute()
    {
        // Same line of code with rigidbody  and adding force 
        _player.AddForce(_force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }

}