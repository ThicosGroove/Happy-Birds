using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransicaoScript : MonoBehaviour
{
    Animator _panelAnimation;

    void Awake()
    {
        _panelAnimation = GetComponent<Animator>();
    }

    void Start()
    {
        if (_panelAnimation != null)
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                _panelAnimation.SetTrigger("InLevel");
            }
        }    
    }

    public void ComecaTransicao(int levelName)
    {
        StartCoroutine(TransicaoFimDeLevel(levelName));
    }

    IEnumerator TransicaoFimDeLevel(int levelName)
    {
        _panelAnimation.SetTrigger("OutLevel");
        Debug.LogWarning("Transicaaooo");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelName);
    }
}
