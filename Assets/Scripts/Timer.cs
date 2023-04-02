using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Security.Cryptography.X509Certificates;

public class Timer : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private TMP_Text _timeText;

    public void SetTime (string time)
    {
        _timeText.text = time;
    }
    
    public void Open()
    {
        _canvas.alpha = 1;
        _canvas.blocksRaycasts = true;
    }

    public void Close()
    {
        _canvas.alpha = 0;
        _canvas.blocksRaycasts = false;
    }
}
