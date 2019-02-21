using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ResourcesManager : Singleton<ResourcesManager>{

	private Dictionary<string, Texture2D> resDic = new Dictionary<string, Texture2D> ();
	private Dictionary<string, Object> prefabDic = new Dictionary<string, Object> ();
	
	public GameObject getCamera()
	{
		return GameObject.Find ("Main Camera");
	}

	public GameObject getUIRoot()
	{
		return  GameObject.Find ("UI Root");
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

	public Object getResourceByName(string name)
	{
		Debug.Log("getResourceByName===" + name);
		if(prefabDic.ContainsKey(name) == false)
		{
			Object data = Resources.Load (name);
			prefabDic[name] = data;
		}
		return prefabDic[name] as Object;
	}

	public Texture2D getResTextureByName(string name)
	{
		if(resDic.ContainsKey(name) == false)
		{
			string filename = "newpng/" + name;
			Texture2D sprite = Resources.Load(filename) as Texture2D;
			resDic [name] = sprite;
		}
		return resDic[name] as Texture2D;
	}


}
