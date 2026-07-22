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
        Vector2 moveInput = ReadMoveInput();
        transform.position += (Vector3)(moveInput * speed * Time.deltaTime);

        bool isMoving = moveInput.sqrMagnitude > 0f;
        animator.SetFloat(MoveXHash, moveInput.x);
        animator.SetFloat(MoveYHash, moveInput.y);
        animator.SetBool(IsMovingHash, isMoving);

        if (isMoving)
        {
            animator.SetFloat(LastMoveXHash, moveInput.x);
            animator.SetFloat(LastMoveYHash, moveInput.y);
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
            (keyboard.dKey.isPressed ? 1f : 0f) - (keyboard.aKey.isPressed ? 1f : 0f),
            (keyboard.wKey.isPressed ? 1f : 0f) - (keyboard.sKey.isPressed ? 1f : 0f));

        return input.normalized;
    }
}
