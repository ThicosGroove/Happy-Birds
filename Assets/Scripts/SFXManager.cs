using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    Jogador _jogador;

    public GameObject[] audioSources;

    public bool hasPlayed { get; set; }

    private void Start()
    {
        _jogador = GameObject.FindObjectOfType<Jogador>();
    }

    public void SFX_Play(int item)
    {
        if (!hasPlayed || _jogador.hasLaunch)
        {
            audioSources[item].GetComponent<AudioSource>().Play();
            hasPlayed = true;
        }       
    }
}
