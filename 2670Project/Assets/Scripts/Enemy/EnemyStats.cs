using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float damageAmount;
    public bool canConfuse;
    public float enemyMaxHeath;
    public float enemySpeed;
    public bool canShoot;
    public float enemyPatrolSpeed;
    public EnemyData enemy;

    void OnEnable()
    {   
       damageAmount = enemy.damageAmount;
       canConfuse = enemy.canConfuse;
       enemyMaxHeath = enemy.enemyMaxHeath;
       enemySpeed = enemy.enemySpeed;
       canShoot = enemy.canShoot;
       enemyPatrolSpeed = enemy.enemyPatrolSpeed;
    }

}
