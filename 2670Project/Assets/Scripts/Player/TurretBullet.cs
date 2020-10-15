using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public GameObject bullet;
    public float lifeTime;
    public float bulletSpeed;

    // Start is called before the first frame update
    private IEnumerator Start()
    {

        yield return new WaitForSeconds(lifeTime);
        Destroy(bullet);
    }

}
