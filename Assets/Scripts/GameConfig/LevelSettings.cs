using UnityEngine;

[CreateAssetMenu(fileName = "level-settings-", menuName = "Level Settings")]
public class LevelSettings : ScriptableObject
{
    [SerializeField] private Texture2D _levelTexture;
    [SerializeField] private Color _paintColor;
    [SerializeField] private TilesColor _tilesColor = new TilesColor(Color.black);

    public Texture2D LevelTexture => _levelTexture;
    public Color PaintColor => _paintColor;
}

[System.Serializable]
public struct TilesColor
{
    [SerializeField] private Color _roadColor;
    [SerializeField] private Color _wallColor;

    public TilesColor(Color _)
    {
        _roadColor = Color.black;
        _wallColor = Color.white;
    }
}