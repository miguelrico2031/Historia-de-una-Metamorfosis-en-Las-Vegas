using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotMachine : MonoBehaviour
{
    public enum State
    {
        Enabled,
        Targetted,
        Spinning,
        Disabled
    }

    public State CurrentState { get; private set; } = State.Enabled;
    public bool HasJackpot { get; private set; }

    [SerializeField] private Transform[] _targets;
    [SerializeField] private Material _selectedMaterial;
    [SerializeField] private Material _disabledMaterial;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _minSpinTime, _maxSpinTime;
    [SerializeField] private ulong _jackpotMoney;
    [SerializeField] private AudioClip _slotSound, _jackpotSound;

    private SpriteRenderer _renderer;
    private Material _defaultMaterial;
    private float _cooldownTimer;
    private float _spinTimer;

    private void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _defaultMaterial = _renderer.material;
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case State.Disabled:
            
                _cooldownTimer -= Time.deltaTime;
                if (_cooldownTimer <= 0f)
                {
                    CurrentState = State.Enabled;
                    _renderer.material = _defaultMaterial;
                }

                break;
            
            case State.Spinning:

                _spinTimer -= Time.deltaTime;
                transform.Rotate(0f, 0f, Time.deltaTime*50f);
                if (_spinTimer <= 0f)
                {
                    transform.rotation = Quaternion.identity;
                    Disable();
                    HasJackpot = true;
                }
                
                break;
        }
    }

    private void OnMouseEnter()
    {
        if (CurrentState is State.Enabled or State.Targetted)
        {
            _renderer.material = _selectedMaterial;
        }
    }

    private void OnMouseExit()
    {
        if (CurrentState is State.Enabled or State.Spinning or State.Targetted && _renderer.material != _defaultMaterial)
            _renderer.material = _defaultMaterial;

        else if (CurrentState is State.Disabled && _renderer.material != _disabledMaterial)
            _renderer.material = _disabledMaterial;
    }

    private void OnMouseDown()
    {
        if (CurrentState is not State.Enabled) return;
        SlotMachineManager.Instance.RegisterSlotClick(this);
    }
    
    public void Disable()
    {
        CurrentState = State.Disabled;
        _renderer.material = _disabledMaterial;
        _cooldownTimer = _cooldown;
    }
    
    public Transform GetClosestTarget(Vector3 pos)
    {
        var minDistance = float.MaxValue;
        Transform closest = null;
        foreach (Transform target in _targets)
        {
            var distance = Vector3.Distance(target.position, pos);
            if (distance <= minDistance)
            {
                minDistance = distance;
                closest = target;
            }
        }

        CurrentState = State.Targetted;

        return closest;
    }

    public void StartSpinning()
    {
        CurrentState = State.Spinning;
        _spinTimer = Random.Range(_minSpinTime, _maxSpinTime);
        AudioManager.Instance.PlaySound(_slotSound);
    }

    public ulong CollectJackpot()
    {
        HasJackpot = false;
        AudioManager.Instance.PlaySound(_jackpotSound);
        return _jackpotMoney;
    }
}