using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData/Enemy")]
public class EnemySO : ScriptableObject
{
    public int damage;

    [Header("Ranges")]
    public float sightRange;
    public float attackRange;

    [Header("Patrolling")]
    public int walkPointRange;

    [Header("Attacking")]
    public float timeBetweenAttacks;
}
