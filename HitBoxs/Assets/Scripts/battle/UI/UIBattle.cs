using UnityEngine;
using System.Collections;

public class UIBattle : MonoBehaviour {

	// Use this for initialization
	UISprite battle_view;
	UISprite battleViewSpeed;
	UILabel speedLabel;
	UILabel scoreLable;

	void Awake()
	{
		initView ();
		EventDispatcher.Instance.AddEventListener("onUpdateScoreView", onUpdateScoreView);
		EventDispatcher.Instance.AddEventListener("onUpdateSpeedView", onUpdateSpeedView);
		EventDispatcher.Instance.AddEventListener("OnShowHomeView", OnShowHomeView);
		EventDispatcher.Instance.AddEventListener("OnStartGame", OnStartGame);
	}

	void OnDestroy()
	{
		EventDispatcher.Instance.RemoveEventListener("onUpdateScoreView", onUpdateScoreView);
		EventDispatcher.Instance.RemoveEventListener("onUpdateSpeedView", onUpdateSpeedView);
		EventDispatcher.Instance.RemoveEventListener("OnShowHomeView", OnShowHomeView);
		EventDispatcher.Instance.RemoveEventListener("OnStartGame", OnStartGame);
	}
	
	void Start () {

	}

	void initView()
	{
		Transform speedView = transform.Find("speedView");
		battle_view = speedView.Find ("battle_view").GetComponent<UISprite>();
		battleViewSpeed = speedView.Find ("battleViewSpeed").GetComponent<UISprite>();
		speedLabel = speedView.Find ("speedLabel").GetComponent<UILabel>();

		Transform scoreView = transform.Find("scoreView");
		scoreLable = scoreView.Find ("scoreLable").GetComponent<UILabel>();
	}

	public void OnShowHomeView(object data)
	{
		gameObject.SetActive(false);
	}
	public void OnStartGame(object data)
	{
		
		gameObject.SetActive(true);
	}
	public void onUpdateScoreView(object data)
	{
		int score = BattleTempData.Instance.score;
		scoreLable.text = score.ToString ();
	}

	public void onUpdateSpeedView(object data)
	{
		string speed = BattleTempData.Instance.speed;
		speedLabel.text = speed;
	}
}
