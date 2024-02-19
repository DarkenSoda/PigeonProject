using UnityEngine;

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
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minimapCamera.position.x - minimapSize, minimapCamera.position.x + minimapSize),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minimapCamera.position.z - minimapSize, minimapCamera.position.z + minimapSize)
        );

        transform.rotation = minimapCamera.rotation;
    }

}
