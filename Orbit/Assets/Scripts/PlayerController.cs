using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D myBody;

    public float horizontalThrust;

    private float horizontalDirection;


    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
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
        applyForce(horizontalThrust * horizontalDirection, 0);
    }


    private void applyForce(float x, float y, ForceMode2D mode = ForceMode2D.Force)
    {
        myBody.AddForce(new Vector2(x, y), mode);
    }

}
