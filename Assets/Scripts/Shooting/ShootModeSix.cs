using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootModeSix : State
{
    public ShootModeSix(FireCanon fireCanon) : base(fireCanon)
    {
    }
    public override void OnStateEnter()
    {
        Debug.Log("enter mode 6");
    }
    public override void OnStateUpdate(FireCanon FireCanon)
    {
        if (Input.GetMouseButton(0))
        {

            FireCanon.Shooting();
            Debug.Log("hello");
        }
    }
    public override void OnStateExit()
    {
        Debug.Log("exit mode 6");
    }
}
