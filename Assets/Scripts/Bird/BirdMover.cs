using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;


[RequireComponent(typeof(Bird))]

public class BirdMover : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _menuPosition;

    [SerializeField] private Game _game;
    [SerializeField] private AudioSource _wingSound;
    [SerializeField] private float _speed;
    [SerializeField] private float _tapForce;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Rigidbody2D _rigidbody;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;
    private Quaternion _dieRotation;

    public bool isGameStarted = false;

    private void OnEnable()
    {
        _game.GameStarted += StartListener;

    }

    private void OnDisable()
    {
        _game.GameStarted -= StartListener;
    }

    void Start()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = Vector2.zero;

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
        _dieRotation = Quaternion.Euler(0, 0, -60f);

        //ResetBird();

    }

    void Update()
    {
        if (isGameStarted)
            Move();
    }

    public void ResetBird()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = _startPosition;
        _rigidbody.velocity = Vector2.zero;
    }

    public void SetMenuPosition()
    {
        transform.position = _menuPosition;
    }

    private void StartListener(bool isStarted)
    {
        isGameStarted = isStarted; 
    }

    private void Move ()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            _rigidbody.velocity = new Vector2(_speed, 0);
            transform.rotation = _maxRotation;
            _rigidbody.AddForce(Vector2.up * _tapForce, ForceMode2D.Force);
            _wingSound.Play();
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }
    public void Fall ()
    {
        _rigidbody.velocity = new Vector2(0, -4);
        transform.rotation = Quaternion.Lerp(transform.rotation, _dieRotation, _rotationSpeed*100 * Time.deltaTime);
    }
}
