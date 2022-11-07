using Scripts.Data;
using UnityEngine;

public class Game : MonoBehaviour, IStartCoroutine
{
    private const string LEVEL_PARAMETER = "Level";

    [SerializeField] private FadePanel _completePanel;

    private SceneLoader _sceneLoader;

    public event System.Action<int> OnStartLevel;
    public event System.Action<int> OnFinishLevel;
    public event System.Action<int> OnFailevel;

    private void Awake()
    {
        _sceneLoader = new SceneLoader(this);
    }

    private void Start()
    {
        StartLevel();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void StartLevel()
    {
        OnStartLevel?.Invoke(PlayerProgress.GetData().Level + 1);
        _completePanel.Hide(true);

        _sceneLoader.OnSceneLoaded += SceneLoaded;

        _sceneLoader.TryLoadLevel(LEVEL_PARAMETER);
    }

    private void SceneLoaded()
    {
        _sceneLoader.OnSceneLoaded -= SceneLoaded;
        Subscribe();
    }

    private void Subscribe()
    {
        Level.Instance.onLevelComplete += ShowWinPanel;
    }

    private void Unsubscribe()
    {
        Level.Instance.onLevelComplete -= ShowWinPanel;
    }

    private void ShowWinPanel()
    {
        _completePanel.Show();
    }

    public void RestartGame()
    {
        Unsubscribe();
        StartLevel();
    }

    public void NextLevel()
    {
        PlayerProgress.GetData().CompleteLevel();
        Unsubscribe();
        StartLevel();
    }
}