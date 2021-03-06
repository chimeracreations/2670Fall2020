﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bullet;
    public Transform instancer;
    public PlayerData player;
    private GameObject character;
    private float hCrosshair;
    private float vCrosshair;
    public float speed = 2f;
    private WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    public bool isEntered;
    private CharacterController cc;
    public GameObject playerTurretPosition;
    private Camera cam;
    public float bulletDelay = 2f;
    private float bulletDelayCount;
    private Vector3 rotationOffset;
    private Vector3 currentEulerAngles;
    private Vector3 origin;
    private Quaternion rotationOrigin;
    public Vector3 runawayCheck;
    private bool resetNeeded;
    private float ejectCount = 1f;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindWithTag("Player");
        cc = character.GetComponent<CharacterController>();
        cam = character.GetComponentInChildren<Camera>();
        origin = transform.position;
        rotationOrigin = transform.rotation;
        isEntered = false;
        bulletDelayCount = bulletDelay + 1;
        ejectCount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEntered)
        {
            if (Input.GetButtonDown("Fire1") && bulletDelayCount >= bulletDelay)
            {
                Instantiate(bullet, instancer.position, instancer.rotation);
                bulletDelayCount = 1f;
            }
            else if (bulletDelayCount <= bulletDelay)
            {
                bulletDelayCount = bulletDelayCount + Time.deltaTime;
            }

            hCrosshair += Input.GetAxis("Horizontal")* speed * Time.deltaTime;
            vCrosshair += Input.GetAxis ("Vertical")* speed * Time.deltaTime;
    
            hCrosshair = Mathf.Clamp(hCrosshair, -90f, 90f);
            vCrosshair = Mathf.Clamp(vCrosshair, -30f, 30f);      

            //You need the rotation offset or if the turret GameObject starts at a rotated position 
            //it will rotate on the forward axis and not the gameObject's axis
            currentEulerAngles = new Vector3(-vCrosshair, hCrosshair + rotationOffset.y, 0f);

            //apply the change to the gameObject
            character.transform.GetChild(0).eulerAngles = currentEulerAngles;
            transform.parent.eulerAngles = currentEulerAngles;

            if (Input.GetButtonDown("Fire2") && isEntered == true)
            {
                StartCoroutine("Escape");
            }
        }
    }

    private IEnumerator OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            ejectCount = 1f;
            player.canControl = false;
            resetNeeded = false;
            var offset = playerTurretPosition.transform.position - character.transform.position;
            while (offset.magnitude > .1f && isEntered == false)
            {
                yield return wffu;
                runawayCheck = transform.parent.position - origin;
                offset = playerTurretPosition.transform.position - character.transform.position;
                if(offset.magnitude > .1f && runawayCheck.y < 2f && resetNeeded == false) 
                {
                    offset = offset.normalized * player.moveSpeed;
                    cc.Move(offset * .5f * Time.deltaTime);
                    character.transform.GetChild(0).rotation = instancer.transform.rotation;
                    transform.parent.position = transform.parent.position + new Vector3(0, 1f * Time.deltaTime, 0);
                }
                else if (offset.magnitude <= .1f) 
                {
                    rotationOffset = character.transform.GetChild(0).eulerAngles;
                    isEntered = true;
                    cam.enabled = true;
                }
                else if (runawayCheck.y >= 2f)
                {
                    resetNeeded = true;
                    cam.enabled = false;
                    transform.parent.position = origin;
                    transform.parent.rotation = rotationOrigin;
                }
            }
        }
    }
    private IEnumerator OnTriggerExit(Collider other)
    {
        isEntered = false;
        player.canControl = true;
        cc.Move(transform.parent.rotation * Vector3.back * .5f);
        cam.enabled = false;
         while (transform.parent.position.y >= (origin.y + .1f))
            {
                yield return wffu;
                transform.parent.position = transform.parent.position + new Vector3(0, -1f * Time.deltaTime, 0);
                transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, rotationOrigin, .1f);
            }
            transform.parent.position = origin;
            transform.parent.rotation = rotationOrigin;
            hCrosshair = 0f;
            vCrosshair = 0f;
    }

    private IEnumerator Escape()
    {
        while (ejectCount < 3f && isEntered == true)
        {
            yield return wffu;
            cc.Move(transform.parent.rotation * Vector3.back * 2.2f * Time.deltaTime);
            ejectCount += ejectCount * Time.deltaTime;
        }
        ejectCount = 1f;
    }
}
