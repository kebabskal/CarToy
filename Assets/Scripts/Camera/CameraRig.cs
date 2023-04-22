using System;
using UnityEngine;

public class CameraRig : MonoBehaviour {
	public Transform Follow;

	void LateUpdate() {
		transform.position = Follow.position;
	}
}
