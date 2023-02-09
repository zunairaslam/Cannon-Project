using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public ParticleSystem explosion;
    float delay = 0.7f;
    float countDown;
    // Start is called before the first frame update
    void Start()
    {
        explosion = GetComponent<ParticleSystem>();
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        explosion.Play();
        if(countDown <= 0) 
        {
            explosion.Stop();
        }
        
    }
}
