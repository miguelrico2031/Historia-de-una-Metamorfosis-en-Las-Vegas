
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Money")]
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private int _digits = 18;
    [SerializeField]private float _metCooldownButtonDuration = 6f;

    [Header("Metamorphose")] [SerializeField]
    private Button _metamorphoseButton;
    
    private ulong _money = 0;
    private float _metButtonCooldownTimer;
    private bool _isMet;
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

    private void Update()
    {
        if (!_isMet) return;
        _metButtonCooldownTimer -= Time.deltaTime;
        _metamorphoseButton.GetComponent<Image>().fillAmount = 
            Mathf.Clamp01(Mathf.Lerp(1f, 0f, _metButtonCooldownTimer / _metCooldownButtonDuration));
        if (_metButtonCooldownTimer > 0f) return;
        _metamorphoseButton.enabled = true;
        _isMet = false;
    }

    private void Start()
    {
        AddMoney(0);
    }

    public void AddMoney(ulong money)
    {
        _money += money;
        
        string paddedMoney = _money.ToString($"D{_digits}");
        
        _moneyText.text = "$" + FormatWithSpaces(paddedMoney);
    }

    public void PressMetamorphoseButton()
    {
        var pm = FindObjectOfType<PlayerMetamorphose>();
        pm.Metamorphose();
        _metamorphoseButton.enabled = false;
        _metamorphoseButton.GetComponent<Image>().fillAmount = 0f;

        _metButtonCooldownTimer = _metCooldownButtonDuration;
        _isMet = true;
    }


    
    
    
    private static string FormatWithSpaces(string input)
    {
        // Comienza desde el final e inserta un espacio cada 3 dígitos.
        for (int i = input.Length - 3; i > 0; i -= 3)
        {
            input = input.Insert(i, " ");
        }
        return input;
    }
}
