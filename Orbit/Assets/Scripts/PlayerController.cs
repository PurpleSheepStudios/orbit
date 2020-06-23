using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D body;

    public float horizontalThrust = 20f;
    public float frictionCoefficient = 0.5f;

    private float horizontalDirection;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        horizontalDirection = 0;
    }


    /** This is called once per frame. Should be used to collect and store
     *  user input.
     */
    void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
    }


    /** This is called at the frequency of the physics system and is frame
     *  rate independent. This is where application of forces should go due
     *  to the constant time difference between each call making things easier.
     */
    private void FixedUpdate()
    {
        // apply thrust in the direction the user is indicating
        applyForce(horizontalThrust * horizontalDirection, 0);

        // apply force in the opposite direction the ship is moving in
        // (kind of like friction)
        applyForce(-body.velocity * frictionCoefficient);
    }


    private void applyForce(float x, float y, ForceMode2D mode = ForceMode2D.Force)
    {
        body.AddForce(new Vector2(x, y), mode);
    }

    private void applyForce(Vector2 force, ForceMode2D mode = ForceMode2D.Force)
    {
        body.AddForce(force, mode);
    }

}
