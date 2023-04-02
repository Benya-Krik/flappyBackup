using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private Score _score;

    private BirdMover _mover;

    public event UnityAction GameOver;
    public event UnityAction BirdFalled;

    void Start()
    {
        _mover = GetComponent<BirdMover>();
    }

    public void ResetPlayer()
    {
        _mover.ResetBird();
        _score.ResetScore();
    }

    public void Die()
    {
        GameOver?.Invoke();
        _score.ResetScore();
    }

    public void Fall()
    {
        BirdFalled?.Invoke();
        _mover.Fall();
    }
}
