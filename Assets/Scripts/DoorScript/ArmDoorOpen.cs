using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmDoorOpen : MonoBehaviour
{
    [Header("AnimationComps")]
    public Animator ArmDoor;
     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDoors();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDoors();
        }
    }
    private void OpenDoors()
    {
        if (ArmDoor != null)
        {
            ArmDoor.SetTrigger("Open");
        }
    }
    private void CloseDoors()
    {
        if (ArmDoor != null)
        {
            ArmDoor.SetTrigger("Close");
        }
    }
}
