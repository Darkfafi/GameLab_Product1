using UnityEngine;
using System.Collections;

public class ProjectileTrap : Trap {
	
	public float fireRate = 0.5f;
	public GameObject projectile;
	private float lastTimeFired = 0f;
	public float shootingPower = 6f;
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
		if(lastTimeFired + fireRate < timeSpendInSeconds){
			lastTimeFired = timeSpendInSeconds;
			Fire();
		}
	}

	void Fire(){
		GameObject bullet;
		bullet = Instantiate (projectile, transform.position - (new Vector3(Mathf.Cos((transform.eulerAngles.z + 90) / 180 * Mathf.PI),Mathf.Sin((transform.eulerAngles.z + 90) / 180 * Mathf.PI), 0) / 2f), transform.rotation) as GameObject;
		bullet.GetComponent<Projectile>().speed = shootingPower;
	}
}
