using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private string _volumeParameter;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private Button _soundButton;


    [SerializeField] private GameObject _noSoundSprite;
    [SerializeField] private GameObject _sound1Sprite;
    [SerializeField] private GameObject _sound2Sprite;
    [SerializeField] private GameObject _sound3Sprite;


    private const float _multiplier = 20f;
    private bool _isSoundOff = false;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnHandleSliderChanged);
        _soundButton.onClick.AddListener(OnOffSoundClick);

    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnHandleSliderChanged);
        _soundButton.onClick.RemoveListener(OnOffSoundClick);
    }

    private void OnHandleSliderChanged(float value)
    {
        float volumeValue = Mathf.Log10(value) * _multiplier;
        _mixer.SetFloat(_volumeParameter, volumeValue);

        if (value == 0.0001f)
        {
            _noSoundSprite.SetActive(true);

            _sound1Sprite.SetActive(false);
            _sound2Sprite.SetActive(false);
            _sound3Sprite.SetActive(false);
            _isSoundOff = true;
        }
        else if (value > 0.0001f && value < 0.3f)
        {
            _sound1Sprite.SetActive(true);

            _noSoundSprite.SetActive(false);
            _sound2Sprite.SetActive(false);
            _sound3Sprite.SetActive(false);
            _isSoundOff = false;

        }
        else if (value > 0.3f && value < 0.6f)
        {
            _sound2Sprite.SetActive(true);

            _noSoundSprite.SetActive(false);
            _sound3Sprite.SetActive(false);
            _sound1Sprite.SetActive(false);
            _isSoundOff = false;

        }
        else if (value > 0.6f)
        {
            _sound3Sprite.SetActive(true);

            _noSoundSprite.SetActive(false);
            _sound1Sprite.SetActive(false);
            _sound2Sprite.SetActive(false);
            _isSoundOff = false;

        }
    }

    private void OnOffSoundClick()
    {
       if (!_isSoundOff)
        {
            _isSoundOff = true;

            _sound1Sprite.SetActive(false);
            _sound2Sprite.SetActive(false);
            _sound3Sprite.SetActive(false);

            _noSoundSprite.SetActive(true);

            _mixer.SetFloat(_volumeParameter, 0.0001f);
            _slider.value = 0.0001f;
        }

        else
        {
            _isSoundOff = false;
            _slider.value = 1f;
            _noSoundSprite.SetActive(false);
            _sound1Sprite.SetActive(false);
            _sound2Sprite.SetActive(false);

            _sound3Sprite.SetActive(true);
        }
    }
}
