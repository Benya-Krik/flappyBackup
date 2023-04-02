using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ShopScreen : DialogScreen
{
    [SerializeField] private Button _birdButton;
    [SerializeField] private Button _fieldButton;
    [SerializeField] private Button _pipesButton;



    public event UnityAction MenuButtonClick;
    public event UnityAction BirdButtonClick;
    public event UnityAction FieldButtonClick;
    public event UnityAction PipesButtonClick;


    public override void Close()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        Button.interactable = false;
        _birdButton.interactable = false;
        _birdButton.onClick.RemoveListener(OnBirdButtonClick);
        _fieldButton.onClick.RemoveListener(OnFieldButtonClick);
        _pipesButton.onClick.RemoveListener(OnPipesButtonClick);


    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        Button.interactable = true;
        CanvasGroup.blocksRaycasts = true;
        _birdButton.interactable = true;
        _birdButton.onClick.AddListener(OnBirdButtonClick);
        _fieldButton.onClick.AddListener(OnFieldButtonClick);
        _pipesButton.onClick.AddListener(OnPipesButtonClick);

    }

    protected override void OnButtonClick()
    {
        MenuButtonClick?.Invoke();
    }

    private void OnBirdButtonClick()
    {
        BirdButtonClick?.Invoke();
    }

    private void OnFieldButtonClick()
    {
        FieldButtonClick?.Invoke();
    }
    private void OnPipesButtonClick()
    {
        PipesButtonClick?.Invoke();
    }
}
