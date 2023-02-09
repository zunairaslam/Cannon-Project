using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _InitialVelocity;
    [SerializeField] private float _Angle;

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            float angle = _Angle * Mathf.Deg2Rad;
            StopAllCoroutines();
            StartCoroutine(Coroutine_Movement(_InitialVelocity, angle));
        }
    }
    IEnumerator Coroutine_Movement(float initialVelocity,float angle)
    {
 
        float t = 1;
        while (t < 100 )
        {
            float x = initialVelocity * t *Mathf.Cos(angle);
            float y = initialVelocity * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
            Debug.Log("x" + x);
            Debug.Log("y" + y);
            transform.position = new Vector3(x, y, 0);

            t += Time.deltaTime;
            yield return null;
        }
    }
}
