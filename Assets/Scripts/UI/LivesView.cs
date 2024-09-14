using UnityEngine;

public class LivesView : MonoBehaviour
{
    [SerializeField] private GameObject[] _hearts;

    private void Start()
    {
        UpdateHearts(3);
    }

    public void UpdateHearts(int lives)
    {
        if (lives < 0 || lives > _hearts.Length)
        {
            Debug.LogError("Lives count out of range!");
            return;
        }

        foreach (var heart in _hearts)
        {
            heart.SetActive(false);
        }

        for (int i = 0; i < lives; i++)
        {
            if (i < _hearts.Length)
            {
                _hearts[i].SetActive(true);
            }
        }
    }
}
