using UnityEngine;

public class UIFactory
{
    private const string StructureSelectionViewPath = "Structures/UI/StructureSelectionView";
    private const string PauseDialogPath = "PopUp/PauseDialog";
    private const string DefeatDialogPath = "PopUp/DefeatDialog";
    private const string VictoryDialogPath = "PopUp/VictoryDialog";

    private readonly IAssetProvider _assetProvider;

    public UIFactory(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public StructureSelectionView CreateStructureSelectionView(Transform uiParent)
    {
        GameObject prefab = _assetProvider.Load<GameObject>(StructureSelectionViewPath);
        if (prefab == null)
        {
            return null;
        }

        GameObject viewObject = Object.Instantiate(prefab, uiParent);
        return viewObject.GetComponent<StructureSelectionView>();
    }

    public GameObject CreatePauseDialog() =>
        Object.Instantiate(_assetProvider.Load<GameObject>(PauseDialogPath));

    public GameObject CreateDefeatDialog() =>
        Object.Instantiate(_assetProvider.Load<GameObject>(DefeatDialogPath));

    public GameObject CreateVictoryDialog() =>
        Object.Instantiate(_assetProvider.Load<GameObject>(VictoryDialogPath));
}


