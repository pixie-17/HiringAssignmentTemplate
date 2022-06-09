using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Animations;

public class Enemy : MonoBehaviour
{
    public Vector3 playerLeader;
    public Rigidbody rb;
    public float speed;
    public Vector3 direction = Vector3.zero;
    public Animator animator;
    
    void Start()
    {
        GameObject playerLeaderObject = GameObject.FindWithTag("Leader");
        playerLeader = playerLeaderObject == null ? Vector3.zero : playerLeaderObject.transform.position;
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", false);
    }

    void Update()
    {
        GameObject playerLeaderObject = GameObject.FindWithTag("Leader");
        if (playerLeaderObject != null)
        {
            playerLeader = playerLeaderObject.transform.position;
        }

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
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Leader")
        {
            EnemyManager em = GetComponentInParent<EnemyManager>();
            int result = CharacterManager.instance.count - em.count;
            if (result < 1)
            {
                CharacterManager.instance.count = 0;
                //Fail level
                Time.timeScale = 0f;
            }

            CharacterManager.instance.count = result;
            CharacterManager.instance.DestroySquad();
            CharacterManager.instance.GenerateSquad();
            Destroy(em.gameObject);
        }
    }
}
