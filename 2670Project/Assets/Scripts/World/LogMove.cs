using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMove : MonoBehaviour
{
    public float lifeTime = 20f;
    public float speed = 30f;
    private int count = 0;
    private float countToSeconds;

    private void FixedUpdate() 
    {
        transform.position = transform.position + new Vector3(0, 0, -speed * Time.deltaTime);
        count++;
        countToSeconds = (count / 50f);
        if (countToSeconds == lifeTime)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = transform.parent.position;
            transform.rotation = transform.parent.rotation;
            GetComponent<Rigidbody>().isKinematic = false;
            count = 0;
        }
    }

    // private IEnumerator Death()
    // {
    //     while (go)
    //     {
    //         yield return new WaitForSeconds(lifeTime);
    //         GetComponent<Rigidbody>().isKinematic = true;
    //         transform.position = transform.parent.position;
    //         transform.rotation = transform.parent.rotation;
    //         GetComponent<Rigidbody>().isKinematic = false;
    //     }
    // }

    // private IEnumerator Float()
    // {
    //     while (go)
    //     {
    //         yield return new WaitForFixedUpdate();
    //         transform.position = transform.position + new Vector3(0, 0, -speed * Time.deltaTime);
    //     }
    // }
}
