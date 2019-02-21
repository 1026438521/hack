using UnityEngine;
using System.Collections;

public class UIHStartPage : MonoBehaviour {
	private Animator _Animator;

	public UILabel _ScoreParentLabel;
	public UILabel _ScoreLabel;
	public UILabel _MaxScoleLabel; 
	void Awake()
	{
		GameObject StartButton = transform.Find("Button").Find ("StartButton").gameObject;
		UIEventListener.Get (StartButton).onClick = OnStartButtonClick;
		_Animator = gameObject.GetComponent<Animator> ();
		//EventDispatcher.Instance.AddEventListener("UpdateHomeUI", UpdateHomeUI);
	}

	void OnDestroy()
	{
		//EventDispatcher.Instance.RemoveEventListener("UpdateHomeUI", UpdateHomeUI);
	}

	void Start () {
		if(BattleTempData.Instance.isFristGame == false)
		{
			//_Animator.Play("HomeIn");
		}else
		{
			BattleTempData.Instance.UpdateData();
			EventDispatcher.Instance.InvokeEvent("OnCreateStartBox");
		}
		
		BattleTempData.Instance.isFristGame = false; //不是第一次进游戏，不需要初始化一些数据
		UpdateHomeUI(null); //获得分数，显示分数
		BattleTempData.Instance.ClearData();  //清空分数
	}

	void UpdateHomeUI(object data)
	{
		_ScoreLabel.text = BattleTempData.Instance.score.ToString();
		_MaxScoleLabel.text = BattleTempData.Instance.maxScore.ToString();
	}

	void OnStartButtonClick(GameObject obj)
	{
		EventDispatcher.Instance.InvokeEvent("OnStartGame");
		GameUIUtil.Instance.ShowView(WindowID.WindowID_Battle);
		// _Animator.speed = 1;
		// _Animator.Play ("HomeOut");
		GameUIUtil.Instance.RemoveView(WindowID.WindowID_Home);
	}

	public void OnAniCallBack()
	{
		Debug.Log("OnAniCallBack====");
		//Destroy (this.gameObject);
		//_Animator.
		GameUIUtil.Instance.RemoveView(WindowID.WindowID_Home);
		AdsManager.Instance.ShowAd ();
	}

	public void OnHomeInAniCallBack()
	{
		// _Animator.Stop();
		//_Animator.Play("NoneAni");
	}
}
