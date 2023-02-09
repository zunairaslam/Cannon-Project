using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootModeFour : State
{
    public ShootModeFour(FireCanon fireCanon) : base(fireCanon)
    {
    }
    public override void OnStateEnter()
    {
        Debug.Log("enter mode 4");
    }
    public override void OnStateUpdate(FireCanon fireCanon)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCanon.Shooting();
            fireCanon.cube.transform.localScale = new Vector3(10, 10, 10);
            fireCanon.cube.GetComponent<DestoryOnBoundry>().blast = fireCanon.exp1;
            //explosion = exp1;
        }
    }
    public override void OnStateExit()
    {
        Debug.Log("exit mode 4");
    }
}
