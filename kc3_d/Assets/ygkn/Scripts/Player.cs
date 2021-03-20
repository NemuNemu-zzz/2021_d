using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float gravity;
    [SerializeField] float jumpSpeed;
    [SerializeField] GroundingCheck groundingCheck;

    Rigidbody2D rb = null;

    bool started = false;

    float jumpElapsedTime = float.PositiveInfinity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGround = groundingCheck.IsGround();
        bool clicked = Input.GetMouseButton(0) || Input.touchCount > 0;

        started = started || clicked;

        if (clicked && isGround) {
            jumpElapsedTime = 0.0f;
        }

        jumpElapsedTime += Time.deltaTime;


        rb.velocity = new Vector2(started ? speed : 0, jumpSpeed - gravity * jumpElapsedTime);

        Debug.Log(rb.velocity);
    }
}
