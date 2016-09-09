using UnityEngine;
using System.Collections;

public abstract class Mover : MonoBehaviour {
    public float speed = 0.15f;
    public float topSpeed = 0.27f;
    public float acceleration = 0.85f;
    public float deceleration = 1.5f;

    [SerializeField]
    protected float currentSpeed = 0f;
    protected float desiredSpeed = 0f;
    protected int direction = 1;

    protected BoxCollider2D boxCollider;
    protected Vector2 size;
    protected Vector2 center;

	protected virtual void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        size = boxCollider.size;
        center = boxCollider.size;
	}

    protected virtual void Update ()
    {
        Move();
	}

    public void Move()
    {
        // Flip transform so matrix math continues to work
        transform.localScale = new Vector2(1, 1);

        float input = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(input) > float.Epsilon)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                desiredSpeed = topSpeed * input;
            }
            else
            {
                desiredSpeed = speed * input;
            }
            currentSpeed = AccelerateTowards(currentSpeed, desiredSpeed, acceleration);
        }
        else
            currentSpeed = AccelerateTowards(currentSpeed, 0, deceleration);

        transform.Translate(new Vector2(currentSpeed, 0));

        // Flip to correct orientation
        if(Mathf.Abs(input) > float.Epsilon)
            direction = input < 0 ? -1 : 1;
        transform.localScale = new Vector3(direction, transform.localScale.y);
    }

    // Accelerates a scalar speed towards a target speed.
    public float AccelerateTowards(float inputSpeed, float targetSpeed, float acceleration)
    {
        // Check if it's already hit the target.
        if(Mathf.Abs(targetSpeed - inputSpeed) < float.Epsilon)
            return targetSpeed;

        int dir = (int)Mathf.Sign(targetSpeed - inputSpeed);
        inputSpeed += dir * acceleration * Time.deltaTime;
        return (dir == Mathf.Sign(targetSpeed - inputSpeed)) ? inputSpeed : targetSpeed;
    }
}
