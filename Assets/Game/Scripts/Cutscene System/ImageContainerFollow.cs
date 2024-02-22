using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigeonProject
{
    public class ImageContainerFollow : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;


        void Update()
        {
            this.transform.position = new Vector3(followTarget.position.x, this.transform.position.y , this.transform.position.z);
        }
    }
}
