using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	private GameObject UIHome, UIBattle;
	void Start () {
		UIHome = transform.Find("UIHome").gameObject;
		UIBattle = transform.Find("UIBattle").gameObject;
		
		UIHome.SetActive (true);
		UIBattle.SetActive (false);

		NotificationCenter.DefaultCenter().AddObserver(this, "onStartBattle");
		NotificationCenter.DefaultCenter().AddObserver(this, "onBackHome");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onStartBattle(Notification notification)
	{
		UIHome.SetActive (false);
		UIBattle.SetActive (true);
		GameManager.Instance.startGame();
		GameManager.Instance.uiState = UIState.E_UI_Battle;
	}

	void onBackHome(Notification notification)
	{
		UIHome.SetActive (true);
		UIBattle.SetActive (false);
		GameManager.Instance.uiState = UIState.E_UI_Home;
	}
}
