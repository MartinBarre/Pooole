using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FeathersBarUI : MonoBehaviour
    {
        private Image[] _images;

        private void Awake()
        {
            PlayerController.OnRemainingJumpChange += UpdateUI;
        }

        private void OnDisable()
        {
            PlayerController.OnRemainingJumpChange -= UpdateUI;
        }

        private void Start()
        {
            _images = GetComponentsInChildren<Image>();
        }

        private void UpdateUI(int nbFeathers)
        {
            for (var i = 0; i < _images.Length; i++)
            {
                _images[i].enabled = i < nbFeathers;
            }
        }
    }
}