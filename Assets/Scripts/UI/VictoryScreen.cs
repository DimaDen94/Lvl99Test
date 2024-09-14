using System;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Button _close;

    private void OnEnable() => _close.onClick.AddListener(OnCloseClick);

    private void OnDisable() => _close.onClick.RemoveListener(OnCloseClick);

    public void OnCloseClick() => Click?.Invoke();
}
