using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace PigeonProject.Scene
{
    public class BootLoader : MonoBehaviour
    {
        public static BootLoader Loader;
        public SceneReference MainScene;
        [SerializeField] List<SceneReference> sceneObjectList;

        void Awake()
        {
            Loader = this;
            SceneManager.LoadSceneAsync(MainScene, LoadSceneMode.Additive);

            foreach (var scene in sceneObjectList)
            {
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
        }
    }

}

