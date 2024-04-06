using System.Collections;
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

        // Camera shake variables
        private bool isShaking = false;
        private float shakeMagnitude = 0.1f; // Initial magnitude
        private float shakeDuration = 0.5f;
        private Vector3 originalPosition;
        private Vector3 shakeOffset = Vector3.zero; // Stores the shake offset

        private void Start()
        {
            if (target != null)
            {
                _middlePointCalculator = target.GetComponent<MiddlePointCalculator>();
            }
            originalPosition = transform.position;
        }

        void FixedUpdate()
        {
            if (target != null)
            {
                Vector3 desiredPosition = target.transform.position + shakeOffset; // Apply shake offset

                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);

                transform.position = new Vector3(smoothedPosition.x, desiredPosition.y + _heightOffset,
                    _middlePointCalculator.GetDistanceBetweenPlayers() * playersDistanceRation + _zOffset);
            }
        }

        // Method to trigger camera shake effect
        public void ShakeCamera(float magnitude)
        {
            if (!isShaking)
            {
                shakeMagnitude = magnitude; // Update shake magnitude
                StartCoroutine(DoShake());
            }
        }

        // Coroutine to handle the shake effect
        private IEnumerator DoShake()
        {
            isShaking = true;
            float endTime = Time.time + shakeDuration;

            while (Time.time < endTime)
            {
                // Generate a new shake offset every frame
                shakeOffset = Random.insideUnitSphere * shakeMagnitude;
                shakeOffset.z = 0; // Ensure shake doesn't affect the z position
                yield return null;
            }

            // Reset shake offset
            shakeOffset = Vector3.zero;
            isShaking = false;
        }
    }
}
