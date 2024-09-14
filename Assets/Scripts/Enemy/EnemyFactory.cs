using UnityEngine;
using UnityEngine.Splines;

public class EnemyFactory
{
    private const string EnemyPrefabsPath = "Enemies/";
    private EnemyData[] _enemyDatas;
    private Hero _hero;
    private Spline _bezierKnots;
    private IAssetProvider _assetProvider;

    public EnemyFactory(EnemyData[] enemyDatas, Hero hero, Spline bezierKnots, IAssetProvider assetProvider)
    {
        _enemyDatas = enemyDatas;
        _hero = hero;
        _bezierKnots = bezierKnots;
        _assetProvider = assetProvider;
    }

    public Enemy CreateEnemy(EnemyType type, Vector3 position)
    {
        EnemyData data = GetEnemyData(type);
        if (data == null)
        {
            Debug.LogError($"No data found for enemy type: {type}");
            return null;
        }

        string path = $"{EnemyPrefabsPath}{type}";
        GameObject prefab = _assetProvider.Load<GameObject>(path);
        if (prefab != null)
        {
            GameObject enemyObj = Object.Instantiate(prefab, position, Quaternion.identity);
            Enemy enemyScript = enemyObj.GetComponent<Enemy>();

            if (enemyScript != null)
            {
                enemyScript.Init(data, _hero, _bezierKnots);
                return enemyScript;
            }
        }
        return null;
    }

    private EnemyData GetEnemyData(EnemyType type)
    {
        foreach (var data in _enemyDatas)
        {
            if (data.enemyType == type)
            {
                return data;
            }
        }
        return null;
    }
}
