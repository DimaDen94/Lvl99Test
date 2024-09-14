using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryState : IGameState
{
    private readonly UIFactory _uiFactory;
    private VictoryScreen _screen;

    public VictoryState(UIFactory uiFactory)
    {
        _uiFactory = uiFactory;
    }

    public void Enter()
    {
        GameObject gameObject = _uiFactory.CreateVictoryDialog();
        _screen = gameObject.GetComponent<VictoryScreen>();
        _screen.Click += Reload;
    }

    private void Reload()
    {
        _screen.Click -= Reload;
        _screen = null;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void Update()
    {

    }

    public void Exit()
    {
        UnityEngine.Object.Destroy(_screen.gameObject);
    }
}
