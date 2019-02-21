using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	private float time = 3f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if(time < 0)
		{
			Destroy(gameObject);
		}
	}
}
