using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bird))]

public class BirdTracker : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private Game _game;

    [SerializeField] private float _xOffset;

    private bool _isMenuClosed = false;

    private void OnEnable()
    {
        _game.MenuClosed += SetCameraPosition;
    }

    private void OnDisable()
    {
        _game.MenuClosed -= SetCameraPosition;
    }

    void LateUpdate()
    {
        if (_isMenuClosed)
            _xOffset = -2;
        else
            _xOffset = 0;


        transform.position = new Vector3(_bird.transform.position.x - _xOffset, transform.position.y, transform.position.z);

    }

    private void SetCameraPosition(bool isMenuClosed)
    {
        _isMenuClosed = isMenuClosed;
    }
}
