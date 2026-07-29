using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    private const float DefaultSpeed = 5f;

    private static readonly int MoveXHash = Animator.StringToHash("MoveX");
    private static readonly int MoveYHash = Animator.StringToHash("MoveY");
    private static readonly int LastMoveXHash = Animator.StringToHash("LastMoveX");
    private static readonly int LastMoveYHash = Animator.StringToHash("LastMoveY");
    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    private static readonly int IsChargingHash = Animator.StringToHash("IsCharging");

    [Min(0f)]
    [SerializeField] private float speed = DefaultSpeed;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // 시작할 때 왼쪽 Idle이 선택되도록 기본 방향을 지정한다.
        animator.SetFloat(LastMoveXHash, -1f);
        animator.SetFloat(LastMoveYHash, 0f);
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        Vector2 moveInput = ReadMoveInput();
        bool isCharging = keyboard.cKey.isPressed;
        Vector2 effectiveMoveInput = isCharging ? Vector2.zero : moveInput;

        transform.position += (Vector3)(effectiveMoveInput * speed * Time.deltaTime);

        bool isMoving = effectiveMoveInput.sqrMagnitude > 0f;
        animator.SetFloat(MoveXHash, moveInput.x);
        animator.SetFloat(MoveYHash, moveInput.y);
        animator.SetBool(IsMovingHash, isMoving);
        animator.SetBool(IsChargingHash, isCharging);

        if (moveInput.sqrMagnitude > 0f)
        {
            animator.SetFloat(LastMoveXHash, moveInput.x);
            animator.SetFloat(LastMoveYHash, moveInput.y);
        }
        else if (Mathf.Abs(animator.GetFloat(LastMoveXHash)) < 0.1f && Mathf.Abs(animator.GetFloat(LastMoveYHash)) < 0.1f)
        {
            animator.SetFloat(LastMoveXHash, -1f);
            animator.SetFloat(LastMoveYHash, 0f);
        }
    }

    private static Vector2 ReadMoveInput()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return Vector2.zero;
        }

        Vector2 input = new(
            (keyboard.rightArrowKey.isPressed ? 1f : 0f) - (keyboard.leftArrowKey.isPressed ? 1f : 0f),
            (keyboard.upArrowKey.isPressed ? 1f : 0f) - (keyboard.downArrowKey.isPressed ? 1f : 0f));

        return input.normalized;
    }
}
