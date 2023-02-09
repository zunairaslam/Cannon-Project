using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootModeThree : State
{
    private bool isCoroutineExecuting = false;
    public ShootModeThree(FireCanon fireCanon) : base(fireCanon)
    {
    }
    public override void OnStateEnter()
    {
        Debug.Log("enter mode 3");
    }
    public override void OnStateUpdate(FireCanon fireCanon)
    {
        fireCanon.CallCoroutine(ExecuteAfterTimeTwo(2f, () => fireCanon.Shooting()));
    }
    IEnumerator ExecuteAfterTimeTwo(float time, Action task)
    {

        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        task();
        Debug.Log("AutoShootRateHalfSecond");
        isCoroutineExecuting = false;
    }
    public override void OnStateExit()
    {
        Debug.Log("exit mode 3");
    }
}
