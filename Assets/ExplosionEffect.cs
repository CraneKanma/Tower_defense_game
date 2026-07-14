using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float duration = 0.4f;
    public float maxScale = 2f;

    private SpriteRenderer sr;
    private float timer = 0f;
    private Vector3 startScale;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startScale = transform.localScale;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / duration;

        // 逐渐放大
        transform.localScale = Vector3.Lerp(startScale, startScale * maxScale, t);

        // 逐渐透明
        Color c = sr.color;
        c.a = Mathf.Lerp(1f, 0f, t);
        sr.color = c;
    }
}