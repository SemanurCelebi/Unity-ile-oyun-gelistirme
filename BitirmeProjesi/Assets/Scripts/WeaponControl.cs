﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject hand;

    public LayerMask obstacleLayer;
    public Vector3 offset;

    RaycastHit hit;

    public GameObject bullet;
    public Transform firePoint;

    private float coolDown;

    public AudioClip gunshot;

    private void Update()
    {

        //Look
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, obstacleLayer))
        {
            hand.transform.LookAt(hit.point);
            hand.transform.rotation *= Quaternion.Euler(offset);
        }


        //Fire

        //Cooldown
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }

        //Shot
        if (Input.GetMouseButtonDown(0) && coolDown <= 0)
        {
            //Create bullet
            Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(90,0,0));

            //Reset cooldown
            coolDown = 0.25f;

            //Sound
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(gunshot , 0.4f);

            //Animation
            GetComponent<Animator>().SetTrigger("shot");
        }
        
    }

}
