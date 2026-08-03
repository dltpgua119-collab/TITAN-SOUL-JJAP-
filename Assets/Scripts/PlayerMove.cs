using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private ArrowController arrowController;
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
    private float cKeyPressedTime = -1f;
    private const float MinChargeTime = 0.3f;
    private bool cKeyUsedForRecall = false;

    public Vector2 LastFacingDirection => lastFacingDirection;

    // 마지막으로 바라본 방향 (대각선 유지용)
    private Vector2 lastFacingDirection = new(-1f, 0f);
    private Vector2 lastNonZeroInput = Vector2.zero;
    private float lastDiagonalTime = 0f;
    private const float DiagonalGracePeriod = 0.12f; // 대각선 유지 유예 시간 (초)

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (arrowController != null)
        {
            arrowController.Initialize(this);
        }

        animator.SetFloat(LastMoveXHash, lastFacingDirection.x);
        animator.SetFloat(LastMoveYHash, lastFacingDirection.y);
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        Vector2 rawMoveInput = ReadRawMoveInput();
        Vector2 moveInput = rawMoveInput.normalized;
        bool arrowIsOut = arrowController != null && arrowController.IsActiveForRecall;

        // C 떼면 recall 플래그 초기화
        if (keyboard.cKey.wasReleasedThisFrame)
            cKeyUsedForRecall = false;

        bool isCharging = keyboard.cKey.isPressed && !arrowIsOut && !cKeyUsedForRecall;
        bool isRecalling = arrowIsOut && keyboard.cKey.isPressed;
        Vector2 effectiveMoveInput = (isCharging || isRecalling) ? Vector2.zero : moveInput;

        if (!arrowIsOut)
        {
            if (keyboard.cKey.wasPressedThisFrame && !cKeyUsedForRecall)
            {
                cKeyPressedTime = Time.time;
            }

            if (keyboard.cKey.wasReleasedThisFrame && !cKeyUsedForRecall)
            {
                if (cKeyPressedTime >= 0f && Time.time - cKeyPressedTime >= MinChargeTime)
                {
                    FireArrow();
                }
                cKeyPressedTime = -1f;
            }
        }
        else
        {
            cKeyPressedTime = -1f;

            if (keyboard.cKey.isPressed)
            {
                cKeyUsedForRecall = true;
                arrowController.Recall();
            }
            else if (arrowController.IsReturning && !keyboard.cKey.isPressed)
            {
                arrowController.StopRecall();
            }
        }

        transform.position += (Vector3)(effectiveMoveInput * speed * Time.deltaTime);

        bool isMoving = effectiveMoveInput.sqrMagnitude > 0f;
        animator.SetFloat(MoveXHash, moveInput.x);
        animator.SetFloat(MoveYHash, moveInput.y);
        animator.SetBool(IsMovingHash, isMoving);
        animator.SetBool(IsChargingHash, isCharging);

        if (rawMoveInput.sqrMagnitude > 0f)
        {
            bool isDiagonal = rawMoveInput.sqrMagnitude > 1.1f;

            if (isDiagonal)
            {
                // 대각선 입력이면 즉시 업데이트 + 시간 기록
                lastFacingDirection = rawMoveInput;
                lastDiagonalTime = Time.time;
            }
            else if (Time.time - lastDiagonalTime > DiagonalGracePeriod)
            {
                // 유예 시간이 지난 뒤에만 카디널로 업데이트
                lastFacingDirection = rawMoveInput;
            }

            lastNonZeroInput = rawMoveInput;
        }
        else
        {
            lastNonZeroInput = Vector2.zero;
        }

        animator.SetFloat(LastMoveXHash, lastFacingDirection.x);
        animator.SetFloat(LastMoveYHash, lastFacingDirection.y);
    }

    private void FireArrow()
    {
        if (arrowController == null)
        {
            return;
        }

        if (!arrowController.CanFire)
        {
            return;
        }

        arrowController.Fire(lastFacingDirection);
    }

    private static Vector2 ReadRawMoveInput()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return Vector2.zero;
        }

        return new Vector2(
            (keyboard.rightArrowKey.isPressed ? 1f : 0f) - (keyboard.leftArrowKey.isPressed ? 1f : 0f),
            (keyboard.upArrowKey.isPressed ? 1f : 0f) - (keyboard.downArrowKey.isPressed ? 1f : 0f));
    }
}
