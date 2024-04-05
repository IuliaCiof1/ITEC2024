using System;
using UnityEngine;

namespace Main.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        private const float SmoothSpeed = 0.125f;
        private readonly float _zOffset = 5f;
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
                // Calculate the desired position for the camera
                Vector3 desiredPosition = target.transform.position;

                // Smoothly interpolate between the current position and the desired position
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);

                // Update the camera's position
                transform.position = new Vector3(desiredPosition.x, transform.position.y,
                    _middlePointCalculator.GetDistanceBetweenPlayers() * 0.5f + _zOffset);

                Debug.Log("Position is " + desiredPosition);
            }
        }
    }
}