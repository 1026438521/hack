using UnityEngine;
using System.Collections;

//加速一下停一下再加速 index = 4
public class EnemyBufferAcceleration : BaseEnemy {

	public Transform startTransform;

	private Rigidbody rigidbody;
	private Vector3 forceData = new Vector3(5, 0, 0);
	private bool isStartGo = false;
	private float slowTime = 0.7f;
	private float currentTime = 0f;
	private float speed = 6;
	void Start () {
		startTransform = ResourcesManager.Instance.getPosObjectByName(BattleConfig.pos_left).transform;
		transform.position = startTransform.position;
		rigidbody = transform.GetComponent<Rigidbody>();
	}
	
	void Update () {
		currentTime += Time.deltaTime;
		if(currentTime > slowTime)
		{
			currentTime = 0;
			forceData = rigidbody.velocity;
			forceData.x = speed;
			rigidbody.velocity = forceData;
		}
	}

}
