
using System;
using UnityEngine;

public class SlotMachineManager : MonoBehaviour
{
    public static SlotMachineManager Instance { get; private set; }
    
    public event Action<SlotMachine> OnSlotClicked;
    
    public void RegisterSlotClick(SlotMachine slot)
    {
        OnSlotClicked?.Invoke(slot);
    }

    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
