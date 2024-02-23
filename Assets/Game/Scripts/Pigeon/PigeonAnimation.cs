using System.Collections;
using PigeonProject.Inputs;
using UnityEngine;

namespace PigeonProject.Pigeon
{
    public class PigeonAnimation : MonoBehaviour
    {
        private string[] GroundedAnimations;
        private const string GROUND1 = "Ground1";
        private const string GROUND2 = "Ground2";
        private const string CANMOVE = "CanMove";
        private const string ISMOVING = "IsMoving";
        private const string ISLANDING = "IsLanding";
        private const string ISELEVATING = "IsElevating";
        private const string FLYIDLE = "FlyIdle";
        private bool IsIdle;
        private bool IsMoving;
        private bool FlyIdle;
        private bool CanMove;
        private bool IsLanding;
        private bool IsElevating;
        private Flight flight;
        private Animator anim;

        [SerializeField] private float rotationTime;
        [SerializeField] private float idleDurationMax;
        [SerializeField] private float flyIdleDurationMax;
        [SerializeField] private GameObject leftTrail;
        [SerializeField] private GameObject rightTrail;
        private float idleDuration;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            flight = GetComponentInParent<Flight>();
            GroundedAnimations = new string[2] { GROUND1, GROUND2 };
            idleDuration = 0;

            DisableTrail();
            StartCoroutine(FlyIdleCoroutine());
        }

        private IEnumerator FlyIdleCoroutine()
        {
            while (true)
            {
                FlyIdle = true;
                anim.SetBool(FLYIDLE, true);
                yield return new WaitForSeconds(flyIdleDurationMax);
                FlyIdle = false;
                anim.SetBool(FLYIDLE, false);
                yield return new WaitForSeconds(flyIdleDurationMax * 1.3f);
            }
        }

        private void Update()
        {
            if (idleDuration > 0)
            {
                idleDuration -= Time.deltaTime;
            }

            CanMove = flight.CanMove;
            IsMoving = GameInput.Singleton.IsMoving;
            IsLanding = GameInput.Singleton.IsLanding;
            IsElevating = GameInput.Singleton.IsElevating;

            anim.SetBool(CANMOVE, CanMove);
            anim.SetBool(ISMOVING, IsMoving && CanMove);

            if (IsIdle && idleDuration <= 0)
            {
                idleDuration = idleDurationMax;
                int num = Random.Range(0, 2);
                anim.CrossFade(GroundedAnimations[num], 0.15f, 0, 0);
            }

            if (CanMove)
            {
                if (IsMoving)
                {
                    if (FlyIdle)
                    {
                        EnableTrail();
                    }
                    else
                    {
                        DisableTrail();
                    }
                    anim.SetBool(ISLANDING, false);
                    anim.SetBool(ISELEVATING, false);
                    HandleElevationRotation();
                    HandleMovementRotation();
                }
                else
                {
                    anim.SetBool(ISLANDING, IsLanding);
                    anim.SetBool(ISELEVATING, IsElevating);
                    ResetMovmentRotation();
                    DisableTrail();

                    if (IsElevating || IsLanding)
                    {
                        float rot = transform.eulerAngles.x;
                        rot = Mathf.LerpAngle(rot, 35, rotationTime);
                        Vector3 rotation = new Vector3(rot, transform.eulerAngles.y, transform.eulerAngles.z);
                        transform.eulerAngles = rotation;
                    }
                    else
                    {
                        ResetElevationRotations();
                    }
                }
            }
            else
            {
                DisableTrail();
            }
        }

        private void HandleElevationRotation()
        {
            if (IsElevating && flight.transform.position.y < flight.HeightRange.y)
            {
                float rot = transform.eulerAngles.x;
                rot = Mathf.LerpAngle(rot, -35, rotationTime);
                Vector3 rotation = new Vector3(rot, transform.eulerAngles.y, transform.eulerAngles.z);
                transform.eulerAngles = rotation;
            }
            else if (IsLanding && flight.transform.position.y > flight.HeightRange.x)
            {
                float rot = transform.eulerAngles.x;
                rot = Mathf.LerpAngle(rot, 35, rotationTime);
                Vector3 rotation = new Vector3(rot, transform.eulerAngles.y, transform.eulerAngles.z);
                transform.eulerAngles = rotation;
            }
            else
            {
                ResetElevationRotations();
            }
        }

        private void HandleMovementRotation()
        {
            if (flight.InputVector.x > 0)
            {
                float rot = transform.eulerAngles.z;
                rot = Mathf.LerpAngle(rot, -45, rotationTime);
                Vector3 rotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rot);
                transform.eulerAngles = rotation;
            }
            else if (flight.InputVector.x < 0)
            {
                float rot = transform.eulerAngles.z;
                rot = Mathf.LerpAngle(rot, 45, rotationTime);
                Vector3 rotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rot);
                transform.eulerAngles = rotation;
            }
            else
            {
                ResetMovmentRotation();
            }
        }

        private void ResetMovmentRotation()
        {
            float rot = transform.eulerAngles.z;
            rot = Mathf.LerpAngle(rot, 0, rotationTime);
            Vector3 rotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rot);
            transform.eulerAngles = rotation;
        }

        private void ResetElevationRotations()
        {
            float rot = transform.eulerAngles.x;
            rot = Mathf.LerpAngle(rot, 0, rotationTime);
            Vector3 rotation = new Vector3(rot, transform.eulerAngles.y, transform.eulerAngles.z);
            transform.eulerAngles = rotation;
        }

        public void OnGrounded()
        {
            IsIdle = true;
        }

        public void OnTakeOff()
        {
            IsIdle = false;
        }

        private void EnableTrail()
        {
            leftTrail.SetActive(true);
            rightTrail.SetActive(true);
        }

        private void DisableTrail()
        {
            leftTrail.SetActive(false);
            rightTrail.SetActive(false);
        }
    }
}
