using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLine : MonoBehaviour
{ 
    public Transform[] _stripPositions;
    public LineRenderer[] _lineRenderers;

    Jogador _player;

    Vector3 _currentPos;

    private void Awake()
    {
        _player = FindObjectOfType<Jogador>();
    }

    void Start()
    {
        _lineRenderers[0].positionCount = 2;
        _lineRenderers[1].positionCount = 2;
        _lineRenderers[0].SetPosition(0, _stripPositions[0].position);
        _lineRenderers[1].SetPosition(0, _stripPositions[1].position);
    }

    void Update()
    {

        if (_player != null && _player.hasLaunch == false)
        {
            _currentPos = _player.transform.position;

            SetStrips(_currentPos);
        }
        else
        {
            BreakingStrips();
            ResetStrips();
        }
    }

    void BreakingStrips()
    {
        if (_player.hasLaunch)
        {
            _lineRenderers[0].enabled = false;
            _lineRenderers[1].enabled = false;
        }   
    }

    void ResetStrips()
    {
        if (_player.isReady())
        {
            _currentPos = _player.transform.position;
            SetStrips(_currentPos);
        }    
    }

    void SetStrips(Vector2 position)
    {
        _lineRenderers[0].enabled = true;
        _lineRenderers[1].enabled = true;
        _lineRenderers[0].SetPosition(1, position);
        _lineRenderers[1].SetPosition(1, position);
    }
}
