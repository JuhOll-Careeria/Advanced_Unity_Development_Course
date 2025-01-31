﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour, IKillable
{
    [SerializeField] EnemyData Data;
    [SerializeField] Transform FirePoint;
    [SerializeField] LayerMask layerMask;
    [SerializeField] ParticleSystem MuzzleFlash;

    [Header("AI")]
    [SerializeField] float IdleTime = 2f;
    [SerializeField] Collider DetectionCol;
    [SerializeField] Transform[] PatrolTransforms;
    List<Vector3> PatrolPoints = new List<Vector3>();
    private bool playerInSight = false;
    private Vector3 playerSightedPosition = Vector3.zero;
    int currentPatrolIndex = 0;

    [Header("On Death")]
    [SerializeField] private GameObject[] DetachablePartsOnDeath;

    Rigidbody RB;
    NavMeshAgent Agent;
    Animator anim;
    int currentHealth = 0;
    bool AttackOnCD = false;
    EnemyState _State = EnemyState.Idle;
    float _IdleTimer = 0f;
    float StopChaseTimer = 0f;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        RB = GetComponent<Rigidbody>();
        currentHealth = Data.MaxHealth;
        _State = EnemyState.Idle;
        _IdleTimer = IdleTime;
        DetectionCol.enabled = true;

        foreach (Transform patrolTrans in PatrolTransforms.ToList())
        {
            PatrolPoints.Add(patrolTrans.position);
            Destroy(patrolTrans.gameObject);
        }
    }
    void FixedUpdate()
    {
        CalculaterDetection();
    }

    void Update()
    {

        switch (_State)
        {
            case EnemyState.Idle:
                anim.SetFloat("velocity", 0);

                if (PatrolPoints.Count <= 0)
                    break;

                _IdleTimer -= Time.deltaTime;

                if (_IdleTimer <= 0)
                {
                    _State = EnemyState.Patrol;
                    Agent.SetDestination(PatrolPoints[currentPatrolIndex]);
                    currentPatrolIndex++;

                    if (currentPatrolIndex >= PatrolPoints.Count)
                    {
                        currentPatrolIndex = 0;
                    }
                }

                break;
            case EnemyState.Patrol:
                if (!Agent.pathPending)
                {
                    if (Agent.remainingDistance <= Agent.stoppingDistance)
                    {
                        if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f)
                        {
                            _IdleTimer = IdleTime;
                            _State = EnemyState.Idle;
                        }
                    }
                }
                break;
            case EnemyState.Chase:

                if (!playerInSight)
                {
                    StopChaseTimer -= Time.deltaTime;

                    if (StopChaseTimer <= 0)
                    {
                        playerSightedPosition = Vector3.zero;
                        _State = EnemyState.Idle;
                        break;
                    }
                }

                Agent.isStopped = false;
                Agent.SetDestination(playerSightedPosition);
                anim.SetFloat("velocity", Agent.velocity.magnitude);
                break;
            case EnemyState.Attack:
                anim.SetFloat("velocity", 0);
                this.transform.LookAt(playerSightedPosition);

                if (AttackOnCD)
                    break;

                Agent.isStopped = true;
                DoAttack();
                break;
            case EnemyState.Dead:
                return;
        }
    }

    void DoAttack()
    {
        anim.Play("Attack", 0, 0);
        AttackOnCD = true;
        Invoke("ResetCD", Data.AttackCD);
    }

    void ResetCD()
    {
        AttackOnCD = false;
    }

    void FireProjectile()
    {
        Vector3 dir = playerSightedPosition - FirePoint.position;

        AudioManager.Instance.PlayClipOnce(Data.AttackSE, this.gameObject);
        Rigidbody ProjectileRB = Instantiate(Data.ProjectilePrefab, FirePoint.position, FirePoint.rotation, null).GetComponent<Rigidbody>();
        ProjectileRB.AddForce(dir * Data.ProjectileForce);
    }

    public void OnDamageTaken(int dmgValue)
    {
        CurrentHealth -= dmgValue;

        AudioManager.Instance.PlayClipOnce(Data.OnHitSound, this.gameObject);

        if (CurrentHealth <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        AudioManager.Instance.PlayClipOnce(Data.OnDeathSound, this.gameObject);

        Destroy(Agent);

        foreach (GameObject part in DetachablePartsOnDeath)
        {
            part.GetComponent<MeshCollider>().enabled = true;
            part.AddComponent<Prop>();
            part.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            part.transform.parent = null;
        }


        Destroy(this.gameObject);
    }

    private void CalculaterDetection()
    {
        GameObject Target = GameManager.Instance.Player;

        if (Target != null)
        {
            if (DetectionCol.bounds.Contains(Target.transform.position))
            {
                Vector3 dir = Target.transform.position - FirePoint.position;

                RaycastHit hit;

                if (Physics.Raycast(this.FirePoint.position, dir * 50f, out hit, layerMask))
                {
                    if (hit.collider.gameObject.Equals(Target))
                    {
                        playerInSight = true;
                        playerSightedPosition = hit.point;
                        _State = EnemyState.Attack;
                    }
                    else
                    {
                        playerInSight = false;
                    }

                    Debug.DrawRay(this.FirePoint.position, dir * 50f, Color.red);
                }
            }
            else
            {
                playerInSight = false;
            }

            if (playerSightedPosition != Vector3.zero && !playerInSight && _State != EnemyState.Chase)
            {
                StopChaseTimer = Data.ChaseTime;
                _State = EnemyState.Chase;
            }
        }
    }
}

public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Dead
}
