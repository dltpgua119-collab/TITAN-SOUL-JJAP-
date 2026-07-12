using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMove : MonoBehaviour
{
    private const float DefaultSpeed = 5f;
    private const float DefaultDiagonalReleaseGraceTime = 0.1f;

    [Min(0f)]
    [SerializeField] private float speed = DefaultSpeed;
    [Min(0f)]
    [SerializeField] private float diagonalReleaseGraceTime = DefaultDiagonalReleaseGraceTime;
    [Header("Movement Animation")]
    [SerializeField] private AnimationClip walkLeftAnimation;
    [Header("Direction Sprites")]
    [SerializeField] private Sprite idleDown;
    [SerializeField] private Sprite idleUp;
    [SerializeField] private Sprite idleLeft;
    [SerializeField] private Sprite idleRight;
    [SerializeField] private Sprite idleUpLeft;
    [SerializeField] private Sprite idleUpRight;
    [SerializeField] private Sprite idleDownLeft;
    [SerializeField] private Sprite idleDownRight;

    private SpriteRenderer spriteRenderer;
    private Sprite lastFacingSprite;
    private bool isKeepingDiagonalFacing;
    private float cardinalInputStartTime = -1f;
    private bool isPlayingLeftWalk;
    private float leftWalkAnimationTime;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        lastFacingSprite = idleDown != null ? idleDown : spriteRenderer.sprite;
    }

    private void Update()
    {
        Vector2 moveInput = ReadMoveInput();
        transform.position += (Vector3)(moveInput * speed * Time.deltaTime);
        UpdateFacingSprite(moveInput);
        UpdateLeftWalkState(moveInput);
    }

    private void LateUpdate()
    {
        if (isPlayingLeftWalk && walkLeftAnimation != null)
        {
            walkLeftAnimation.SampleAnimation(gameObject, leftWalkAnimationTime);
            leftWalkAnimationTime = (leftWalkAnimationTime + Time.deltaTime) % walkLeftAnimation.length;
            return;
        }

        if (lastFacingSprite != null)
        {
            spriteRenderer.sprite = lastFacingSprite;
        }
    }

    private void UpdateLeftWalkState(Vector2 moveInput)
    {
        bool shouldPlayLeftWalk = moveInput.x < 0f && Mathf.Approximately(moveInput.y, 0f);

        if (shouldPlayLeftWalk && !isPlayingLeftWalk)
        {
            leftWalkAnimationTime = 0f;
        }

        isPlayingLeftWalk = shouldPlayLeftWalk;
    }

    private static Vector2 ReadMoveInput()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return Vector2.zero;
        }

        Vector2 input = new(
            (keyboard.dKey.isPressed ? 1f : 0f) - (keyboard.aKey.isPressed ? 1f : 0f),
            (keyboard.wKey.isPressed ? 1f : 0f) - (keyboard.sKey.isPressed ? 1f : 0f));

        return input.normalized;
    }

    private void UpdateFacingSprite(Vector2 moveInput)
    {
        if (moveInput == Vector2.zero)
        {
            isKeepingDiagonalFacing = false;
            cardinalInputStartTime = -1f;
            return;
        }

        Sprite nextSprite = GetDiagonalSprite(moveInput);
        if (nextSprite != null)
        {
            isKeepingDiagonalFacing = true;
            cardinalInputStartTime = -1f;
            lastFacingSprite = nextSprite;
            return;
        }

        if (isKeepingDiagonalFacing)
        {
            if (cardinalInputStartTime < 0f)
            {
                cardinalInputStartTime = Time.unscaledTime;
            }

            if (Time.unscaledTime - cardinalInputStartTime < diagonalReleaseGraceTime)
            {
                return;
            }

            isKeepingDiagonalFacing = false;
        }

        if (nextSprite == null && Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
        {
            nextSprite = moveInput.x > 0f ? idleRight : idleLeft;
        }
        else if (nextSprite == null)
        {
            nextSprite = moveInput.y > 0f ? idleUp : idleDown;
        }

        if (nextSprite != null)
        {
            lastFacingSprite = nextSprite;
        }
    }

    private Sprite GetDiagonalSprite(Vector2 moveInput)
    {
        if (Mathf.Approximately(moveInput.x, 0f) || Mathf.Approximately(moveInput.y, 0f))
        {
            return null;
        }

        if (moveInput.y > 0f)
        {
            return moveInput.x > 0f ? idleUpRight : idleUpLeft;
        }

        return moveInput.x > 0f ? idleDownRight : idleDownLeft;
    }
}
