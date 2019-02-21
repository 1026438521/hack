using UnityEngine;
using System.Collections;

public class UIHome : MonoBehaviour {

	private UILabel score, best_score;
	private GameObject BtnStart;
	
	void Start () {
		BtnStart = transform.Find("BtnStart").gameObject;
		score = transform.Find ("score").GetComponent<UILabel> ();
		best_score = transform.Find ("best_score").GetComponent<UILabel> ();
		UIEventListener.Get (BtnStart).onClick = OnButtonClick;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void updateView()
	{
		
	}
	void OnButtonClick(GameObject obj)
	{
		NotificationCenter.DefaultCenter().PostNotification(this, "onStartBattle");
	}
}
