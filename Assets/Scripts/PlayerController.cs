using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    //MOVEMENT
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 3.0f;
    //HEALTH
    public int maxHealth = 5;
    public int health { get {return currentHealth; }}
    int currentHealth = 1;
    //INVINCIBILITY
    public float timeIncincible = 2.0f;
    bool isInvincible;
    float damageCooldown;


    // Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }
    
    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * 3.0f * Time.deltaTime;
        rigidbody2d.MovePosition(position);
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }
    }

    public void ChangeHealth (int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeIncincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

}