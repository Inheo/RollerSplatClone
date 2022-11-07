using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

public class BallMovement : MonoBehaviour
{
    private const float MAX_RAY_DISTANCE = 100f;

    [SerializeField] private InputService _inputService;
    [SerializeField] private Level _levelManager;

    [SerializeField] private float _stepDuration = 0.1f;
    [SerializeField] private LayerMask _wallsAndRoadsLayer;

    public UnityAction<List<RoadTile>, float> onMoveStart;

    private Vector3 moveDirection;
    private bool canMove = true;

    private void Start()
    {
        transform.position = _levelManager.DefaultBallRoadTile.Position;

        _inputService.onChangedDirection += OnChangedDirection;
    }

    private void OnChangedDirection(Vector2 direction)
    {
        moveDirection = new Vector3(direction.x, 0, direction.y);
        MoveBall();
    }

    private void MoveBall()
    {
        if (canMove == true && Level.Instance.IsLevelCompleted == false)
        {
            canMove = false;
            // add raycast in the swipe direction (from the ball) :
            RaycastHit[] hits = Physics.RaycastAll(transform.position, moveDirection, MAX_RAY_DISTANCE, _wallsAndRoadsLayer.value)
                                       .OrderBy(hit => hit.distance).ToArray(); // added this line to sort tiles by distance from the ray's origin

            Vector3 targetPosition = transform.position;

            int steps = 0;

            List<RoadTile> pathRoadTiles = new List<RoadTile>();

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.isTrigger)
                {
                    pathRoadTiles.Add(hits[i].transform.GetComponent<RoadTile>());
                }
                else
                {
                    if (i == 0)
                    {
                        canMove = true;
                        return;
                    }

                    steps = i;
                    targetPosition = hits[i - 1].transform.position;
                    break;
                }
            }

            float moveDuration = _stepDuration * steps;
            transform
               .DOMove(targetPosition, moveDuration)
               .SetEase(Ease.OutExpo)
               .OnComplete(() => canMove = true);

            if (onMoveStart != null)
                onMoveStart.Invoke(pathRoadTiles, moveDuration);
        }
    }
}
