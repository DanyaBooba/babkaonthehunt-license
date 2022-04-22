using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyHealth uiHealth;
    
    [Header("Health")]
    [SerializeField] private float health = 1f;

    [Header("Another")]
    [SerializeField] private GameObject effect;
    [SerializeField] private float damage = 1f;

    private bool dead;
    private float transperent = 1f;
    private float speedDead = 1.2f;

    private SpriteRenderer sprite;
    private CapsuleCollider2D collider;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();

        uiHealth = GetComponent<EnemyHealth>();
        if(uiHealth != null)
            uiHealth.HealthUIInit((int) health);
    }

    public void Damage(float value)
    {
        ChangeHealth(-value);
    }

    public float ReturnDamage()
    {
        return damage;
    }

    private void ChangeHealth(float value)
    {
        health += value;

        if(health <= 0f)
            Dead();
        
        if(uiHealth != null)
            uiHealth.HealthUIUpdate((int) health);
    }

    private void Dead()
    {
        dead = true;
        collider.enabled = false;
        Effects();
    }
    
    private void Effects()
    {
        if (effect != null)
            Instantiate(effect, transform.position, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        if (dead)
        {
            if (transperent > 0)
            {
                transperent -= speedDead * Time.deltaTime;
                sprite.color = ColorTransperent(transperent);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private Color ColorTransperent(float transperent)
    {
        Color color = Color.white;
        color.a = transperent;
        return color;
    }
}
