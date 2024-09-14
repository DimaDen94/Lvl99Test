using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuMediator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _timeLeftText;
    [SerializeField] private Slider _currentWaveProgressBar;
    [SerializeField] private Image _pauseTimeRadial;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private LivesView _livesView;

    private IWalletService _walletService;
    private WaveManager _waveManager;
    private GameStateMachine _gameStateMachine;

    public void Construct(IWalletService walletService, WaveManager waveManager, GameStateMachine gameStateMachine)
    {
        _walletService = walletService;
        _waveManager = waveManager;
        _gameStateMachine = gameStateMachine;

        UpdateCoinsUI(_walletService.Coins);
        _walletService.OnCoinsChanged += UpdateCoinsUI;

        _waveManager.OnCurrentWaveProgressChanged += UpdateWaveProgressBar;
        _waveManager.OnPauseTimeRemainingChanged += UpdatePauseTimeRadial;
        _waveManager.OnStateChanged += HandleWaveStateChanged;
    }


    private void HandleWaveStateChanged(IWaveState state)
    {
        switch (state)
        {
            case WaitingState:
                _currentWaveProgressBar.gameObject.SetActive(false);
                _pauseTimeRadial.gameObject.SetActive(true);
                break;

            case SpawningState:
                _pauseTimeRadial.gameObject.SetActive(false);
                _currentWaveProgressBar.gameObject.SetActive(true);
                break;
        }
    }

    public void UpdateLifes(int lifeLeft)
    {
        _livesView.UpdateHearts(lifeLeft);
    }

    private void OnEnable()
    {
        _musicButton.onClick.AddListener(MusicClick);
        _pauseButton.onClick.AddListener(OnClickPause);
    }

    private void OnDisable()
    {
        _musicButton.onClick.RemoveListener(MusicClick);
        _pauseButton.onClick.RemoveListener(OnClickPause);
    }

    private void OnClickPause() => _gameStateMachine.Enter<PauseState>();

    private void MusicClick() => Debug.Log("Audio not supported");

    private void OnDestroy()
    {
        if (_walletService != null)
        {
            _walletService.OnCoinsChanged -= UpdateCoinsUI;
        }

        if (_waveManager != null)
        {
            _waveManager.OnCurrentWaveProgressChanged -= UpdateWaveProgressBar;
            _waveManager.OnPauseTimeRemainingChanged -= UpdatePauseTimeRadial;
            _waveManager.OnStateChanged -= HandleWaveStateChanged;
        }
    }

    private void UpdateCoinsUI(int newAmount)
    {
        _coinsText.text = $"{newAmount}";
    }

    public void UpdateTime(int timeLeft)
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        _timeLeftText.text = $"{minutes:D2}:{seconds:D2}";
    }

    private void UpdateWaveProgressBar(float progress)
    {
        _currentWaveProgressBar.value = progress;
    }

    private void UpdatePauseTimeRadial(float timeLeft)
    {
        _pauseTimeRadial.fillAmount = timeLeft / _waveManager.WaveDelay;
    }
}
