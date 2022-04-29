using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton class: GameManager

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    public bool isDragging { get;  private set;  }
    public Trajectory _trajectory;

    [SerializeField] float _pushForce = 4f;
    [SerializeField] float _maxForce = 30f;

    Jogador _jogador;
    Camera cam;

    Vector2 _startPoint;
    Vector2 _endPoint;
    Vector2 _direction;
    Vector2 _force;
    float _distance;

    void OnEnable()
    {
        _jogador = FindObjectOfType<Jogador>();
    }

    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;
        OnDragStart();
    }

    void OnMouseDrag()
    {
        OnDrag();
    }

    void OnMouseUp()
    {
        isDragging = false;
        OnDragEnd();
    }

    void OnDragStart()
    { 
        _startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        _trajectory.Show();
    }

    void OnDrag()
    {
        _endPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        ClampDistance();

        _distance = Vector2.Distance(_startPoint, _endPoint);
        _direction = (_startPoint - _endPoint).normalized;

        ClampForce();

        _force = _direction * _distance * _pushForce;

        Debug.DrawLine(_startPoint, _endPoint);

        _trajectory.UpdateDots(_jogador._pos, _force);
    }

    void OnDragEnd()
    {
        _jogador.ActivateRb();
        _jogador.Push(_force);

        _trajectory.Hide();
    }

    Vector2 ClampDistance()
    {
        if (_endPoint.x > _startPoint.x )
        {
            _endPoint.x = _startPoint.x;
            return _endPoint;
        }
        else
            return _endPoint;
    }

    float ClampForce()
    {
        if (_distance > _maxForce)
        {
            _distance = _maxForce;
            return _distance;
        }
        else
            return _distance;
    }

}
