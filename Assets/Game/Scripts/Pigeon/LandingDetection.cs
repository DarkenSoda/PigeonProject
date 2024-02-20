using System;
using System.Linq;
using PigeonProject.LandingArea;
using UnityEngine;

namespace PigeonProject.Pigeon
{
    public class LandingDetection : MonoBehaviour
    {
        [SerializeField, Range(1, 10)] private int maxColliders;
        [SerializeField] private float detectionRadius;
        [SerializeField] private float maxAngle;
        [SerializeField] private LayerMask landingPointMask;

        private Transform nearestLandingPoint = null;
        public Transform NearestLandingPoint { get => nearestLandingPoint; }

        private void Update()
        {
            Collider[] points = new Collider[maxColliders];
            int numberOfPoints = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, points, landingPointMask);
            var orderedPoints = points.Where(p => p != null && IsInFront(p)).OrderBy(p => (transform.position - p.transform.position).sqrMagnitude).ToArray();

            nearestLandingPoint = orderedPoints.Length > 0 ? orderedPoints[0].transform : null;
        }

        private bool IsInFront(Collider target)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            float angle = Vector3.Angle(Camera.main.transform.forward, targetDirection);
            return Mathf.Abs(angle) <= maxAngle;
        }
    }
}
