using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaVitoria : MonoBehaviour
{
    public void RetornarTelaInicial()
    {
        SceneManager.LoadScene(0);
    }
}
