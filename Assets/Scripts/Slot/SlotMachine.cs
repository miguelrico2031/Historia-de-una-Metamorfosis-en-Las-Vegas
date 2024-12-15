using System;
using UnityEngine;
using UnityEngine.UI;
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
    private Animator _animator;
    private Material _defaultMaterial;
    private ParticleSystem _particles;
    private float _cooldownTimer;
    private float _spinTimer, _spinDuration;
    private Transform _selectedTarget;
    private Image _triangle;

    private void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _particles = GetComponentInChildren<ParticleSystem>();
        _defaultMaterial = _renderer.material;
        _selectedMaterial.SetTexture("_EmissiveTex", _defaultMaterial.mainTexture);
        _triangle = GetComponentInChildren<Image>();
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
                // transform.Rotate(0f, 0f, Time.deltaTime*50f);
                _triangle.fillAmount = Mathf.Lerp(0f, 1f, _spinTimer / _spinDuration);
                if (_spinTimer <= 0f)
                {
                    transform.rotation = Quaternion.identity;
                    HasJackpot = true;
                    _animator.Play("Jackpot");
                    CurrentState = State.Disabled;
                    _cooldownTimer = _cooldown;
                    _triangle.fillAmount = 0f;
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
    
    public void SetDisableGraphic()
    {
        _renderer.material = _disabledMaterial;
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

        _selectedTarget = closest;
        return closest;
    }

    public void StartSpinning()
    {
        CurrentState = State.Spinning;
        _spinDuration = Random.Range(_minSpinTime, _maxSpinTime);
        _spinTimer = _spinDuration;
        AudioManager.Instance.PlaySound(_slotSound);
        _animator.Play("Spinning");
        _triangle.fillAmount = 1f;
    }

    public ulong CollectJackpot()
    {
        HasJackpot = false;
        AudioManager.Instance.PlaySound(_jackpotSound);
        PlayParticles();
        return _jackpotMoney;
    }

    private void PlayParticles()
    {
        var particlesTransform = _particles.transform;
        var direction = _selectedTarget.position - particlesTransform.position;
        var angle = Vector3.SignedAngle(particlesTransform.up, direction.normalized, Vector3.forward);
        particlesTransform.Rotate(0f, 0f, angle);
        _particles.Play();

    }
}