using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pause : MonoBehaviour

{
    public event UnityAction PauseButtonClick;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        PauseButtonClick?.Invoke();
    }
}
