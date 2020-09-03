using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private Transform target;  
    public float smoothSpeed = 0.2f;
    public Vector3 offset;

    void Start()
    {
        player = GameObject.Find("PlayerCharacter");
        target = player.GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = desiredPosition;
        transform.position = smoothedPosition;

        
    }
}
