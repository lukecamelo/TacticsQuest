using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhatRobit
{
	public class FpsCameraDirection : CameraDirection
	{
		private Camera _fpsCamera;

		public override Facing Facing { get { return _facing; } }
		public override Vector2 Angle { get { return _fpsCamera.transform.rotation.eulerAngles; } }

		public override void Awake()
		{
			_fpsCamera = GetComponent<Camera>();
		}

		public override void LateUpdate()
		{
			Vector3 angle = Angle;
			RobitTools.ClampAngle(ref angle);

			float rX = angle.y;
			float x = Mathf.Abs(rX);

			if(x < 22.5f)
			{
				_facing = Facing.Up;
			}
			else if(x < 67.5f)
			{
				_facing = rX < 0 ? Facing.UpLeft : Facing.UpRight;
			}
			else if(x < 112.5f)
			{
				_facing = rX < 0 ? Facing.Left : Facing.Right;
			}
			else if(x < 157.5f)
			{
				_facing = rX < 0 ? Facing.DownLeft : Facing.DownRight;
			}
			else
			{
				_facing = Facing.Down;
			}
		}
	}
}