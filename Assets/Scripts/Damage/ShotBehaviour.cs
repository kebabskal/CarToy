using UnityEngine;

class ShotBehaviour : MonoBehaviour
{
	public virtual void Awake()
	{
		Shot = GetComponent<Shot>();
	}

	public Shot Shot { get; private set; }

	protected virtual void Start() { }
}
