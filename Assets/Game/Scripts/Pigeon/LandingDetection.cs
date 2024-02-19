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
        [SerializeField] private LayerMask landingPointMask;

        private Transform nearestLandingPoint = null;
        public Transform NearestLandingPoint { get => nearestLandingPoint; }

        private void Update()
        {
            Collider[] points = new Collider[maxColliders];
            int numberOfPoints = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, points, landingPointMask);

            var orderedPoints = points.Where(p => p != null).OrderBy(p => (transform.position - p.transform.position).sqrMagnitude).ToArray();

            nearestLandingPoint = numberOfPoints > 0 ? orderedPoints[0].transform : null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}
