using System;
using Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public static event Action OnLevelFinished;
    
    public GameObject eggList;

    private int _totalEggs;
    
    private void Start()
    {
        _totalEggs = eggList.transform.childCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.CHICKEN))
        {
            var old = PlayerPrefs.GetInt("egg" + SceneManager.GetActiveScene().name, -1);
            var now = GameManager.Instance.GetEggs();
            PlayerPrefs.SetInt("egg" + SceneManager.GetActiveScene().name, Mathf.Max(old, now));
            PlayerPrefs.SetInt("totalEgg" + SceneManager.GetActiveScene().name, _totalEggs);
            PlayerPrefs.SetInt("levelFinished" + SceneManager.GetActiveScene().name, 1);
            OnLevelFinished?.Invoke();
        }
    }
}