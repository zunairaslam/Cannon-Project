using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootModeOne : State
{

    public ShootModeOne(FireCanon fireCanon) : base(fireCanon)
    {
    }
    public override void OnStateEnter()
    {
        Debug.Log("enter mode 1");
    }
    public override void OnStateUpdate(FireCanon FireCanon)
    {
        if (Input.GetButtonDown("Fire1"))
        {

            FireCanon.Shooting();
            Debug.Log("hello");
        }
    }
    public override void OnStateExit()
    {
        Debug.Log("exit mode 1");
    }

}
