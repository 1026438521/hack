using UnityEngine;
using System.Collections;

public enum UIState
{
	E_UI_Home = 1,
	E_UI_Battle
};
public class GameManager : Singleton<GameManager> {
// public class GameManager : MonoBehaviour {
	public float score
	{
		get;
		set;
	}
	public float getScore()
	{
		return score;
	}
	public void addScore()
	{
		score += 1f;
	}
	public UIState uiState
	{
		get;
		set;
	}
	
	void Start () {
		uiState = UIState.E_UI_Home;
		score = 0;
	}

	public void startGame()
	{
		clear();
		NotificationCenter.DefaultCenter().PostNotification(this, "onUpdateUIBattleView");
		EnemySpawn.Instance.createNestEnemy();
	}
	void Update () {
	
	}
	
	public void clear()
	{
		score = 0f;
		EnemySpawn.Instance.clear();
		NotificationCenter.DefaultCenter().PostNotification(this, "onResetCube");
		
	}

}
