using UnityEngine;
using System.Collections;

//很多一起， index = 6
public class EnemyMany : BaseEnemy {

	public Transform startTransform;
	public Transform rightTransform;
	private Vector3 forceData = new Vector3(3, 0, 0);
	private bool isStartGo = false;
	private float goTime = 1f;
	public bool isMama = true;
	private float speed = 8;
	public int childendIndex = 0;
	void Awake()
	{
		base.Awake ();
	}
	void Start () {

		PreInit();
	}
	
	void PreInit()
	{
		if (isMama) {
			UpdateSpeed(speed);
			MoveLeftRightRandomMoveDir ();
			int childrenCount = Random.Range (3, 6);
			//int childrenCount = 1;
			for (int i = 0; i < childrenCount; i++) {
				GameObject childObj = EnemySpawn.Instance.CreateEnemyByIndex (base.index);
				EnemyMany _EnemyMany = childObj.GetComponent<EnemyMany> ();
				_EnemyMany.isMama = false;
				_EnemyMany.UpdateSpeed(speed);
				_EnemyMany.SetLRPos (_dir);
				_EnemyMany.childendIndex = i;
				_EnemyMany.index = base.index;
				_EnemyMany.isLastChild = i == childrenCount - 1;
			}

		} else 
		{

			int parm = 0;

			if(_dir == BoxMoveDir.MoveToLeft)
			{
				parm = 1;
			}else if(_dir == BoxMoveDir.MoveToRight)
			{
				parm = -1;
			}
			_Transform.position = new Vector3 (_Transform.position.x + (childendIndex + 1) * parm * 1f, _Transform.position.y , _Transform.position.z);
		}
		

	}
	public void init(bool _isMama, Vector3 pos)
	{

	}

	void Update () 
	{
		updatePosition();
	}
	void updatePosition()
	{
		
		forceData = _Transform.position;
		forceData.x += _speed * Time.deltaTime;
		//Debug.Log("_speed == " + _speed);
		_Transform.position = forceData;
	}
}
