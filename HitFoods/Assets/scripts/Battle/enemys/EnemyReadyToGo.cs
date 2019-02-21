using UnityEngine;
using System.Collections;

//掉下来，向左走，再向右加速 index = 5
public class EnemyReadyToGo : BaseEnemy {

	private enum EnemyState
	{
		droping = 1,
		back,
		wait,
		go
	};
	public Transform startTransform;
	public Transform LeftTransform;
	private Rigidbody rigidbody;
	private Vector3 forceData = new Vector3(3, 0, 0);
	private EnemyState state = EnemyState.droping;
	private float dropingTime = 1f;
	private float backTime = 0.5f;
	private float waitTime = 0.5f;
	void Start () {
		startTransform = ResourcesManager.Instance.getPosObjectByName(BattleConfig.pos_left).transform;
		transform.position = startTransform.position;
		rigidbody = transform.GetComponent<Rigidbody>();
	}
	
	void Update () {
		if(state == EnemyState.droping)
		{
			dropingTime = dropingTime - Time.deltaTime;
			if(dropingTime < 0)
			{
				state = EnemyState.back;
				//iTween.MoveTo(this.gameObject, LeftTransform.position, backTime);
			}
		}else if(state == EnemyState.back)
		{
			
			backTime = backTime - Time.deltaTime;
			if(backTime < 0)
			{
				state = EnemyState.wait;
			}
		}else if(state == EnemyState.wait)
		{
			waitTime -= Time.deltaTime;
			if(waitTime < 0)
			{
				state = EnemyState.go;
			}
		}else if(state == EnemyState.go)
		{
			rigidbody.velocity = forceData;
		}
		
	}

}
