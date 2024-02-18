using System;
using PigeonProject.Inputs;
using UnityEngine;

namespace PigeonProject.Pigeon
{
    public class Flight : MonoBehaviour
    {
        [SerializeField, Min(0)] private Vector2 heightRange;
        [SerializeField] private float flightSpeed;
        [SerializeField] private float speedChangeTime;
        [SerializeField] private float elevationSpeed;
        [SerializeField] private float rotationTime;

        private float targetRotation;
        private Vector2 inputVector;
        private Vector3 currentDirection;
        private Vector3 moveDir;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            currentDirection = transform.forward;
        }

        private void Update()
        {
            inputVector = GameInput.Singleton.GetDirection();

            HandleRotation();
            if (GameInput.Singleton.IsMoving)
            {
                FlyInDirection();
            }
            else
            {
                StopFlying();
            }
        }

        private void HandleRotation()
        {
            // Rotate according to currentRotation
            if (rb.velocity == Vector3.zero)
                return;

            if (inputVector == Vector2.zero)
                return;

            targetRotation = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            moveDir = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;
            currentDirection = Vector3.Slerp(currentDirection, moveDir, rotationTime * Time.deltaTime);
        }

        private void FlyInDirection()
        {
            rb.velocity = new Vector3(currentDirection.x, 0, currentDirection.z) * flightSpeed;
        }

        private void StopFlying()
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, speedChangeTime * Time.deltaTime);
        }
    }
}
