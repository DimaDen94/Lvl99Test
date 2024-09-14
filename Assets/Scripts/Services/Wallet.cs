using System;

public class Wallet : IWalletService
{
    private int _coins;
    public int Coins => _coins;

    public event Action<int> OnCoinsChanged;


    public void SetStartCoins(int initialCoins)
    {
        _coins = initialCoins;
    }

    public bool TrySpendCoins(int amount)
    {
        if (amount <= 0) return false;

        if (_coins >= amount)
        {
            _coins -= amount;
            OnCoinsChanged?.Invoke(_coins);
            return true;
        }

        return false;
    }

    public void AddCoins(int amount)
    {
        if (amount > 0)
        {
            _coins += amount;
            OnCoinsChanged?.Invoke(_coins);
        }
    }
}
