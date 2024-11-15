using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public Animator anim;
    private SpriteRenderer spriteRenderer;
    public GameObject popUp;
    GameObject popDown;

    public string monsterName;
    public string grade;
    public float speed;
    public int maxHealth;
    public int currentHealth;
    public bool isDead = false;

    public Slider healthBarSlider;

    private Transform player;
    private Transform targetPosition;

    public event Action<GameObject> OnDeath;

    private void OnEnable()
    {
        isDead = false;
        anim.SetBool("Encount", false);

        if (healthBarSlider == null)
        {
            healthBarSlider = FindObjectOfType<Slider>();
            if (healthBarSlider == null)
            {
                Debug.LogWarning("HealthBarSlider not found in the scene.");
            }
        }

        if (targetPosition == null)
        {
            targetPosition = FindObjectOfType<SpawnPoint>().transform;
            if (targetPosition == null)
            {
                Debug.LogWarning("targetPosition not found in the scene.");
            }
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogWarning("SpriteRenderer not found in the children of Monster.");
            }
        }

        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = maxHealth;
            healthBarSlider.value = currentHealth;
        }

        currentHealth = maxHealth;
        if (spriteRenderer != null) spriteRenderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (FindObjectOfType<PopUp>() == null)
        {
            var canvas = FindObjectOfType<Canvas>();
            popDown = Instantiate(popUp, canvas.transform);
            PopUp.Instance.DataWrite(monsterName, speed, maxHealth);
        }
        else
        {
            popDown = FindObjectOfType<PopUp>().gameObject;
            if (popDown.activeSelf)
            {
                Destroy(popDown);
            }
            else
            {
                popDown.SetActive(true);
                PopUp.Instance.DataWrite(monsterName, speed, maxHealth);
            }
        }
    }
    public void Initialize(MonsterData data)
    {
        monsterName = data.name;
        grade = data.grade;
        speed = data.speed;
        maxHealth = data.maxHealth;
        currentHealth = maxHealth;

        if (healthBarSlider == null)
        {
            healthBarSlider = FindObjectOfType<Slider>();
            if (healthBarSlider == null)
            {
                Debug.LogWarning("HealthBarSlider not found in Initialize.");
            }
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogWarning("SpriteRenderer not found in Initialize.");
            }
        }

        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = maxHealth;
            healthBarSlider.value = currentHealth;
        }

        player = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player != null && !isDead)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.arrivePosition.position, speed * Time.deltaTime);
        }
        if (transform.position == GameManager.Instance.arrivePosition.position)
        {
            anim.SetBool("Encount", true);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarSlider.value = currentHealth;

        StartCoroutine(DamageEffect());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator DamageEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    void Die()
    {
        transform.position = targetPosition.position;
        isDead = true;
        anim.SetBool("Encount", false);
        spriteRenderer.color = Color.white;
        OnDeath?.Invoke(gameObject);
    }
}