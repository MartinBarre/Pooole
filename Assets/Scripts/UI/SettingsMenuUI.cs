using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsMenuUI : MonoBehaviour
    {
        public Slider sliderMusic;
        public Slider sliderEffects;
        public Slider sliderUI;

        private void Start()
        {
            sliderMusic.value = AudioManager.Instance.GetVolumeMusic();
            sliderEffects.value = AudioManager.Instance.GetVolumeEffects();
            sliderUI.value = AudioManager.Instance.GetVolumeUI();
        }
        
        public void SetVolumeMusic(float volume)
        {
            AudioManager.Instance.SetVolumeMusic(volume);
        }

        public void SetVolumeEffects(float volume)
        {
            AudioManager.Instance.SetVolumeEffects(volume);
        }

        public void SetVolumeUI(float volume)
        {
            AudioManager.Instance.SetVolumeUI(volume);
        }
    }
}