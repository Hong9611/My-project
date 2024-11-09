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
                // ���Ϳ� �浹���� �� ������ ����
                target.GetComponent<Monster>().TakeDamage(damage);
                gameObject.SetActive(false); // ����ü ��Ȱ��ȭ (������Ʈ Ǯ���� ����)
            }
        }
    }
}