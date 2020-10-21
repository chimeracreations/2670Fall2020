using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject[] respawnPoint;
    private WaitForSeconds wfs;
    private int i;
    public Renderer render1;
    public Renderer render2;
    public TrailRenderer tailTrail;
    private CharacterController controller;
    private CharacterMover mover;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        wfs = new WaitForSeconds(2f);
        controller = GetComponent<CharacterController>();
        mover = GetComponent<CharacterMover>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -20)
        {
            StartCoroutine(playerRespawn());
        }
    }

    public IEnumerator playerRespawn()
    {
        mover.enabled = false;
        controller.enabled = false;
        render1.enabled = false;
        render2.enabled = false;
        tailTrail.enabled = false;
        transform.position = respawnPoint[i].transform.position;
        yield return wfs;
        render1.enabled = true;
        render2.enabled = true;
        tailTrail.enabled = true;
        controller.enabled = true;
        mover.enabled = true;
    }

    public void setRespawn(int value)
    {
        i = value - 1;
    }
}
