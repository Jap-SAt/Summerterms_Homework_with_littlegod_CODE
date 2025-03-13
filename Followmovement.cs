using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followmovement : MonoBehaviour
{
    // Movement speed that can be adjusted in the Inspecto
    public float moveSpeed = 5f;

    // Rotation speed for smooth turning
    public float rotationSpeed = 10f;

    // Optional: Allow diagonal movement speed modification
    public bool allowDiagonalMovement = true;

    public Animator animator;
    public List<Animator> BodyAnim; 
    public string walk = "Walk";
    private bool isMoving = false;
    // Reference to the Rigidbody component for physics-based movement
    [Header("Gameobject")]
    public List<GameObject> Leg;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("No Animator component found!");
        }
        //Get the Rigidbody component attached to this object
        rb = GetComponent<Rigidbody>();

        // If no Rigidbody is found, add one
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Optional: Freeze rotation to prevent physics from affecting rotation
        rb.freezeRotation = true;
        StartCoroutine(RandomReg());

    }

    void Update()
    {
        // Get input from WASD keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // If diagonal movement should be limited
        if (!allowDiagonalMovement && movement.magnitude > 1f)
        {
            movement = movement.normalized;

        }
        if (horizontalInput > 0 || horizontalInput <  0 ||verticalInput > 0 || verticalInput < 0)
        {
            foreach (var ba in BodyAnim)
            {
                ba.Play("walk");
            }
       
        }else{
            foreach (var ba in BodyAnim)
            {
                ba.Play("idle");
            }
        }
        // Move and rotate the object
        MoveAndRotateObject(movement);
        // animator.Play("Armature_walk(stay)");

        isMoving = Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f;

    }

    //mockup Random Reg function
    IEnumerator RandomReg(){
        yield return new WaitForSeconds(1);
        foreach(var L in Leg){
            L.SetActive(false);
        }
        Leg[UnityEngine.Random.Range(0, 2)].SetActive(true);
        StartCoroutine(RandomReg());
    }

    void MoveAndRotateObject(Vector3 direction)
    {
        // Move using Rigidbody velocity
        rb.velocity = direction * moveSpeed;

        // Only rotate if there's significant movement
        if (direction.magnitude > 0.1f)
        {
            // Create a rotation that looks in the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly interpolate current rotation to target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
