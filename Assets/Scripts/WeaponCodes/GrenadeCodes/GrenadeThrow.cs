using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    [Header("References")]
    public Transform Cam;
    public Transform ThrowPoint;
    public GameObject GrenadeObject;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;
    private InputManager inputManager;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        if (inputManager.ThrowGrenade() && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }
    
    private void Throw()
    {
        readyToThrow = false;
        GameObject Grenade = Instantiate(GrenadeObject, ThrowPoint.position, Cam.rotation);
        Rigidbody GrenadeRb = Grenade.GetComponent<Rigidbody>();

        GrenadeRb.useGravity = true;
        GrenadeRb.drag = 0f;
        GrenadeRb.angularDrag = 0.05f;

        GrenadeRb.mass = 0.3f;
        Physics.gravity = new Vector3(0, -29.43f ,0);

        Vector3 forceDirection = Cam.transform.forward;
        RaycastHit hit;

        if(Physics.Raycast(Cam.position, Cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - ThrowPoint.position).normalized;
        }
        
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        GrenadeRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
