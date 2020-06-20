using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]

public class BounceOffCameraEdge : MonoBehaviour
{

    private Rigidbody2D body;
    private Collider2D componentCollider;

    public float bounceCoefficient = 1.3f;

    private readonly float viewportLeftX = 0f;
    private readonly float viewportRightX = 1f;


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        componentCollider = GetComponent<Collider2D>();
    }


    void FixedUpdate()
    {
        // get the left-most and right-most piont of the object's collider
        Vector3 leftColliderBound = componentCollider.bounds.min;
        Vector3 rightColliderBound = componentCollider.bounds.max;

        // get the world x positions for the left and right edges of the screen
        float cameraLeftBound = ViewportXToWorldX(viewportLeftX, leftColliderBound.z);
        float cameraRightBound = ViewportXToWorldX(viewportRightX, rightColliderBound.z);


        bool collidingWithLeftEdge = leftColliderBound.x <= cameraLeftBound;
        bool collidingWithRightEdge = cameraRightBound <= rightColliderBound.x;

        if (collidingWithLeftEdge || collidingWithRightEdge)
        {
            // place the object right on the edge it's colliding with so
            // it can glitch it's way through
            float xOverlap = collidingWithLeftEdge
                ? cameraLeftBound - leftColliderBound.x
                : cameraRightBound - rightColliderBound.x;

            body.position += new Vector2(xOverlap, 0);

            // add a bounce that has a strength proportional to the speed that the
            // object collides with the edge
            Vector2 bounceDirection = collidingWithLeftEdge
                ? Vector2.right
                : Vector2.left;

            // only perform a bounce if the object is moving towards the 
            body.AddForce(
                bounceDirection * bounceCoefficient * body.velocity.magnitude,
                ForceMode2D.Impulse);             
        }
    }


    private float ViewportXToWorldX(float x, float worldZ)
    {
        return Camera.main.ViewportToWorldPoint(new Vector3(x, 0, worldZ - Camera.main.transform.position.z)).x;
    }
}
