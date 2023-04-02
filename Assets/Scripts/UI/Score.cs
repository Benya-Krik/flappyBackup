using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    [SerializeField] private CanvasGroup CanvasGroup;
    [SerializeField] private Bird _bird;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private Money _money;


    private int _scorePoint = 0;

    private void OnEnable()
    {
        _bird.GameOver += SetTotalScore;
    }

    private void OnDisable()
    {
        _bird.GameOver -= SetTotalScore;
    }

    public void Open ()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
    }

    public void IncrementScore()
    {
        _scorePoint += 1;
        _score.text = _scorePoint.ToString();
    }

    public void ResetScore()
    {
        _scorePoint = 0;
        _score.text = _scorePoint.ToString();
    }

    private void SetTotalScore()
    {
        _endScreen.SetTotalScore(_scorePoint);

        _money.IncreaseMoney(_scorePoint);
    }

}
