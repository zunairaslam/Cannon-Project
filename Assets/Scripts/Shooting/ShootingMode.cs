using static FireCanon;
using UnityEngine;

public class ShootingMode : State
{
    public ShootingMode(FireCanon fireCanon) : base(fireCanon)
    {

    }

    //public override void Idle()
    //{
    //    Debug.Log("shootingMode");
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        FireCanon.SetState(new ShootModeOne(FireCanon));
    //        Debug.Log("shoot 1");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        FireCanon.SetState(new ShootModeTwo(FireCanon));
    //        Debug.Log("shoot 2");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        FireCanon.SetState(new ShootModeThree(FireCanon));
    //        Debug.Log("shoot 3");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha4))
    //    {
    //        FireCanon.SetState(new ShootModeFour(FireCanon));
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha5))
    //    {
    //        FireCanon.SetState(new ShootModeFive(FireCanon));
    //    }
    //}


}
