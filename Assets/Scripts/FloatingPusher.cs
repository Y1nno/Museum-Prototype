using UnityEngine;

public class FloatingPusher : WorldItem
{
    private Transform tipTransform;
    public float pushForce = 10f;
    private Vector2 launchDirection = Vector2.up; // Default push direction is upwards
    private float rotationUpperBound = 0f; // Maximum angle for randomization
    private float rotationLowerBound = -90f; // Minimum angle for randomization

    public override void Start()
    {
        base.Start();
        tipTransform = transform.Find("Tip");
        if (tipTransform == null)
        {
            Debug.LogError("FloatingPusher: No child named 'Tip' found. Please add a child GameObject named 'Tip' to define the push point.");
            return;
        }
        randomizeDirection();
        launchDirection = (tipTransform.position - transform.position).normalized;
    }

    public override void Update()
    {
        base.Update();
        KeepNearPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && tipTransform != null)
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.AddForce(launchDirection * pushForce, ForceMode2D.Impulse);
            }
        }
    }

    private void randomizeDirection()
    {
        float randomAngle = Random.Range(rotationLowerBound, rotationUpperBound);
        transform.rotation = Quaternion.Euler(0, 0, randomAngle);
    }
}
