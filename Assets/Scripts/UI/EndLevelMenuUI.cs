using System;
using System.Collections;
using Utils;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EndLevelMenuUI : MonoBehaviour
    {
        public static event Action GoToNextLevel;
        public static event Action OnRestartLevel;
        
        [SerializeField] private TMP_Text eggsTxt;
        [SerializeField] private GameObject messageGood;
        [SerializeField] private GameObject messageBad;
        [SerializeField] private AudioClip eggSound;

        private int _eggs;
        private int _totalEggs;

        private void Start()
        {
            messageGood.SetActive(false);
            messageBad.SetActive(false);
            SetEggsTotal(GameObject.FindGameObjectsWithTag(Tag.EGG).Length);
            StartCoroutine(OnLevelFinished());
        }

        private void SetEggs(int number)
        {
            _eggs = number;
            eggsTxt.SetText(_eggs + " / " + _totalEggs);
        }

        private void SetEggsTotal(int number)
        {
            _totalEggs = number;
            eggsTxt.SetText(_eggs + " / " + _totalEggs);
        }

        private void DrawMessage(bool win)
        {
            messageGood.SetActive(win);
            messageBad.SetActive(!win);
        }
        
        private IEnumerator OnLevelFinished()
        {
            yield return new WaitForSeconds(3f);

            for (var i = 1; i <= GameManager.Instance.GetEggs(); i++)
            {
                SetEggs(i);
                AudioManager.Instance.PlaySoundUI(eggSound);
                yield return new WaitForSeconds(0.04f);
            }

            yield return new WaitForSeconds(.5f);

            DrawMessage(GameManager.Instance.GetEggs() == _totalEggs);
        }

        public void OnClickNextLevel()
        {
            GoToNextLevel?.Invoke();
        }

        public void OnClickRestart()
        {
            OnRestartLevel?.Invoke();
        }
    }
}