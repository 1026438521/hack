using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour {

	void Start () {
		GameUIUtil.Instance.ShowView(WindowID.WindowID_Home);
		AdsManager.Instance.Init ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
