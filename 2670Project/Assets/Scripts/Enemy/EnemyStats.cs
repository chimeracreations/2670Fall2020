using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float damageAmount;
    public bool canConfuse;
    public float EnemyMaxHeath;
    public float EnemySpeed;
    public bool canShoot;
    public EnemyData enemy;

    void OnEnable()
    {   
       damageAmount = enemy.damageAmount;
       canConfuse = enemy.canConfuse;
       EnemyMaxHeath = enemy.EnemyMaxHeath;
       EnemySpeed = enemy.EnemySpeed;
       canShoot = enemy.canShoot;
    }

}
