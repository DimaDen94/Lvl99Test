using System;
using UnityEngine;

public class Hero : MonoBehaviour, IPausable
{
    public event Action OnDeath;
    public event Action<int> OnLifeLost;

    private int _lives = 3;

    public void ApplyDamage(int damage)
    {
        if (_lives <= 0) return;

        _lives -= damage;

        if (_lives > 0)
        {
            OnLifeLost?.Invoke(_lives);
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public void Pause()
    {

    }

    public void Resume()
    {

    }
}
