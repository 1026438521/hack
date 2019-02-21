using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BoxMoveDir
{
	MoveToRight = 0,
	MoveToLeft,
	TopMoveToRight,
	TopMoveToLeft
};

public class ObjectFactory : Singleton<ObjectFactory> {

	private string FullPath = "prefab/";

	// public int[] enemysIndexList = {1};
	public int[] enemysIndexList = {2, 2, 3, 1, 2 ,1, 2, 3, 3}; //test
	private List<string> prefabDic = new List<string> ()
	{
		{"enemy/1_enemy_keepgo"},
		{"enemy/2_enemy_go_andgo"},
		{"enemy/3_enemy_dropandgo"},
		{"enemy/4_enemy_many"},
		//
		
	};
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject getObjectByName(string name)
	{
		GameObject enemy = Instantiate(ResourcesManager.Instance.getPrefabByName(name), 
			new Vector3(0, 0, 0), Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
		return enemy;
	}

	public GameObject GetObjectByIndex(int index)
	{
		string prefabName = prefabDic[index];
		prefabName = FullPath + prefabName;
		//Debug.Log("prefabName===" + prefabName);
		GameObject obj = Resources.Load<GameObject> (prefabName);
		GameObject enemy = Instantiate(obj, new Vector3(0, 0, 0), Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
		BaseEnemy _BaseEnemy = enemy.GetComponent<BaseEnemy> ();
		if(_BaseEnemy != null)
		{
			_BaseEnemy.index = index;
			_BaseEnemy.pathName = prefabName;
		}
		return enemy;
	}
	public void DestroyAllEnemy()
	{
		Transform parent = ResourcesManager.Instance.getObjectByName(BattleConfig.EnemysObject).transform;
		parent.DestroyChildren ();
	}
	public BoxMoveDir GetRandomMoveDir()
	{
		int dir = Random.Range(1, 3);
		return dir > 1 ? BoxMoveDir.MoveToRight : BoxMoveDir.MoveToLeft;
	}

	public BoxMoveDir GetRandomTopMoveDir()
	{
		int dir = Random.Range(1, 3);
		return dir > 1 ? BoxMoveDir.TopMoveToRight : BoxMoveDir.TopMoveToLeft;
	}
}
