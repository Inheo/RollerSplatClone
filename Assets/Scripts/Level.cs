using UnityEngine;
using TMPro;
using Scripts.Data;

public class Level : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private BallRoadPainter _roadPointer;
    [SerializeField] private WallTile prefabWallTile;
    [SerializeField] private RoadTile prefabRoadTile;

    private float unitPerPixel;
    private LevelGenerator _generator;

    public Color PaintColor => _gameSettings.CurrentLevelSetting.PaintColor;
    public int RoadTilesCount => _generator.RoadTilesCount;
    public RoadTile DefaultBallRoadTile { get; private set; }
    public bool IsLevelCompleted { get; private set; }

    public event System.Action onLevelComplete;

    public static Level Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        IsLevelCompleted = false;

        _generator = new LevelGenerator();
        _generator.Generate(prefabWallTile, prefabRoadTile, _gameSettings.CurrentLevelSetting.LevelTexture, transform);
        DefaultBallRoadTile = _generator.GetRoadTile(0);

        _roadPointer.onEndRoadPaint += CheckGameComplete;

        _levelText.text = "Level: " + (PlayerProgress.GetData().Level + 1).ToString();
    }

    private void OnDestroy()
    {
        _roadPointer.onEndRoadPaint -= CheckGameComplete;
    }

    private void CheckGameComplete()
    {
        for (int i = 0; i < RoadTilesCount; i++)
        {
            if (_generator.GetRoadTile(i).IsPainted == false)
                return;
        }

        onLevelComplete?.Invoke();
        PlayerProgress.GetData().CompleteLevel();
        IsLevelCompleted = true;
    }
}
