using UnityEngine;

namespace PigeonProject.LandingArea
{
    public class LandingPoint : MonoBehaviour
    {
        [SerializeField] private Transform landingPosition;
        [SerializeField] private Transform spline;

        public Transform LandingPosition { get => landingPosition ?? transform; }
        public Transform Spline { get => spline; }
    }
}
