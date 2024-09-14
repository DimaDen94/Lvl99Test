using UnityEngine;
[CreateAssetMenu(menuName = "Enemies/Enemy Data", order = 51)]
public class EnemyData : ScriptableObject
{
    public EnemyType enemyType;
    public float Health;
    public int Damage;
    public float Speed;
    public int Reward;
}
