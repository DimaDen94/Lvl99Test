using System.Collections.Generic;

public class EnemyWaveCounter
{
    public Dictionary<EnemyType, int> CountEnemiesByType(LvlWaves lvlWaves)
    {
        var typeCounts = new Dictionary<EnemyType, int>();

        foreach (var wave in lvlWaves.Wawes)
        {
            foreach (var waveUnit in wave.WaveUnits)
            {
                if (typeCounts.ContainsKey(waveUnit.enemy))
                {
                    typeCounts[waveUnit.enemy]++;
                }
                else
                {
                    typeCounts[waveUnit.enemy] = 1;
                }
            }
        }

        return typeCounts;
    }
}

