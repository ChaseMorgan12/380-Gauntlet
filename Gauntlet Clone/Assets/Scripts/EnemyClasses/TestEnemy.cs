using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : BaseEnemy
{
    private void Awake()
    {
        speed = 1f;
        detectionRange = 25f;
        moveRange = 0.1f;
    }
    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }
}
