using UnityEngine;

[ExecuteInEditMode]
public class CameraClamp : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _mapRenderer;
    [SerializeField] private Camera _camera;

    private void Start() => SetCameraToMap();

    private void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            SetCameraToMap();
        }
#endif
    }

    private void SetCameraToMap()
    {
        if(_camera == null)
            _camera = Camera.main;

        Bounds mapBounds = _mapRenderer.bounds;

        float mapWidth = mapBounds.size.x;

        _camera.orthographicSize = mapWidth * Screen.height / Screen.width * 0.5f;

        float cameraHeight = _camera.orthographicSize;

        _camera.transform.position = new Vector3(
            mapBounds.center.x,
            mapBounds.min.y + cameraHeight,
            _camera.transform.position.z
        );
    }
}
