using TMPro;
using UnityEngine;

namespace UI
{
    public class LifeBarUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text countTxt;

        private void Awake()
        {
            GameManager.OnLivesChanged += UpdateUI;
        }

        private void UpdateUI(int nbLives)
        {
            countTxt.SetText(nbLives.ToString());
        }
    }
}