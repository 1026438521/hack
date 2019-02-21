using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceData : MonoBehaviour {

	public static ResourceData Instance;
	
	public GameObject MovingBoxCube;
	public GameObject cube;
	public GameObject boxs;
	public GameObject MovingBoxsObject;
	public GameObject ParticalObject;
	public GameObject DeathSplash;
	
	void Start () {
		Instance = this;
	}
}
