using UnityEngine;

[CreateAssetMenu(menuName = "Structures/StructureData", order = 51)]
public class StructureData : ScriptableObject
{
    public StructureType Type;
    public int Cost;
    public int Damage;
    public float AttackRange;
    public float AttackInterval;
}

