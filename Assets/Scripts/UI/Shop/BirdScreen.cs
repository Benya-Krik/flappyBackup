using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BirdScreen : DialogScreen
{
    [SerializeField] private Transform _animatedCanvas;
    
    public event UnityAction CloseButtonClick;

    private void Start()
    {
        _animatedCanvas.localScale = Vector2.zero;
    }

    public override void Close()
    {
        _animatedCanvas.LeanScale(Vector2.zero, 0.2f).setIgnoreTimeScale(true);
        StartCoroutine(CloseDialog());
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        Button.interactable = true;
        CanvasGroup.blocksRaycasts = true;
        _animatedCanvas.LeanScale(Vector2.one, 0.2f).setIgnoreTimeScale(true);

    }

    protected override void OnButtonClick()
    {
        CloseButtonClick?.Invoke();
    }

    IEnumerator CloseDialog()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        Button.interactable = false;
    }
}
