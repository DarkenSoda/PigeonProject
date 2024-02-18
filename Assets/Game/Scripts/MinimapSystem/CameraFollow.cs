using UnityEngine;

namespace Game.Scripts.MinimapSystem {
    public class CameraFollow : MonoBehaviour {
        [SerializeField] private Transform target;
        [SerializeField] private float cameraOffset = 5f;
        private void Update() {
            this.transform.position = new Vector3(this.target.position.x, this.target.position.y + cameraOffset, this.target.position.z);
            this.transform.rotation = Quaternion.Euler(90f, this.target.rotation.y, 0f);
        }
    }
}
