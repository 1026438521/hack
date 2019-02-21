using UnityEngine;
using System.Collections;

public class EnemySpawn : Singleton<EnemySpawn> {

	
	private int currentIndex = 0;
	private int enemyIndex = 0;
	
	
	private int enemyNum = 0;
	void Start () {
		enemyNum = ObjectFactory.Instance.enemysIndexList.Length;
	}
	
	void Update () {
	
	}

	public int getEnemyIndex()
	{
		return enemyIndex;
	}

	public GameObject createNestEnemy()
	{
		// int index = Random.Range(1, enemyNum);
		//enemyIndex += 1;
		
		if(currentIndex >= enemyNum)
		{
			currentIndex = 0;
		}
		int index = ObjectFactory.Instance.enemysIndexList[currentIndex];
		//Debug.Log("index = " + index);
		currentIndex += 1;
		return CreateEnemyByIndex(index);
//		GameObject enemy = ObjectFactory.Instance.getObjectByName(BattleConfig.EnemyPrefabName);
		
//		switch(index)
//		{
//			case 1:
//				enemy.transform.gameObject.AddComponent<EnemyKeepGo> ();
//				break;
//			case 2:
//				enemy.transform.gameObject.AddComponent<EnemyGoAndGo> ();
//				break;
//			case 3:
//				enemy.transform.gameObject.AddComponent<EnemyDropAndGo> ();
//				break;
//			case 4:
//				enemy.transform.gameObject.AddComponent<EnemyBufferAcceleration> ();
//				break;
//			case 5:
//				enemy.transform.gameObject.AddComponent<EnemyMany> ();
//				break;
//		}
		//return enemy;
	}

	public GameObject CreateEnemyByIndex(int index)
	{
		GameObject enemy = ObjectFactory.Instance.GetObjectByIndex(index);
		enemy.transform.parent = ResourcesManager.Instance.getObjectByName(BattleConfig.EnemysObject).transform;
		return enemy;
	}
	public void stopAllEnemys()
	{
		GameObject enemysObject = ResourcesManager.Instance.getObjectByName (BattleConfig.EnemysObject);
		
		Transform[] children = enemysObject.GetComponentsInChildren<Transform>();
		foreach (Transform data in children) {
			BaseEnemy baseEnemy = data.GetComponent<BaseEnemy>();
			if(baseEnemy)
			{
				baseEnemy.removeScripts();
			}
		}
	}

	public void clear()
	{
		enemyIndex = 0;
		GameObject enemysObject = ResourcesManager.Instance.getObjectByName (BattleConfig.EnemysObject);
		Transform[] children = enemysObject.GetComponentsInChildren<Transform>(false);
		int count = children.Length;
		for(int i = 0; i < count ; i++)
		{
			Transform data = children[i];
			if(data.gameObject.tag == "Enemy")
			{
				Destroy (data.gameObject);
			}

		}

	}
}
