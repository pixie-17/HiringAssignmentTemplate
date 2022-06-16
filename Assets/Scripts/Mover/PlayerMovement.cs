using System.Collections;
using UnityEngine;

/* Controls the movement and detects collision of player */

public class PlayerMovement : MonoBehaviour
{
    private Vector3 direction = Vector3.forward;

    public delegate void HitSign(Sign sign);
    public static event HitSign OnHit;

    public delegate void FinishLevel();
    public static event FinishLevel OnFinish;

    void Update()
    {
        if (!GameManager.Instance.LevelFinished)
        {
            UpdateDirection();
            Move();
        }
    }

    private void UpdateDirection()
    {
        direction = Vector3.forward * PlayerManager.Instance.VerticalSpeed;
        float horizontal = 1f;

#if UNITY_ANDROID || UNITY_IOS
        horizontal = GetHorizontalViaTouch();
#elif UNITY_EDITOR
        horizontal = GetHorizontalViaKeyBoard();
#endif
        direction = new Vector3(horizontal * PlayerManager.Instance.HorizontalSpeed, 0f, PlayerManager.Instance.VerticalSpeed);
    }

    private float GetHorizontalViaTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            return touchDeltaPosition.x;
        }
        return 0f;
    }

    private float GetHorizontalViaKeyBoard()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            return Input.GetAxis("Horizontal");
        }
        return 0f;
    }

    private void Move()
    {
        Vector3 position = transform.position + direction * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, 0.05f, 0.95f);
        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sign" && collision.gameObject.TryGetComponent<Sign>(out Sign sign))
        {
            OnHit(sign);

        } else if (collision.gameObject.tag == "End")
        {
            OnFinish();
        }
    }
}
