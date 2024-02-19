using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Audio;
using TMPro;

namespace PigeonProject.UI.Settings
{
    public class SettingsManager : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] TMP_Dropdown _graphicsDropdown;
        [SerializeField] AudioMixer _mainAudioMixer;
        [SerializeField] Slider _masterVolume;
        [SerializeField] Slider _musicVolume;
        [SerializeField] Slider _sfxVolume;

        public void ChangeGraphicsQuality(){
            QualitySettings.SetQualityLevel(_graphicsDropdown.value);
        }

        public void ChangeMasterVolume(){
            _mainAudioMixer.SetFloat("MasterVolume" , _masterVolume.value);
        }
        public void ChangeMusicVolume(){
            _mainAudioMixer.SetFloat("MusicVolume" , _musicVolume.value);
        }
        public void ChangeSFXVolume(){
            _mainAudioMixer.SetFloat("SFXVolume" , _sfxVolume.value);
        }
    }
}
