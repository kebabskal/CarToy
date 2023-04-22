using System;
using UnityEngine;

public class CarGun : CarBehaviour {
	public float Interval = 0.1f;
	public float Force = 30f;
	public Rigidbody ShotPrefab;
	public ParticleSystem MuzzleParticles;
	
	float lastShot = -99f;

	void Update() {
		var readyToShoot = Time.time - lastShot >= Interval;
		if (Car.InputShoot && readyToShoot)
			Shoot();
	}

	void Shoot() {
		// Fire!
		var shot = Instantiate(
			ShotPrefab,
			transform.position + transform.forward,
			Quaternion.LookRotation(transform.forward)
		);

		shot.AddForce(shot.transform.forward * Force, ForceMode.Impulse);
		lastShot = Time.time;
		
		MuzzleParticles.Emit(10);
	}
}
