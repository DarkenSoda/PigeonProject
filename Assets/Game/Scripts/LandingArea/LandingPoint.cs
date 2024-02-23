using UnityEngine;

namespace PigeonProject.LandingArea
{
    public class LandingPoint : MonoBehaviour
    {
        [SerializeField] private Transform landingPosition;
        [SerializeField] private Transform spline;
        [SerializeField] private Transform landingPointUI;
        public Transform LandingPosition { get => landingPosition ?? transform; }
        public Transform Spline { get => spline; }
        public Transform landingPointUIPosition { get => landingPointUI; }
    }
}
