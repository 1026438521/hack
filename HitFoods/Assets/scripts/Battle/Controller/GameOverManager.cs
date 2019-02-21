using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "onGameOver");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// void OnTriggerEnter(Collider collider)
	// {
	// 	if(collider.transform.tag == "Enemy")
	// 	{
	// 		onGameOver(null);
	// 	}
	// }
	void onGameOver(Notification notification)
	{
		//EnemySpawn.Instance.stopAllEnemys();
		//NotificationCenter.DefaultCenter().PostNotification(this, "onBackHome");
	}
}
