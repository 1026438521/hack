using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleFactory : Singleton<BattleFactory>{
	//
	private Vector3 tempVec3 = new Vector3(0,0,0);
	public List<GameObject> _emptyObjects = new List<GameObject>();//空obj
	private int _objsIndex = 0;

	//在指定的地点创建一个box,flying box
    public GameObject createBoxAtIndex(int index)
    {
		GameObject item = GameObject.Instantiate(ResourceData.Instance.MovingBoxCube, tempVec3, Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
		//float startPosX = index * Values.CameraWidth * 0.25f - Values.CameraWidth * 0.625f;
		MovingBox movingBox = item.AddComponent<MovingBox>();
		movingBox.init(index);
		float startPosX = Utils.getPosXByIndex(index);
		float startPosY = Values.CameraBottomY;
		tempVec3.x = startPosX;
		tempVec3.y = startPosY + 2;
		tempVec3.z = 0;

		item.transform.parent = ResourceData.Instance.MovingBoxsObject.transform;
		item.transform.localPosition = tempVec3;

		tempVec3.x = Values.BoxWidth;
		tempVec3.y = Values.BoxHeight;
		tempVec3.z = Values.BoxThickness;

		item.transform.localScale = tempVec3;
		return item;
    }

	//继续创建boxs
	public void createNewBoxs()
	{
		List<GameObject> groupsObj = BattleTempData.Instance.groupsObj;
		int count = groupsObj.Count;
		Transform topObj = groupsObj[count - 1].transform;
		float posY = topObj.position.y;//最高boxs的高度
		if(posY < Values.CameraUpY)
		{

			string name = topObj.gameObject.name;
			GameObject newGroupObj = createBoxsRandomOneUnVisiable(topObj.localPosition.y + Values.HeightInterval + Values.BoxHeight);
			
		}
	}

	public void insertBottomGroup(int index)
	{
		List<GameObject> groupsObj = BattleTempData.Instance.groupsObj;
		
		Transform bottomObj = groupsObj[0].transform;

		float posY = bottomObj.localPosition.y - Values.HeightInterval - Values.BoxHeight;
		GameObject newGroupObj = createBoxs(posY);
		Transform _Transform = newGroupObj.transform;
		for(int i = 1; i <= Values.Number; i++)
		{
			GameObject unVisibleBox = _Transform.Find(i.ToString()).gameObject;
			bool isVisible = i == index ? true: false;
			unVisibleBox.SetActive(isVisible);
		}
		BattleTempData.Instance.groupsObj.Insert (0, newGroupObj);
	}

	//创建一组boxs
	public GameObject createBoxsRandomOneUnVisiable(float posY)
	{
		//Debug.Log("posY===" + posY);
		// GameObject newGroupObj = new GameObject();
		// newGroupObj.transform.parent = ResourceData.Instance.boxs.transform;
		// int emptyNum = Random.Range (1, Values.Number);
		// for(int i = 1; i <= Values.Number; i++)
		// {
		// 	float posX = Utils.getPosXByIndex(i);
		// 	tempVec3.x = posX;
		// 	tempVec3.y = 0;
		// 	tempVec3.z = 0;

		// 	GameObject item = GameObject.Instantiate(ResourceData.Instance.cube, tempVec3, Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
		// 	item.transform.parent = newGroupObj.transform;
		// 	item.transform.localPosition = tempVec3;

		// 	tempVec3.x = Values.BoxWidth;
		// 	tempVec3.y = Values.BoxHeight;
		// 	tempVec3.z = Values.BoxThickness;
		// 	item.transform.localScale = tempVec3;
		// 	item.name = i.ToString();
		// 	if(emptyNum == i)
		// 	{
		// 		item.SetActive (false);
		// 	}
		// }
		GameObject newGroupObj = createBoxs(posY);
		//int emptyNum = Random.Range (1, Values.Number);
		int emptyNum = getRandomIndex();
		GameObject unVisibleBox = newGroupObj.transform.Find(emptyNum.ToString()).gameObject;
		unVisibleBox.SetActive(false);
		BattleTempData.Instance.groupsObj.Add(newGroupObj);
		return newGroupObj;
	}

	private int lastIndex = 1;
	private int lastIndexTimes = 0;
	private int maxTimes = 2;
	int getRandomIndex()
	{
		int index = Random.Range (1, Values.Number);
		if(index == lastIndex)
		{
			lastIndexTimes ++;
			if(lastIndexTimes > maxTimes)
			{
				index ++;
				if(index > Values.Number)
				{
					index = 0;
				}
			}
		}
		lastIndex = index;
		return index;
	}

	GameObject createBoxs(float posY)
	{
		GameObject newGroupObj = new GameObject();
		newGroupObj.transform.parent = ResourceData.Instance.boxs.transform;
		for(int i = 1; i <= Values.Number; i++)
		{
			float posX = Utils.getPosXByIndex(i);
			tempVec3.x = posX;
			tempVec3.y = 0;
			tempVec3.z = 0;

			GameObject item = GameObject.Instantiate(ResourceData.Instance.cube, tempVec3, Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
			item.transform.parent = newGroupObj.transform;
			item.transform.localPosition = tempVec3;

			tempVec3.x = Values.BoxWidth;
			tempVec3.y = Values.BoxHeight;
			tempVec3.z = Values.BoxThickness;
			item.transform.localScale = tempVec3;
			item.name = i.ToString();
		}
		_objsIndex ++;
		GroupManager groupManager = newGroupObj.AddComponent<GroupManager>();
		groupManager.initData(_objsIndex);
		newGroupObj.name = _objsIndex.ToString();
		tempVec3.x = 0;
		tempVec3.y = posY;
		tempVec3.z = 0;
		newGroupObj.transform.localPosition = tempVec3;

		return newGroupObj;
	}

	//groupIndex: 0 1  index: 1 2
	public void insertBoxIntoBoxs(int groupIndex, int index)
	{
//		Debug.Log("insertBoxIntoBoxs ===groupIndex==index==" + groupIndex.ToString() + "=====" + index.ToString());
		GameObject groupObj = BattleTempData.Instance.groupsObj[groupIndex];
		if(groupObj)
		{
			// groupObj.SetActive(false);
			Transform hideBox = groupObj.transform.Find(index.ToString());
			if(hideBox)
			{
				hideBox.gameObject.SetActive(true);
			}
		}
		checkConnectLine(groupIndex);
	}

	//检查是否连成一整行
	void checkConnectLine(int groupIndex)
	{
		for(int j = 0; j <= groupIndex; j++)//检查当前以及上一行，是否连成一行
		{
			if(isConnectLineOfOneGroup(j) == false)
			{
				return;
			}
		}
		//处理连成一行
		//Debug.Log("-----------连成一行了");
		destroyLineGroup();
	}

	bool isConnectLineOfOneGroup(int groupIndex)
	{
		GameObject groupObj = BattleTempData.Instance.groupsObj[groupIndex];
		for(int i = 0; i <= Values.Number; i++)
		{
			Transform box = groupObj.transform.Find(i.ToString());
			if(box)
			{
				if(box.gameObject.activeSelf == false)
				{
					return false;
				}
			}
		}
		return true;
	}
	//删除连成一行的,从第一行检查,循环检查第一行,一定要删除，并且一定发通知
	void destroyLineGroup()
	{
		float posYBeforeDestroy = -1;
		int addScore = 0;
		while(true)
		{
			if(isConnectLineOfOneGroup(0))
			{
				posYBeforeDestroy = BattleTempData.Instance.getBottomPosY();
				// if(posYBeforeDestroy == -1)
				// {
				// 	posYBeforeDestroy = BattleTempData.Instance.getBottomPosY();
				// }
				//OnShowParticle(BattleTempData.Instance.groupsObj[0]);
				GameObject.Destroy(BattleTempData.Instance.groupsObj[0]);
				BattleTempData.Instance.groupsObj.RemoveAt (0);
				addScore += 1;
			}else
			{
				BattleTempData.Instance.score += addScore;
				EventDispatcher.Instance.InvokeEvent("onUpdateScoreView");
				EventDispatcher.Instance.InvokeEvent("onUpdateMoveSpeed");
				EventDispatcher.Instance.InvokeEvent("onUpdateSpeedView");
				EventDispatcher.Instance.InvokeEvent("onAddSpeedForClearBox", posYBeforeDestroy);
				return;
			}
		}
	}

	void OnShowParticle(GameObject obj)
	{
		for(int i = 1;i <= Values.Number;i++)
		{
			Transform item = obj.transform.Find(i.ToString());
			Vector3 pos = item.position;
			GameObject particle = GameObject.Instantiate(ResourceData.Instance.DeathSplash, pos, Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
			particle.transform.parent = ResourceData.Instance.ParticalObject.transform;
		}
		
	}

	public void OnClearAllBoxs()
	{
		Transform box = ResourceData.Instance.boxs.transform;
		int count = box.childCount;
		for (int i = 0; i < count; i++)
		{
			GameObject.Destroy (box.GetChild (i).gameObject);  
		} 
		box.position = new Vector3(0,0,0);
	}

}








