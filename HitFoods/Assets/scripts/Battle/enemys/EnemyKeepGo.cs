using UnityEngine;
using System.Collections;

public class EnemyKeepGo : BaseEnemy {

	
	private Rigidbody rigidbody;
	
	private float speed = 4;
	void Start () {
		getStartPos();

	}
	
	void getStartPos()
	{
		UpdateSpeed(speed);
		MoveLeftRightRandomMoveDir();
	}

	void Update () 
	{
		updatePosition();
	}

	void updatePosition()
	{
		forceData = _Transform.position;
		forceData.x += _speed * Time.deltaTime;
		_Transform.position = forceData;
	}

}








