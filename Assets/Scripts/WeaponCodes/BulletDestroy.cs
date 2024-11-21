using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public GameObject ImpactParticle;
    public float speed = 50f;
    private Vector3 targetPoint;
    private bool isMoving = false;
    private float damage = 10f;

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

            if (transform.position == targetPoint)
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetTarget(Vector3 target)
    {
        targetPoint = target;
        isMoving = true;
    }
    void OnCollisionEnter(Collision collision)
    {
          if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyDMG enemyHealth = collision.gameObject.GetComponent<EnemyDMG>();
            if (enemyHealth != null)
            {
                enemyHealth.EnemyDamage(damage);
            }
            if (ImpactParticle != null)
            {
                Instantiate(ImpactParticle, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
            }

            Destroy(gameObject);
        }
        else
        {
            if (ImpactParticle != null)
            {
                Instantiate(ImpactParticle, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
            }
            Destroy(gameObject);
        }
    }

    public void SetDamage(float newdamage)
    {
        damage = newdamage;
    }
}
