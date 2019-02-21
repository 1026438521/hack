using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BallteGameState
{
	None = 1,
	Gaming,
	Deading
}

public class BattleManeger : MonoBehaviour {
	//添加挂在控件
	private BallteGameState _GameState = BallteGameState.None;
	private BoxsMoveController _BoxsMoveController;

	public static  BattleManeger Instance;
	private GameObject tempGameObject;
	//创建之后只会执行一次
	void Awake()
	{
		Instance = this;
		Values.init();
		_BoxsMoveController = gameObject.AddComponent<BoxsMoveController> ();
		
		EventDispatcher.Instance.AddEventListener("OnStartGame", OnStartGame);
		EventDispatcher.Instance.AddEventListener("onTouchStart", onTouchStart);
		EventDispatcher.Instance.AddEventListener("OnCreateStartBox", OnCreateStartBox);
	}

	//onCreateNewBoxs
	public void OnCreateStartBox(object data)
	{
		TimerManager.Instance.Invoke (OnCallBack, 0.03f, 10);
	}

	void OnCallBack(object data)
	{
		onCreateNewBoxs ();
		//Debug.Log("1111111");
	}
	public void OnStartGame(object data)
	{
		Debug.Log("OnStartGame");
		_GameState = BallteGameState.Gaming;
		_BoxsMoveController.OnStart();
		BattleTempData.Instance.gameState = GameState.Gaming;
	}

	public BallteGameState GetGameState()
	{
		return _GameState;
	}

	void Update () {
		if(_GameState == BallteGameState.Gaming)
		{
			
		}
		TimerManager.Instance.onTimerUpdate();//时间总控
	}

	void FixedUpdate()
	{
		//Debug.Log("_GameState===" + _GameState);
		if(_GameState == BallteGameState.Gaming)
		{
			checkDead();//检测死亡
			onCreateNewBoxs();//持续创建新的box
			_BoxsMoveController.OnFixedUpdate();
		}
		TimerManager.Instance.onTimerFixedUpdate();//时间总控
	}

    void onTouchStart(object data)
    {
    	float posX = (float)data;
    	int touchIndex = Utils.getTouchIndex(posX);
    	GameObject item = BattleFactory.Instance.createBoxAtIndex(touchIndex);//创建一个飞行box

		MovingBox movingBox = item.GetComponent<MovingBox> ();
		movingBox.startFly();
    }

	//创建新的组
	void onCreateNewBoxs()
	{
		List<GameObject> groupsObj = BattleTempData.Instance.groupsObj;
		int count = groupsObj.Count;

		if(count == 0)//没有box，那么创建一组
		{
			BattleFactory.Instance.createBoxsRandomOneUnVisiable(Values.BoxStartPosY);
		}else
		{
			BattleFactory.Instance.createNewBoxs();
		}
	}

	void checkDead()
	{
		List<GameObject> groupsObj = BattleTempData.Instance.groupsObj;
		if(groupsObj.Count > 0 && groupsObj[0] && groupsObj[0].transform.position.y <= Values.CameraBottomY)//最下面的到底，代表死亡
		{
			Debug.Log("fail ____----------");
			BattleTempData.Instance.WriteMaxScore();
			_GameState = BallteGameState.Deading;
			TimerManager.Instance.Clear();
			BattleTempData.Instance.gameState = GameState.Deaded;

			playerDeadAnimation();
			TimerManager.Instance.Invoke (OnPlayerAniOver, 1f);
		}
	}

	void OnPlayerAniOver(object data)
	{
		GameUIUtil.Instance.ShowView(WindowID.WindowID_Home);
		GameUIUtil.Instance.RemoveView(WindowID.WindowID_Battle);
		BattleFactory.Instance.OnClearAllBoxs();
		BattleTempData.Instance.Clear();
		EventDispatcher.Instance.InvokeEvent("OnCreateStartBox");
	}

	void playerDeadAnimation()
	{
		Debug.Log("playerDeadAnimation");
		Transform boxs = ResourceData.Instance.boxs.transform;
		//boxs整体移动出一格来,向上
		float addDis = Values.HeightInterval + Values.BoxHeight;
		Vector3 targetPos = new Vector3(boxs.position.x, boxs.position.y + addDis * 2.1f, boxs.position.z);
		MoveTo moveTo = boxs.gameObject.AddComponent<MoveTo> ();
		moveTo.moveToPos(targetPos, 0.5f).start();
		playerFlash();
	}

	void playerFlash()
	{
		List<GameObject> groupsObj = BattleTempData.Instance.groupsObj;
		tempGameObject = groupsObj [0];
		StartFlash();
		TimerManager.Instance.Invoke (CallBack, 0.1f, 7);
	}

	void StartFlash()
	{
		//Debug.Log("tempGameObject.activeSelf===" + tempGameObject.activeSelf);
		if(tempGameObject.activeSelf)
		{
			tempGameObject.SetActive (false);
		}else
		{
			tempGameObject.SetActive (true);
		}
	}

	void CallBack(object data)
	{
		StartFlash ();
	}
}










