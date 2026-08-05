using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private ArrowController arrowController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float defaultCameraSize = 5f;
    [SerializeField] private float chargedCameraSize = 3f;
    [SerializeField] private float cameraZoomOutDuration = 0.2f;
    private float zoomOutStartTime = -1f;
    private float zoomOutStartSize = 5f;

    [Header("Camera Offset")]
    [SerializeField] private float maxCameraOffset = 2f;
    [SerializeField] private float cameraOffsetReturnDuration = 0.2f;
    [SerializeField] private float cameraOffsetShiftDuration = 0.2f;
    private float offsetReturnStartTime = -1f;
    private Vector2 currentCameraOffset = Vector2.zero;
    private Vector2 targetCameraOffset = Vector2.zero;

    [Header("Vignette")]
    [SerializeField] private Volume globalVolume;
    [SerializeField] [Range(0f, 1f)] private float maxVignetteIntensity = 0.5f;
    [SerializeField] [Range(0f, 1f)] private float maxVignetteSmoothness = 0.5f;
    private Vignette vignette;
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
    [SerializeField] private float minChargeTime = 0.3f;
    [SerializeField] private float maxChargeTime = 1.5f;
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
            arrowController.Initialize(this);

        if (mainCamera == null)
            mainCamera = Camera.main;

        if (globalVolume != null)
            globalVolume.profile.TryGet(out vignette);

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
                if (cKeyPressedTime >= 0f && Time.time - cKeyPressedTime >= minChargeTime)
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

        UpdateCameraZoom(isCharging);
        UpdateCameraOffset(isCharging);
        UpdateVignette(isCharging);
    }

    private void UpdateCameraZoom(bool isCharging)
    {
        if (mainCamera == null) return;

        if (isCharging && cKeyPressedTime >= 0f)
        {
            zoomOutStartTime = -1f;
            float heldTime = Mathf.Max(0f, Time.time - cKeyPressedTime - minChargeTime);
            float chargeRatio = Mathf.Clamp01(heldTime / (maxChargeTime - minChargeTime));
            mainCamera.orthographicSize = Mathf.Lerp(defaultCameraSize, chargedCameraSize, chargeRatio);
        }
        else
        {
            if (zoomOutStartTime < 0f)
            {
                zoomOutStartTime = Time.time;
                zoomOutStartSize = mainCamera.orthographicSize;
            }
            float t = Mathf.Clamp01((Time.time - zoomOutStartTime) / cameraZoomOutDuration);
            mainCamera.orthographicSize = Mathf.Lerp(zoomOutStartSize, defaultCameraSize, t);
        }
    }

    private void UpdateCameraOffset(bool isCharging)
    {
        if (mainCamera == null) return;

        Vector3 basePos = transform.position;
        basePos.z = mainCamera.transform.position.z;

        if (isCharging && cKeyPressedTime >= 0f)
        {
            offsetReturnStartTime = -1f;
            float heldTime = Mathf.Max(0f, Time.time - cKeyPressedTime - minChargeTime);
            float chargeRatio = Mathf.Clamp01(heldTime / (maxChargeTime - minChargeTime));
            targetCameraOffset = lastFacingDirection.normalized * (maxCameraOffset * chargeRatio);
            float speed = cameraOffsetShiftDuration > 0f ? 1f / cameraOffsetShiftDuration : 100f;
            currentCameraOffset = Vector2.MoveTowards(currentCameraOffset, targetCameraOffset, speed * maxCameraOffset * Time.deltaTime);
        }
        else
        {
            targetCameraOffset = Vector2.zero;
            float speed = cameraOffsetReturnDuration > 0f ? 1f / cameraOffsetReturnDuration : 100f;
            currentCameraOffset = Vector2.MoveTowards(currentCameraOffset, Vector2.zero, speed * maxCameraOffset * Time.deltaTime);
        }

        mainCamera.transform.position = basePos + new Vector3(currentCameraOffset.x, currentCameraOffset.y, 0f);
    }

    private void UpdateVignette(bool isCharging)
    {
        if (vignette == null) return;

        float chargeRatio = 0f;
        if (isCharging && cKeyPressedTime >= 0f)
        {
            float heldTime = Mathf.Max(0f, Time.time - cKeyPressedTime - minChargeTime);
            chargeRatio = Mathf.Clamp01(heldTime / (maxChargeTime - minChargeTime));
        }

        vignette.intensity.value = Mathf.Lerp(0f, maxVignetteIntensity, chargeRatio);
        vignette.smoothness.value = Mathf.Lerp(0f, maxVignetteSmoothness, chargeRatio);
    }

    private void FireArrow()
    {
        if (arrowController == null || !arrowController.CanFire) return;

        float heldTime = Mathf.Max(0f, Time.time - cKeyPressedTime - minChargeTime);
        float chargeRatio = Mathf.Clamp01(heldTime / (maxChargeTime - minChargeTime));
        arrowController.Fire(lastFacingDirection, chargeRatio);
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
