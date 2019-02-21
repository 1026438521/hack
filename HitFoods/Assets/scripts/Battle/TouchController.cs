using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
		#if UNITY_EDITOR
			if(GameManager.Instance.uiState == UIState.E_UI_Home)
			{
				return;
			}

			if(Input.GetMouseButtonDown (0))
			{
				NotificationCenter.DefaultCenter().PostNotification(this, "onTouchDown");
			}else if(Input.GetMouseButtonUp(0))
			{
				
			}
			
		#elif UNITY_ANDROID
			if(TempData.Instance.gameState == GameState.Fighting && Input.touchCount > 0)
			{
				if(Input.GetTouch(0).phase == TouchPhase.Began)
				{
					NotificationCenter.DefaultCenter().PostNotification(this, "onTouchDown");
				}else if(Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					
				}
			}
		#endif
	}
}
