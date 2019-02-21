using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ResourcesManager : Singleton<ResourcesManager>{

	private Dictionary<string, Object> prefabDic = new Dictionary<string, Object> ();
	private Dictionary<string, GameObject> objDic = new Dictionary<string, GameObject> ();
	public void onStart()
	{

	}

	public GameObject getObjectByName(string name)
	{
		if(objDic.ContainsKey(name) == false)
		{
			prefabDic [name] = GameObject.Find (name).gameObject;
		}
		return prefabDic[name] as GameObject;
	}
	public GameObject getPosObjectByName(string name)
	{
		if(objDic.ContainsKey(name) == false)
		{
			prefabDic [name] = GameObject.Find ("StartPos").transform.Find (name).gameObject;
		}
		return prefabDic[name] as GameObject;
	}

	public Object getPrefabByName(string name)
	{
		if(prefabDic.ContainsKey(name) == false)
		{
			string filename = "prefab/" + name;
			Object data = Resources.Load (filename);
			prefabDic[name] = data;
		}
		return prefabDic[name] as Object;
	}


}
