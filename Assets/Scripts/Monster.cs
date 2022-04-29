using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] // O GameObject com o script é selecionado no Scene mode.
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;

    SFXManager SFX;

    bool _hasDied;

    void Start()
    {
        SFX = GameObject.FindObjectOfType<SFXManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
           StartCoroutine(Die());        
    }

    IEnumerator Die()
    {
        _hasDied = true;
        SFX.SFX_Play(2);
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();

        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }

    bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
            return false;

        if (collision.gameObject.tag == "Player")
        {
            return true;
        }

        if (collision.contacts[0].normal.y < -0.1)
        {
            return true;
        }
       
        return false;
    }
}
