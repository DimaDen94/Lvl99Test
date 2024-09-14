using System.Collections.Generic;
using UnityEngine;

public class StructureManager
{
    private readonly StructureFactory _structureFactory;
    private readonly UIFactory _uiFactory;
    private readonly List<PlacementPoint> _placementPoints;
    private readonly IWalletService _wallet;
    private readonly StructureData[] _structureDatas;
    private readonly StructureHolder _structureHolder;
    private StructureSelectionController _currentController;
    private PlacementPoint _currentPlacementPoint;

    public StructureManager(StructureFactory structureFactory, UIFactory uiFactory, List<PlacementPoint> placementPoints, IWalletService wallet, StructureData[] structureDatas, StructureHolder structureHolder)
    {
        _structureFactory = structureFactory;
        _uiFactory = uiFactory;
        _placementPoints = placementPoints;
        _wallet = wallet;
        _structureDatas = structureDatas;
        _structureHolder = structureHolder;
    }

    public void Activate() {
        foreach (PlacementPoint point in _placementPoints)
        {
            point.Clicked += OnPlacementPointClicked;
        }
    }

    public void Deactivate()
    {
        foreach (PlacementPoint point in _placementPoints)
        {
            point.Clicked -= OnPlacementPointClicked;
        }
    }
    private void OnPlacementPointClicked(PlacementPoint placementPoint)
    {
        if (_currentController != null && _currentPlacementPoint != placementPoint)
        {
            if (_currentController.View != null)
            {
                _currentController.Hide();
            }
            else
            {
                Debug.LogWarning("StructureSelectionView was already destroyed.");
            }
        }

        StructureSelectionView view = _uiFactory.CreateStructureSelectionView(placementPoint.transform);
        if (view != null)
        {
            _currentController = new StructureSelectionController(view);
            _currentController.OnStructureSelected += (type, point) => PlaceStructure(type, point);
            _currentController.Show(placementPoint);
            _currentPlacementPoint = placementPoint;
        }
    }



    private void PlaceStructure(StructureType type, PlacementPoint placementPoint)
    {
        if (placementPoint != _currentPlacementPoint)
        {
            Debug.LogError("Placement point mismatch.");
            return;
        }

        StructureData structureData = System.Array.Find(_structureDatas, data => data.Type == type);
        if (structureData == null)
        {
            Debug.LogError("Structure data not found.");
            return;
        }

        if (!_wallet.TrySpendCoins(structureData.Cost))
        {
            Debug.Log("Not enough coins to place structure.");
            return;
        }

        Structure structure = _structureFactory.CreateStructure(type, placementPoint.transform, structureData);
        if (structure != null)
        {
            placementPoint.PlaceStructure(structure);
            _currentController.Hide();
            _currentPlacementPoint = null;
            _currentController = null;
            _structureHolder.AddStructure(structure);
        }
    }
}
