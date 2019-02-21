using UnityEngine;
using System.Collections;

public enum DirState
{

};
public class BaseEnemy : MonoBehaviour {

	public int index = 0;
	public string pathName;
	private bool isDead = false;
	public bool isCanMoving = true;
	//private Rigidbody rigidbody;
	protected Vector3 forceData = new Vector3(0, 0, 0);
	protected Transform _Transform;
	protected float _speed;
	protected BoxMoveDir _dir;
	protected bool isLastChild = false;
	public void Awake () 
	{
		_Transform = transform;
	}

	public void UpdateSpeed(float speed)
	{
		_speed = speed + Random.Range(1, EnemySpawn.Instance.getEnemyIndex()) / 2;
		if(_speed > BattleConfig.SpeedMax)
		{
			_speed = BattleConfig.SpeedMax;
		}
	}

	public BoxMoveDir MoveLeftRightRandomMoveDir()
	{
		BoxMoveDir dir = ObjectFactory.Instance.GetRandomMoveDir();
		SetLRPos(dir);
		return dir;
	}

	public void SetLRPos(BoxMoveDir dir)
	{
		Transform startTransform  = null;
		_dir = dir;
		if(dir == BoxMoveDir.MoveToLeft)//从右往左
		{
			_speed = -_speed;
			startTransform = ResourcesManager.Instance.getPosObjectByName(BattleConfig.pos_right).transform;
			
		}else if(dir == BoxMoveDir.MoveToRight)
		{
			startTransform = ResourcesManager.Instance.getPosObjectByName(BattleConfig.pos_left).transform;
		}

		forceData = startTransform.position;
		forceData.y = forceData.y + _Transform.localScale.y * 0.5f;
		_Transform.position = forceData;
	}
	public void MoveTopLeftRightRandomMoveDir()
	{
		BoxMoveDir dir = ObjectFactory.Instance.GetRandomTopMoveDir();
		Transform startTransform = null;
		_dir = dir;
		if(dir == BoxMoveDir.TopMoveToLeft)//从右往左
		{
			_speed = -_speed;
			startTransform = ResourcesManager.Instance.getPosObjectByName(BattleConfig.pos_right_top).transform;
			
		}else if(dir == BoxMoveDir.TopMoveToRight)
		{
			startTransform = ResourcesManager.Instance.getPosObjectByName(BattleConfig.pos_left_top).transform;
		}

		forceData = startTransform.position;
		forceData.y = forceData.y + _Transform.localScale.y * 0.5f;
		_Transform.position = forceData;
	}

	public void onEnemyDie()
	{
		if(isDead == false)
		{
			isDead = true;
			GameObject enemy = ObjectFactory.Instance.getObjectByName(BattleConfig.DeathSplash);
			Vector3 pos = _Transform.position;
			// pos.z = -3f;
			enemy.transform.position = pos;
			Destroy (_Transform.gameObject);
			
			GameManager.Instance.addScore();
			NotificationCenter.DefaultCenter().PostNotification(this, "onUpdateUIBattleView");
			if(index == 3 )//很多的box
			{
				if(isLastChild)
				{
					EnemySpawn.Instance.createNestEnemy();
				}
			}else
			{
				EnemySpawn.Instance.createNestEnemy();
			}

		}

	}

	public void removeScripts()
	{
		if(isCanMoving)
		{
			isCanMoving = false;
//			rigidbody = transform.GetComponent<Rigidbody>();
//			rigidbody.velocity = new Vector3(0, 0, 0);
//			rigidbody.useGravity = false;
			//BoxCollider collier = transform.GetComponent<BoxCollider> ();
			//collier.enabled = false;
			Destroy(this);
		}

	}
	void LateUpdate()
	{
		CheckOutDeath ();
	}

	void CheckOutDeath()
	{
		if (_dir == BoxMoveDir.MoveToLeft || _dir == BoxMoveDir.TopMoveToLeft) {//to right
			if(_Transform.position.x < -3.0f)
			{
				onEnemyDie();
				// Debug.Log("11111111====" + _Transform.position.x);
				NotificationCenter.DefaultCenter().PostNotification(this, "onGameOver");
			}
		} else if(_dir == BoxMoveDir.MoveToRight || _dir == BoxMoveDir.TopMoveToRight){
			if(_Transform.position.x > 3.0f)
			{
				onEnemyDie();
				// Debug.Log("222222222====" + _Transform.position.x);
				NotificationCenter.DefaultCenter().PostNotification(this, "onGameOver");
			}
		}
	}
}




