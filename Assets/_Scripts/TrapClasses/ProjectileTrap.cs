using UnityEngine;
using System.Collections;

public class ProjectileTrap : Trap {
	
	public float fireRate = 4f;
	public GameObject projectile;
	public float shootingPower = 6f;

	public float cooldownTime = 0.1f;
	private float lastTimeFired = 0f;
	private bool coolingDown = false;

	private bool spawned = true;

	void Start(){
		gameManEffect.manipulateAnimationSystem = false;
	}

	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();

		if(spawned){
			if(gameManEffect.timeSpendInSeconds > 1.5f){
				spawned = false;
				animator.Play("Shoot");
			}
		}

		animator.speed = allAroundSpeed.allAroundSpeed * fireRate;

		if(coolingDown){
			if(lastTimeFired + cooldownTime < gameManEffect.timeSpendInSeconds){
				coolingDown = false;
				animator.Play("Shoot");
			}
		}
	}

	void StartCooldown(){
		coolingDown = true;
		lastTimeFired = gameManEffect.timeSpendInSeconds;
	}

	void Fire(){
		GameObject bullet;
		// - (new Vector3(Mathf.Cos((transform.eulerAngles.z + 90) / 180 * Mathf.PI),Mathf.Sin((transform.eulerAngles.z + 90) / 180 * Mathf.PI), 0) / 10f)
		bullet = Instantiate (projectile, transform.position, transform.rotation) as GameObject;
		bullet.GetComponent<Projectile>().speed = shootingPower;
	}
}
