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
            direction = new Vector3(touchDeltaPosition.x * speed * 10f * Time.deltaTime, 0f, speed);
        }

        Vector3 move = transform.position + direction * Time.deltaTime;
        move.x = Mathf.Clamp(move.x, 0.05f, 0.95f);
        transform.position = move;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sign")
        {
            if (!inCollision)
            {
                inCollision = true;
                Sign sign = collision.gameObject.GetComponent<Sign>();
                Destroy(sign.neighbouringSign);
                int result = sign.operation.Compute(CharacterManager.instance.count, sign.operand);

                if (result < 1)
                {
                    // Fail level
                    CharacterManager.instance.DestroySquad();
                    Time.timeScale = 0f;
                }
                CharacterManager.instance.count = result;
                CharacterManager.instance.DestroySquad();
                CharacterManager.instance.GenerateSquad();
            }

            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "End")
        {
            // End Level
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        inCollision = false;
    }
}
