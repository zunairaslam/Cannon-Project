using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float mouseX;
    public float mouseY;

    FireCanon canon;
    private void Awake()
    {
        //canon = FireCanon.instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //FireCanon.HandlerCanonRotation(delta, mouseX, mouseY);
    }

    private void MoveInput(float delta)
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = -(Input.GetAxis("Mouse Y"));

    }
}
