﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;

    Vector3 shootDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        this.transform.Translate(shootDirection * speed, Space.World);
    }

    public void FireProjectile(Ray shootRay)
    {
        this.shootDirection = shootRay.direction;
        this.transform.position = shootRay.origin;
        rotateInShootDirection();
    }

    void OnCollisionEnter(Collision col)
    {
        Enemy enemy = col.collider.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }

    void rotateInShootDirection()
    {
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, shootDirection, 0.01f, 0.0f);
        transform.rotation = Quaternion.LookRotation(newRotation);
    }


}
