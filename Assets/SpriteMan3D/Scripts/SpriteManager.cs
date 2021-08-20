using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace SpriteMan3D
{
    /// <summary>
    /// A sprite manager controls all aspects of animating a multi-directional 2D sprite character in 3D environments.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteManager : MonoBehaviour, ISpriteManagerState
    {
        private SpriteRenderer spriteRend;
        //private int currentStateHash = IdleState.IdleHash;

        /// <summary>
        /// The direction mode of this sprite manager.
        /// </summary>
        public DirectionMode directionMode = DirectionMode.EightWay;
        /// <summary>
        /// Determine the reflection direction - east to west or west to east.
        /// </summary>
        public ReflectionMode reflectionMode = ReflectionMode.None;
        /// <summary>
        /// The current frame of a state's animation.
        /// </summary>
        /// <remarks>
        /// Animate this frame using an animator for the states of a character.
        /// Animation is managed with this variable so that states can animate
        /// independently of the directions a character points in. This keeps 
        /// the movement of characters fluid as a character rotates and sprites change.
        /// </remarks>
        [SerializeField]
        private float currentFrame = 0f;
        /// <summary>
        /// Sets the current state index of this manager.
        /// </summary>
        /// <remarks>
        /// Set the current state index on the first frame in animations.
        /// This is the state index shown next to State names.
        /// </remarks>
        [SerializeField]
        private float currentStateIndex = 0f;
        /// <summary>
        /// The SpriteManager to follow. For multi-layer characters, this lets an Animator animate only root managers. This manager will inherit info from its root.
        /// </summary>
        public SpriteManager rootManager;
        /// <summary>
        /// Allows disabling billboarding in case this is a child of a billboarded sprite manager.
        /// </summary>
        public bool billboard = true;
        /// <summary>
        /// Contains the character's states and sprites to use for animations.
        /// </summary>
        public CharacterStateMapping[] stateMapping = IdleState.GetNewDefaultIdleStateSet();
        
        /// <summary>
        /// ISpriteManagerState.Billboard property.
        /// </summary>
        public bool Billboard
        {
            get { return billboard; }
            set { billboard = value; }
        }

        /// <summary>
        /// ISpriteManagerState.DirectionMode property.
        /// </summary>
        public DirectionMode DirectionMode
        {
            get { return directionMode; }
            set { directionMode = value; }
        }

        /// <summary>
        /// ISpriteManagerState.ReflectionMode property.
        /// </summary>
        public ReflectionMode ReflectionMode
        {
            get { return reflectionMode; }
            set { reflectionMode = value; }
        }

        /// <summary>
        /// ISpriteManagerState.StateMapping property.
        /// </summary>
        public CharacterStateMapping[] StateMapping
        {
            get { return stateMapping; }
            set { stateMapping = value; }
        }

        void Start()
        {
            spriteRend = gameObject.GetComponent<SpriteRenderer>();
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
        /// works in new render pipeline
        /// </summary>
        /// <param name="context"></param>
        /// <param name="camera"></param>
        void MyWillRender(ScriptableRenderContext context, Camera camera)
        {
            MyWillRender(camera);
        }
            
        /// <summary>
        /// works in classic render pipeline
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
                var myStateIndex = (int)currentStateIndex;
                var frame = (int)currentFrame;

                // use the root transform fields when rootManager is set
                if (rootManager != null)
                {
                    parent = rootManager.transform.parent;
                    myStateIndex = (int)rootManager.currentStateIndex;
                    frame = (int)rootManager.currentFrame;
                }

                if (parent != null)
                {
                    var cameraTransform = camera.transform;

                    BillboardSprite(cameraTransform, parent);

                    var stateDir = GetStateDirection(cameraTransform, parent);

                    if (stateDir != CardinalDirection.NotSet)
                    {
                        stateDir = ApplyEastToWestReflection(stateDir);

                        var currState = stateMapping[myStateIndex];
                        UpdateDisplayedSprite(parent, currState, stateDir, frame);
                    }
                }
                else if (rootManager == null && !name.EndsWith("(Clone)")) // rely on root manager parent error - prefabs don't have rootManager.parent
                {
                    Debug.LogErrorFormat("GameObject '{0}' requires a parent object for a SpriteManager to work!",
                        gameObject.name);
                }
            }
        }

        /// <summary>
        /// Billboards this sprite.
        /// </summary>
        /// <param name="cameraTransform"></param>
        /// <param name="parent"></param>
        private void BillboardSprite(Transform cameraTransform, Transform parent)
        {            
            // billboards the sprite
            if (billboard)
            {
                var lookAtPosition = transform.position + cameraTransform.forward;
                Vector3 newLookAtPosition = new Vector3(lookAtPosition.x, transform.position.y, lookAtPosition.z);
                transform.LookAt(newLookAtPosition, parent.up);
            }
        }

        /// <summary>
        /// Finds the correct sprite direction to display.
        /// </summary>
        /// <param name="cameraTransform"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private CardinalDirection GetStateDirection(Transform cameraTransform, Transform parent)
        {
            // finds the correct angle to load
            var direction = parent.position - cameraTransform.position;
            var dirRot = Quaternion.LookRotation(direction).eulerAngles.y;
            var angle = parent.rotation.eulerAngles.y - dirRot;
            if (angle < 0)
            {
                angle = 360f + angle;
            }

            var angleMap = DirectionInventory.GetStateOptions(directionMode);

            var result = CardinalDirection.NotSet;
            if (angle >= 0f && angle <= 360f)
            {
                result = angleMap.GetDirection(angle);
            }

            return result;
        }

        /// <summary>
        /// Applies the east to west reflection if applicable.
        /// </summary>
        /// <param name="stateDir"></param>
        private CardinalDirection ApplyEastToWestReflection(CardinalDirection stateDir)
        {
            // sets the reflection if reflection is enabled
            Dictionary<CardinalDirection, CardinalDirection> reflectionMap = null;

            if (reflectionMode != ReflectionMode.None)
            {
                reflectionMap =
                    reflectionMode == ReflectionMode.EastToWest ?
                    DirectionInventory.EWReflectionMap :
                    DirectionInventory.WEReflectionMap;

                if (reflectionMap != null)
                {
                    var dirHasReflection = reflectionMap.Keys.Contains(stateDir);

                    if (dirHasReflection)
                    {
                        stateDir = reflectionMap[stateDir];
                    }
                    SetReflectionScale(dirHasReflection);
                }
            }

            return stateDir;
        }

        /// <summary>
        /// Flips or unflips the SpriteRenderer.flipX for EastToWest reflections.
        /// </summary>
        /// <param name="isReflected"></param>
        private void SetReflectionScale(bool isReflected)
        {
            if (spriteRend)
            {
                if (isReflected && !spriteRend.flipX)
                {
                    spriteRend.flipX = true;
                }
                else if (!isReflected && spriteRend.flipX)
                {
                    spriteRend.flipX = false;
                }
            }
        }

        /// <summary>
        /// Loads the correct sprite for the given direction.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="currState"></param>
        /// <param name="stateDir"></param>
        /// <param name="frame"></param>
        private void UpdateDisplayedSprite(Transform parent, CharacterStateMapping currState, CardinalDirection stateDir, int frame)
        {
            if (currState != null)
            {
                var dir = currState
                    .directions
                    .FirstOrDefault(o => o.direction == stateDir);

                if (dir != null)
                {
                    //Debug.Log(string.Format("{0}:{1}", name, dir.direction));

                    if (frame < dir.frames.Length)
                    {
                        var sprite = dir.frames[frame];

                        // this allows empty sprites to be set when rootManager is set
                        if ((sprite != null || rootManager != null) && spriteRend != null)
                        {
                            spriteRend.sprite = sprite;
                        }
                    }
                    else
                    {
                        Debug.LogError(
                            string.Format(
                                "Playing State '{0}' on '{1}' has '{2}' frames defined, but the animation is on frame index {3}. Please make sure its '{0}' animation doesn't set CurrentFrame beyond {4}, or add more frames to the '{0}' state.",
                                currState.stateName,
                                string.Format("{0} : {1} : SpriteManager", parent.name, gameObject.name),
                                currState.frameCount,
                                frame,
                                frame - 1));
                    }
                }
            }
        }
    }
}