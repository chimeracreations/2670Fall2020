using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EctoTrap : MonoBehaviour
{
    private GameObject instancer;
    private MeshRenderer mesh;
    public Color nextColor;
    public Color startColor;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        instancer = transform.parent.gameObject;
        mesh.material.color = startColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mesh.material.color = nextColor;
            var thisBullet = Instantiate(bullet, instancer.transform.position, instancer.transform.rotation);
            thisBullet.transform.parent =  gameObject.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            mesh.material.color = startColor;
        }
    }

}

