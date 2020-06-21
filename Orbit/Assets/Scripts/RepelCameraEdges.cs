using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class RepelCameraEdges : MonoBehaviour
{

    private Rigidbody2D body;

    private readonly float viewportLeftX = 0f;
    private readonly float viewportRightX = 1f;

    public float repelStrength = 10;
    public float distanceExponent = 4;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Vector3 worldPosition = body.position;

        // get the world x positions for the left and right edges of the screen
        float cameraLeftBound = ViewportXToWorldX(viewportLeftX, worldPosition.z);
        float cameraRightBound = ViewportXToWorldX(viewportRightX, worldPosition.z);

        float distanceFromLeft = Math.Abs(worldPosition.x - cameraLeftBound);
        float distanceFromRight = Math.Abs(cameraRightBound - worldPosition.x);

        // calculate and apply force from the left
        float dl = (float) Math.Pow(distanceFromLeft, distanceExponent);
        body.AddForce(Vector2.right * repelStrength / dl, ForceMode2D.Force);

        // calculate and apply force from the right
        float dr = (float) Math.Pow(distanceFromRight, distanceExponent);
        body.AddForce(Vector2.left * repelStrength / dr, ForceMode2D.Force);
    }


    private float ViewportXToWorldX(float x, float worldZ)
    {
        return Camera.main.ViewportToWorldPoint(new Vector3(x, 0, worldZ - Camera.main.transform.position.z)).x;
    }
}
