using System.Collections;
using UnityEngine;

public class ArrowPopEffect : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float duration = 0.2f;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) sr = gameObject.AddComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    public void Play()
    {
        if (frames == null || frames.Length == 0) return;
        StopAllCoroutines();
        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        sr.enabled = true;
        float frameTime = duration / frames.Length;

        for (int i = 0; i < frames.Length; i++)
        {
            sr.sprite = frames[i];
            yield return new WaitForSeconds(frameTime);
        }

        sr.enabled = false;
    }
}
