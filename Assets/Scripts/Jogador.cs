using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    public bool hasLaunch{ get; private set; }
    public int _tentativa = 0;

    [SerializeField] float _maxDragDistance = 2f;
    [HideInInspector] public Vector3 _pos { get { return transform.position; } }

    SFXManager SFX;
    GameManager _gameManager;

    Rigidbody2D _rb;
    PolygonCollider2D _collider;

    Vector2 _initialPos;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        SFX = GameObject.FindObjectOfType<SFXManager>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<PolygonCollider2D>();
    }

    void Start()
    {
        _initialPos = _rb.transform.position;
        _rb.isKinematic = true;
        _collider.enabled = false;
    }

    void Update()
    {
        ArrastaJogador();
    }

    void ArrastaJogador()
    {
        if (_gameManager.isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 desirePos = mousePosition;
            float distance = Vector2.Distance(desirePos, _initialPos);

            if (distance > _maxDragDistance)
            {
                Vector2 direction = desirePos - _initialPos;
                direction.Normalize();
                desirePos = _initialPos + (direction * _maxDragDistance);
            }

            if (desirePos.x > _initialPos.x)
                desirePos.x = _initialPos.x;

            _rb.position = desirePos;
        }
    }

    public void Push(Vector2 force)
    {
        _rb.AddForce(force, ForceMode2D.Impulse);
        _collider.enabled = true;

        PlayLaunchSFX();

        Debug.LogError("Lançou");
        hasLaunch = true;
        _tentativa++;
    }

    public void ActivateRb()
    {
        _rb.isKinematic = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {        
       StartCoroutine(RestartPosAfterDelay());
    }

    IEnumerator RestartPosAfterDelay()
    {
        yield return new WaitForSeconds(3.5f);

        _rb.position = _initialPos;
        _rb.isKinematic = true;
        _rb.velocity = Vector2.zero;
        _collider.enabled = false;
        hasLaunch = false;
    }

    public bool isReady()
    {
        if (_rb.position == _initialPos)
            return true;

        return false;
    }

    void PlayLaunchSFX()
    {
        SFX.SFX_Play(3);
        SFX.hasPlayed = false;
    }
}
