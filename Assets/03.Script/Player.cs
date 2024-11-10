using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public float attackRange = 5f;
    public int damage = 100;
    public float attackInterval = 1f;
    public float finalAttackTime = 0f;

    private Monster targetMonster;

    private void Start()
    {
        StartCoroutine(CheckForMonsterCoroutine());
    }

    IEnumerator CheckForMonsterCoroutine()
    {
        while (true)
        {
            CheckForMonster();
            yield return null;
        }
    }

    void CheckForMonster()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        if (hitColliders == null || hitColliders.Length == 0)
        {
            anim.SetBool("Encount", false);
            targetMonster = null;
            return;
        }

        List<Collider2D> aliveMonsters = new List<Collider2D>();

        foreach (var hitCollider in hitColliders)
        {
            Monster monster = hitCollider.GetComponent<Monster>();
            if (monster != null)
            {
                if (monster.isDead)
                {
                    continue;
                }
                else
                {
                    aliveMonsters.Add(hitCollider);
                    targetMonster = monster;
                    anim.SetBool("Encount", true);
                    return;
                }
            }
        }

        if (aliveMonsters.Count == 0)
        {
            anim.SetBool("Encount", false);
            targetMonster = null;
        }
    }

    public void OnAttackAnimation()
    {
        if (targetMonster != null && Time.time - finalAttackTime >= attackInterval)
        {
            AttackMonster();
        }
    }

    void AttackMonster()
    {
        if (targetMonster != null)
        {
            targetMonster.TakeDamage(damage);
            finalAttackTime = Time.time;
        }
    }
}