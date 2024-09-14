using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class SafeAreaFilter : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    private void Awake()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();

        ApplySafeArea();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            ApplySafeArea();
        }
#endif
    }

    private void ApplySafeArea()
    {
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform is not assigned in SafeAreaFilter.");
            return;
        }

        var safeArea = Screen.safeArea;

        var anchorMin = safeArea.position;
        var anchorMax = anchorMin + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}
