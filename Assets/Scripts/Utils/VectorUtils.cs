using UnityEngine;

public static class VectorUtils {
	public static Vector3 SetX(this Vector3 v, float x) {
		v.x = x;
		return v;
	}	
	
	public static Vector3 SetY(this Vector3 v, float y) {
		v.y = y;
		return v;
	}
	
	public static Vector3 SetZ(this Vector3 v, float z) {
		v.z = z;
		return v;
	}
	
	public static Vector3 LimitLength(this Vector3 v, float maxLength) {
		if (v.magnitude > maxLength)
			return v.normalized * maxLength;

		return v;
	}
	
	public static Vector3 Damp(Vector3 source, float factor, float deltaTime) {
		return source * Mathf.Pow(factor, deltaTime);
	}
}
