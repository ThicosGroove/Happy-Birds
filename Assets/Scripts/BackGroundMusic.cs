using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    SFXManager _sFXManager;

    void Awake()
    {
        _sFXManager = GameObject.FindObjectOfType<SFXManager>();
    }

    void Start()
    {
        _sFXManager.SFX_Play(0);
    }
}
