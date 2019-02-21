using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState
{
	Home = 0,
	Gaming,
	Deaded
}

public class BattleTempData : Singleton<BattleTempData>{

	public List<GameObject> groupsObj = new List<GameObject>();//组object

	public int score = 0; //分数

	public int maxScore = 0; //最高分
	public string speed = ""; //速度

	public GameState gameState = GameState.Home;

	public bool isFristGame = true; //是否是第一次进入游戏
	public float getBottomPosY()
	{
		if(groupsObj.Count > 0)
		{
			return groupsObj[0].transform.position.y - Values.BoxHeight * 0.5f;
		}
		return 0f;
		
	}

	public void ClearData()
	{
		score = 0;
	}
	
	public void UpdateData()
	{
		maxScore = ReadMaxScore();
	}
	public void Clear()
	{
		groupsObj.Clear();
		gameState = GameState.Home;
		TimerManager.Instance.Clear();
		//EventDispatcher.Instance.Clear();
	}

	public void WriteMaxScore()
	{
		int MaxScore = ReadMaxScore();
		if(score > MaxScore)
		{
			maxScore = score;
			PlayerPrefs.SetInt("MaxScore", score); 
		}
	}

	public int ReadMaxScore()
	{
		bool hasKeyInt = PlayerPrefs.HasKey("MaxScore");  
		if(hasKeyInt)
		{
			return PlayerPrefs.GetInt("MaxScore");
		}
		 return 0;
	}
}











