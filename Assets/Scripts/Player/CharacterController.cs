using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public bool keyPressed;
    public bool keyHoldDown;
    public bool keyReleased;    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keyPressed = Input.GetKeyDown(KeyCode.Space);
        keyHoldDown = Input.GetKey(KeyCode.Space);
        keyReleased = Input.GetKeyUp(KeyCode.Space);
    }
}
