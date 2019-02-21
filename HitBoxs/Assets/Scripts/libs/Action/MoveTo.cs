using UnityEngine;
using System.Collections;

//移动状态
public enum MoveState
{
	Move_Null = 0,
	Move_To_Pos,
	Move_To_Object
};

public delegate void EventHandlerParameter(object data);
public delegate void EventHandler();
public class MoveTo : Action {

	//public

	private MoveState _MoveState = MoveState.Move_Null;
	private Vector3 _startPos;//开始位置
	private float _moveSpeed; //移动速度
	private float _startTime;
	private EventHandler _EventHandler;
	private GameObject _targetObject;
	private float _time;

	private Vector3 temPos;
	private float friction;
	
	//moveTo gameobject

	//move to pos
	private Vector3 _targetPos;//目标位置
	private float _distance;//距离
	private Transform myTransform;
	//move to pos
	void Start () {
		
	}
	
	public override void OnUpdate() // 重新实现了虚函数   
    {
	    if(_MoveState == MoveState.Move_To_Pos)
	    {
	        handleMoveToPos();
	    }else if(_MoveState == MoveState.Move_To_Object)
	    {
	        handleMoveToObj();
	    }
    }

    void handleMoveToPos()
    {
    	if(checkMoveOver())
    	{
			myTransform.position = _targetPos;
			stopTask ();
    		//Debug.Log("moveover");
			Destroy (gameObject.GetComponent<MoveTo>());
			if(_EventHandler != null)
			{
				_EventHandler ();
			}
    		return;
    	}
    	float step = _moveSpeed * Time.deltaTime; 
    	//Debug.Log("step ===" + step);
		//transform.position = Vector3.Lerp(transform.position, _targetPos, step);
		temPos.x = myTransform.position.x;
		temPos.y = myTransform.position.y + step;
		temPos.z = myTransform.position.z;

		transform.position = temPos;
    }

    //检查是否移动到相应位置
    bool checkMoveOver()
    {
		//Debug.Log ("myTransform.position===" + myTransform.position);
		//Debug.Log ("_targetPos===" + _targetPos);
		return (myTransform.position - _startPos).sqrMagnitude > (_targetPos - _startPos).sqrMagnitude;
    }

    void handleMoveToObj()
    {
    	temPos = _targetObject.transform.position;
		float distance = Utils.pGetDistance(_targetPos, temPos);
        friction = (Time.time - _startTime) * _moveSpeed / distance;
		transform.position = Vector3.Lerp(_targetPos, temPos, friction);
    }

    public MoveTo moveToPos(Vector3 targetPos, float time)
    {
		return moveToPos(targetPos, time, null);
    }

	public MoveTo moveToPos(Vector3 targetPos, float time, EventHandler handler)
	{
		myTransform = gameObject.transform;
		_MoveState = MoveState.Move_To_Pos;
		_startPos = myTransform.position;
		_targetPos = targetPos;
		_distance = Utils.pGetDistance(_startPos, _targetPos);
		_moveSpeed = _distance / time;
		_EventHandler = handler;
		return this;
	}
    public void start()
    {
    	TimerManager.Instance.AddActionTask(this);
    }
	public void init(GameObject targetObject, float time)
	{
		if(targetObject == null)
		{
			return;
		}

		_targetObject = targetObject;
		_time = time;
		_moveSpeed = Utils.pGetDistance(transform.position, targetObject.transform.position) / time;
		_startTime = Time.time;
		_targetPos = transform.position;
	}

	public override void stopAction() // 重新实现了虚函数   
	{
		stopTask();
	}
	
    public void stopTask()
    {
		TimerManager.Instance.removeActionTask(this);
    }
}
