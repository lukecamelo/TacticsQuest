using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhatRobit
{
	public static class RobitTools
	{
		public static void ClampAngle(ref Vector3 angle)
		{
			if(angle.x < -180) angle.x += 360;
			else if(angle.x > 180) angle.x -= 360;

			if(angle.y < -180) angle.y += 360;
			else if(angle.y > 180) angle.y -= 360;

			if(angle.z < -180) angle.z += 360;
			else if(angle.z > 180) angle.z -= 360;
		}
	}
}