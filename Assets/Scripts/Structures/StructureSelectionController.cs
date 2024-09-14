using System;

public class StructureSelectionController
{
    private StructureSelectionView _view;
    private PlacementPoint _currentPlacementPoint;

    public StructureSelectionView View => _view;

    public event Action<StructureType, PlacementPoint> OnStructureSelected;

    public StructureSelectionController(StructureSelectionView view)
    {
        _view = view;
        _view.Setup(this);
    }

    public void Show(PlacementPoint placementPoint)
    {
        _currentPlacementPoint = placementPoint;
        _view.Show();
    }

    public void Hide()
    {
        _view.Hide();
    }

    public void SelectGnome()
    {
        OnStructureSelected?.Invoke(StructureType.Gnome, _currentPlacementPoint);
        Hide();
    }

    public void SelectMage()
    {
        OnStructureSelected?.Invoke(StructureType.Mage, _currentPlacementPoint);
        Hide();
    }

    public void Cancel()
    {
        Hide();
    }
}
