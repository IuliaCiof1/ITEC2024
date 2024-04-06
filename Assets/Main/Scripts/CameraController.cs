using System;
using UnityEngine;

namespace Main.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        private const float SmoothSpeed = 0.125f;
        private readonly float _zOffset = 5f;
        private float playersDistanceRation = 0.3f;
        private float _heightOffset = 10f;
        private MiddlePointCalculator _middlePointCalculator;

        private void Start()
        {
            if (target != null)
            {
                _middlePointCalculator = target.GetComponent<MiddlePointCalculator>();
            }
        }

        void FixedUpdate()
        {
            if (target != null)
            {
                Vector3 desiredPosition = target.transform.position;

                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);

                transform.position = new Vector3(smoothedPosition.x, desiredPosition.y + _heightOffset,
                    _middlePointCalculator.GetDistanceBetweenPlayers() * playersDistanceRation + _zOffset);
            }
        }
    }
}