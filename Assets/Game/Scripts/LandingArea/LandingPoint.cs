using UnityEngine;

namespace PigeonProject.LandingArea
{
    public class LandingPoint : MonoBehaviour
    {
        [SerializeField] private Transform landingPosition;

        public Transform LandingPosition { get => landingPosition ?? transform; }
    }
}
