using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Animator")]
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private const string RUN_FRONT = "IsRunFront";
    private const string RUN_BACK = "IsRunBack";
    private const string RUN_SIDE = "isRunSide";

    private enum AnimationState { Idle, Front, Back, Side }

    private Rigidbody2D rb;
    private Vector2 moveInput;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ReadInput();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ReadInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(x, y);
    }

    private void Move()
    {
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }

    private void UpdateAnimator()
    {
        AnimationState state = DetermineAnimationState();
        ApplyAnimationState(state);
        
        if (state == AnimationState.Side)
        {
            ChangeDirection(moveInput);
        }
    }

    private AnimationState DetermineAnimationState()
    {
        if (moveInput.y > 0) return AnimationState.Back;
        if (moveInput.y < 0) return AnimationState.Front;
        if (moveInput.x != 0) return AnimationState.Side;
        return AnimationState.Idle;
    }

    private void ApplyAnimationState(AnimationState state)
    {
        animator.SetBool(RUN_FRONT, state == AnimationState.Front);
        animator.SetBool(RUN_BACK, state == AnimationState.Back);
        animator.SetBool(RUN_SIDE, state == AnimationState.Side);
    }

    private void ChangeDirection(Vector2 input)
    {
        if (input.x > 0)
            spriteRenderer.flipX = true;
        else if (input.x < 0)
            spriteRenderer.flipX = false;
    }
}
