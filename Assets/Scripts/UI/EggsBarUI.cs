using TMPro;
using UnityEngine;

namespace UI
{
    public class EggsBarUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text countTxt;

        private void Awake()
        {
            GameManager.OnEggsChanged += UpdateUI;
        }

        private void UpdateUI(int eggs)
        {
            countTxt.SetText(eggs.ToString());
        }
    }
}