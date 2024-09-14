using UnityEngine;
using System;

public class PlacementPoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _frame;
    [SerializeField] private PlacementPointSide _side;
    public event Action<PlacementPoint> Clicked;

    private bool _isOccupied;

    private void OnMouseDown()
    {
        if (!_isOccupied)
        {
            Clicked?.Invoke(this);
        }
    }

    public void PlaceStructure(Structure structure)
    {
        if (!_isOccupied)
        {
            structure.GetComponent<SpriteRenderer>().flipX = _side == PlacementPointSide.Left;
            _isOccupied = true;
            _frame.enabled = false;
        }
    }
}

public enum PlacementPointSide {
    Left = 0, Right = 1
}

