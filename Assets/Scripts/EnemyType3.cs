using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType3 : EnemyType1
{
    public float routeRange = 3.0f; 

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();

        if (!chasing)
        {

        }
        

    }
}
