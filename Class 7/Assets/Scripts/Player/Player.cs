using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    public float acceleration;
    public float maxSpeed;

    public int health;
    public int damage;

    private Rigidbody rigidBody;
    private KeyCode[] inputKeys;
    private Vector3[] keyDirections;

    public event Action<Player> onPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        inputKeys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
        keyDirections = new Vector3[] { Vector3.forward, Vector3.left, Vector3.back, Vector3.right };
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < inputKeys.Length; i++)
        {
            var currentKey = inputKeys[i];

            if (Input.GetKey(currentKey))
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

    void collidedWithEnemy(Enemy enemy)
    {
        enemy.Attack(this);
        if (health <= 0)
        {
            

            if (onPlayerDeath != null)
            {
                onPlayerDeath(this);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.collider.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            collidedWithEnemy(enemy);
        }
    }

}
