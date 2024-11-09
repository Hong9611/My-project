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
        // 공격 범위 내 몬스터 감지
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Monster"))
            {
                // 공격 애니메이션 전환
                playerAnimator.SetTrigger("Attack");

                // 투사체 발사
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