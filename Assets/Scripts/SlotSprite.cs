using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSprite : MonoBehaviour
{
    public void OnJackpotFinish()
    {
        GetComponentInParent<SlotMachine>().SetDisableGraphic();
    }
}
