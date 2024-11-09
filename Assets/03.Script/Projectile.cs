using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private int damage;

    public void Initialize(Transform target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 10f * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                // 몬스터와 충돌했을 때 데미지 적용
                target.GetComponent<Monster>().TakeDamage(damage);
                gameObject.SetActive(false); // 투사체 비활성화 (오브젝트 풀링을 위해)
            }
        }
    }
}