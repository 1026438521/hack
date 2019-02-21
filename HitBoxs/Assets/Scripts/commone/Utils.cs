using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utils
{

	public static float pGetDistance(Vector3 pos1, Vector3 pos2)
	{
		float x = pos1.x - pos2.x;
		float z = pos1.z - pos2.z;
		float y = pos1.y - pos2.y;
		return Mathf.Sqrt(x * x + z * z + y * y);
	}

	public static int getTouchIndex(float posX)
	{
		if(posX < Screen.width * 0.25f)
		{
			return 1;
		}else if(posX < Screen.width * 0.5f)
		{
			return 2;
		}else if(posX < Screen.width * 0.75f)
		{
			return 3;
		}else if(posX < Screen.width)
		{
			return 4;
		}
		return 1;
	}

	//根据位置获得坐标 x
	public static float getPosXByIndex(int index)
	{
		// float gap = Values.BoxWidth * Values.WidthIntervalRatio;
		// float leftGap = Values.CameraWidth * Values.BothSideRatio;
		// float leftPoX = 0 - (Values.CameraWidth * 0.5f);
		// float posX = leftPoX + leftGap + index * gap + Values.BoxWidth * 0.5f + index * Values.BoxWidth;
		//return posX;
		float startPosX = index * Values.CameraWidth * 0.25f - Values.CameraWidth * 0.625f;
		return startPosX;
	}

	public static float getBottomPosYByPosY(float posY)
	{
		return posY - Values.HeightInterval - Values.BoxHeight;
	}

	public static int getFrontIndexOfList(List<GameObject> list, GameObject obj)
	{
		int count = list.Count;

		for(int i = 0; i < count; i++)
		{
			if(list[i] == obj)
			{
				return i - 1;
			}
		}
		return 0;
	}
}





