using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaInicialScript : MonoBehaviour
{
    TransicaoScript _transicaoScript;
    int _nextLevel;

    void Awake()
    {
        _transicaoScript = FindObjectOfType<TransicaoScript>();
    }

    void Start()
    {
        _nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void CliqueParaIniciar()
    {
        _transicaoScript.ComecaTransicao(_nextLevel);      
    }
}
