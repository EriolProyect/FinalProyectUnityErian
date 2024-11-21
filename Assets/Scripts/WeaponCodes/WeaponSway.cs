using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount = 0.02f;  
    public float swaySpeed = 5f;      
    public float returnSpeed = 10f;   

    private Vector3 initialPosition;  
    private Quaternion initialRotation;  

    private void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        ApplySway();
    }

    private void ApplySway()
    {
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

        Vector3 swayOffset = new Vector3(-horizontalInput, -verticalInput, 0) * swayAmount;
        
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition + swayOffset, Time.deltaTime * swaySpeed);

        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * returnSpeed);

        float rotationX = verticalInput * swayAmount;
        float rotationY = -horizontalInput * swayAmount;
        Quaternion rotationOffset = Quaternion.Euler(rotationX, rotationY, 0);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, initialRotation * rotationOffset, Time.deltaTime * swaySpeed);
    }
}
