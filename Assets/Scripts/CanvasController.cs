using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public GameObject _estrelaCheia1;
    public GameObject _estrelaCheia2;
    public GameObject _estrelaCheia3;

    public GameObject _estrelaApagada1;
    public GameObject _estrelaApagada2;
    public GameObject _estrelaApagada3;

    public GameObject _panel;
    public TMP_Text _nTentativasTexto;
    public TMP_Text _nivelBloqueadoTexto;
    public TMP_Text _nivelAtualTexto;

    public int _estrelaPorFase;
    public int _nivelAtualInt;

    LevelController _levelController;
    Jogador _jogador;

    bool isGamePaused = false;

    void Start()
    {
        _levelController = FindObjectOfType<LevelController>();
        _jogador = FindObjectOfType<Jogador>();

        _nTentativasTexto.text = "Tentativas: " + _jogador._tentativa.ToString();
        _nivelAtualTexto.text = SceneManager.GetActiveScene().name;
        _nivelBloqueadoTexto.text = "";
    }

    void Update()
    {
        UpdateTentativa();
    }

    void UpdateTentativa()
    {
        if (_jogador.hasLaunch)
        {
            _nTentativasTexto.text = "Tentativas: " + _jogador._tentativa.ToString();
        }
    }

   public void PauseAndUnpause()
   {
        if (!isGamePaused)
        {
            Debug.LogError("Pausado");
            Time.timeScale = 0f;
            _panel.SetActive(true);
            isGamePaused = true;
        }
        else if (isGamePaused)
        {
            Debug.LogError("Despausado");
            Time.timeScale = 1f;
            _panel.SetActive(false);
            isGamePaused = false;
        }
   }

    public void GoToNextLevel()
    {
        if (_levelController.CanPassLevel())
        {
            _levelController.GoToNextLevel();
            _nivelBloqueadoTexto.text = "";
        }
        else
        {
            _nivelBloqueadoTexto.text = "Não pode passar de nível sem alguma estrela";
        }
    }

    public void RestartLevel()
    {
        PauseAndUnpause();
        _levelController.RestartLevel();
    }

    public void HomeButton()
    {
        if (isGamePaused)
        {
            PauseAndUnpause();
        }       
        _levelController.ReturnToMainMenu();
    }

    public void TempoAnimacaoEstrela()
    {
        if (_jogador._tentativa <= _levelController._nTentativaEstrela3)
        {
            //Animaçao da 3 estrela
            _estrelaApagada1.SetActive(false);
            _estrelaApagada2.SetActive(false);
            _estrelaApagada3.SetActive(false);

            _estrelaCheia1.SetActive(true);
            _estrelaCheia2.SetActive(true);
            _estrelaCheia3.SetActive(true);
        }
        else if (_jogador._tentativa <= _levelController._nTentativaEstrela2)
        {
            //animacao da 2 estrela
            _estrelaApagada1.SetActive(false);
            _estrelaApagada2.SetActive(false);
            _estrelaApagada3.SetActive(true);

            _estrelaCheia1.SetActive(true);
            _estrelaCheia2.SetActive(true);
            _estrelaCheia3.SetActive(false);
        }
        else if (_jogador._tentativa <= _levelController._nTentativaEstrela1)
        {
            //Animaçao da 1 estrela
            _estrelaApagada1.SetActive(false);
            _estrelaApagada2.SetActive(true);
            _estrelaApagada3.SetActive(true);

            _estrelaCheia1.SetActive(true);
            _estrelaCheia2.SetActive(false);
            _estrelaCheia3.SetActive(false);
        }
        else 
        {
            //Nenhuma animacao
            _estrelaApagada1.SetActive(true);
            _estrelaApagada2.SetActive(true);
            _estrelaApagada3.SetActive(true);

            _estrelaCheia1.SetActive(false);
            _estrelaCheia2.SetActive(false);
            _estrelaCheia3.SetActive(false);
        }
    }    
}
