using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

	private bool isTouching = false;
	private Animator pAnimator;
	private bool isHitEnemy = false;
	private float defaultSpeed = 2f;
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "onTouchDown");
		NotificationCenter.DefaultCenter().AddObserver(this, "onResetCube");
		pAnimator = transform.GetComponent<Animator>();
		pAnimator.speed = defaultSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision collision) {
		if(collision.transform.tag == "Enemy")
		{
			isHitEnemy = true;
			BaseEnemy baseEnemy = collision.transform.gameObject.GetComponent<BaseEnemy>();
			if(baseEnemy && baseEnemy.isCanMoving)
			{
				baseEnemy.onEnemyDie();
				//Debug.Log("0000000000000000");
				pAnimator.SetBool ("down", false);
			}

		}
	}
	
	void onResetCube(Notification notification)
	{
		pAnimator.speed = defaultSpeed;

	}
	void onTouchDown(Notification notification)
	{
		if(isTouching)
		{
			return;
		}
		isTouching = true;
		pAnimator.SetBool ("down", true);

	}

	public void onDown()
	{
		if(isHitEnemy == false)
		{
			//pAnimator.speed = 0f;
			//NotificationCenter.DefaultCenter().PostNotification(this, "onGameOver");
		}
	}

	public void onAniOver()
	{
		isHitEnemy = false;
		isTouching = false;
		pAnimator.SetBool ("down", false);
	}
}
