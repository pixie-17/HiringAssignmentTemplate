using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 3.5f;
    private Vector3 direction = Vector3.forward;
    private bool inCollision = false;

    void Update()
    {
        direction = Vector3.forward * speed;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            direction = new Vector3(touchDeltaPosition.x * speed * 3f * Time.fixedDeltaTime, 0f, speed);
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 move = transform.position + direction * Time.fixedDeltaTime;
        move.x = Mathf.Clamp(move.x, 0.05f, 0.95f);
        rb.MovePosition(move);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sign")
        {
            if (!inCollision)
            {
                inCollision = true;
                Sign sign = collision.gameObject.GetComponent<Sign>();
                CharacterManager.instance.count = sign.operation.Compute(CharacterManager.instance.count, sign.operand);
            }

            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        inCollision = false;
    }
}
