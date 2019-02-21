using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

	private Vector3 startPos;
	private bool isTouchBegin = false;

	void LateUpdate()
	{
		if(BattleTempData.Instance.gameState == GameState.Gaming)
		{
			OnUpdateTouch();
		}
	}

	void OnUpdateTouch()
	{
		#if UNITY_EDITOR
			if(Input.GetMouseButtonDown (0))//start
			{
				startPos = Input.mousePosition;
				isTouchBegin = true;
				EventDispatcher.Instance.InvokeEvent("onTouchStart", startPos.x);
			}
			else if(Input.GetMouseButton(0))//move
			{
			}
			else if(Input.GetMouseButtonUp(0))//end
			{
				
			}

		#elif UNITY_ANDROID
			Touch touch = Input.GetTouch(0);
			var pos = touch.position;
			if(touch.phase == TouchPhase.Began)
			{
				startPos = pos;
				isTouchBegin = true;
				EventDispatcher.Instance.InvokeEvent("onTouchStart", startPos.x);
				// return;
			}

		#endif
	}
}
