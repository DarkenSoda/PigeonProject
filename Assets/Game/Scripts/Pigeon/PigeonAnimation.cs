using PigeonProject.Inputs;
using UnityEngine;

namespace PigeonProject.Pigeon
{
    public class PigeonAnimation : MonoBehaviour
    {
        private const string ISMOVING = "IsMoving";
        private const string ISLANDING = "IsLanding";
        private const string ISELEVATING = "IsElevating";
        private bool IsMoving;
        private bool CanMove;
        private bool IsLanding;
        private bool IsElevating;
        private Flight flight;
        private Animator anim;

        [SerializeField] private float rotationTime;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            flight = GetComponentInParent<Flight>();
        }

        private void Update()
        {
            CanMove = flight.CanMove;
            IsMoving = GameInput.Singleton.IsMoving;
            IsLanding = GameInput.Singleton.IsLanding;
            IsElevating = GameInput.Singleton.IsElevating;

            anim.SetBool(ISMOVING, IsMoving && CanMove);

            if (CanMove)
            {
                if (IsMoving)
                {
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
            // else
            // {
            //     anim.SetBool(ISLANDING, false);
            //     anim.SetBool(ISELEVATING, false);
            //     ResetMovmentRotation();
            // }
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
    }
}
