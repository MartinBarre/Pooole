using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action<int> OnRemainingJumpChange;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioChicken audioChicken;
    
    [Header("MOVEMENT")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField, Range(0, 1)] private float jumpCut;
    [SerializeField] private float glideSpeed;
    [SerializeField] private float glideTimer;
    [SerializeField] private bool infiniteJump;
    
    [Header("CHECKS")]
    [SerializeField] private Vector2 groundCheckOffset;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayerMask;
    
    [Header("OTHER")]
    public bool isInvincible;
    public bool isGrounded;

    private Vector2 _velocity = Vector2.zero;
    private float _currentGlideTimer;
    private int _featherRemainingJump;

    private void OnEnable()
    {
        PlayerInput.OnJumpPressed += OnJumpPressed;
        PlayerInput.OnJumpReleased += OnJumpReleased;
        GameManager.OnPickFeather += OnPickFeather;
        GameManager.OnHeartsChanged += OnHeartsChanged;
    }
    
    private void OnDisable()
    {
        PlayerInput.OnJumpPressed -= OnJumpPressed;
        PlayerInput.OnJumpReleased -= OnJumpReleased;
        GameManager.OnPickFeather -= OnPickFeather;
        GameManager.OnHeartsChanged -= OnHeartsChanged;
    }

    private void OnPickFeather()
    {
        _featherRemainingJump++;
        OnRemainingJumpChange?.Invoke(_featherRemainingJump);
    }

    private void OnDestroy()
    {
        GameManager.OnHeartsChanged -= OnHeartsChanged;
    }

    private void OnHeartsChanged(int hearts)
    {
        animator.SetFloat("Hearts", hearts / 3f);
    }

    private void Update()
    {
        AnimTriggers();
    }

    private void FixedUpdate()
    {
        CheckGround();
        Flip();
        MovePlayer();
        Glide();
    }
    
    private void CheckGround()
    {
        var position = (Vector2)transform.position + groundCheckOffset;
        isGrounded = Physics2D.OverlapBox(position, groundCheckSize, 0, groundLayerMask);
        
        if (isGrounded || infiniteJump)
        {
            _currentGlideTimer = 0;
            _featherRemainingJump = 0;
            OnRemainingJumpChange?.Invoke(_featherRemainingJump);
            GameManager.Instance.ResetFeathers();
        }
    }

    private void AnimTriggers()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("Falling", isGrounded && Mathf.Abs(rb.velocity.y) < 0);
    }

    private void Flip()
    {
        if (PlayerInput.Horizontal != 0)
        {
            transform.localScale = new Vector3(-Mathf.Sign(PlayerInput.Horizontal), 1, 1);
        }
    }

    private void MovePlayer()
    {
        var horizontal = PlayerInput.Horizontal * moveSpeed * Time.fixedDeltaTime;
        var targetVelocity = new Vector2(horizontal, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref _velocity, isGrounded ? 0.1f : 0.3f);
    }
    
    private void OnJumpPressed()
    {
        if (infiniteJump || isGrounded || _featherRemainingJump > 0)
        {
            if (!isGrounded)
            {
                _featherRemainingJump--;
                OnRemainingJumpChange?.Invoke(_featherRemainingJump);
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            SetTrigger("Jump");
        }
    }

    private void OnJumpReleased()
    {
        if (!isGrounded && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCut);
        }
    }

    private void Glide()
    {
        if (PlayerInput.Glide && _currentGlideTimer <= glideTimer && rb.velocity.y < 0)
        {
            _currentGlideTimer += Time.deltaTime;
            rb.velocity = new Vector2 (rb.velocity.x, -glideSpeed);
            
            if (_currentGlideTimer < glideTimer / 2.1f)
            {
                SetTrigger("Gliding");
            }
            else
            {
                SetTrigger("EndGlide");
            }
        }
    }

    public void Damage(Vector2 contact)
    {
        StartCoroutine(Hit(contact));
    }

    private IEnumerator Hit(Vector2 contact)
    {
        if (!isInvincible)
        {
            GameManager.Instance.TakeDamage();
            
            audioChicken.Hurt();
            isInvincible = true;
            
            var knockbackDirection = ((Vector2)transform.position - contact).normalized;
            rb.velocity = knockbackDirection * 5f;
            
            yield return new WaitForSeconds(1);
            isInvincible = false;
        }
    }

    private void SetTrigger(string triggerName)
    {
        foreach (var param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(param.name);
            }
        }
        animator.SetTrigger(triggerName);
    }

    private void OnDrawGizmosSelected()
    {
        if(!enabled) return;
        Gizmos.color = new Color(255, 0, 0, 150);
        Gizmos.DrawWireCube(transform.position + (Vector3)groundCheckOffset, groundCheckSize);
    }
}
