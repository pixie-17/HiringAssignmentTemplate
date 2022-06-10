using System.Collections;
using UnityEngine;

/* Controls the movement and collision of player */
public class PlayerMovement : MonoBehaviour
{

    private Vector3 direction = Vector3.forward;

    void Update()
    {
        if (!GameManager.instance.LevelFinished)
        {
            direction = Vector3.forward * GameManager.instance.playerSpeed;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                direction = new Vector3(touchDeltaPosition.x * GameManager.instance.playerSpeed * GameManager.instance.playerHorizontalSpeed * Time.deltaTime, 0f, GameManager.instance.playerSpeed);
            }

            Vector3 move = transform.position + direction * Time.deltaTime;
            move.x = Mathf.Clamp(move.x, 0.05f, 0.95f);
            transform.position = move;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sign")
        {
            if (!GameManager.instance.playerSquad.InCollision)
            {
                GameManager.instance.playerSquad.InCollision = true;
                Sign sign = collision.gameObject.GetComponent<Sign>();
                Destroy(sign.neighbouringSign);
                int result = sign.operation.Compute(GameManager.instance.playerSquad.count, sign.operand);
                if (result < 1)
                {
                    // Fail level
                    GameManager.instance.playerSquad.DestroySquad();
                    FindObjectOfType<UI>().OpenFailed();
                    Time.timeScale = 0f;
                } else
                {
                    if (GameManager.instance.survival)
                    {
                        GameManager.instance.survivalMode.score++;
                    }
                    GameManager.instance.playerSquad.count = result;
                    GameManager.instance.playerSquad.DestroySquad();
                    GameManager.instance.playerSquad.GenerateSquad(transform.position);
                }
            }

            Destroy(collision.gameObject);

        } else if (collision.gameObject.tag == "End")
        {
            // End Level
            GameManager.instance.LevelFinished = true;
            Destroy(collision.gameObject);
            FindObjectOfType<UI>().OpenFinished();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(CancelCollision());
    }

    IEnumerator CancelCollision()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.playerSquad.InCollision = false;
    }
}
