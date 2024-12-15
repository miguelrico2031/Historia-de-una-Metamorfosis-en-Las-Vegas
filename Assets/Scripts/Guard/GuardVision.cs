
using System;
using UnityEngine;

public class GuardVision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PERDISTE JEJ");
            Debug.Break();
        }
    }
}
