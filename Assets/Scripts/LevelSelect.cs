using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public Button[] _levelButtons;
    public GameObject[] _estrelas;

    TransicaoScript _transicaoScript;

    int _levelName;

    void Awake()
    {
        _transicaoScript = FindObjectOfType<TransicaoScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("LevelAt", 2);
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            if (i + 2 > levelReached)
            {
                _levelButtons[i].interactable = false;
            }
        }

        MostraEstrelaGanha();
    }

    public void SelectLevel(int LevelName)
    {
        _levelName = LevelName;
    }

    public void GoToSelectLevel()
    {
        if (_levelName >= 1)
        {           
            _transicaoScript.ComecaTransicao(_levelName);  
        }
    }

    public void HomeButton()
    {
        _transicaoScript.ComecaTransicao(0);
    }

    void MostraEstrelaGanha()
    {
        if (PlayerPrefs.GetInt("EstrelaGanha") > 0)
        {
            for (int i = 0; i < _levelButtons.Length; i++)//Fazer todas sa fases
            {
                if (PlayerPrefs.GetInt("FaseGanha") == i)
                {
                    for (int j = 0; j < PlayerPrefs.GetInt("EstrelaGanha"); j++)
                    {
                        _estrelas[j].SetActive(true);
                    }
                }
            }
        }
    }

    //Se o jogador conseguir 1 estrela na fase 1, o LevelSelect precisa salvar e ligar a _estrelaGanha[0] 
    //no Botao[0] da fase jogada.

    //Botao[_canvasController._nivelAtualInt] é o botao da fase jogada.
}
