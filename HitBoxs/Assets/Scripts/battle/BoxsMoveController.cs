using UnityEngine;
using System.Collections;

public enum BoxsMoveState
{
	Move_normal = 1,

};

public class BoxsMoveController : MonoBehaviour {

	// private float _MoveSpeed = 0.1f;
	private float _MoveSpeed = 0.015f;
	private float _baseMoveSpeed = 0.015f; //基础移动速度
	// private float _baseMoveSpeed = 0.0f; //基础移动速度
	private float _addSpeedForHard = 0.01f; //随时时间，增加移动速度
	private float _addSpeedTimeForClearBox = 0.1f;
	private float _addSpeedForClearBox = 0.1f;
	private BoxsMoveState boxsMoveState = BoxsMoveState.Move_normal;
	void Start () {
		EventDispatcher.Instance.AddEventListener("onAddSpeedForClearBox", onAddSpeedForClearBox);
		EventDispatcher.Instance.AddEventListener("onUpdateMoveSpeed", onUpdateMoveSpeed);
	}

	public void OnStart()
	{
		_baseMoveSpeed = _MoveSpeed;
	}

	public void OnFixedUpdate()
	{
		onBoxsGameObjectMove();
	}

	void onUpdateMoveSpeed(object data)
	{
		if(BattleTempData.Instance.score > 300f)
		{
			_MoveSpeed = 0.05f;
			BattleTempData.Instance.speed = "5x";
		}
		else if(BattleTempData.Instance.score > 200f)
		{
			_MoveSpeed = 0.04f;
			BattleTempData.Instance.speed = "4x";
		}
		else if(BattleTempData.Instance.score > 100f)
		{
			_MoveSpeed = 0.03f;
			BattleTempData.Instance.speed = "3x";
		}else if(BattleTempData.Instance.score > 50f)
		{
			_MoveSpeed = 0.015f;
			BattleTempData.Instance.speed = "2x";
		}else{
			_MoveSpeed = 0.01f;
			BattleTempData.Instance.speed = "1x";
		}
	}

	//向下移动
	void onBoxsGameObjectMove()
	{
		//Debug.Log("onBoxsGameObjectMove-----");
		Vector3 pos = ResourceData.Instance.boxs.transform.position;

		if(boxsMoveState == BoxsMoveState.Move_normal)
		{
			pos.y -= _baseMoveSpeed;
			ResourceData.Instance.boxs.transform.position = pos;
		}
	}

	//清理box之后，加速移动，如果剩余组数比较小，那么应该移动的时间应该比较长，长到能够到达
	void onAddSpeedForClearBox(object data)
	{
		float time = getTimeForClearBox((float)data);
		TimerManager.Instance.addTimeAction(time * Time.deltaTime, onAddSpeedForClearBoxTimeOver);
	}

	void onAddSpeedForClearBoxTimeOver(object data)
	{
		_baseMoveSpeed = _MoveSpeed;
	}
	
	//考虑到了要到达目的地的问题
	float getTimeForClearBox(float posYBeforeDestroy)
	{

		float nowBottomPosY = BattleTempData.Instance.getBottomPosY();
		float moveToPosY = posYBeforeDestroy + Values.BoxHeight * 0.5f;
		float time = 0;
		if(moveToPosY > Values.BoxsStopPosY)
		{
			_baseMoveSpeed += _addSpeedForClearBox;
			moveToPosY =  Values.BoxsStopPosY;
			_baseMoveSpeed *= 2; //速度翻倍
			time = Mathf.Abs(moveToPosY - nowBottomPosY) / _baseMoveSpeed;
		}else
		{
			_baseMoveSpeed += _addSpeedForClearBox;
			time = Mathf.Abs(moveToPosY - nowBottomPosY) / _baseMoveSpeed;
			// _baseMoveSpeed = 0;
			// time = 0.1f;
		}
		
		return time;
	}

}
