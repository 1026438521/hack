using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum WindowID
{
	WindowID_Home = 1,
	WindowID_Battle,
	WindowID_Result
}

public class GameUIUtil : Singleton<GameUIUtil>
{
	public string ui_path = "UI/";
	public Dictionary<WindowID, GameObject> window_View = new Dictionary<WindowID, GameObject>();
	public Dictionary<WindowID, string> window_Path = new Dictionary<WindowID, string>()
	{
		{ WindowID.WindowID_Home, "UIHome" },
		{ WindowID.WindowID_Battle, "UIBattle" },
		{ WindowID.WindowID_Result, "UIResult" }
	};

	public void ShowView(WindowID windowID)
	{
		if(window_View.ContainsKey(windowID))
		{
			return;
		}
		Debug.Log("windowID===" + windowID);
		string path = window_Path[windowID];
		path = ui_path + path;
		Debug.Log("path===" + path);
		GameObject view = NGUITools.AddChild (ResourcesManager.Instance.getUIRoot(), ResourcesManager.Instance.getResourceByName(path) as GameObject);
		window_View [windowID] = view;
		view.SetActive (true);

	}
	public void RemoveView(WindowID windowID)
	{
		if(window_View.ContainsKey(windowID))
		{
			NGUITools.Destroy (window_View[windowID]);
			window_View.Remove (windowID);
		}
	}
}





