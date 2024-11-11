using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using NUnit.Framework;

public class EnemyController : MonoBehaviour
{
    [Header("EnemyData")]
    [SerializeField] private int currentLife;
    [SerializeField] private int maxLife;
    [SerializeField] private int enemyScorePoint;

    [Header("Patrol")]
    [SerializeField] private GameObject patrolPointsContainer;
    private List<Transform> patrolPoints = new List<Transform>();
    private int destinationPoint = 0;
    [SerializeField] private bool isChasing;

    private NavMeshAgent agent;

    private WeaponController weaponController;

    private Transform playerTransformn;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransformn = GameObject.FindGameObjectWithTag("Player").transform;
        weaponController = GetComponent<WeaponController>();

        //Take all the children of patrolPointContainer and add in the PatrolPoints array
        foreach(Transform child in patrolPointsContainer.transform)
            patrolPoints.Add(child);

        GotoNextPatrolPoint();
    }
    private void Update()
    {
        SearchPlayer();

        //TODO Choose next destination point when the agents get close to the current one
        if (!isChasing && !agent.pathPending && agent.remainingDistance < 3f)
            GotoNextPatrolPoint();
    }
    /// <summary>
    /// Enemy Go to next destinationPoint
    /// </summary>
    private void GotoNextPatrolPoint()
    {
        agent.stoppingDistance = 0f;
        //set the agent to the curently destination Point
        agent.SetDestination(patrolPoints[destinationPoint].position);
        destinationPoint = (destinationPoint + 1) % patrolPoints.Count;
    }

    void SearchPlayer()
    {
        NavMeshHit hit;

        if (!agent.Raycast(playerTransformn.position, out hit))
        {
            if (hit.distance <= 10f)
            {
                agent.SetDestination(playerTransformn.position);
                agent.stoppingDistance = 5f;
                transform.LookAt(playerTransformn.position);
                isChasing=true;

                //shoot Player if distance between them is lower than 7

                if (hit.distance <= 7f)
                {
                    if(weaponController.CanShoot())
                        weaponController.Shoot();
                }
            }
            else
            {
                isChasing = false;
                GotoNextPatrolPoint();
            }

            
        }
        else
        {
            isChasing = false;
            if (!agent.pathPending && agent.remainingDistance < 3f)
                GotoNextPatrolPoint();
        }
    }
    /// <summary>
    /// handle when the enemy receive a bullet
    /// </summary>
    /// <param name="quantity">Damage quantity</param>
    public void DamageEnemy(int quantity)
    {
        currentLife -= quantity;
        if(currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
