using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnBoundry : MonoBehaviour
{
    public ParticleSystem blast;

    private void Start()
    {
        //blast = GetComponentInChildren<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boundry")
        {
            Destroy();
            
            
        }

    }

    public void Destroy()
    {
        Instantiate(blast, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
