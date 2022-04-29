using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaSFx : MonoBehaviour
{
    SFXManager SFX;
    Jogador _jogador;

    void Start()
    {
        SFX = GameObject.FindObjectOfType<SFXManager>();
        _jogador = GameObject.FindObjectOfType<Jogador>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_jogador.hasLaunch)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Monster")
            {
                SFX.SFX_Play(1);
            }
        }  
    }
}

