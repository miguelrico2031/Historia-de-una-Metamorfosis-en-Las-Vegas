
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

    [Header("Metamorphose")] [SerializeField]
    private Button _metamorphoseButton;
    
    private ulong _money = 0;
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
        StartCoroutine(WaitAndEnableButton(pm));
    }

    private IEnumerator WaitAndEnableButton(PlayerMetamorphose pm)
    {
        yield return new WaitForSeconds(pm.MetamorphoseDuration);
        _metamorphoseButton.enabled = true;
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
