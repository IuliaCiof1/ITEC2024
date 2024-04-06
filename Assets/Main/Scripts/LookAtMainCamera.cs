using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Scripts
{
    public class LookAtMainCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        [SerializeField] private PlayerController.PlayerNr playerNr;

        void Start()
        {
            // Find the main camera in the scene
            _mainCamera = Camera.main;

            // Ensure the main camera exists
            if (_mainCamera == null)
            {
                Debug.LogError("Main camera not found in the scene!");
                return;
            }
        }

        void Update()
        {
            if (playerNr == PlayerController.PlayerNr.Player1)
            {
                Vector3 directionToCamera = transform.position - _mainCamera.transform.position;

                // Calculate the rotation to look in the opposite direction
                Quaternion lookRotation = Quaternion.LookRotation(-directionToCamera);

                // Apply the rotation to the canvas
                transform.rotation = lookRotation;
            }
            else
            {
                transform.LookAt(_mainCamera.transform);
            }
        }
    }
}