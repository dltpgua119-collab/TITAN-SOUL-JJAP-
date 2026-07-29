using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float recallSpeed = 12f;
    [SerializeField] private float returnDistanceThreshold = 0.1f;

    private PlayerMove owner;
    private Vector2 direction = Vector2.right;
    private bool isFlying;
    private bool isReturning;
    private bool isStuck;
    private int wallLayerMask = -1;
    private LineRenderer lineRenderer;

    public bool CanFire => owner != null && !isFlying && !isReturning && !isStuck;
    public bool IsActiveForRecall => isFlying || isStuck;

    public bool IsReturning => isReturning;

    public void Initialize(PlayerMove playerOwner)
    {
        owner = playerOwner;
        wallLayerMask = LayerMask.GetMask("Wall");
        if (wallLayerMask == 0)
        {
            wallLayerMask = -1;
        }

        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.enabled = false;

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (owner == null)
        {
            return;
        }

        if (isFlying)
        {
            MoveForward();
        }
        else if (isReturning)
        {
            MoveTowardPlayer();
        }

        UpdateLineRenderer();
    }

    public void Fire(Vector2 firingDirection)
    {
        if (!CanFire || owner == null)
        {
            return;
        }

        direction = firingDirection.sqrMagnitude > 0.001f ? firingDirection.normalized : Vector2.right;
        isFlying = true;
        isReturning = false;
        isStuck = false;
        transform.SetParent(null, true);
        transform.position = owner.transform.position;
        gameObject.SetActive(true);
        lineRenderer.enabled = true;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Recall()
    {
        if (owner == null || (!isFlying && !isStuck))
        {
            return;
        }

        isReturning = true;
        isFlying = false;
        isStuck = false;
        lineRenderer.enabled = true;
    }

    private void MoveForward()
    {
        Vector2 currentPosition = transform.position;
        Vector2 nextPosition = currentPosition + direction * (speed * Time.deltaTime);

        if (Physics2D.Linecast(currentPosition, nextPosition, wallLayerMask))
        {
            isFlying = false;
            isStuck = true;
            transform.position = currentPosition;
            return;
        }

        transform.position = nextPosition;

        if (Vector2.Distance(transform.position, owner.transform.position) < 0.2f)
        {
            ResetToPlayer();
        }
    }

    private void MoveTowardPlayer()
    {
        if (owner == null)
        {
            return;
        }

        Vector2 toPlayer = (Vector2)owner.transform.position - (Vector2)transform.position;
        if (toPlayer.sqrMagnitude <= returnDistanceThreshold * returnDistanceThreshold)
        {
            ResetToPlayer();
            return;
        }

        Vector2 move = toPlayer.normalized * (recallSpeed * Time.deltaTime);
        transform.position += (Vector3)move;
    }

    public void StopRecall()
    {
        if (!isReturning) return;
        isReturning = false;
        isStuck = true;
    }

    private void ResetToPlayer()
    {
        if (owner == null)
        {
            return;
        }

        isFlying = false;
        isReturning = false;
        isStuck = false;
        lineRenderer.enabled = false;
        gameObject.SetActive(false);
    }

    private void UpdateLineRenderer()
    {
        if (lineRenderer == null || !lineRenderer.enabled)
        {
            return;
        }

        Vector3 from = transform.position;
        Vector3 to = transform.position + (Vector3)(direction.normalized * 0.5f);
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
    }
}
