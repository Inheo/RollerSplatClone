using UnityEngine;
using DG.Tweening;

public class RoadTile : MonoBehaviour
{
   [SerializeField] private MeshRenderer _meshRenderer;

   public Vector3 position { get; private set; }
   public bool isPainted { get; private set; }

   private void Awake()
   {
      position = transform.position;
      isPainted = false;
   }

   public void Paint(Color color, float duration, float delay) 
   {
      _meshRenderer.material
         .DOColor(color, duration)
         .SetDelay(delay);

      isPainted = true;
   }
}
