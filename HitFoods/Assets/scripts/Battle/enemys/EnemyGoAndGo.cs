using UnityEngine;
using System.Collections;

//走，停走 index = 2
public class EnemyGoAndGo : BaseEnemy {

	private enum EnemyState
	{
		start = 0,
		wait,
		go
	};

	private Transform startTransform;

	private bool isStartGo = false;
	private float startTime = 0f;
	private float startTimeMin = 0.5f;
	private float startTimeMax = 0.85f;
	private float waitTime = 1f;
	private float speed = 7;
	private EnemyState state = EnemyState.start;
	void Start () 
	{
		startTime = Random.Range(startTimeMin, startTimeMax);
		getStartPos();
	}
	void getStartPos()
	{
		UpdateSpeed(speed);
		MoveLeftRightRandomMoveDir();
	}

	void Update () {
		if(state == EnemyState.start)
		{
			startTime = startTime - Time.deltaTime;
			if(startTime < 0)
			{
				state = EnemyState.wait;
			}else
			{
				updatePosition();
			}
		}else if(state == EnemyState.wait)
		{
			waitTime = waitTime - Time.deltaTime;
			if(waitTime < 0)
			{
				state = EnemyState.go;
			}
		}else if(state == EnemyState.go)
		{
			updatePosition();
			_speed *= 1.05f;
		}
	}

	void updatePosition()
	{
		
		forceData = _Transform.position;
		forceData.x += _speed * Time.deltaTime;
		//Debug.Log("forceData.x == " + forceData.x);
		_Transform.position = forceData;
	}
}






