using UnityEngine;
using System.Collections;
using DG.Tweening;
//掉下来，然后走 index = 3
public class EnemyDropAndGo : BaseEnemy {

	private Transform startTransform;
	private Rigidbody rigidbody;
	private Vector3 forceData = new Vector3(3, 0, 0);
	private bool isStartGo = false;
	private float goTime = 2f;
	private float speed = 6;
	void Start () 
	{
		getStartPos();
	}

	void getStartPos()
	{
		UpdateSpeed(speed);
		MoveTopLeftRightRandomMoveDir();
		Invoke ("updatePosition", 1);
	}

	void Update () {
		if(isStartGo == false)
		{
			return;
		}
		updatePosition();
	}

	void updatePosition()
	{
		isStartGo = true;
		forceData = _Transform.position;
		forceData.x += _speed * Time.deltaTime;
		_Transform.position = forceData;
	}
}
