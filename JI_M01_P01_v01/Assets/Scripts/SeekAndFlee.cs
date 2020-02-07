using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekAndFlee : MonoBehaviour
{
    //Define max speed for the AI to move
    public float maxSpeed = 10f;
    // Defines the radius around the player in which the AI will become active
    public float ActivationRange = 20f;
    // Determine whether the object seeks or flees from the player
    public bool seek = true;
    // Transform of player and rigidbody of AI
    public Transform playerTrans;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // grab the rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Solve for the magnitude of distance between the player and AI
        float dist = Vector3.Distance(transform.position, playerTrans.position);
        // if its in range and seek is activated, have the AI seek the player
        if (seek && (dist < ActivationRange)) {
            // Determine based on algorithm type what the aiVelocity will be
            // seek uses player pos - ai pos
            Vector3 aiVelocity = playerTrans.position - transform.position;
            AIKinematicMove(aiVelocity);
        }
        // otherwise if its in range flee from the player
        if (!seek && (dist < ActivationRange))
        {
            // Determine based on algorithm type what the aiVelocity will be
            // seek uses ai pos - player pos
            Vector3 aiVelocity = transform.position - playerTrans.position;
            AIKinematicMove(aiVelocity);
        }
    }
    

    // recalculate the velocity vector before reassigning
    // Determine rotation based on vector
    void AIKinematicMove(Vector3 aiVel)
    {
        // Normalize and apply magnitude of Max Speed
        aiVel.Normalize();
        // Multiply by Time.deltaTime to allow for consistency in variable frame rates
        aiVel *= (maxSpeed * Time.deltaTime);
        // assign the new velocity vector to the rigidbody velocity component
        rb.velocity = aiVel;
        // transform.LookAt(aiVel);
        // Create a new orientation vector based off the AI velocity (arguments for step and max rotation left at 2pi rad because no specific step is needed currently)
        Vector3 orientation = Vector3.RotateTowards(transform.forward, aiVel, 2 * Mathf.PI, 2 * Mathf.PI);
        // assign that orientation value to the transform
        transform.rotation = Quaternion.LookRotation(orientation);
    }
}
