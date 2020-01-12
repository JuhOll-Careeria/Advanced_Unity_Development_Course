using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerAgentController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    NavMeshAgent agent;
    Rigidbody rb;
    Animator anim;

    Vector2 movementDir = Vector2.zero;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        agent.speed = this.speed;
    }

    private void FixedUpdate()
    {
        SimpleAgentMove();

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void SimpleAgentMove()
    {
        movementDir.x = Input.GetAxis("Horizontal");
        movementDir.y = Input.GetAxis("Vertical");

        if (movementDir.magnitude > 0f && agent.isActiveAndEnabled)
        {
            agent.SetDestination(transform.localPosition + new Vector3(movementDir.x, 0, movementDir.y));
        }

        GetComponentInChildren<Animator>().SetFloat("velocity", agent.velocity.magnitude);
    }

    void Attack()
    {
        // Toteuta Attack animaatio, layerillä 1
        anim.Play("Attack", 1, 0);
    }
}