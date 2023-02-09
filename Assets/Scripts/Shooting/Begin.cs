using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : State
{
    public Begin(FireCanon fireCanon) : base(fireCanon)
    {
    }

    //public override void Idle()
    //{
    //   // FireCanon.SetState(new ShootingMode(FireCanon));
    //    Debug.Log("Begin");
    //}
}
