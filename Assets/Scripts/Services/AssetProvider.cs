using UnityEngine;

public class AssetProvider: IAssetProvider
{
    public T Load<T>(string path) where T : Object
    {
        T asset = Resources.Load<T>(path);
        if (asset == null)
        {
            Debug.LogError($"Ресурс не найден по пути: {path}");
        }
        return asset;
    }
}

public interface IAssetProvider
{
    T Load<T>(string path) where T : Object;
}