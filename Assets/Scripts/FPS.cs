using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private int _fpsValue = 60;

    private void OnValidate()
    {
        Application.targetFrameRate = _fpsValue;
    }
}
