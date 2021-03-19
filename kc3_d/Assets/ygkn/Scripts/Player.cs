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

        if (clicked && rb.velocity.x == 0) {
            rb.velocity += new Vector2(speed, 0);
        }

        if (isGround && clicked) {
            rb.velocity += new Vector2(0, jumpSpeed);
        }

        if (!isGround) {
            rb.velocity += new Vector2(0, -gravity);
        }

    }
}
