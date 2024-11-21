using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBoom : MonoBehaviour
{
[Header("Settings")]
    public GameObject boomParticle;           
    public GameObject hitboxPrefab;           
    public float timeToExplode = 5f;          
    public float explosionRadius = 5f;        
    public float explosionForce = 10f;        
    public float damage = 50f;                

    private float timer;

    void Start()
    {
        timer = timeToExplode;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Explosion();
        }
    }

    void Explosion()
    {
        Instantiate(boomParticle, transform.position, transform.rotation);

        GameObject hitbox = Instantiate(hitboxPrefab, transform.position, Quaternion.identity);
        hitbox.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            EnemyDMG enemy = hitCollider.GetComponent<EnemyDMG>();
            if (enemy != null)
            {
                enemy.EnemyDamage(damage);
            }
        }
        Destroy(gameObject);
        Destroy(hitbox, 3f);
    }
}