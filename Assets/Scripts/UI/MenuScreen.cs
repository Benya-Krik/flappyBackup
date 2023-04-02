using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class MenuScreen : DialogScreen
{

    [SerializeField] private Yandex _yandexSDK;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _rateButton;
    [SerializeField] private Button _recordButton;


    public event UnityAction PlayButtonClick;
    public event UnityAction ShopButtonClick;

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        Button.interactable = false;
        _shopButton.interactable = false;
        _rateButton.onClick.RemoveListener(OnRateButtonClick);
        _shopButton.onClick.RemoveListener(OnShopButtonClick);
        _recordButton.onClick.RemoveListener(OnRecordButtonClick);


    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        Button.interactable = true;
        CanvasGroup.blocksRaycasts = true;
        _shopButton.interactable = true;
        _rateButton.onClick.AddListener(OnRateButtonClick);
        _shopButton.onClick.AddListener(OnShopButtonClick);
        _recordButton.onClick.AddListener(OnRecordButtonClick);
    }

    protected override void OnButtonClick()
    {
        PlayButtonClick?.Invoke();
    }

    private void OnShopButtonClick()
    {
        ShopButtonClick?.Invoke();
    }

    private void OnRateButtonClick()
    {
        _yandexSDK.RateTheGame();
    }

    private void OnRecordButtonClick()
    {
        PlayerPrefs.DeleteAll();
    }
}
