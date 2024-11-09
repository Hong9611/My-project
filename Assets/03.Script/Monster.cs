using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public float speed;
    public string monsterName;
    public GameObject healthBar; // ���� ü�¹� UI

    private bool isInAttackRange = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isInAttackRange)
        {
            // Idle ���·� ���
            // ������ �ִϸ��̼��� Idle�� ��ȯ (�ִϸ����� Ȱ��)
        }
        else
        {
            //transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, speed * Time.deltaTime);
        }

        // ü�� �� ������Ʈ
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
        // ���Ͱ� ���� �� ó�� (ü�¹� ����, ������Ʈ Ǯ�� ��ȯ ��)
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