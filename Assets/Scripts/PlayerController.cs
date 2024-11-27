using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  // Variables related to player character movement
  public InputAction MoveAction;
  Rigidbody2D rigidbody2d;
  Vector2 move;
  public float speed = 3.0f;


  // Variables related to the health system
  public int maxHealth = 5;
  public int currentHealth;
  public int health { get { return currentHealth; }}
  public GameManagerScript gameManager;

  private bool isDead;


  // Variables related to temporary invincibility
  public float timeInvincible = 2.0f;
  bool isInvincible;
  float damageCooldown;


   // Variables related to animation
  Animator animator;
  Vector2 lookDirection = new Vector2(1,0);
  public ParticleSystem HitFX;


  // Variables related to projectiles
  public GameObject projectilePrefab;
  public InputAction launchAction;
  public List<EnemyController> Enemies;
  public static PlayerController Player;

  // Peppermint Pickups
  public PeppermintPickup pp;
  public AudioSource PickupDing;




  // Start is called before the first frame update
  void Start()
  {
     MoveAction.Enable();
     rigidbody2d = GetComponent<Rigidbody2D>();
     animator = GetComponent<Animator>();

     currentHealth = maxHealth;
     Player = this;
  }
 
  // Update is called once per frame
  void Update()
  {
     move = MoveAction.ReadValue<Vector2>();


      if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y,0.0f))
        {
           lookDirection.Set(move.x, move.y);
           lookDirection.Normalize();
        }


     animator.SetFloat("Look X", lookDirection.x);
     animator.SetFloat("Look Y", lookDirection.y);
     animator.SetFloat("Speed", move.magnitude);


     if (isInvincible)
       {
           damageCooldown -= Time.deltaTime;
           if (damageCooldown < 0)
               isInvincible = false;
       }


     if(Input.GetKeyDown(KeyCode.C))
        {
           Launch();
        }

   }


// FixedUpdate has the same call rate as the physics system
  void FixedUpdate()
  {
     Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
     rigidbody2d.MovePosition(position);
  }


  public void ChangeHealth (int amount)
  {
    
     
  
     if (amount < 0)
       {
           if (isInvincible)
               return;
          
           isInvincible = true;
           damageCooldown = timeInvincible;
           animator.SetTrigger("Hit");
           var fx = Instantiate(HitFX.gameObject,transform);
           fx.transform.localPosition = new Vector3(0,0,0);
       }

     currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
     UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
     
     if (currentHealth <= 0 && !isDead)
       {
           isDead = true;
           gameObject.SetActive(false);
           gameManager.gameOver();   
           Debug.Log("ded");
       }
      
 


     
  }
  



  void Launch()
  {
     GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
     Projectile projectile = projectileObject.GetComponent<Projectile>();
     projectile.Launch(lookDirection, 300);


     animator.SetTrigger("Launch");
  }
  
  void OnTriggerEnter2D(Collider2D other)
  {
      if(other.gameObject.CompareTag("Peppermint"))
      {
         Destroy(other.gameObject);
         pp.pepCount++;
         PickupDing.Play();
      }
  }

}