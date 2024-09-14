using UnityEngine;

public class Mage : Structure
{
    protected override void DealDamage(Enemy enemy)
    {
        enemy.ApplyDamage(_damage);
        Debug.Log($"Mage attacked {enemy.name} for {_damage} damage!");
    }
}
