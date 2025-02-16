using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
	public ParticleSystem SprinkleBurst;

   void OnTriggerEnter2D(Collider2D other)
   {
	   PlayerController controller = other.GetComponent<PlayerController>();

	   if (controller != null && controller.health < controller.maxHealth)
	   {
		   controller.ChangeHealth(1);
		   Destroy(gameObject);
		   Instantiate(SprinkleBurst.gameObject);
		   
	   }
   }
}