using System;
using UnityEngine;

// Abstract base class for things referring to a Car
public abstract class CarBehaviour : MonoBehaviour {
	public Car Car { get; private set; }

	public virtual void OnEnable() {
		Car = GetComponentInParent<Car>();
	}
}
