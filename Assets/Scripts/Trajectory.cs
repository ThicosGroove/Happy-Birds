using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] GameObject _dotsParent;
    [SerializeField] GameObject _dotPrefab;
    [SerializeField] int _dotsNumber;
    [SerializeField] float _dotSpacing;

    Transform[] _dotsList;

    Vector2 _dotPos;
    float _timeStamp;

    void Start()
    {
        Hide();
        PrepareDots();
    }

    void PrepareDots()
    {
        _dotsList = new Transform[_dotsNumber];

        for (int i = 0; i < _dotsNumber; i++)
        {
            _dotsList[i] = Instantiate(_dotPrefab, null).transform;
            _dotsList[i].parent = _dotsParent.transform;
        }
    }

    public void UpdateDots(Vector2 ballPos, Vector2 forceApplied)  
    {
        _timeStamp = _dotSpacing;
        for (int i = 0; i < _dotsNumber; i++)
        {
            _dotPos.x = (ballPos.x + forceApplied.x * _timeStamp);
            _dotPos.y = (ballPos.y + forceApplied.y * _timeStamp) - (Physics2D.gravity.magnitude * _timeStamp * _timeStamp) / 2f;

            _dotsList[i].position = _dotPos;
            _timeStamp += _dotSpacing;
        }
    }

    public void Show()
    {
        _dotsParent.SetActive(true);
    }

    public void Hide()
    {
        _dotsParent.SetActive(false);
    }
}
