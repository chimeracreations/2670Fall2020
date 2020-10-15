using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bullet;
    public GameObject instancer;
    private CharacterMover characterMover;
    private GameObject player;
    private float hCrosshair;
    private float vCrosshair;
    public float speed;
    public WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    public bool isEntered;
    private CharacterController cc;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        characterMover = player.GetComponent<CharacterMover>();
        cc = player.GetComponent<CharacterController>();
        isEntered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Instantiate(bullet, instancer.transform.position, instancer.transform.rotation);
        }
        hCrosshair = Input.GetAxis("Horizontal") * speed;
        vCrosshair = Input.GetAxis("Vertical") * speed;
    }

    private IEnumerator OnTriggerEnter(Collider other) 
    {
        characterMover.canControl = false;
        var offset = transform.position - player.transform.position;
        while (offset.magnitude > .1f && isEntered == false)
        {
            yield return wffu;
            offset = transform.position - player.transform.position;
            if(offset.magnitude > .6f) 
            {
                offset = offset.normalized * characterMover.moveSpeed;
                cc.Move(offset * .5f * Time.deltaTime);
                player.transform.GetChild(0).rotation = instancer.transform.rotation;
                transform.parent.position = transform.parent.position + new Vector3(0, 1f * Time.deltaTime, 0);
            }
            else if (offset.magnitude <= .6f) 
            {
                isEntered = true;
                Camera cam = player.GetComponentInChildren<Camera>();
                cam.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isEntered = false;
        characterMover.canControl = true;
    }
}
