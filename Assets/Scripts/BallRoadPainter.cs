using UnityEngine;
using System.Collections.Generic;

public class BallRoadPainter : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private BallMovement ballMovement;
    [SerializeField] private MeshRenderer ballMeshRenderer;

    public int paintedRoadTiles = 0;

   private void Start()
   {
      ballMeshRenderer.material.color = levelManager.paintColor;
      Paint(levelManager.defaultBallRoadTile, .5f, 0f);

      ballMovement.onMoveStart += OnBallMoveStart;
   }

   private void OnBallMoveStart(List<RoadTile> roadTiles, float totalDuration)
   {
      float stepDuration = totalDuration / roadTiles.Count;
      float duration = totalDuration / 2f;

      for (int i = 0; i < roadTiles.Count; i++)
      {
         RoadTile roadTile = roadTiles[i];
         if (!roadTile.isPainted)
         {
            float delay = i * (stepDuration / 2f);
            Paint(roadTile, duration, delay);
         }
      }

      CheckCompletedRoad();
   }

    private void CheckCompletedRoad()
    {
        if (paintedRoadTiles == levelManager.roadTilesList.Count)
        {
            Debug.Log("Level Completed");
        }
    }

    private void Paint(RoadTile roadTile, float duration, float delay)
    {
        roadTile.Paint(levelManager.paintColor, duration, delay);
        paintedRoadTiles++;
    }
}
