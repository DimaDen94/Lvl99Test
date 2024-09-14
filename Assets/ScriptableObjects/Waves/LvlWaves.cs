using UnityEngine;

[CreateAssetMenu(menuName = "Wave/Lvl", order = 51)]
public class LvlWaves : ScriptableObject
{
    [SerializeField] private Wave[] _wawes;

    public Wave[] Wawes => _wawes; 
}
