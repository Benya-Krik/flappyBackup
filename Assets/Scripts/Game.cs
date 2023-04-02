using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private BirdMover _birdMover;

    [SerializeField] private BarrierGenerator _barrierGenerator;

    [SerializeField] private MenuScreen _menuScreen;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private PauseScreen _pauseScreen;

    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private BirdScreen _birdScreen;
    [SerializeField] private FieldScreen _fieldScreen;
    [SerializeField] private PipesScreen _pipesScreen;


    [SerializeField] private Button _pauseButton;
    [SerializeField] private GameObject _pauseButtonObject;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _coins;
    [SerializeField] private Transform _skins;




    [SerializeField] private Score _score;
    [SerializeField] private Timer _timer;

    [SerializeField] private Animator _groundAnim;
    [SerializeField] private Animator _birdAnim;



    public event UnityAction<bool> GameStarted;
    public event UnityAction<bool> MenuClosed;


    private void Start()
    {
        _birdMover.SetMenuPosition();
        Time.timeScale = 0;
        _menuScreen.Open();
        _pauseButtonObject.SetActive(false);
    }

    private void OnEnable()
    {
        _startScreen.StartButtonClick += OnStartButtonClick;
        _menuScreen.PlayButtonClick += OnPlayButtonClick;
        _menuScreen.ShopButtonClick += OnShopButtonClick;
        _endScreen.RestartButtonClick += OnRestartButtonClick;
        _endScreen.MenuButtonClick += OnMenuButtonClick;
        _pauseScreen.MenuButtonClick += OnMenuButtonClick;
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _pauseScreen.UnpauseButtonClick += OnUnpauseButtonClick;
        _shopScreen.MenuButtonClick += OnMenuButtonClick;
        _shopScreen.BirdButtonClick += OnBirdButtonClick;
        _shopScreen.FieldButtonClick += OnFieldButtonClick;
        _shopScreen.PipesButtonClick += OnPipesButtonClick;

        _birdScreen.CloseButtonClick += OnCloseButtonClick;
        _fieldScreen.CloseButtonClick += OnCloseButtonClick;
        _pipesScreen.CloseButtonClick += OnCloseButtonClick;

        _bird.GameOver += OnGameOver;
        _bird.BirdFalled += OnBirdFalled;
    }

    private void OnDisable()
    {
        _startScreen.StartButtonClick -= OnStartButtonClick;
        _menuScreen.PlayButtonClick -= OnPlayButtonClick;
        _menuScreen.ShopButtonClick -= OnShopButtonClick;
        _endScreen.RestartButtonClick -= OnRestartButtonClick;
        _endScreen.MenuButtonClick -= OnMenuButtonClick;
        _pauseScreen.MenuButtonClick -= OnMenuButtonClick;
        _pauseScreen.UnpauseButtonClick -= OnUnpauseButtonClick;
        _shopScreen.MenuButtonClick -= OnMenuButtonClick;
        _shopScreen.BirdButtonClick -= OnBirdButtonClick;
        _shopScreen.FieldButtonClick -= OnFieldButtonClick;
        _shopScreen.PipesButtonClick -= OnPipesButtonClick;

        _birdScreen.CloseButtonClick -= OnCloseButtonClick;
        _fieldScreen.CloseButtonClick -= OnCloseButtonClick;
        _pipesScreen.CloseButtonClick -= OnCloseButtonClick;

        _bird.GameOver -= OnGameOver;
        _bird.BirdFalled -= OnBirdFalled;
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
    }

    private void OnPlayButtonClick()
    {
        _menuScreen.Close();
        _startScreen.Open();
        _bird.ResetPlayer();
        MenuClosed?.Invoke(true);
        _coins.SetActive(false);
    }

    private void OnStartButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void OnRestartButtonClick()
    {
        _endScreen.Close();
        _barrierGenerator.ResetPool();
        _startScreen.Open();
        _birdAnim.StopPlayback();
        _groundAnim.StopPlayback();
        _bird.ResetPlayer();
    }

    private void OnPauseButtonClick()
    {
        _pauseButtonObject.SetActive(false);
        Time.timeScale = 0;
        _score.Close();
        _groundAnim.StartPlayback();
        _pauseScreen.Open();
        GameStarted?.Invoke(false);
    }

    private void OnUnpauseButtonClick()
    {
        _pauseScreen.Close();
        StartCoroutine(StartTimer());
    }

    private void OnMenuButtonClick()
    {
        _endScreen.Close();
        _pauseScreen.Close();
        _player.SetActive(true);
        _shopScreen.Close();
        _barrierGenerator.ResetPool();
        _menuScreen.Open();
        _birdAnim.StopPlayback();
        _groundAnim.StopPlayback();
        _bird.ResetPlayer();
        _birdMover.SetMenuPosition();
        MenuClosed?.Invoke(false);
        _coins.SetActive(true);
    }

    private void OnShopButtonClick()
    {
        _menuScreen.Close();
        _player.SetActive(false);
        _shopScreen.Open();

    }

    private void OnBirdButtonClick()
    {
        _birdScreen.Open();

        StartCoroutine(ShowBird());

        IEnumerator ShowBird()
        {
            yield return new WaitForSecondsRealtime(0.25f);
            _player.SetActive(true);

            for (int i = 0; i < _skins.childCount; i++)
            {
                _skins.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
        }
    }

    private void OnFieldButtonClick()
    {
        _fieldScreen.Open();
    }

    private void OnPipesButtonClick()
    {
        _pipesScreen.Open();
    }

    private void OnCloseButtonClick()
    {
        _player.SetActive(false);
        _birdScreen.Close();
        _fieldScreen.Close();
        _pipesScreen.Close();

        for (int i = 0; i < _skins.childCount; i++)
        {
            _skins.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _groundAnim.StopPlayback();
        GameStarted?.Invoke(true);
        _pauseButtonObject.SetActive(true);
        _score.Open();
    }

    public void OnGameOver ()
    {
        _score.Close();
        _groundAnim.StartPlayback();
        _birdAnim.StartPlayback();
        GameStarted?.Invoke(false);
        _pauseButtonObject.SetActive(false);
        _endScreen.Open();

    }

    public void OnBirdFalled()
    {
        _groundAnim.StartPlayback();
        GameStarted?.Invoke(false);
    }

    public void StartBeforeTimer ()
    {
        StartGame();
        _score.Open();
        GameStarted?.Invoke(true);
    }

    IEnumerator StartTimer ()
    {
        _timer.Open();
        _timer.SetTime("3");
        yield return new WaitForSecondsRealtime(1);
        _timer.SetTime("2");
        yield return new WaitForSecondsRealtime(1);
        _timer.SetTime("1");
        yield return new WaitForSecondsRealtime(0.5f);
        _timer.SetTime("GO");
        yield return new WaitForSecondsRealtime(0.5f);
       _timer.Close();
        StartBeforeTimer();
    }
}
