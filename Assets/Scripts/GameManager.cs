using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<int> OnEggsChanged;
    public static event Action<int> OnLivesChanged;
    public static event Action<int> OnHeartsChanged;
    public static event Action OnGameOver;
    public static event Action OnPickFeather;
    
    [Header("CAMERA")]
    public bool cameraFollowPlayer;
    
    [Header("PLAYER")]
    public GameObject player;
    public Transform playerSpawn;
    public int lives;
    public int hearts;

    [Header("PLAYER STATS")]
    private int eggs;
    
    [Header("SOUNDS")]
    [SerializeField] private AudioClip soundTakeEgg;

    [SerializeField] private SceneEnum nextLevel;

    public List<GameObject> pickedEggs;
    public List<GameObject> pickedFeathers;

    private Collider2D[] _colliders;
    private void Awake()
    {
        Instance = this;
        CheckpointReach.OnCheckpointReach += OnCheckpointReach;
        EndLevel.OnLevelFinished += OnLevelFinished;
        EndLevelMenuUI.OnRestartLevel += RestartLevel;
        EndLevelMenuUI.GoToNextLevel += LoadNextLevel;
    }

    private void OnDestroy()
    {
        CheckpointReach.OnCheckpointReach -= OnCheckpointReach;
        EndLevel.OnLevelFinished -= OnLevelFinished;
        EndLevelMenuUI.OnRestartLevel -= RestartLevel;
        EndLevelMenuUI.GoToNextLevel -= LoadNextLevel;
    }

    private void OnLevelFinished()
    {
        cameraFollowPlayer = false;
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene((int)nextLevel);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void Start()
    {
        OnEggsChanged?.Invoke(eggs);
        OnLivesChanged?.Invoke(lives);
        OnHeartsChanged?.Invoke(hearts);
        player.transform.position = playerSpawn.position;
        
        _colliders = player.GetComponentsInChildren<Collider2D>();
    }

    public void PickFeather(GameObject feather)
    {
        feather.SetActive(false);
        pickedFeathers.Add(feather);
        OnPickFeather?.Invoke();
    }

    public void ResetFeathers()
    {
        foreach (var feather in pickedFeathers)
        {
            feather.SetActive(true);
        }
        pickedFeathers.Clear();
    }

    public void TakeDamage()
    {
        hearts = Mathf.Max(0, --hearts);
        OnHeartsChanged?.Invoke(hearts);
        if (hearts <= 0)
        {
            StartCoroutine(LifeLost());
        }
    }

    public IEnumerator LifeLost()
    {
        cameraFollowPlayer = false;

        foreach (var col in _colliders)
        {
            col.enabled = false;
        }

        yield return new WaitForSeconds(2);
        
        lives -= 1;
        OnLivesChanged?.Invoke(lives);
        
        if (lives == 0)
        {
            OnGameOver?.Invoke();
        }
        else
        {
            cameraFollowPlayer = true;
            
            foreach (var col in _colliders)
            {
                col.enabled = true;
            }
        
            hearts = 3;
            OnHeartsChanged?.Invoke(hearts);
        
            ReturnToCheckpoint();
        }
    }

    public int GetEggs()
    {
        return eggs;
    }
    
    public void PickEgg(GameObject egg)
    {
        AudioManager.Instance.PlaySound(soundTakeEgg, 0.2f);
        egg.SetActive(false);
        pickedEggs.Add(egg);
        eggs += 1;
        OnEggsChanged?.Invoke(eggs);
    }

    public void ReturnToCheckpoint()
    {
        player.transform.position = playerSpawn.position;
        eggs -= pickedEggs.Count;
        
        foreach (var egg in pickedEggs)
        {
            egg.SetActive(true);
        }

        pickedEggs.Clear();
        OnEggsChanged?.Invoke(eggs);
    }

    private void OnCheckpointReach(Vector3 position)
    {
        playerSpawn.position = position;
    }
}