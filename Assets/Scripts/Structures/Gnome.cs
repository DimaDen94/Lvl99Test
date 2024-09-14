using UnityEngine;

public class Gnome : Structure
{
    protected override void DealDamage(Enemy enemy)
    {
        enemy.ApplyDamage(_damage);
        Debug.Log($"Gnome attacked {enemy.name} for {_damage} damage!");
    }
}
