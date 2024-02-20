using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.CutsceneSystem
{
[CreateAssetMenu(fileName = "CutSceneSO", menuName = "Scriptable Objects/CutSceneSO", order = 50)]
    public class CutSceneSO : ScriptableObject {
        public List<Image> imageList;
        public List<AudioClip> audioList;
    }
}
