using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Mover : MonoBehaviour {
    public float speed = 0.15f;
    public float topSpeed = 0.27f;
    public float acceleration = 0.85f;
    public float deceleration = 1.5f;
    public float jumpHeight = 0.2f;

    public int divisionsX = 3;
    public int divisionsY = 9;

    public LayerMask collisionMask;

    [SerializeField]
    protected Vector2 currentSpeed = new Vector2(0, 0);
    [SerializeField]
    protected static float gravity = 0.5f;
    protected float desiredSpeed = 0f;
    protected int direction = 1;
    protected bool grounded = false;
    protected bool jumped = false;

    protected BoxCollider2D boxCollider;
    protected Vector2 size;
    protected Vector2 center;

    private float skin = 0.001f;
    private bool wallCollision = false;

    protected virtual void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        size = boxCollider.size;
        center = boxCollider.offset;
	}

    protected virtual void Update ()
    {
        
	}

    public void Move(bool active)
    {
        // Flip transform so matrix math continues to work
        transform.localScale = new Vector2(1, 1);

        float input = Input.GetAxisRaw("Horizontal");
        if (active)
        {
            if (Mathf.Abs(input) > float.Epsilon)
            {
                if (Input.GetKey(KeyCode.LeftShift) && grounded)
                {
                    desiredSpeed = topSpeed * input;
                }
                else
                {
                    desiredSpeed = speed * input;
                }
                currentSpeed.x = AccelerateTowards(currentSpeed.x, desiredSpeed, acceleration);
            }
            else
                currentSpeed.x = AccelerateTowards(currentSpeed.x, 0, deceleration);

            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                currentSpeed.y += jumpHeight;
                grounded = false;
                jumped = true;
            }
        }
        else
        {
            currentSpeed.x = AccelerateTowards(currentSpeed.x, 0, deceleration);
        }

        currentSpeed.y -= gravity * Time.deltaTime;


        CollisionMove();

        // Flip to correct orientation
        if (active)
        {
            if (Mathf.Abs(input) > float.Epsilon)
                direction = input < 0 ? -1 : 1;
        }
        transform.localScale = new Vector3(direction, transform.localScale.y);
    }

    public void CollisionMove()
    {
        float deltaX = currentSpeed.x;
        float deltaY = currentSpeed.y;

        Vector2 p = transform.position;

        // Check y directions
        int yDir = (int)Mathf.Sign(deltaY);
        for (int i = 0; i < divisionsX; i++)
        {
            float x = (p.x + center.x - size.x / 2) + i * size.x / (divisionsX - 1);
            float y = p.y + center.y + yDir * size.y / 2;

            Ray2D ray = new Ray2D(new Vector2(x, y), new Vector2(0, yDir));
            Debug.DrawRay(ray.origin, ray.direction);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Abs(deltaY) + skin, collisionMask);
            if (hit.collider)
            {
                float d = Vector2.Distance(ray.origin, hit.point);

                if(yDir < 0)
                {
                    grounded = true;
                    currentSpeed.y = 0;
                }
                
                if (d > skin)
                    deltaY = yDir * (d - skin);
                else
                    deltaY = 0;
                break;
            }
        }

        // Check x directions
        int xDir = (int)Mathf.Sign(deltaX);
        for (int i = 0; i < divisionsY; i++)
        {
            float x = p.x + center.x + xDir * size.x / 2;
            float y = (p.y + center.y - size.y / 2) + i * size.y / (divisionsY - 1);

            Ray2D ray = new Ray2D(new Vector2(x, y), new Vector2(xDir, 0));
            Debug.DrawRay(ray.origin, ray.direction);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Abs(deltaX) + skin, collisionMask);
            if (hit.collider)
            {
                float d = Vector2.Distance(ray.origin, hit.point);
                wallCollision = true;
                currentSpeed.x = 0;
                if (d > skin)
                    deltaX = xDir * (d - skin);
                else
                    deltaX = 0;
                break;
            }
        }

        // Check player velocity angle
        if (!grounded && !wallCollision)
        {
            Vector2 playerDir = new Vector2(deltaX, deltaY).normalized;
            Vector2 o = new Vector2(p.x + center.x + Mathf.Sign(deltaX) * size.x / 2, p.y + center.y + Mathf.Sign(deltaY) * size.y / 2);
            Ray2D ray = new Ray2D(o, playerDir);
            Debug.DrawRay(ray.origin, ray.direction);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY), collisionMask);
            if (hit.collider)
            {
                grounded = true;
                deltaY = 0;
            }
        }

        transform.Translate(new Vector2(deltaX, deltaY));
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
