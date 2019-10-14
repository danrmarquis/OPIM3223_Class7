using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;

    private Rigidbody rigidBody;
    private KeyCode[] inputKeys;
    private Vector3[] keyDirections;

    // Start is called before the first frame update
    void Start()
    {
        inputKeys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
        keyDirections = new Vector3[] { Vector3.forward, Vector3.left, Vector3.back, Vector3.right };
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       for (int i = 0; i< inputKeys.Length; i++)
        {
            var currentKey = inputKeys[i];

            if(Input.GetKey(currentKey))
            {
                Vector3 move = keyDirections[i] * acceleration * Time.deltaTime;
                movePlayer(move);
            }
        }
    }

    void movePlayer(Vector3 movement)
    {
        if (rigidBody.velocity.magnitude * acceleration > maxSpeed)
        {
            rigidBody.AddForce(movement * -1);
        }
        else
        {
            rigidBody.AddForce(movement);
        }
    }

}
