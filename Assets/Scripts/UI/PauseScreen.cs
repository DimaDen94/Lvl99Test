using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public event Action Play;

    [SerializeField] private Button _play;

    private void OnEnable() => _play.onClick.AddListener(OnClickPlay);

    private void OnDisable() => _play.onClick.RemoveListener(OnClickPlay);

    private void OnClickPlay() => Play?.Invoke();
}
