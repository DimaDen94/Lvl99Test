using UnityEngine;
using UnityEngine.UI;

public class StructureSelectionView : MonoBehaviour
{
    [SerializeField] private Button _gnomeButton;
    [SerializeField] private Button _mageButton;
    [SerializeField] private Button _cancelButton;

    private StructureSelectionController _controller;

    public void Setup(StructureSelectionController controller)
    {
        _controller = controller;
        _gnomeButton.onClick.AddListener(() => _controller.SelectGnome());
        _mageButton.onClick.AddListener(() => _controller.SelectMage());
        _cancelButton.onClick.AddListener(() => _controller.Cancel());
    }

    public void Show() => gameObject.SetActive(true);

    public void Hide() => Destroy(gameObject);
}
