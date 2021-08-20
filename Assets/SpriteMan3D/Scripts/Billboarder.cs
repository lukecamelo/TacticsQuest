using UnityEngine;
using UnityEngine.Rendering;

namespace SpriteMan3D
{
    /// <summary>
    /// Billboards rendered objects - usually sprites.
    /// </summary>
    [ExecuteInEditMode]
    public class Billboarder : MonoBehaviour
    {
        /// <summary>
        /// Billboard the vertical axis.
        /// </summary>
        public bool billboardV = true;
        /// <summary>
        /// Points at camera when true. Points towards camera plane forward direction when false.
        /// </summary>
        public bool pointAtCamera = false;

        void Start()
        {
        }

        public void OnEnable()
        {
            RenderPipelineManager.beginCameraRendering += MyWillRender;
        }

        public void OnDisable()
        {
            RenderPipelineManager.beginCameraRendering -= MyWillRender;
        }

        /// <summary>
        /// works in the new render pipeline
        /// </summary>
        void MyWillRender(ScriptableRenderContext context, Camera camera)
        {
            MyWillRender(camera);
        }

        /// <summary>
        /// works in classic render pipeline. 
        /// NOTE: This only gets called if there's a Renderer attached to the GameObject.
        /// </summary>
        void OnWillRenderObject()
        {
            var current = Camera.current;
            if (current != null)
            {
                MyWillRender(current);
            }
        }

        void MyWillRender(Camera camera)
        {
            if (gameObject.activeInHierarchy)
            {
                var parent = gameObject.transform.parent;

                if (parent != null)
                {
                    var cameraTransform = camera.transform;

                    Billboard(cameraTransform, parent);
                }
                else if (!name.EndsWith("(Clone)")) // rely on root manager parent error - prefabs don't have rootManager.parent
                {
                    Debug.LogErrorFormat("GameObject '{0}' requires a parent object for a SpriteBillboarder to work!",
                        gameObject.name);
                }
            }
        }

        /// <summary>
        /// Billboards this sprite.
        /// </summary>
        /// <param name="cameraTransform"></param>
        /// <param name="parent"></param>
        private void Billboard(Transform cameraTransform, Transform parent)
        {
            Vector3 lookAtPosition;

            lookAtPosition = pointAtCamera ?
                GetLookAtCamera(cameraTransform, transform)
                : GetLookAtSameDirectionAsCamera(cameraTransform, transform);
            
            transform.LookAt(lookAtPosition, parent.up);
        }

        private Vector3 GetLookAtSameDirectionAsCamera(Transform cameraTransform, Transform transform)
        {
            var camForward = cameraTransform.forward;

            var lookAtPosition = transform.position + camForward;

            if (!billboardV)
            {
                lookAtPosition = transform.position + new Vector3(camForward.x, 0, camForward.z);
            }

            return lookAtPosition;
        }

        private Vector3 GetLookAtCamera(Transform cameraTransform, Transform transform)
        {
            var lookAtPosition = 2 * transform.position - cameraTransform.position;

            if (!billboardV)
            {
                lookAtPosition.y = transform.position.y;
            }

            return lookAtPosition;
        }
    }
}
