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
        float horizontal = InputController.GetHorizontalAxis();
        direction = new Vector3(horizontal * PlayerManager.Instance.HorizontalSpeed, 0f, PlayerManager.Instance.VerticalSpeed);
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
