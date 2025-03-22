using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Movement speed
    public float moveSpeed = 5.0f;
    
    // Update is called once per frame
    void Update()
    {
        // Get input from WASD keys
        float horizontalInput = 0;
        float verticalInput = 0;
        
        // Check for W and S keys (forward/backward)
        if (Input.GetKey(KeyCode.W))
            verticalInput = 1;
        else if (Input.GetKey(KeyCode.S))
            verticalInput = -1;
            
        // Check for A and D keys (left/right)
        if (Input.GetKey(KeyCode.A))
            horizontalInput = -1;
        else if (Input.GetKey(KeyCode.D))
            horizontalInput = 1;
        
        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        
        // Normalize to prevent faster diagonal movement
        if (movement.magnitude > 0)
            movement.Normalize();
            
        // Move the object
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
