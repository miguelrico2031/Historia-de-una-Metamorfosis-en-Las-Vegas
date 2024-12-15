
using System;
using UnityEngine;

public class GuardController : MonoBehaviour
{


    private Transform _player;


    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;   
    }
}
