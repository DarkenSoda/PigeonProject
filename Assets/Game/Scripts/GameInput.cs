using System;
using UnityEngine;

namespace PigeonProject.Inputs
{
    public class GameInput : MonoBehaviour
    {
        public static GameInput Singleton { get; private set; }

        private PlayerInputAction playerInput;

        public Action OnCutsceneSkip;
        public bool IsMoving { get; private set; }
        public bool IsLanding { get; private set; }
        public bool IsElevating { get; private set; }
        private void Awake()
        {
            if (Singleton != null && Singleton != this)
            {
                Destroy(this);
            }
            else
            {
                Singleton = this;
            }

            playerInput = new();
            playerInput.Enable();
        }

        private void Start()
        {
            playerInput.Flight.Move.started += _ => IsMoving = true;
            playerInput.Flight.Move.canceled += _ => IsMoving = false;
            playerInput.Flight.Land.started += _ => IsLanding = true;
            playerInput.Flight.Land.canceled += _ => IsLanding = false;
            playerInput.Flight.Elevate.started += _ => IsElevating = true;
            playerInput.Flight.Elevate.canceled += _ => IsElevating = false;
            playerInput.CutScene.SkipCutScene.started += _ => OnCutsceneSkip?.Invoke();
        }

        public Vector2 GetDirection()
        {
            return playerInput.Flight.Rotate.ReadValue<Vector2>();
        }

        private void OnDestroy()
        {
            playerInput.Dispose();
        }
    }
}
