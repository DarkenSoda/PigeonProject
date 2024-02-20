using UnityEngine;

namespace Game.Scripts.MinimapSystem {
    public class StayInside : MonoBehaviour {
        [SerializeField] private Transform minimapCamera;
        [SerializeField] private float minimapSize;
        private Vector3 tempVector;
        
        private void Update() {
            tempVector = transform.parent.transform.position;
            tempVector.y = transform.position.y;
            transform.position = tempVector;
        }
        private void LateUpdate() {
            Vector3 centerPosition = minimapCamera.transform.localPosition;
            centerPosition.y -= 0.5f;
            float Distance = Vector3.Distance(transform.position, centerPosition);
            if (Distance > minimapSize)
            {
                Vector3 fromOriginToObject = transform.position - centerPosition;
                fromOriginToObject *= minimapSize / Distance;
                transform.position = centerPosition + fromOriginToObject;
            }
            transform.rotation = minimapCamera.rotation;
        }

    }
}