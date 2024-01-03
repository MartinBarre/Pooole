using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class EndLevel : MonoBehaviour
{
    public static event Action OnLevelFinished;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.CHICKEN))
        {
            var old = PlayerPrefs.GetInt("egg" + SceneManager.GetActiveScene().name, -1);
            var now = GameManager.Instance.GetEggs();
            PlayerPrefs.SetInt("egg" + SceneManager.GetActiveScene().name, Mathf.Max(old, now));
            PlayerPrefs.SetInt("totalEgg" + SceneManager.GetActiveScene().name, GameManager.Instance.TotalEggs);
            PlayerPrefs.SetInt("levelFinished" + SceneManager.GetActiveScene().name, 1);
            OnLevelFinished?.Invoke();
        }
    }
}