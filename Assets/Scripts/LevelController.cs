using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] public int _nTentativaEstrela1;
    [SerializeField] public int _nTentativaEstrela2;
    [SerializeField] public int _nTentativaEstrela3;

    public GameObject _panel;

    Jogador _jogador;
    TransicaoScript _transicaoScript;
    CanvasController _canvasController;
    Monster[] _monsters;

    int _nivelAtual;
    int _nextLevel;
    string _levelMenu = "Level_Select_Menu";
    
    void OnEnable()
    {
        _jogador = FindObjectOfType<Jogador>();
        _transicaoScript = FindObjectOfType<TransicaoScript>();
        _canvasController = FindObjectOfType<CanvasController>();

        _monsters = FindObjectsOfType<Monster>();
    }

    void Start()
    {
        _nivelAtual = SceneManager.GetActiveScene().buildIndex - 2; //Para ser o mesmo numero das fases dos botoes;
        _nextLevel = SceneManager.GetActiveScene().buildIndex + 1;     
    }

    void Update()
    {
        if (MonsterAreAllDead())
        {
            SaveProgress();
            OpenWinningPanel();
        }
    }

    bool MonsterAreAllDead()
    {
        foreach (var monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
                return false;            
        }   

        return true;
    }

    public void GoToNextLevel()
    {
        Debug.LogWarning("Go To level " + _nextLevel);
        _transicaoScript.ComecaTransicao(_nextLevel);       
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SaveProgress()
    {
        if (_nextLevel > PlayerPrefs.GetInt("LevelAt"))
        {
            PlayerPrefs.SetInt("LevelAt", _nextLevel);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(_levelMenu);
    }

    void OpenWinningPanel()
    {
        _panel.SetActive(true);
        _canvasController.TempoAnimacaoEstrela();
        return;
    }

    public bool CanPassLevel()
    {
        if (_jogador._tentativa <= _nTentativaEstrela1)       
            return true;        
        else if (_jogador._tentativa <= _nTentativaEstrela2)        
            return true;       
        else if (_jogador._tentativa <= _nTentativaEstrela3)        
            return true;        
        else
            return false;
    }

    public int EstrelasGanhas()
    {
        if (_jogador._tentativa <= _nTentativaEstrela1)      
            return 1;        
        else if (_jogador._tentativa <= _nTentativaEstrela2)        
            return 2;        
        else if (_jogador._tentativa <= _nTentativaEstrela3)        
            return 3;        
        else
            return 0;
    }

    public int LevelJogado()
    {
        if (EstrelasGanhas() != 0)        
            return _nivelAtual;        
        else        
            return -1;       
    }
}
