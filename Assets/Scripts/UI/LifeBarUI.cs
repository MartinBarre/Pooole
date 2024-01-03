using TMPro;
using UnityEngine;

namespace UI
{
    public class LifeBarUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text countTxt;

        private void OnEnable()
        {
            GameManager.OnLivesChanged += UpdateUI;
        }

        private void OnDisable()
        {
            GameManager.OnLivesChanged -= UpdateUI;
        }

        private void UpdateUI(int nbLives)
        {
            countTxt.SetText(nbLives.ToString());
        }
    }
}