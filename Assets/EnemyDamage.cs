using UnityEngine;
using UnityEngine.AI;

public class EnemyDamage : MonoBehaviour
{
    private enum State { Patrol, Chase, Attack}
    private State currentState = State.Patrol;

    private NavMeshAgent agent = null;
    [SerializeField] public Transform target;
    [SerializeField] public float detectionRange = 40f;
    [SerializeField] public float attackRange = 2f;
    [SerializeField] public float damage = 20f;
    [SerializeField] public float attackCooldown = 1f;
    [SerializeField] private float attackWindupDelay = 1f;

    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    private bool isWindingUp = false;
    private float windUpStartTime = 0f;
    private float lastAttackTime = 0f;

    private void Start()
    {
        GetReference();
        agent.stoppingDistance =  attackRange;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        switch(currentState)
        {
            case State.Patrol:
                Patrol();

                if(distanceToPlayer <= detectionRange)
                    currentState = State.Chase;
                break;

            case State.Chase:
                MoveToTarget();

                if(distanceToPlayer <= attackRange)
                    currentState = State.Attack;
                else if(distanceToPlayer > detectionRange + 2f)
                    currentState = State.Patrol;
                break;

            case State.Attack:
                TryAttackPlayer();

                if (distanceToPlayer > attackRange)
                    currentState = State.Chase;
                break;
        }
        LookAtTarget();
    }

    private void MoveToTarget()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void Patrol()
    {
        if(waypoints.Length == 0)
            return;
        if(!agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            GoToNextWaypoint();
        }
    }

    private void GoToNextWaypoint()
    {
        if(waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    private void TryAttackPlayer()
    {
        if (target == null)
            return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attackRange)
        {
            if (!isWindingUp && Time.time > lastAttackTime + attackCooldown)
            {
                isWindingUp = true;
                windUpStartTime = Time.time;
            }

            if (isWindingUp && Time.time >= windUpStartTime + attackWindupDelay)
            {
                PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    Debug.Log("Enemy Hit the Player!");
                }

                lastAttackTime = Time.time;
                isWindingUp = false;
            }
        }
        else
        {
            isWindingUp = false;
        }

        MoveToTarget();
    }
    private void LookAtTarget()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position;
        targetPosition.y = transform.position.y;
        Vector3 direction = (targetPosition - transform.position).normalized;

        if (Mathf.Abs(target.position.y - transform.position.y) < 1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private void GetReference()
    {
        agent = GetComponent<NavMeshAgent>();

        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }

        }
    }
}
