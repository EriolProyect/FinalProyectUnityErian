using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    private InputManager inputManager;
    public Transform shootPoint;
    public GameObject muzzleFlashPrefab;
    public GameObject bulletPrefab;
    public float shootForce = 20f;
     public float shootRange = 100f;
    public float fireRate = 0.2f;
    public float damage = 10f;
     public LayerMask shootableLayers;

    private float nextFireTime = 0f;

    void Start()
    {
        inputManager = InputManager.Instance;
    }

    void FixedUpdate()
    {
        if (inputManager.PlayerShooting() && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

 void Shoot()
    {
        if (muzzleFlashPrefab != null)
        {
            Instantiate(muzzleFlashPrefab, shootPoint.position, shootPoint.rotation);
        }

        RaycastHit hit;
        Ray shootRay = new Ray(shootPoint.position, shootPoint.forward);

        if (Physics.Raycast(shootRay, out hit, shootRange, shootableLayers))
        {
            SpawnBullet(shootRay.direction, hit.point);
        }
        else
        {
            SpawnBullet(shootRay.direction, shootRay.origin + shootRay.direction * shootRange);
        }

    }  
    void SpawnBullet(Vector3 direction, Vector3 targetPoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(direction));

        BulletDestroy bulletScript = bullet.GetComponent<BulletDestroy>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(targetPoint);
            bulletScript.SetDamage(damage);
        }
    }

    public void SetDamage(float newdamage)
    {
        damage = newdamage;
    }
}
