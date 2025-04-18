using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class FruitManager : MonoBehaviour
{
    public float bounceStopHeight;
    public float initialBounceForce;
    public float bounceDecayRate;
    public float minBounceForce;

    public float slideForce;

    // X軸の制限範囲（Inspector で設定）
    public float minX;
    public float maxX = 3f;

    private Rigidbody2D rb;
    private bool hasBounced = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = new Vector2(0, -5f);
            StartCoroutine(BounceStopRoutine());
        }
    }

    private System.Collections.IEnumerator BounceStopRoutine()
    {
        float currentBounceForce = initialBounceForce;

        while (true)
        {
            if (!hasBounced && transform.position.y <= bounceStopHeight)
            {
                rb.velocity = new Vector2(rb.velocity.x, currentBounceForce);
                hasBounced = true;
            }

            if (hasBounced)
            {
                if (Mathf.Abs(rb.velocity.y) < 0.1f)
                {
                    currentBounceForce *= bounceDecayRate;

                    if (currentBounceForce <= minBounceForce)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                        break;
                    }

                    rb.velocity = new Vector2(rb.velocity.x, currentBounceForce);
                }
            }

            // Y軸の制限（地面より下に行かないように）
            if (transform.position.y < bounceStopHeight)
            {
                transform.position = new Vector2(transform.position.x, bounceStopHeight);
            }

            // X軸の制限
            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
            transform.position = new Vector2(clampedX, transform.position.y);

            yield return null;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.contacts[0].point;
        Vector2 direction = (Vector2)transform.position - contactPoint;
        direction = new Vector2(direction.x, 0).normalized; // 横方向のみ

        rb.AddForce(direction * slideForce, ForceMode2D.Force);
    }
}
