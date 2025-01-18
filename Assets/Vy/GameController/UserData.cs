using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData : MonoBehaviour
{
    public int passedObstacles = 0;

    public void Restart()
    {
        passedObstacles = 0;
    }
}
