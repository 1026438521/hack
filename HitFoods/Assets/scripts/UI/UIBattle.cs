using UnityEngine;
using System.Collections;

public class UIBattle : MonoBehaviour {

	private UILabel ScoreLabel;
	void Start () {
		ScoreLabel = transform.Find ("ScoreLabel").GetComponent<UILabel> ();
		NotificationCenter.DefaultCenter().AddObserver(this, "onUpdateUIBattleView");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void onUpdateUIBattleView(Notification notification)
	{
		float score = GameManager.Instance.getScore();
		ScoreLabel.text = score.ToString();
	}
}
