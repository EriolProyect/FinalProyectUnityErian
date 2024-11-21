using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDMG : MonoBehaviour
{
    public float health = 100f;

    public void EnemyDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
