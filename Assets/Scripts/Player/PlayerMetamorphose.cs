
using System;
using UnityEngine;

public class PlayerMetamorphose : MonoBehaviour
{
    [field: SerializeField] public float MetamorphoseDuration { get; private set; }
    public bool IsBug { get; private set; } = false;
    public event Action<bool> OnMetamorphose;

    [SerializeField] private GameObject _human;
    [SerializeField] private GameObject _bug;
    
    private PlayerMovement _movement;
    private float _metamorphoseTimer;

    
    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!IsBug) return;
        _metamorphoseTimer -= Time.deltaTime;
        if (_metamorphoseTimer <= 0f)
        {
            Demetamorphose();
        }
    }
    
    
    public void Metamorphose()
    {
        if (IsBug) throw new Exception("intentando metamorphosear siendo una cucaracha!");

        IsBug = true;
        _movement.SetBug();
        _metamorphoseTimer = MetamorphoseDuration;
        OnMetamorphose?.Invoke(true);
        _human.SetActive(false);
        _bug.SetActive(true);
        AudioManager.Instance.PlayMetamorphoseBeginSound();
    }

    private void Demetamorphose()
    {
        if (!IsBug) throw new Exception("intentando demetamorphosear siendo un man!");

        IsBug = false;
        _movement.SetHuman();
        OnMetamorphose?.Invoke(false);
        _human.SetActive(true);
        _bug.SetActive(false);
        AudioManager.Instance.PlayMetamorphoseEndSound();
    }
}
