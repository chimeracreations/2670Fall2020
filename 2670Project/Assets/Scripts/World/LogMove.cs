using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMove : MonoBehaviour
{
    public float lifeTime = 20f;
    public float speed = 30f;

    private void Update() 
    {
        transform.position = transform.position + new Vector3(0, 0, -speed * Time.deltaTime);
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
