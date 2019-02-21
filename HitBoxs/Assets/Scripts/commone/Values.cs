using UnityEngine;
using System.Collections;

public class Values
{

	// public static float aspectRatio = 0.8f;//box 高度相对于宽度
	public static float BothSideRatio = 0.027f; //两边宽度占总宽度比例
	public static float WidthIntervalRatio = 0.3f; //box之间的间隔占box的宽度比例

	public static float HeightInterval = 0.3f; //boxs高度间隔比例

	public static float BoxWidth = 1.0f;//box的宽度,根据屏幕宽高定的
	public static float BoxHeight = 1.0f;//box的高度
	public static float BoxThickness = 1.0f;//box的厚度
	public static float BoxWHRatio = 0.57f; //box宽高比
	public static float BoxWTRatio = 0.27f; //box高厚比

	public static int Number = 4;//每行的数量
	public static float CameraWidth = 0f; //摄像机看到的宽高
	public static float CameraHeight = 0f;
	public static float OrthographicSize = 0f;

	public static float CameraUpY = 0f;//摄像机看到的最上方
	public static float CameraBottomY = 0f;//摄像机看到的最下方
	public static float BoxStartPosY = 0f;//开始时，box的位置
	public static float BoxsStopPosY = 0f;//
	
	public static void init()
	{
		Values.OrthographicSize = ResourcesManager.Instance.getCamera().GetComponent<Camera>().orthographicSize;
		Values.CameraHeight = Values.OrthographicSize;
		Values.CameraWidth = Screen.width * 1f / Screen.height * Values.CameraHeight * 2;

		Transform Commone = GameObject.Find("Commone").transform;
		Commone.gameObject.SetActive (false);
		Values.CameraUpY = Commone.Find("UpCube").transform.position.y;
		Values.CameraBottomY = Commone.Find("BottomCube").transform.position.y;
		Values.BoxStartPosY = Commone.Find("StartCube").transform.position.y;
		Values.BoxsStopPosY = Commone.Find("StopCube").transform.position.y;

		float value1 = Values.CameraWidth - Values.CameraWidth * Values.BothSideRatio * 2;

		Values.BoxWidth = value1 / (Values.Number + (Values.Number - 1) * Values.WidthIntervalRatio);

		Values.BoxHeight = Values.BoxWidth * Values.BoxWHRatio;

		// Values.BoxThickness = Values.BoxHeight * Values.BoxWTRatio;

		Values.HeightInterval = Values.HeightInterval * Values.BoxHeight;
	}
}
