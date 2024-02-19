using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace PigeonProject.Achievement
{
    public class AchievementManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] List<Page> Pages;

        [Header("Properties")]
        [SerializeField] int currentUnlockedAct = 0;

        void Start()
        {
            
        }
        
        public void GetCurrentUnlockedPages(){

        }
    }

    [Serializable]
    public class Page{
        public TMP_Text Act_Text;
        public TMP_Text SubActTitle_Text;
        public Image image;
        public TMP_Text Image_Text;
        public TMP_Text Description_Text;

    }
}
