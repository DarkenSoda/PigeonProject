using System;
using PigeonProject.Inputs;
using UnityEngine;

namespace PigeonProject.Pigeon
{
    public class Flight : MonoBehaviour
    {
        public bool CanMove = true;

        [Header("Flight")]
        [SerializeField, Min(0)] private float flightSpeed;
        [SerializeField, Min(0)] private float flightSpeedChangeTime;
        [SerializeField, Min(0)] private float rotationTime;

        [Header("Elevation")]
        [SerializeField, Min(0)] private float elevationSpeed;
        [SerializeField, Min(0)] private float elevationSpeedChangeTime;
        [SerializeField, Min(0)] private Vector2 heightRange;

        private float currentFlightSpeed = 0f;
        private float currentElevationSpeed = 0f;
        private float targetRotation;

        private Vector2 inputVector;
        public Vector2 InputVector { get => inputVector; }
        public Vector2 HeightRange { get => heightRange; }

        private CharacterController controller;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            inputVector = GameInput.Singleton.GetDirection();

            if (CanMove)
            {
                HandleSpeed();
                HandleRotation();
                FlyInDirection();
                HandleElevation();
            }
            else
            {
                currentFlightSpeed = 0;
                currentElevationSpeed = 0;
            }

            if (transform.position.y >= heightRange.y)
            {
                transform.position = new Vector3(transform.position.x, heightRange.y, transform.position.z);
            }

            if (transform.position.y <= heightRange.x)
            {
                transform.position = new Vector3(transform.position.x, heightRange.x, transform.position.z);
            }
        }

        private void FixedUpdate()
        {
            // FlyInDirection();
            // HandleElevation();
        }

        private void HandleSpeed()
        {
            UpdateFlightSpeed();
            UpdateElevationSpeed();
        }

        private void UpdateFlightSpeed()
        {
            currentFlightSpeed += (GameInput.Singleton.IsMoving ? 1 : -1) * flightSpeedChangeTime * Time.deltaTime;
            currentFlightSpeed = Mathf.Clamp(currentFlightSpeed, 0, 1);
        }

        private void UpdateElevationSpeed()
        {
            if (GameInput.Singleton.IsElevating || GameInput.Singleton.IsLanding)
            {
                currentElevationSpeed += (GameInput.Singleton.IsElevating ? 1 : -1) * elevationSpeedChangeTime * Time.deltaTime;
                currentElevationSpeed = Mathf.Clamp(currentElevationSpeed, -1, 1);
                return;
            }

            if (currentElevationSpeed > 0)
            {
                currentElevationSpeed -= elevationSpeedChangeTime * Time.deltaTime;
                currentElevationSpeed = Mathf.Clamp(currentElevationSpeed, 0, 1);
            }
            else if (currentElevationSpeed < 0)
            {
                currentElevationSpeed += elevationSpeedChangeTime * Time.deltaTime;
                currentElevationSpeed = Mathf.Clamp(currentElevationSpeed, -1, 0);
            }
        }

        private void HandleRotation()
        {
            if (currentFlightSpeed <= 0 || inputVector == Vector2.zero)
                return;

            targetRotation = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float currentRotation = 0;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref currentRotation, rotationTime);
        }

        private void FlyInDirection()
        {
            Vector3 flyDir = transform.forward * currentFlightSpeed * flightSpeed;
            controller.Move(new Vector3(flyDir.x, 0, flyDir.z) * Time.deltaTime);
        }

        private void HandleElevation()
        {
            float elevationForce = currentElevationSpeed * elevationSpeed;
            controller.Move(new Vector3(0, elevationForce, 0) * Time.deltaTime);
        }
    }
}
