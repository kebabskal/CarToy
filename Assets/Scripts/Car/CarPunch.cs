using System;
using System.Collections;
using UnityEngine;

public class CarPunch : CarBehaviour {
	public float Interval = 1f;
	public float Duration = 0.25f;
	public float Force = 50f;
	public CarAnimator CarAnimator;
	public ParticleSystem Effect;

	float lastPunch = -999f;
	
	void Update() {
		bool readyToPunch = Time.time - lastPunch > Interval;
		if (Car.InputPunch && readyToPunch)
			StartCoroutine(Punch());
	}

	IEnumerator Punch() {
		lastPunch = Time.time;
		
		Car.Rigidbody.useGravity = false;
		Car.Rigidbody.drag = 10f;
		Car.SetVelocity(Vector3.zero);
		CarAnimator.DesiredRotationEuler = new Vector3(-50, 0, 0);
		CarAnimator.DesiredScale = new Vector3(1.5f, 0.5f, 1.5f);
		
		yield return new WaitForSeconds(0.125f);
		
		Car.SetVelocity(Car.transform.forward * Force);
		
		CarAnimator.DesiredScale = new Vector3(0.5f, 0.5f, 1.5f);
		Instantiate(Effect, transform.position + Vector3.up, transform.rotation);
		
		yield return new WaitForSeconds(Duration);
		Car.Rigidbody.drag = 0f;
		
		Car.Rigidbody.useGravity = true;
		CarAnimator.DesiredRotationEuler = Vector3.zero;
		
		lastPunch = Time.time;
	}
}
