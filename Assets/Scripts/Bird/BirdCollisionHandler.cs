using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bird))]

public class BirdCollisionHandler : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private AudioSource _incrementSound;
    [SerializeField] private AudioSource _fallSound;
    [SerializeField] private AudioSource _dieSound;

    private Bird _bird;

    void Start()
    {
        _bird = GetComponent<Bird>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ScoreZone scoreZone))
        {
            _score.IncrementScore();
            _incrementSound.Play();
        }

        else if (collision.TryGetComponent(out Ground ground))
        {
            _bird.Die();
            Time.timeScale = 0;
            _dieSound.Play();
        }

        else
        {
            
                _bird.Fall();
                _fallSound.Play();
            
        }

    }

}
