using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommandLog {
    public static Queue<Command> log = new Queue<Command>();
    public static Queue<float> commandStart = new Queue<float>();
    public static Queue<float> commandRunTime = new Queue<float>();
}