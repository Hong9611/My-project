using UnityEngine;
using UnityEngine.Pool;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;      
    public float attackSpeed = 1f;           
    public int damage = 100;                 
    public float attackRange = 5f;           
    public Animator playerAnimator;          

    private void Start()
    {
        InvokeRepeating("Attack", 1f, attackSpeed);
    }

    void Attack()
    {
        // ���� ���� �� ���� ����
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Monster"))
            {
                // ���� �ִϸ��̼� ��ȯ
                playerAnimator.SetTrigger("Attack");

                // ����ü �߻�
                ShootProjectile(enemy.transform);
            }
        }
    }

    void ShootProjectile(Transform target)
    {
        GameObject projectile = ObjectPool.Instance.GetProjectile();
        projectile.transform.position = transform.position;
        projectile.GetComponent<Projectile>().Initialize(target, damage);
    }
}