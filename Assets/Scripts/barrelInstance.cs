using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelInstance : MonoBehaviour
{
    public static barrelInstance instance;
    private void Awake()
    {
        if(instance == null) {

            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
