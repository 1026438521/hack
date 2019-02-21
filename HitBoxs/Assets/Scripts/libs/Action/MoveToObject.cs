using UnityEngine;
using System.Collections;

public class MoveToObject : Action {

	private GameObject _targetObject;
	private float _time;
	private float speed;
	private Vector3 temPos;
	private float friction;
	private Vector3 startPos;
	private float startTime;
	private bool _b_Move = false;
	void Start () {
		
	}
	
	public override void OnUpdate() // 重新实现了虚函数   
    {
        if(!_b_Move) return;
        
        temPos = _targetObject.transform.position;
        float distance = Utils.pGetDistance(startPos, temPos);
        Debug.Log("distance=====" + distance);
        if(distance <= 10)
        {
        	_b_Move = false;
        	return;
        }
        friction = (Time.time - startTime) * speed / distance;
        transform.position = Vector3.Lerp(startPos, temPos, friction);
        
    }

	public void init(GameObject targetObject, float time)
	{
		if(targetObject == null)
		{
			return;
		}
		_b_Move = true;
		_targetObject = targetObject;
		_time = time;
		speed = Utils.pGetDistance(transform.position, targetObject.transform.position) / time;
		startTime = Time.time;
		startPos = transform.position;
	}

}
