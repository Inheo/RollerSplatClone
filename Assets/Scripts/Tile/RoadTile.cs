using UnityEngine;
using DG.Tweening;

public class RoadTile : MonoBehaviour
{
   [SerializeField] private MeshRenderer _meshRenderer;

   public Vector3 Position { get; private set; }
   public bool IsPainted { get; private set; }

   private void Awake()
   {
      Position = transform.position;
      IsPainted = false;
   }

   public void Paint(Color color, float duration, float delay) 
   {
      _meshRenderer.material
         .DOColor(color, duration)
         .SetDelay(delay);

      IsPainted = true;
   }
}
