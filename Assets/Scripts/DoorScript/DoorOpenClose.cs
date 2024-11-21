using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    [Header("AnimationComps")]
    public Animator RightDoor;
    public Animator LeftDoor;
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
        if (LeftDoor != null)
        {
            LeftDoor.SetTrigger("Open");
        }

        if (RightDoor != null)
        {
            RightDoor.SetTrigger("Open");
        }
    }
    private void CloseDoors()
    {
        if (LeftDoor != null)
        {
            LeftDoor.SetTrigger("Close");
        }

        if (RightDoor != null)
        {
            RightDoor.SetTrigger("Close");
        }
    }
}
