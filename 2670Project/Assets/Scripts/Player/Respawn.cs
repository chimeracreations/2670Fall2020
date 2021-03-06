﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject[] respawnPoint;
    private WaitForSeconds wfs;
    public Renderer render1;
    public Renderer render2;
    public TrailRenderer tailTrail;
    private CharacterController controller;
    public PlayerData player;
    public IntData respawnValue;

    // Start is called before the first frame update
    void Start()
    {
        respawnValue.value = 0;
        wfs = new WaitForSeconds(2f);
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -40)
        {
            StartCoroutine(playerRespawn());
        }
    }

    public IEnumerator playerRespawn()
    {
        player.canControl = false;
        controller.enabled = false;
        render1.enabled = false;
        render2.enabled = false;
        tailTrail.enabled = false;
        transform.position = respawnPoint[respawnValue.value].transform.position;
        yield return wfs;
        render1.enabled = true;
        render2.enabled = true;
        tailTrail.enabled = true;
        controller.enabled = true;
        player.canControl = true;
        Time.timeScale = 1;
    }

    public void setRespawn(int value)
    {
        respawnValue.value = value;
    }

    public void CallRespawn()
    {
        StartCoroutine(playerRespawn());
    }
    
}
