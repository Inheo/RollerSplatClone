using UnityEngine;
using Scripts.Data;

[CreateAssetMenu(fileName = "game-settings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private LevelSettings[] _levelSettings;

    public LevelSettings this[int index]
    {
        get
        {
            return _levelSettings[index % _levelSettings.Length];
        }
    }

    public LevelSettings CurrentLevelSetting => this[PlayerProgress.GetData().Level];
}
