using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShootModeTwo : State
{
    private bool isCoroutineExecuting = false;


    public ShootModeTwo(FireCanon fireCanon) : base(fireCanon)
    {
    }
    public override void OnStateEnter()
    {
        Debug.Log("enter mode 2");
    }
    public override void OnStateUpdate(FireCanon fireCanon)
    {
        fireCanon.CallCoroutine(ExecuteAfterTime(0.5f , () => fireCanon.Shooting()));
    }

    IEnumerator ExecuteAfterTime(float time, Action task)
    {

        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        Debug.Log("AutoShootRateHalfSecond");
        isCoroutineExecuting = false;
    }
    public override void OnStateExit()
    {
        Debug.Log("exit mode 2");
    }
}
