using System.Collections;
using System.Collections.Generic;
using PigeonProject.Loading;
using UnityEngine;

namespace PigeonProject
{
    public class EndCredit : MonoBehaviour
    {
        [SerializeField] float _endTimer = 108f;
        IEnumerator Start(){
            yield return new WaitForSeconds(_endTimer);
            LoadingManager.Instance.LoadScene(0);
        }
    }
}
