
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
        _metamorphoseTimer = MetamorphoseDuration;
        OnMetamorphose?.Invoke(true);
        _human.SetActive(false);
        _bug.SetActive(true);
        _bug.GetComponent<Animator>().Play("Metamorphose");
        _movement.SetBug();
        AudioManager.Instance.PlayMetamorphoseBeginSound();
    }

    private void Demetamorphose()
    {
        if (!IsBug) throw new Exception("intentando demetamorphosear siendo un man!");

        IsBug = false;
        OnMetamorphose?.Invoke(false);
        _bug.SetActive(false);
        _human.SetActive(true);
        _human.GetComponent<Animator>().Play("Metamorphose");
        _movement.SetHuman();
        AudioManager.Instance.PlayMetamorphoseEndSound();
    }
}
