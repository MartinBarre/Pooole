using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static event Action OnPausePressed;
    public static event Action OnJumpPressed;
    public static event Action OnJumpReleased;

    private bool canMove = true;
    private bool canPause = true;

    private void Awake()
    {
        GameManager.OnGameOver += () => canMove = false;
        EndLevel.OnLevelFinished += () => StartCoroutine(OnLevelFinished());
    }

    public static float Horizontal { get; private set; }
    public static bool Glide { get; private set; }
    
    public void OnMove(InputValue value)
    {
        if(!canMove) return;
        Horizontal = value.Get<float>();
    }
    
    public void OnJump(InputValue value)
    {
        if(!canMove) return;
        if (value.isPressed)
        {
            OnJumpPressed?.Invoke();
        }
        else
        {
            OnJumpReleased?.Invoke();
        }
    }
    
    public void OnGlide(InputValue value)
    {
        if(!canMove) return;
        Glide = value.Get<float>() > 0.9f;
    }
    
    public void OnPause(InputValue value)
    {
        if (!canPause) return;
        if (value.isPressed)
        {
            OnPausePressed?.Invoke();
        }
    }

    private IEnumerator OnLevelFinished()
    {
        canMove = false;
        canPause = false;
        Horizontal = 1;
        
        yield return new WaitForSeconds(3f);
        
        canMove = false;
        Horizontal = 0;
    }
}