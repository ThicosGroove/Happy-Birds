using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    public static SaveData instance;

    public GameObject[] EstrelasGanhas;

    LevelController _levelController;
 
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _levelController = FindObjectOfType<LevelController>();
    }

    void Update()
    {
        SalvaEstrelaGanhas();
        SalvaLevelJogado();
    }

    void SalvaEstrelaGanhas()
    {
        if (EstrelasGanhas != null && _levelController != null)
        {
            if (_levelController.EstrelasGanhas() == 1)
            {
                PlayerPrefs.SetInt("EstrelaGanha", 1);
            }
            else if (_levelController.EstrelasGanhas() == 2)
            {
                PlayerPrefs.SetInt("EstrelaGanha", 2);
            }
            else if (_levelController.EstrelasGanhas() == 3)
            {
                PlayerPrefs.SetInt("EstrelaGanha", 3);
            }
            else
                PlayerPrefs.SetInt("EstrelaGanha", -1); //Só pra nao errar
        }
    }

    void SalvaLevelJogado()
    {
        if (_levelController != null)
        {
            if (_levelController.LevelJogado() >= 0)
                PlayerPrefs.SetInt("FaseGanha", _levelController.LevelJogado());
        }         
    }

}
