using UnityEngine;
using System.Collections;

namespace PhatRobit
{
	public class CameraDirection : MonoBehaviour
	{
		protected CameraController _camera;
		protected Facing _facing = Facing.Down;

		public virtual Facing Facing { get { return _facing; } }
		public virtual Vector2 Angle { get { return _camera.CurrentRotation; } }

		public virtual void Awake()
		{
			_camera = GetComponent<CameraController>();
		}

		public virtual void LateUpdate()
		{
			float rX = _camera.CurrentRotation.x;
			// float rX = _camera.transform.localEulerAngles.x;
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