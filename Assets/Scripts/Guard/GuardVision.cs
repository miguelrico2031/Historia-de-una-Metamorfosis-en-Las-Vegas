
using System;
using UnityEngine;

public class GuardVision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMetamorphose>().IsBug) return;
            Debug.Log("PERDISTE JEJ");
            UIManager.Instance.EndGame();
            GetComponentInParent<GuardMovement>().CancelMovement();
            other.GetComponent<PlayerMovement>().CancelMovement();
        }
    }
}
