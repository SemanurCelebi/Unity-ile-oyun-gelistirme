﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private Transform player;

    public float speed = 1;
    public float follow_distance = 10f;

    private float cooldown = 2f;

    public GameObject mesh;
    public GameObject bullet;

    public float health = 100f;

    public GameObject death_effect;

    public AudioClip death_sound;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        FollowPlayer();
        Shot();
        Death();
    }

    private void FollowPlayer() 
    {
        //Look to player
        transform.LookAt(player.position);
        transform.rotation *= Quaternion.Euler(new Vector3(-90,0,0));

        //Move to player
        if (Vector3.Distance(transform.position, player.position) >= follow_distance)
        {
            transform.Translate(transform.forward * Time.deltaTime * speed * -1);
        }
        else
        {
            transform.RotateAround(player.position, transform.forward, Time.deltaTime * speed* Random.Range(0.2f,3f));
            
        }
    }


    private void Shot() 
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = 2f;
            //Shot
            mesh.GetComponent<Animator>().SetTrigger("shot");
            Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(new Vector3(90,0,0)));
        }
    }

    private void Death()
    {
        if (health <= 0)
        {
            //spawn particle
            Instantiate(death_effect, transform.position, Quaternion.identity);

            //play sound effect
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(death_sound);

            //destroy gameobject
            Destroy(this.gameObject);
        }
    }
}
