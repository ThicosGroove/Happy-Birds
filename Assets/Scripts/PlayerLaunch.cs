using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunch : MonoBehaviour
{
    public Trajectory trajectory;

    [SerializeField] float _forcaLancamento = 500f;
    [SerializeField] float _maxDragDistance = 5f;
    [SerializeField] bool _isNormalized;

    public bool hasLaunch { get; private set; }

    Rigidbody2D _rb;
    PolygonCollider2D _collider;

    Vector2 _initialPos;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<PolygonCollider2D>();
    }

    void Start()
    {
        _rb.isKinematic = true;
        _initialPos = _rb.position;
    }

    void OnMouseDown()
    {
        hasLaunch = false;
    }

    void OnMouseUp()
    {
        Vector2 currentPos = _rb.position;
        Vector2 direcao = _initialPos - currentPos;

        if (_isNormalized)
        {
            direcao.Normalize();
        }

        _rb.isKinematic = false;
        _rb.AddForce(direcao * _forcaLancamento);

        trajectory.Hide();
        _collider.enabled = true;
        hasLaunch = true;
    }

    void OnMouseDrag()
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

        Vector2 currentPos = _rb.position;
        Vector2 trajetoria = currentPos - _initialPos;

        trajectory.UpdateDots(_rb.position, trajetoria);
        trajectory.Show();      
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(RestartPosAfterDelay());
    }

    IEnumerator RestartPosAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        _rb.position = _initialPos;
        _rb.isKinematic = true;
        _rb.velocity = Vector2.zero;
    }

    public bool isReady()
    {
        if (_rb.position == _initialPos)
            return true;

        return false;       
    }
}
