using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class StartScreen: DialogScreen
{

    [SerializeField] private AudioSource _swooshSound;

    public event UnityAction StartButtonClick;

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        Button.interactable = false;
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        Button.interactable = true;
        CanvasGroup.blocksRaycasts = true;

    }

    protected override void OnButtonClick()
    {
        StartButtonClick?.Invoke();
        _swooshSound.Play();
    }
}
