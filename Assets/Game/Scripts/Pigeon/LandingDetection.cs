using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using PigeonProject.Inputs;
using PigeonProject.LandingArea;
using UnityEngine;
using UnityEngine.Splines;

namespace PigeonProject.Pigeon
{
    public class LandingDetection : MonoBehaviour
    {
        [SerializeField, Range(1, 10)] private int maxColliders;
        [SerializeField] private float detectionRadius;
        [SerializeField] private float maxAngle;
        [SerializeField] private LayerMask landingPointMask;

        private PigeonAnimation anim;
        private SplineAnimate splineAnim;
        private Transform nearestLandingPoint = null;
        public Transform NearestLandingPoint { get => nearestLandingPoint; }

        public bool IsGrounded { get; set; }

        private void Awake()
        {
            anim = GetComponentInChildren<PigeonAnimation>();
            splineAnim = GetComponent<SplineAnimate>();
        }

        private void Start()
        {
            GameInput.Singleton.OnInteract += StartLanding;
        }

        private void Update()
        {
            Collider[] points = new Collider[maxColliders];
            int numberOfPoints = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, points, landingPointMask);
            var orderedPoints = points.Where(p => p != null && IsInFront(p)).OrderBy(p => (transform.position - p.transform.position).sqrMagnitude).ToArray();

            nearestLandingPoint = orderedPoints.Length > 0 ? orderedPoints[0].transform : null;

            AdjustSpline();
        }

        private bool IsInFront(Collider target)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            float angle = Vector3.Angle(Camera.main.transform.forward, targetDirection);
            return Mathf.Abs(angle) <= maxAngle;
        }

        private void AdjustSpline()
        {
            if (!nearestLandingPoint)
            {
                splineAnim.Container = null;
                return;
            }

            Vector3 directionFromPlayer = nearestLandingPoint.position - transform.position;
            Transform spline = nearestLandingPoint.GetComponent<LandingPoint>().Spline;
            spline.eulerAngles = new Vector3(0, Mathf.Atan2(directionFromPlayer.x, directionFromPlayer.z) * Mathf.Rad2Deg, 0);
            SetSplineAnimate(spline);
        }

        private void SetSplineAnimate(Transform spline)
        {
            SplineContainer container = spline.GetComponent<SplineContainer>();
            if (splineAnim.Container == container)
                return;

            splineAnim.Container = container;
        }

        private void StartLanding()
        {
            if (!nearestLandingPoint)
                return;

            Sequence sequence = DOTween.Sequence();
            Vector3 pos = splineAnim.Container.transform.TransformPoint(splineAnim.Container.Spline.ToArray()[0].Position);
            sequence.Append(transform.DOLookAt(pos, .3f, AxisConstraint.Y, Vector3.up));
            sequence.Append(transform.DOMove(pos, 1f).SetEase(Ease.Linear));
            sequence.AppendCallback(GoToLandingPoint);

            GetComponent<Flight>().CanMove = false;
            anim.StartMoving();
            sequence.Play();
        }

        private void GoToLandingPoint()
        {
            anim.StartLanding();
            splineAnim.Restart(true);
            StartCoroutine(LandingFinished());
        }

        private IEnumerator LandingFinished()
        {
            while (splineAnim.NormalizedTime < 1)
            {
                yield return null;
            }

            anim.IdleDuration = 0;
            anim.OnGrounded();
            IsGrounded = true;
        }
    }
}
