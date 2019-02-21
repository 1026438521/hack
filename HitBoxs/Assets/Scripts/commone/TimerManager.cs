using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ActionType
{
	ActionTime = 0,
	ActioMotion
}

public class TimeAction
{
	public ActionType type = ActionType.ActionTime;
	public float delay = 0; //时间
	public float startTime = 0; //开始时间
	public int times = 0; //执行次数
	public EventHandlerParameter callBack;
	public object data;
}

public class TimerManager : Singleton<TimerManager>
{
	private List<Action> _actions = new List<Action>(); //move
	private List<TimeAction> _timeActions = new List<TimeAction>();
	private List<TimeAction> _removeTimeActions = new List<TimeAction>();
	private int _actionCount = 0;
	public void onTimerUpdate()
	{
		handleUpdate();
	}

	public void onTimerFixedUpdate()
	{
		 handleUpdate();
	}

	void handleUpdate()
	{
		handleActioMotion();
		handleActionTime();
	}

	void handleActioMotion()
	{
		_actionCount = _actions.Count;
		for(int i = 0;i<_actionCount; i++)
		{
			_actions[i].OnUpdate();
		}
	}

	void handleActionTime()
	{
		_actionCount = _timeActions.Count;
		//Debug.Log("_actionCount===" +_actionCount );
		for(int i = 0;i<_actionCount; i++)
		{
			TimeAction action = _timeActions[i];
			if(Time.time - action.startTime >= action.delay)
			{
				action.callBack(action.data);
				action.startTime = Time.time;
				action.times --;
				if(action.times <= 0)
				{
					_removeTimeActions.Add(action);
				}
			}
		}

		_actionCount = _removeTimeActions.Count;
		for(int i = 0; i<_actionCount; i++)
		{
			_timeActions.Remove(_removeTimeActions[i]);
		}
		_removeTimeActions.Clear();
	}

	//添加一个任务
	public void AddActionTask(Action action)
	{
		_actions.Add(action);
	}

	public void removeActionTask(Action action)
	{
		_actions.Remove (action);
	}
	
	public void moveToObject(GameObject thisObject, GameObject targetObject, float time)
	{
		MoveToObject _MoveToObject = thisObject.AddComponent<MoveToObject> ();
		_MoveToObject.init(targetObject, time);
		_actions.Add(_MoveToObject);
	}

	public void addTimeAction(float time, EventHandlerParameter callBack)
	{
//		Debug.Log("addTimeAction   time == " + time);
		TimeAction action = new TimeAction();
		action.delay = time;
		action.startTime = Time.time;
		action.callBack = callBack;
		_timeActions.Add(action);
	}

	public void Invoke(EventHandlerParameter callBack, float delay, int times = 0, object data = null)
	{
		TimeAction action = new TimeAction();
		action.delay = delay;
		action.times = times;
		action.startTime = Time.time;
		action.callBack = callBack;
		action.data = data;
		_timeActions.Add(action);
	}

	public bool IsInvoking(EventHandlerParameter callBack)
	{
		_actionCount = _timeActions.Count;
		for (int i = 0; i < _actionCount; i++) 
		{
			TimeAction action = _timeActions [i];
			if(action.callBack == callBack)
			{
				return true;
			}
		}
		return false;
	}

	public void CancelInvoke(EventHandlerParameter callBack)
	{
		_actionCount = _timeActions.Count;
		for (int i = 0; i < _actionCount; i++) 
		{
			TimeAction action = _timeActions [i];
			if(action.callBack == callBack)
			{
				_timeActions.RemoveAt (i);
				return;
			}
		}
	}

	public void Clear()
	{
		_timeActions.Clear();
		_actions.Clear();
		_removeTimeActions.Clear();
	}
	
}
