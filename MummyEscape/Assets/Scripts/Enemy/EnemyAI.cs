using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public EnemySO enemyData;

    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask playerLayerMask;

    public bool isPatroller;

    private Transform player;
    private Vector3 walkPoint;

    private bool isWalkPointSet;
    private bool hasAlreadyAttacked;
    private bool isInSightRange;
    private bool isInAttackRange;

    private bool IsPatrolling() => !isInSightRange && !isInAttackRange;
    private bool IsChasing() => isInSightRange && !isInAttackRange;
    private bool IsAttacking() => isInSightRange && isInAttackRange;

    private void Awake()
    {
        if (player == null)
            player = GameObject.Find("Player").transform;

        if (animatorController == null)
            animatorController = GetComponent<AnimatorController>();

        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameState.StartGamePlay)
            return;

        CheckRange();
        SwitchState();
    }

    private void CheckRange()
    {
        //Check for sight and attack range
        isInSightRange = Physics.CheckSphere(transform.position, enemyData.sightRange, playerLayerMask);
        isInAttackRange = Physics.CheckSphere(transform.position, enemyData.attackRange, playerLayerMask);
    }


    private void SwitchState()
    {
        if (IsPatrolling())
            Patrolling();
        if (IsChasing())
            ChasePlayer();
        if (IsAttacking())
            AttackPlayer();
    }

    private void Patrolling()
    {
        //If no walk point set, search for one
        if (!isWalkPointSet)
        {
            SearchWalkPoint();
            animatorController.PlayIdle();

        }

        //if walk point set already, set destination to that walk point
        if (isWalkPointSet)
        {
            agent.SetDestination(walkPoint);
            animatorController.PlayRun();
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            isWalkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        int randomXPos = Random.Range(-enemyData.walkPointRange, enemyData.walkPointRange);
        int randomZPos = Random.Range(-enemyData.walkPointRange, enemyData.walkPointRange);

        walkPoint = new Vector3(
            transform.position.x + randomXPos,
            transform.position.y,
            transform.position.z + randomZPos);

        float maxDistance = 2f;

        if (Physics.Raycast(walkPoint, -transform.up, maxDistance, groundLayerMask))
            isWalkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animatorController.PlayRun();
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        animatorController.PlayIdle();

        if (!hasAlreadyAttacked)
        {
            if (player.TryGetComponent(out Health health))
            {
                //deal damage to player
                health.TakeDamage(enemyData.damage);

                hasAlreadyAttacked = true;
                StartCoroutine(ResetAttackRoutine());
            }
        }
    }

    private IEnumerator ResetAttackRoutine()
    {
        yield return new WaitForSeconds(enemyData.timeBetweenAttacks);
        hasAlreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyData.sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemyData.walkPointRange);
    }
}