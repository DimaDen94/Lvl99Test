using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatState : IGameState
{
    private readonly UIFactory _uIFactory;
    private DefeatScreen _screen;

    public DefeatState(UIFactory uIFactory)
    {
        _uIFactory = uIFactory;
    }

    public void Enter()
    {
        GameObject gameObject = _uIFactory.CreateDefeatDialog();
        _screen = gameObject.GetComponent<DefeatScreen>();
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
