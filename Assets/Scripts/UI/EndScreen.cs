using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class EndScreen: DialogScreen
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _coinsCount;
    [SerializeField] private TMP_Text _bestScore;
    [SerializeField] private GameObject _newBestIcon;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Transform _animatedCanvas;


    public event UnityAction RestartButtonClick;
    public event UnityAction MenuButtonClick;


    public int BestScore;

    public override void Close()
    {
        _animatedCanvas.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setIgnoreTimeScale(true);
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        Button.interactable = false;
        _menuButton.onClick.RemoveListener(OnMenuButtonClick);
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        Button.interactable = true;
        CanvasGroup.blocksRaycasts = true;
        _animatedCanvas.localPosition = new Vector2 (0, -Screen.height*2);
        _animatedCanvas.LeanMoveLocalY(0, 0.7f).setEaseInExpo().setIgnoreTimeScale(true).delay = 0.1f;
        _menuButton.onClick.AddListener(OnMenuButtonClick);
    }

    protected override void OnButtonClick()
    {
        RestartButtonClick?.Invoke();
    }

    private void OnMenuButtonClick()
    {
        MenuButtonClick?.Invoke();
    }

    public void SetTotalScore(int score)
    {
        _score.text = score.ToString();

        SetCoinsCount(score);

        if (BestScore < score)
        {
            BestScore = score;
            _newBestIcon.SetActive(true);
        }
        else
        {
            _newBestIcon.SetActive(false);
        }

        _bestScore.text = BestScore.ToString();
    }

    private void SetCoinsCount(int score)
    {
        _coinsCount.text = score.ToString();
    }
}
