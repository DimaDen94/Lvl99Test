using UnityEngine;

public class StructureFactory
{
    private readonly IAssetProvider _assetProvider;
    private readonly ActiveEnemyHolder _enemyHolder;

    private const string GnomePrefabPath = "Structures/GnomePrefab";
    private const string MagePrefabPath = "Structures/MagePrefab";

    public StructureFactory(IAssetProvider assetProvider, ActiveEnemyHolder enemyHolder)
    {
        _assetProvider = assetProvider;
        _enemyHolder = enemyHolder;
    }

    public Structure CreateStructure(StructureType type, Transform transform, StructureData structureData)
    {
        string path = type switch
        {
            StructureType.Gnome => GnomePrefabPath,
            StructureType.Mage => MagePrefabPath,
            _ => null
        };

        return CreateAndSetupStructure(path, transform, structureData);
    }

    private Structure CreateAndSetupStructure(string prefabPath, Transform transform, StructureData structureData)
    {
        if (string.IsNullOrEmpty(prefabPath))
        {
            Debug.LogError("Prefab path is null or empty.");
            return null;
        }

        GameObject prefab = _assetProvider.Load<GameObject>(prefabPath);

        if (prefab == null)
        {
            Debug.LogError($"Prefab not found at path: {prefabPath}");
            return null;
        }

        GameObject structureObject = Object.Instantiate(prefab, transform);

        Structure structure = structureObject.GetComponent<Structure>();
        if (structure == null)
        {
            Debug.LogError("Structure component not found on the prefab.");
            return null;
        }

        structure.Construct(_enemyHolder, structureData);

        return structure;
    }
}
