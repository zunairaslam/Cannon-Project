using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootModeFive : State
{
    public ShootModeFive(FireCanon fireCanon) : base(fireCanon)
    {
    }
    public override void OnStateEnter()
    {
        Debug.Log("enter mode 5");
    }
    public override void OnStateUpdate(FireCanon fireCanon)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCanon.Shooting();
            fireCanon.mt = fireCanon.cube.GetComponent<MeshRenderer>().material;
            fireCanon.colors = new Color32[7]
            {
            new Color(255, 0, 0, 255), //red
            new Color32(255, 165, 0, 255), //orange
            new Color32(255, 255, 0, 255), // yellow 
            new Color32(0, 255, 0, 255), // green 
            new Color32(0, 0, 255, 255), // blue 
            new Color32(75, 0, 130, 255), // indigo 
            new Color32(238, 130, 238, 255), // vilot 
            };
            fireCanon.CallCoroutine(Cycle(fireCanon));
        }
    }
     IEnumerator Cycle(FireCanon fireCanon)
    {
        int i = 0;
        while (true)
        {
            for (float interpolant = 0f; interpolant < 1f; interpolant += 0.01f)
            {
                fireCanon.mt.color = Color.Lerp(fireCanon.colors[i % 7], fireCanon.colors[(i + 1) % 7], interpolant);
                yield return null;
            }
            i++;
        }
    }
    public override void OnStateExit()
    {
        Debug.Log("exit mode 5");
    }
}
