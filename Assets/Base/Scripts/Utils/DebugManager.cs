using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager
{
    public static bool isDebug = false;

    public static void PrintDebug(string value)
    {
        if(DebugManager.isDebug)
        {
            Debug.Log(value);
        }
    }
}
