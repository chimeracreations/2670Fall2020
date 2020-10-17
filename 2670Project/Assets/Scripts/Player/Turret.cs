using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bullet;
    public Transform instancer;
    private CharacterMover characterMover;
    private GameObject player;
    private float hCrosshair;
    private float vCrosshair;
    public float speed = 2f;
    public WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    public bool isEntered;
    private CharacterController cc;
    public GameObject playerTurretPosition;
    private Camera cam;
    public float bulletDelay = 2f;
    private float bulletDelayCount;
    private Vector3 rotationOffset;
    private Vector3 currentEulerAngles;
    private Vector3 origin;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        characterMover = player.GetComponent<CharacterMover>();
        cc = player.GetComponent<CharacterController>();
        cam = player.GetComponentInChildren<Camera>();
        origin = transform.position;
        isEntered = false;
        bulletDelayCount = bulletDelay + 1;
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
            vCrosshair = Mathf.Clamp(vCrosshair, -50f, 30f);      

            //You need the rotation offset or if the turret GameObject starts at a rotated position 
            //it will rotate on the forward axis and not the gameObject's axis
            currentEulerAngles = new Vector3(-vCrosshair, hCrosshair + rotationOffset.y, 0f);

            //apply the change to the gameObject
            player.transform.GetChild(0).eulerAngles = currentEulerAngles;
            transform.parent.eulerAngles = currentEulerAngles;
        }
    }

    private IEnumerator OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            characterMover.canControl = false;
            var offset = playerTurretPosition.transform.position - player.transform.position;
            while (offset.magnitude > .1f && isEntered == false)
            {
                yield return wffu;
                offset = playerTurretPosition.transform.position - player.transform.position;
                if(offset.magnitude > .1f) 
                {
                    offset = offset.normalized * characterMover.moveSpeed;
                    cc.Move(offset * .5f * Time.deltaTime);
                    player.transform.GetChild(0).rotation = instancer.transform.rotation;
                    transform.parent.position = transform.parent.position + new Vector3(0, 1f * Time.deltaTime, 0);
                }
                else if (offset.magnitude <= .1f) 
                {
                    rotationOffset = player.transform.GetChild(0).eulerAngles;
                    isEntered = true;
                    cam.enabled = true;
                }
            }
        }
    }
    private IEnumerator OnTriggerExit(Collider other)
    {
        isEntered = false;
        characterMover.canControl = true;
        cam.enabled = false;
         while (transform.parent.position.y >= (origin.y + .1f))
            {
                yield return wffu;
                transform.parent.position = transform.parent.position + new Vector3(0, -1f * Time.deltaTime, 0);
            }
            transform.parent.position = origin;
    }
}
