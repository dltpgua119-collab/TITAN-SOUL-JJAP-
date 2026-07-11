using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            vertical = -1f;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            vertical = 1f;
        }

        Vector3 direction = new Vector3(horizontal, vertical, 0f).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
