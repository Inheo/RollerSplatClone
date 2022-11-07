using UnityEngine;
using System.Collections.Generic;

public class BallRoadPainter : MonoBehaviour
{
    [SerializeField] private Level levelManager;
    [SerializeField] private BallMovement ballMovement;

    public event System.Action onEndRoadPaint;

    private void Start()
    {
        Paint(levelManager.DefaultBallRoadTile, .5f, 0f);
        ballMovement.onMoveStart += OnBallMoveStart;
    }

    private void OnDestroy()
    {
        ballMovement.onMoveStart -= OnBallMoveStart;
    }

    private void OnBallMoveStart(List<RoadTile> roadTiles, float totalDuration)
    {
        float stepDuration = totalDuration / roadTiles.Count;
        float duration = totalDuration / 2f;

        for (int i = 0; i < roadTiles.Count; i++)
        {
            RoadTile roadTile = roadTiles[i];
            if (!roadTile.IsPainted)
            {
                float delay = i * (stepDuration / 2f);
                Paint(roadTile, duration, delay);
            }
        }

        onEndRoadPaint?.Invoke();
    }

    private void Paint(RoadTile roadTile, float duration, float delay)
    {
        roadTile.Paint(levelManager.PaintColor, duration, delay);
    }
}
