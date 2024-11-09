using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public float speed;
    public string monsterName;
    public GameObject healthBar; // 몬스터 체력바 UI

    private bool isInAttackRange = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isInAttackRange)
        {
            // Idle 상태로 대기
            // 몬스터의 애니메이션을 Idle로 전환 (애니메이터 활용)
        }
        else
        {
            //transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, speed * Time.deltaTime);
        }

        // 체력 바 업데이트
        //healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 몬스터가 죽을 때 처리 (체력바 제거, 오브젝트 풀로 반환 등)
        //ObjectPool.Instance.ReturnMonster(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttackRange"))
        {
            isInAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerAttackRange"))
        {
            isInAttackRange = false;
        }
    }
}