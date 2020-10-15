using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EnvironmentalHazzard : MonoBehaviour
{
    private MeshRenderer mesh;
    public Color startColor;
    public Color nextColor;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material.color = startColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        mesh.material.color = nextColor;
    } 
    private void OnTriggerExit(Collider other)
    {
        mesh.material.color = startColor;
    } 
    
}
