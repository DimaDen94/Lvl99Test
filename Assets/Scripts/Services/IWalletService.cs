using System;

public interface IWalletService
{
    int Coins { get; }

    event Action<int> OnCoinsChanged;

    void AddCoins(int amount);
    void SetStartCoins(int initialCoins);
    bool TrySpendCoins(int amount);
}
