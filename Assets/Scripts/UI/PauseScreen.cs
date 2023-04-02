using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseScreen : DialogScreen
{
    [SerializeField] private Button _menuButton;

    public event UnityAction UnpauseButtonClick;
    public event UnityAction MenuButtonClick;

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        Button.interactable = false;
        _menuButton.interactable = false;
        _menuButton.onClick.RemoveListener(OnMenuButtonClick);
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        Button.interactable = true;
        _menuButton.interactable = true;
        CanvasGroup.blocksRaycasts = true;
        _menuButton.onClick.AddListener(OnMenuButtonClick);
    }

    protected override void OnButtonClick()
    {
        UnpauseButtonClick?.Invoke();
    }

    private void OnMenuButtonClick()
    {
        MenuButtonClick?.Invoke();
    }
}
