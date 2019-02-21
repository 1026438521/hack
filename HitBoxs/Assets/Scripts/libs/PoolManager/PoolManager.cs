using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PrefabPool
{
	public string name;
    public int preloadAmount = 1;
    public float preloadDelay = 0.1f;
    public MonoBehaviour target;
    public void onStartTask()
	{
		preloadAmount = preloadAmount - PoolManager.Instance.getObjectCountByName(name);
		startTask();
	}

	private void startTask()
	{
		if(preloadAmount > 0)
		{
			//Debug.Log("preloadAmount ====" + preloadAmount);
			target.StartCoroutine(this.PreloadOverTime());
			preloadAmount -= 1;		
			
		}else
		{
			//Debug.Log("preloadAmount 00000000====" + preloadAmount);
		}
	}
	private IEnumerator PreloadOverTime()
    {
        yield return new WaitForSeconds(this.preloadDelay);
        PoolManager.Instance.createNewObject(name);
        startTask();

    }
}
public class PoolManager : Singleton<PoolManager>{
	private int nameIndex = 0;

	private Dictionary<string, List<GameObject> > dicList = new Dictionary<string, List<GameObject> >();
	//private Dictionary<string, Transform> _prefabs = new Dictionary<string, Transform>();
	public void _add(string name, Transform prefab)
	{
		//_prefabs.Add(name, prefab);
	}

	public int getObjectCountByName(string name)
	{
		if(dicList.ContainsKey(name))
		{
			return dicList[name].Count;
		}
		return 0;
	}

	public void createNewObject(string name)
	{
		GameObject item = GameObject.Instantiate(ResourcesManager.Instance.getPrefabByName(name) as GameObject, new Vector3(0, 0, 0), Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
		//item.transform.parent = ResourcesManager.Instance.getTempObject().transform;

		item.SetActive(false);
		if(dicList.ContainsKey(name) == false)
		{
			List<GameObject> objList = new List<GameObject>();
			dicList[name] = objList;
		}
		dicList[name].Add(item);
	}

	public GameObject getObjectByName(string name)
	{
		GameObject item = null;
		if(dicList.ContainsKey(name) == false)
		{
			List<GameObject> objList = new List<GameObject>();
			dicList[name] = objList;
			item = GameObject.Instantiate(ResourcesManager.Instance.getPrefabByName(name) as GameObject, new Vector3(0, 0, 0), Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
		}else
		{
			List<GameObject> objList = dicList[name];
			if(objList.Count == 0)
			{
				item = GameObject.Instantiate(ResourcesManager.Instance.getPrefabByName(name) as GameObject, new Vector3(0, 0, 0), Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
			}else
			{
				int index = objList.Count - 1;
				item = objList[index];
				objList.RemoveAt (index);
			}
		}
		if(item)
		{
			item.SetActive (true);
			SpawnPool pool = item.GetComponent<SpawnPool>();
			if(pool == null)
			{
				pool = item.gameObject.AddComponent<SpawnPool> ();
			}
			pool.reset(name);
		}
		
		return item;
	}

	public void removePoolObject(GameObject obj)
	{
		SpawnPool pool = obj.GetComponent<SpawnPool>();
		removePoolObjectByName(pool.name, obj);
	}

	public void removePoolObjectByName(string name, GameObject obj)
	{
		obj.SetActive (false);
		if(dicList.ContainsKey(name) == false)
		{
			List<GameObject> objList = new List<GameObject>();
			dicList[name] = objList;
			objList.Add(obj);
		}else
		{
			dicList[name].Add(obj);
		}
	}
}






