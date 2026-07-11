using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMove : MonoBehaviour
{
    private const float DefaultSpeed = 5f;

    [Min(0f)]
    [SerializeField] private float speed = DefaultSpeed;
    [Header("Direction Sprites")]
    [SerializeField] private Sprite idleDown;
    [SerializeField] private Sprite idleUp;
    [SerializeField] private Sprite idleLeft;
    [SerializeField] private Sprite idleRight;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (idleDown != null)
        {
            spriteRenderer.sprite = idleDown;
        }
    }

    private void Update()
    {
        Vector2 moveInput = ReadMoveInput();
        transform.position += (Vector3)(moveInput * speed * Time.deltaTime);
        UpdateFacingSprite(moveInput);
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
            return;
        }

        Sprite nextSprite;
        if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
        {
            nextSprite = moveInput.x > 0f ? idleRight : idleLeft;
        }
        else
        {
            nextSprite = moveInput.y > 0f ? idleUp : idleDown;
        }

        if (nextSprite != null)
        {
            spriteRenderer.sprite = nextSprite;
        }
    }
}
