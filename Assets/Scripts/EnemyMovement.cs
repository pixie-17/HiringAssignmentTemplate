using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private SquadManager squadManager;
    private Vector3 playerLeader;
    private Vector3 direction = Vector3.zero;
    private Animator animator;
    
    void Start()
    {
        playerLeader = GameManager.instance.PlayerLeader == null ? Vector3.zero : GameManager.instance.PlayerLeader.transform.position;
        animator = GetComponent<Animator>();
        squadManager = GetComponentInParent<SquadManager>();
    }

    void Update()
    {
        playerLeader = GameManager.instance.PlayerLeader == null ? Vector3.zero : GameManager.instance.PlayerLeader.transform.position;
        direction = playerLeader - transform.position;
        transform.LookAt(playerLeader);
        
        if (direction.magnitude < 3f)
        {
            animator.SetBool("isRunning", true);
            direction.Normalize();
            transform.position += speed * Time.deltaTime * direction;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Leader") && !squadManager.InCollision)
        {
            squadManager.InCollision = true;
            int result = GameManager.instance.playerSquad.count - squadManager.count;

            if (result < 1)
            {
                // Fail level
                squadManager.count -= GameManager.instance.playerSquad.count;
                squadManager.DestroySquad();
                squadManager.GenerateSquad(transform.position);
                GameManager.instance.playerSquad.DestroySquad();
                FindObjectOfType<UI>().OpenFailed();
                Time.timeScale = 0f;
            } else
            {
                GameManager.instance.playerSquad.count = result;
                Vector3 center = GameManager.instance.PlayerLeader == null ? collision.gameObject.transform.position : GameManager.instance.PlayerLeader.gameObject.transform.position;
                GameManager.instance.playerSquad.DestroySquad();
                GameManager.instance.playerSquad.GenerateSquad(center);
                Destroy(squadManager.gameObject);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Leader")
        {
            squadManager.InCollision = false;
        }
    }
}
