using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float minSpeed = 5f;              // 최소 발사 속도 (최소 차징)
    [SerializeField] private float maxSpeed = 20f;             // 최대 발사 속도 (풀 차징)
    [SerializeField] private float flyFriction = 8f;          // 발사 마찰 (클수록 빨리 멈춤)
    [SerializeField] private float recallAcceleration = 20f;  // 회수 가속도
    [SerializeField] private float recallMaxSpeed = 20f;      // 회수 최대 속도
    [SerializeField] private float recallDeceleration = 15f;  // C 뗐을 때 감속도
    [SerializeField] private float returnDistanceThreshold = 0.1f;
    [SerializeField] private float playerTouchGraceDuration = 0.7f;

    private PlayerMove owner;
    private Vector2 direction = Vector2.right;
    private bool isFlying;
    private bool isReturning;
    private bool isDecelerating;
    private bool isStuck;
    private int wallLayerMask = -1;
    private float fireStartedAtTime = -1f;
    private float currentSpeed = 0f;       // 현재 발사 속도
    private Vector2 recallVelocity;        // 현재 회수 속도

    public bool CanFire => owner != null && !isFlying && !isReturning && !isStuck;
    public bool IsActiveForRecall => isFlying || isStuck || isReturning || isDecelerating;
    public bool IsReturning => isReturning;

    public void Initialize(PlayerMove playerOwner)
    {
        owner = playerOwner;
        wallLayerMask = LayerMask.GetMask("wall");
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (owner == null) return;

        if (isFlying)
        {
            MoveForward();
        }
        else if (isReturning || isDecelerating)
        {
            MoveTowardPlayer();
        }
    }

    public void Fire(Vector2 firingDirection, float chargeRatio = 1f)
    {
        if (!CanFire || owner == null) return;

        direction = firingDirection.sqrMagnitude > 0.001f ? firingDirection.normalized : Vector2.right;
        isFlying = true;
        isReturning = false;
        isStuck = false;
        fireStartedAtTime = Time.time;
        currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, chargeRatio);
        recallVelocity = Vector2.zero;
        transform.SetParent(null, true);
        transform.position = owner.transform.position;
        gameObject.SetActive(true);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Recall()
    {
        if (owner == null || (!isFlying && !isStuck)) return;

        isReturning = true;
        isFlying = false;
        isStuck = false;
        recallVelocity = Vector2.zero;
    }

    public void StopRecall()
    {
        if (!isReturning) return;
        isReturning = false;
        isDecelerating = true;
    }

    private void MoveForward()
    {
        // 마찰로 감속
        currentSpeed = Mathf.Max(0f, currentSpeed - flyFriction * Time.deltaTime);

        // 속도가 0이면 멈춤
        if (currentSpeed <= 0f)
        {
            isFlying = false;
            isStuck = true;
            return;
        }

        Vector2 currentPosition = transform.position;
        Vector2 nextPosition = currentPosition + direction * (currentSpeed * Time.deltaTime);

        if (wallLayerMask != 0 && Physics2D.Linecast(currentPosition, nextPosition, wallLayerMask))
        {
            isFlying = false;
            isStuck = true;
            transform.position = currentPosition;
            return;
        }

        transform.position = nextPosition;

        bool isInGracePeriod = fireStartedAtTime >= 0f && Time.time - fireStartedAtTime < playerTouchGraceDuration;
        if (!isInGracePeriod && Vector2.Distance(transform.position, owner.transform.position) < 0.2f)
        {
            ResetToPlayer();
        }
    }

    private void MoveTowardPlayer()
    {
        if (owner == null) return;

        Vector2 toPlayer = (Vector2)owner.transform.position - (Vector2)transform.position;
        if (toPlayer.sqrMagnitude <= returnDistanceThreshold * returnDistanceThreshold)
        {
            ResetToPlayer();
            return;
        }

        if (isReturning)
        {
            // C 누르는 중: 가속
            recallVelocity += toPlayer.normalized * (recallAcceleration * Time.deltaTime);
            if (recallVelocity.magnitude > recallMaxSpeed)
                recallVelocity = recallVelocity.normalized * recallMaxSpeed;
        }
        else if (isDecelerating)
        {
            // C 뗀 후: 감속
            recallVelocity = Vector2.MoveTowards(recallVelocity, Vector2.zero, recallDeceleration * Time.deltaTime);
            if (recallVelocity.sqrMagnitude < 0.01f)
            {
                isDecelerating = false;
                isStuck = true;
                return;
            }
        }

        // 벽 충돌 체크
        Vector2 currentPos = transform.position;
        Vector2 nextPos = currentPos + recallVelocity * Time.deltaTime;
        if (wallLayerMask != 0 && Physics2D.Linecast(currentPos, nextPos, wallLayerMask))
        {
            recallVelocity = Vector2.zero;
            isReturning = false;
            isDecelerating = false;
            isStuck = true;
            return;
        }

        transform.position += (Vector3)(recallVelocity * Time.deltaTime);
    }

    private void ResetToPlayer()
    {
        if (owner == null) return;

        isFlying = false;
        isReturning = false;
        isDecelerating = false;
        isStuck = false;
        recallVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
