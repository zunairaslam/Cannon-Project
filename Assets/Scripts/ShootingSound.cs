using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSound : MonoBehaviour
{
    AudioSource shooting;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootingAudio()
    {
        shooting.Play();
    }
}
