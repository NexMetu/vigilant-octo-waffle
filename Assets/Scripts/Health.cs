using UnityEngine;

public class Health : MonoBehaviour {
	public const int maxHealth =  100;
	public int currentHealth = maxHealth;


	public void TakeDamge(int amount){

		currentHealth -= amount;
		if(currentHealth <=0) {
			currentHealth = 0;
			Debug.Log ("Dead");
			//Destroy (this.gameObject);
		}
	}
}