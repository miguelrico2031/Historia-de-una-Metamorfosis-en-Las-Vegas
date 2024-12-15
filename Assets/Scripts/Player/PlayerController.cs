using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public APlayerState CurrentState { get; private set; }
    
    private void Start()
    {
        SetState(new PlayerIdle(this));
    }

    private void Update()
    {
        CurrentState?.Update(Time.deltaTime);
        
    }


    public void SetState(APlayerState state)
    {
        CurrentState?.Exit(state);
        var old = CurrentState;
        CurrentState = state;
        CurrentState?.Begin(old);
    }

}
