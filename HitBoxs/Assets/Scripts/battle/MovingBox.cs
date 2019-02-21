using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BoxState
{
	Box_Fly = 1,
	Box_Destroyed,
	Box_Hode,
};
public class MovingBox : MonoBehaviour {

	public int _columnIndex;//所处的位置
	private Vector3 tempVec = new Vector3(0,0,0);
	private BoxState _BoxState = BoxState.Box_Fly;
	private Transform _transform;
	private MoveTo _MoveTo = null;
	private List<Action> actionList = new List<Action>();
	void Start () {
		_transform = transform;
	}
	
	void Update () {

	}

	void FixedUpdate()
	{
		// if(_BoxState == BoxState.Box_Fly)
		// {
		// 	onFlying();
		// }
	}
	public void changeMoveState(BoxState state)
	{
		_BoxState = state;
	}
	
	public void init(int index)
	{
		_columnIndex = index;
		
		tempVec.x = Utils.getPosXByIndex(index);
	}
	
	public void startFly()
	{
		if(_MoveTo == null)
		{
			Vector3 targetPos = new Vector3(Utils.getPosXByIndex(_columnIndex), Values.CameraUpY, 0);
			_MoveTo = gameObject.AddComponent<MoveTo> ();
	    	_MoveTo.moveToPos(targetPos, 0.5f).start();
	    	// actionList.Add(_MoveTo);
		}
	}

	void OnCollisionEnter(Collision collisionInfo)
    {
        // Debug.Log("碰撞到的物体的名字是：" + collisionInfo.gameObject.name);
        if(_BoxState == BoxState.Box_Fly)
        {
        	onFlyingCollision(collisionInfo);
        }
    }

    //飞行的时候碰撞上了
	public void onFlyingCollision(Collision collisionInfo)
	{
		GameObject parent = collisionInfo.gameObject.transform.parent.gameObject;
		// Debug.Log("collisionInfo.name ==" + collisionInfo.gameObject.name);
		// Debug.Log("parent.name ==" + parent.name);
		int rowIndex = BattleTempData.Instance.groupsObj.IndexOf (parent);
		// int currnetIndex = BattleTempData.Instance.groupsObj.FindIndex(parent);
		//int currnetIndex = Utils.getFrontIndexOfList(BattleTempData.Instance.groupsObj, parent);

		int realRowIndex = getRealRowIndex(rowIndex, _columnIndex);
		if(realRowIndex <= 0)//说明需要新建了
		{
			BattleFactory.Instance.insertBottomGroup(_columnIndex);
		}else
		{
			BattleFactory.Instance.insertBoxIntoBoxs(realRowIndex - 1, _columnIndex);//碰撞的box属于第二个，那么添加的应该减一
		}
		removeSelf();

	}

	//获得真实的行
	int getRealRowIndex(int rowIndex,int columnIndex)
	{
		List<GameObject> groupsObj = BattleTempData.Instance.groupsObj;
		while(true)
		{
			if(rowIndex == 0)
			{
				return -1;
			}
			GameObject rowObjs = groupsObj[rowIndex - 1];
			GameObject columnObj = rowObjs.transform.Find(columnIndex.ToString()).gameObject;
			if(columnObj.activeSelf)
			{
				rowIndex = rowIndex - 1;
				Debug.Log("eeeeeeeeeeeee");
			}else
			{
				return rowIndex;
			}

		}
		return -1;
	}
	//检查是否到达
	public void checkMoveStop()
	{
		
		
		// GameObject bottomObj = groupsObj[groupsIndex];
		// Debug.Log("_index===" + _index);
		
		// GameObject obj = bottomObj.transform.Find(_index.ToString()).gameObject;//找到这个列中的空间
		// float posY = 0;
		// if(obj.activeSelf)//没隐藏，那么算是全消整行
		// {
		// 	//获得最先面的位置
		// 	posY = Utils.getBottomPosYByPosY(bottomObj.transform.position.y);
		// 	if(Mathf.Abs(_transform.position.y - posY) < 1f)
		// 	{
		// 		BattleFactory.Instance.insertBoxIntoBoxs(groupsIndex, _index);
		// 		removeSelf();
		// 	}
		// }
	}

	bool isMoveCollisionPos()
	{
		List<GameObject> groupsObj = BattleTempData.Instance.groupsObj;
		if(groupsObj.Count <= 0)
		{
			return false;
		}
		GameObject touchObj = groupsObj[0];
		float posY = Utils.getBottomPosYByPosY(touchObj.transform.position.y);
		return Mathf.Abs(_transform.position.y - posY) < 0.5f;
	}

	int getUnHideGroupsIndex()
	{
		List<GameObject> groupsObj = BattleTempData.Instance.groupsObj;
		int count = groupsObj.Count;
		int findedIndex = 0;
		for(int i = 0; i < count;i++)//从下往上找
		{
			GameObject groupsItem = groupsObj[i];
			GameObject obj = groupsItem.transform.Find(_columnIndex.ToString()).gameObject;
			if(obj && obj.activeSelf == false)//如果是隐藏状态
			{
				if(findedIndex == 0)//第一次找到
				{
					findedIndex = 0;
				}else
				{
					if(i - findedIndex > 1)//不是连接的
					{
						return findedIndex;
					}
				}
			}
		}
		if(findedIndex == 0)//这一列没有没找到空隙，那么需要另开辟新一行
		{

		}
		return findedIndex;
	}

	void removeSelf()
	{
		_BoxState = BoxState.Box_Destroyed;
		_MoveTo.stopTask();
		_MoveTo = null;
		Destroy(gameObject);
	}
}




