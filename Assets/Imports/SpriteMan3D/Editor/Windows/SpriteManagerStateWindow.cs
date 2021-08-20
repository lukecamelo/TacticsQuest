using UnityEditor;
using UnityEngine;
using System.Linq;

namespace SpriteMan3D.UnityEditor
{
    internal class SpriteManagerStateWindow : EditorWindow
    {
        public SerializedObject serializedObject;
        public SerializedProperty states;
        private CardinalDirection stateDirection;
        private int frameCount = -1;
        private DirectionSelector directionSelector = new DirectionSelector();

        public SerializedProperty state;
        public int stateIndex = 0;

        private Vector2 scrollPosition = Vector2.zero;

        internal class DirectionSelector
        {
            private CardinalDirection selectedDirection;

            public void SetSelected(CardinalDirection direction)
            {
                selectedDirection = direction;
            }

            public bool IsSelected(CardinalDirection direction)
            {
                return selectedDirection == direction;
            }
        }

        private static class DirectionPrefabs
        {
            public static Texture2D TwoWay { get; private set; }
            public static Texture2D TwoWayE { get; private set; }
            public static Texture2D TwoWayW { get; private set; }

            public static Texture2D FourWay { get; private set; }
            public static Texture2D FourWayN { get; private set; }
            public static Texture2D FourWayE { get; private set; }
            public static Texture2D FourWayS { get; private set; }
            public static Texture2D FourWayW { get; private set; }

            public static Texture2D EightWay { get; private set; }
            public static Texture2D EightWayN { get; private set; }
            public static Texture2D EightWayNE { get; private set; }
            public static Texture2D EightWayE { get; private set; }
            public static Texture2D EightWaySE { get; private set; }
            public static Texture2D EightWayS { get; private set; }
            public static Texture2D EightWaySW { get; private set; }
            public static Texture2D EightWayW { get; private set; }
            public static Texture2D EightWayNW { get; private set; }

            public static Texture2D SixteenWay { get; private set; }
            public static Texture2D SixteenWayN { get; private set; }
            public static Texture2D SixteenWayNNE { get; private set; }
            public static Texture2D SixteenWayNE { get; private set; }
            public static Texture2D SixteenWayENE { get; private set; }
            public static Texture2D SixteenWayE { get; private set; }
            public static Texture2D SixteenWayESE { get; private set; }
            public static Texture2D SixteenWaySE { get; private set; }
            public static Texture2D SixteenWaySSE { get; private set; }
            public static Texture2D SixteenWayS { get; private set; }
            public static Texture2D SixteenWaySSW { get; private set; }
            public static Texture2D SixteenWaySW { get; private set; }
            public static Texture2D SixteenWayWSW { get; private set; }
            public static Texture2D SixteenWayW { get; private set; }
            public static Texture2D SixteenWayWNW { get; private set; }
            public static Texture2D SixteenWayNW { get; private set; }
            public static Texture2D SixteenWayNNW { get; private set; }

            public static Texture2D DirectionButton { get; private set; }
            public static Texture2D DirectionButtonActive { get; private set; }

            public static Texture2D ThirtyTwoWay { get; private set; }
            public static Texture2D ThirtyTwoWayN { get; private set; }
            public static Texture2D ThirtyTwoWayNbE { get; private set; }
            public static Texture2D ThirtyTwoWayNNE { get; private set; }
            public static Texture2D ThirtyTwoWayNEbN { get; private set; }
            public static Texture2D ThirtyTwoWayNE { get; private set; }
            public static Texture2D ThirtyTwoWayNEbE { get; private set; }
            public static Texture2D ThirtyTwoWayENE { get; private set; }
            public static Texture2D ThirtyTwoWayEbN { get; private set; }
            public static Texture2D ThirtyTwoWayE { get; private set; }
            public static Texture2D ThirtyTwoWayEbS { get; private set; }
            public static Texture2D ThirtyTwoWayESE { get; private set; }
            public static Texture2D ThirtyTwoWaySEbE { get; private set; }
            public static Texture2D ThirtyTwoWaySE { get; private set; }
            public static Texture2D ThirtyTwoWaySEbS { get; private set; }
            public static Texture2D ThirtyTwoWaySSE { get; private set; }
            public static Texture2D ThirtyTwoWaySbE { get; private set; }
            public static Texture2D ThirtyTwoWayS { get; private set; }
            public static Texture2D ThirtyTwoWaySbW { get; private set; }
            public static Texture2D ThirtyTwoWaySSW { get; private set; }
            public static Texture2D ThirtyTwoWaySWbS { get; private set; }
            public static Texture2D ThirtyTwoWaySW { get; private set; }
            public static Texture2D ThirtyTwoWaySWbW { get; private set; }
            public static Texture2D ThirtyTwoWayWSW { get; private set; }
            public static Texture2D ThirtyTwoWayWbS { get; private set; }
            public static Texture2D ThirtyTwoWayW { get; private set; }
            public static Texture2D ThirtyTwoWayWbN { get; private set; }
            public static Texture2D ThirtyTwoWayWNW { get; private set; }
            public static Texture2D ThirtyTwoWayNWbW { get; private set; }
            public static Texture2D ThirtyTwoWayNW { get; private set; }
            public static Texture2D ThirtyTwoWayNWbN { get; private set; }
            public static Texture2D ThirtyTwoWayNNW { get; private set; }
            public static Texture2D ThirtyTwoWayNbW { get; private set; }

            private static Texture2D GetTextureByName(Sprite[] sprites, string frameName)
            {
                Texture2D result = null;

                var sprite = sprites.FirstOrDefault(o => o.name == frameName);
                if(sprite != null)
                {
                    var rect = sprite.rect;
                    result = new Texture2D((int)rect.width, (int)rect.height);
                    var pixels = sprite.texture.GetPixels(
                        (int)sprite.textureRect.x,
                        (int)sprite.textureRect.y,
                        (int)sprite.textureRect.width,
                        (int)sprite.textureRect.height);
                    result.SetPixels(pixels);
                    result.Apply();
                    result.hideFlags = HideFlags.HideAndDontSave;
                }

                return result;
            }

            static DirectionPrefabs()
            {
                var sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/SpriteMan3D/Images/all-dirs.png").OfType<Sprite>().ToArray();

                TwoWay = GetTextureByName(sprites, "4-way");
                TwoWayE = GetTextureByName(sprites, "4-way-e");
                TwoWayW = GetTextureByName(sprites, "4-way-w");

                FourWay = GetTextureByName(sprites, "4-way");
                FourWayN = GetTextureByName(sprites, "4-way-n");
                FourWayE = GetTextureByName(sprites, "4-way-e");
                FourWayS = GetTextureByName(sprites, "4-way-s");
                FourWayW = GetTextureByName(sprites, "4-way-w");

                EightWay = GetTextureByName(sprites, "8-way");
                EightWayN = GetTextureByName(sprites, "8-way-n");
                EightWayNE = GetTextureByName(sprites, "8-way-ne");
                EightWayE = GetTextureByName(sprites, "8-way-e");
                EightWaySE = GetTextureByName(sprites, "8-way-se");
                EightWayS = GetTextureByName(sprites, "8-way-s");
                EightWaySW = GetTextureByName(sprites, "8-way-sw");
                EightWayW = GetTextureByName(sprites, "8-way-w");
                EightWayNW = GetTextureByName(sprites, "8-way-nw");

                SixteenWay = GetTextureByName(sprites, "16-way");
                SixteenWayN = GetTextureByName(sprites, "16-way-n");
                SixteenWayNNE = GetTextureByName(sprites, "16-way-nne");
                SixteenWayNE = GetTextureByName(sprites, "16-way-ne");
                SixteenWayENE = GetTextureByName(sprites, "16-way-ene");
                SixteenWayE = GetTextureByName(sprites, "16-way-e");
                SixteenWayESE = GetTextureByName(sprites, "16-way-ese");
                SixteenWaySE = GetTextureByName(sprites, "16-way-se");
                SixteenWaySSE = GetTextureByName(sprites, "16-way-sse");
                SixteenWayS = GetTextureByName(sprites, "16-way-s");
                SixteenWaySSW = GetTextureByName(sprites, "16-way-ssw");
                SixteenWaySW = GetTextureByName(sprites, "16-way-sw");
                SixteenWayWSW = GetTextureByName(sprites, "16-way-wsw");
                SixteenWayW = GetTextureByName(sprites, "16-way-w");
                SixteenWayWNW = GetTextureByName(sprites, "16-way-wnw");
                SixteenWayNW = GetTextureByName(sprites, "16-way-nw");
                SixteenWayNNW = GetTextureByName(sprites, "16-way-nnw");

                DirectionButton = GetTextureByName(sprites, "dir-button");
                DirectionButtonActive = GetTextureByName(sprites, "dir-button-active");

                var sprites32 = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/SpriteMan3D/Images/32-way.png").OfType<Sprite>().ToArray();

                ThirtyTwoWay = GetTextureByName(sprites32, "32-way");
                ThirtyTwoWayN = GetTextureByName(sprites32, "32-way-n");
                ThirtyTwoWayNbE = GetTextureByName(sprites32, "32-way-nbe");
                ThirtyTwoWayNNE = GetTextureByName(sprites32, "32-way-nne");
                ThirtyTwoWayNEbN = GetTextureByName(sprites32, "32-way-nebn");
                ThirtyTwoWayNE = GetTextureByName(sprites32, "32-way-ne");
                ThirtyTwoWayNEbE = GetTextureByName(sprites32, "32-way-nebe");
                ThirtyTwoWayENE = GetTextureByName(sprites32, "32-way-ene");
                ThirtyTwoWayEbN = GetTextureByName(sprites32, "32-way-ebn");
                ThirtyTwoWayE = GetTextureByName(sprites32, "32-way-e");
                ThirtyTwoWayEbS = GetTextureByName(sprites32, "32-way-ebs");
                ThirtyTwoWayESE = GetTextureByName(sprites32, "32-way-ese");
                ThirtyTwoWaySEbE = GetTextureByName(sprites32, "32-way-sebe");
                ThirtyTwoWaySE = GetTextureByName(sprites32, "32-way-se");
                ThirtyTwoWaySEbS = GetTextureByName(sprites32, "32-way-sebs");
                ThirtyTwoWaySSE = GetTextureByName(sprites32, "32-way-sse");
                ThirtyTwoWaySbE = GetTextureByName(sprites32, "32-way-sbe");
                ThirtyTwoWayS = GetTextureByName(sprites32, "32-way-s");
                ThirtyTwoWaySbW = GetTextureByName(sprites32, "32-way-sbw");
                ThirtyTwoWaySSW = GetTextureByName(sprites32, "32-way-ssw");
                ThirtyTwoWaySWbS = GetTextureByName(sprites32, "32-way-swbs");
                ThirtyTwoWaySW = GetTextureByName(sprites32, "32-way-sw");
                ThirtyTwoWaySWbW = GetTextureByName(sprites32, "32-way-swbw");
                ThirtyTwoWayWSW = GetTextureByName(sprites32, "32-way-wsw");
                ThirtyTwoWayWbS = GetTextureByName(sprites32, "32-way-wbs");
                ThirtyTwoWayW = GetTextureByName(sprites32, "32-way-w");
                ThirtyTwoWayWbN = GetTextureByName(sprites32, "32-way-wbn");
                ThirtyTwoWayWNW = GetTextureByName(sprites32, "32-way-wnw");
                ThirtyTwoWayNWbW = GetTextureByName(sprites32, "32-way-nwbw");
                ThirtyTwoWayNW = GetTextureByName(sprites32, "32-way-nw");
                ThirtyTwoWayNWbN = GetTextureByName(sprites32, "32-way-nwbn");
                ThirtyTwoWayNNW = GetTextureByName(sprites32, "32-way-nnw");
                ThirtyTwoWayNbW = GetTextureByName(sprites32, "32-way-nbw");
            }
        }

        void OnSelectionChange()
        {
            var activeObject = Selection.activeGameObject;

            if(activeObject == null)
            {
                serializedObject = null;
                state = null;

                Repaint();
            }

            if (activeObject != null)
            {
                var manager = activeObject.GetComponent<SpriteManager>();
                if (manager != null)
                {
                    var serObj = new SerializedObject(manager);
                    var states = serObj.FindProperty("stateMapping");

                    if (states.arraySize > 0)
                    {
                        var state = states.GetArrayElementAtIndex(0);

                        this.serializedObject = serObj;
                        this.states = states;
                        this.state = state;
                        this.Repaint();
                    }
                }
                else if (serializedObject != null && this.state != null)
                {
                    serializedObject = null;
                    states = null;
                    state = null;
                    this.Repaint();
                }
            }
        }

        void DisplayStates()
        {
            if (states.arraySize > 0)
            {
                var options = new string[states.arraySize];

                for (int x = 0; x < states.arraySize; x++)
                {
                    var state = states.GetArrayElementAtIndex(x);
                    var name = state.FindPropertyRelative("stateName");
                    options[x] = string.Format("[{0}] {1}", x.ToString(), name.stringValue);
                }

                int popupIndex = stateIndex;
                if (stateIndex >= states.arraySize)
                {
                    stateIndex = 0;
                }

                stateIndex = EditorGUILayout.Popup("States", stateIndex, options);
                this.state = states.GetArrayElementAtIndex(stateIndex);
            }
        }

        void OnGUI()
        {
            if (serializedObject != null && states != null && state != null)
            {
                DisplayStates();

                UpdateDirectionCounts();

                serializedObject.Update();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                GUILayout.Label("Settings", EditorStyles.boldLabel);

                {
                    EditorGUILayout.BeginHorizontal();

                    var leftWidth = GUILayout.Width(250f);

                    {
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.PropertyField(state.FindPropertyRelative("stateName"), leftWidth);
                        
                        var height = GUILayout.Height(40f);
                        var width = GUILayout.Width(40f);

                        var mode = (DirectionMode)serializedObject.FindProperty("directionMode").enumValueIndex;
                        var wayImage = GetWayImage(mode, stateDirection);

                        {
                            var leftStyle = new GUIStyle();
                            leftStyle.margin = new RectOffset(10, 0, 10, 0);

                            EditorGUILayout.BeginVertical(leftStyle);

                            TopButtons(mode, height, width);
                            {
                                var style = new GUIStyle();
                                style.margin = new RectOffset(0, 0, 25, 0);

                                EditorGUILayout.BeginHorizontal(style, GUILayout.Width(290f));

                                LeftButtons(mode, height, width);

                                Rect rt = GUILayoutUtility.GetRect(128f, 128f);
                                GUI.Label(rt, wayImage);

                                RightButtons(mode, height, width);

                                EditorGUILayout.EndHorizontal();
                            }
                            BottomButtons(mode, height, width);

                            EditorGUILayout.EndVertical();
                        }

                        EditorGUILayout.EndVertical();
                    }

                    {
                        EditorGUILayout.BeginVertical();

                        var directions = state.FindPropertyRelative("directions");
                        
                        SerializedProperty direction = null;
                        int dir = 0;

                        for(int x = 0; x < directions.arraySize; x++)
                        {
                            direction = directions.GetArrayElementAtIndex(x);
                            var elemDir = direction.FindPropertyRelative("direction").intValue;
                            if(elemDir == (int)stateDirection)
                            {
                                dir = elemDir;
                                break;
                            }
                        }

                        EditorGUILayout.LabelField("Frame Count");
                        EditorGUILayout.PropertyField(state.FindPropertyRelative("frameCount"), GUIContent.none);

                        if (dir > 0)
                        {
                            GUILayout.Label(string.Format("Direction: {0}", ((CardinalDirection)dir).ToString()), EditorStyles.boldLabel);

                            var frames = direction.FindPropertyRelative("frames");

                            EditorGUILayout.LabelField(new GUIContent("Frames"));
                            for (int x = 0; x < frames.arraySize; x++)
                            {
                                EditorGUILayout.PropertyField(frames.GetArrayElementAtIndex(x), GUIContent.none);
                            }
                        }

                        EditorGUILayout.EndVertical();
                    }
                    
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();

                serializedObject.ApplyModifiedProperties();
            }
        }

        private void UpdateDirectionCounts()
        {
            var size = state.FindPropertyRelative("frameCount").intValue;

            if (frameCount < 0)
            {
                frameCount = size;
            }
            else if (size != frameCount)
            {
                frameCount = size;

                serializedObject.Update();

                var directions = state.FindPropertyRelative("directions");
                for (int x = 0; x < directions.arraySize; x++)
                {
                    var direction = directions.GetArrayElementAtIndex(x);
                    var frames = direction.FindPropertyRelative("frames");
                    frames.arraySize = size;
                }

                serializedObject.ApplyModifiedProperties();
            }
        }

        private GUIContent GetWayImage(DirectionMode mode, CardinalDirection stateDirection)
        {
            Texture2D texture = null;

            if(mode == DirectionMode.TwoWay)
            {
                texture = DirectionPrefabs.TwoWay;
                if (stateDirection == CardinalDirection.E)
                {
                    texture = DirectionPrefabs.TwoWayE;
                }
                else if (stateDirection == CardinalDirection.W)
                {
                    texture = DirectionPrefabs.TwoWayW;
                }
            }
            else if (mode == DirectionMode.FourWay)
            {
                texture = DirectionPrefabs.FourWay;
                if (stateDirection == CardinalDirection.N)
                {
                    texture = DirectionPrefabs.FourWayN;
                }
                else if(stateDirection == CardinalDirection.E)
                {
                    texture = DirectionPrefabs.FourWayE;
                }
                else if(stateDirection == CardinalDirection.S)
                {
                    texture = DirectionPrefabs.FourWayS;
                }
                else if(stateDirection == CardinalDirection.W)
                {
                    texture = DirectionPrefabs.FourWayW;
                }
            }
            else if(mode == DirectionMode.EightWay)
            {
                texture = DirectionPrefabs.EightWay;
                if (stateDirection == CardinalDirection.N)
                {
                    texture = DirectionPrefabs.EightWayN;
                }
                else if (stateDirection == CardinalDirection.NE)
                {
                    texture = DirectionPrefabs.EightWayNE;
                }
                else if (stateDirection == CardinalDirection.E)
                {
                    texture = DirectionPrefabs.EightWayE;
                }
                else if (stateDirection == CardinalDirection.SE)
                {
                    texture = DirectionPrefabs.EightWaySE;
                }
                else if (stateDirection == CardinalDirection.S)
                {
                    texture = DirectionPrefabs.EightWayS;
                }
                else if (stateDirection == CardinalDirection.SW)
                {
                    texture = DirectionPrefabs.EightWaySW;
                }
                else if (stateDirection == CardinalDirection.W)
                {
                    texture = DirectionPrefabs.EightWayW;
                }
                else if (stateDirection == CardinalDirection.NW)
                {
                    texture = DirectionPrefabs.EightWayNW;
                }
            }
            else if(mode == DirectionMode.SixteenWay)
            {
                texture = DirectionPrefabs.SixteenWay;
                if (stateDirection == CardinalDirection.N)
                {
                    texture = DirectionPrefabs.SixteenWayN;
                }
                else if (stateDirection == CardinalDirection.NNE)
                {
                    texture = DirectionPrefabs.SixteenWayNNE;
                }
                else if (stateDirection == CardinalDirection.NE)
                {
                    texture = DirectionPrefabs.SixteenWayNE;
                }
                else if (stateDirection == CardinalDirection.ENE)
                {
                    texture = DirectionPrefabs.SixteenWayENE;
                }
                else if (stateDirection == CardinalDirection.E)
                {
                    texture = DirectionPrefabs.SixteenWayE;
                }
                else if (stateDirection == CardinalDirection.ESE)
                {
                    texture = DirectionPrefabs.SixteenWayESE;
                }
                else if (stateDirection == CardinalDirection.SE)
                {
                    texture = DirectionPrefabs.SixteenWaySE;
                }
                else if (stateDirection == CardinalDirection.SSE)
                {
                    texture = DirectionPrefabs.SixteenWaySSE;
                }
                else if (stateDirection == CardinalDirection.S)
                {
                    texture = DirectionPrefabs.SixteenWayS;
                }
                else if (stateDirection == CardinalDirection.SSW)
                {
                    texture = DirectionPrefabs.SixteenWaySSW;
                }
                else if (stateDirection == CardinalDirection.SW)
                {
                    texture = DirectionPrefabs.SixteenWaySW;
                }
                else if (stateDirection == CardinalDirection.WSW)
                {
                    texture = DirectionPrefabs.SixteenWayWSW;
                }
                else if (stateDirection == CardinalDirection.W)
                {
                    texture = DirectionPrefabs.SixteenWayW;
                }
                else if (stateDirection == CardinalDirection.WNW)
                {
                    texture = DirectionPrefabs.SixteenWayWNW;
                }
                else if (stateDirection == CardinalDirection.NW)
                {
                    texture = DirectionPrefabs.SixteenWayNW;
                }
                else if (stateDirection == CardinalDirection.NNW)
                {
                    texture = DirectionPrefabs.SixteenWayNNW;
                }
            }
            else if (mode == DirectionMode.ThirtyTwoWay)
            {
                texture = DirectionPrefabs.ThirtyTwoWay;

                switch(stateDirection)
                {
                    case CardinalDirection.NbW:
                        texture = DirectionPrefabs.ThirtyTwoWayNbW;
                        break;
                    case CardinalDirection.N:
                        texture = DirectionPrefabs.ThirtyTwoWayN;
                        break;
                    case CardinalDirection.NbE:
                        texture = DirectionPrefabs.ThirtyTwoWayNbE;
                        break;
                    case CardinalDirection.NNE:
                        texture = DirectionPrefabs.ThirtyTwoWayNNE;
                        break;
                    case CardinalDirection.NEbN:
                        texture = DirectionPrefabs.ThirtyTwoWayNEbN;
                        break;
                    case CardinalDirection.NE:
                        texture = DirectionPrefabs.ThirtyTwoWayNE;
                        break;
                    case CardinalDirection.NEbE:
                        texture = DirectionPrefabs.ThirtyTwoWayNEbE;
                        break;
                    case CardinalDirection.ENE:
                        texture = DirectionPrefabs.ThirtyTwoWayENE;
                        break;
                    case CardinalDirection.EbN:
                        texture = DirectionPrefabs.ThirtyTwoWayEbN;
                        break;
                    case CardinalDirection.E:
                        texture = DirectionPrefabs.ThirtyTwoWayE;
                        break;
                    case CardinalDirection.EbS:
                        texture = DirectionPrefabs.ThirtyTwoWayEbS;
                        break;
                    case CardinalDirection.ESE:
                        texture = DirectionPrefabs.ThirtyTwoWayESE;
                        break;
                    case CardinalDirection.SEbE:
                        texture = DirectionPrefabs.ThirtyTwoWaySEbE;
                        break;
                    case CardinalDirection.SE:
                        texture = DirectionPrefabs.ThirtyTwoWaySE;
                        break;
                    case CardinalDirection.SEbS:
                        texture = DirectionPrefabs.ThirtyTwoWaySEbS;
                        break;
                    case CardinalDirection.SSE:
                        texture = DirectionPrefabs.ThirtyTwoWaySSE;
                        break;
                    case CardinalDirection.SbE:
                        texture = DirectionPrefabs.ThirtyTwoWaySbE;
                        break;
                    case CardinalDirection.S:
                        texture = DirectionPrefabs.ThirtyTwoWayS;
                        break;
                    case CardinalDirection.SbW:
                        texture = DirectionPrefabs.ThirtyTwoWaySbW;
                        break;
                    case CardinalDirection.SSW:
                        texture = DirectionPrefabs.ThirtyTwoWaySSW;
                        break;
                    case CardinalDirection.SWbS:
                        texture = DirectionPrefabs.ThirtyTwoWaySWbS;
                        break;
                    case CardinalDirection.SW:
                        texture = DirectionPrefabs.ThirtyTwoWaySW;
                        break;
                    case CardinalDirection.SWbW:
                        texture = DirectionPrefabs.ThirtyTwoWaySWbW;
                        break;
                    case CardinalDirection.WSW:
                        texture = DirectionPrefabs.ThirtyTwoWayWSW;
                        break;
                    case CardinalDirection.WbS:
                        texture = DirectionPrefabs.ThirtyTwoWayWbS;
                        break;
                    case CardinalDirection.W:
                        texture = DirectionPrefabs.ThirtyTwoWayW;
                        break;
                    case CardinalDirection.WbN:
                        texture = DirectionPrefabs.ThirtyTwoWayWbN;
                        break;
                    case CardinalDirection.WNW:
                        texture = DirectionPrefabs.ThirtyTwoWayWNW;
                        break;
                    case CardinalDirection.NWbW:
                        texture = DirectionPrefabs.ThirtyTwoWayNWbW;
                        break;
                    case CardinalDirection.NW:
                        texture = DirectionPrefabs.ThirtyTwoWayNW;
                        break;
                    case CardinalDirection.NWbN:
                        texture = DirectionPrefabs.ThirtyTwoWayNWbN;
                        break;
                    case CardinalDirection.NNW:
                        texture = DirectionPrefabs.ThirtyTwoWayNNW;
                        break;
                }
            }

            var result = new GUIContent();
            result.image = texture;

            return result;
        }

        private GUIStyle GetButtonStyle(CardinalDirection direction)
        {
            var buttonState = new GUIStyleState();
            buttonState.background = DirectionPrefabs.DirectionButton;

            var activeState = GetActiveButtonState();

            var selected = directionSelector.IsSelected(direction);

            var style = new GUIStyle();
            style.normal = selected ? activeState : buttonState;
            style.active = activeState;
            style.alignment = TextAnchor.MiddleCenter;
            return style;
        }

        private GUIStyleState GetActiveButtonState()
        {
            var activeState = new GUIStyleState();
            activeState.background = DirectionPrefabs.DirectionButtonActive;

            return activeState;
        }

        private void TopButtons(DirectionMode mode, GUILayoutOption height, GUILayoutOption width)
        {
            if (mode == DirectionMode.ThirtyTwoWay)
            {
                EditorGUILayout.BeginHorizontal();

                {
                    var buttonContent = new GUIContent("NWbN", "Northwest by North");

                    var buttonStyle = GetButtonStyle(CardinalDirection.NWbN);
                    buttonStyle.margin = new RectOffset(63, 0, 15, 0);

                    if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                    {
                        stateDirection = CardinalDirection.NWbN;
                        directionSelector.SetSelected(CardinalDirection.NWbN);
                    }
                }

                {
                    var buttonContent = new GUIContent("NbW", "North by West");

                    var buttonStyle = GetButtonStyle(CardinalDirection.NbW);
                    buttonStyle.margin = new RectOffset(13, 0, 0, 0);

                    if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                    {
                        stateDirection = CardinalDirection.NbW;
                        directionSelector.SetSelected(CardinalDirection.NbW);
                    }
                }

                {
                    var buttonContent = new GUIContent("NbE", "North by East");

                    var buttonStyle = GetButtonStyle(CardinalDirection.NbE);
                    buttonStyle.margin = new RectOffset(15, 0, 0, 0);

                    if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                    {
                        stateDirection = CardinalDirection.NbE;
                        directionSelector.SetSelected(CardinalDirection.NbE);
                    }
                }

                {
                    var buttonContent = new GUIContent("NEbN", "Northeast by North");

                    var buttonStyle = GetButtonStyle(CardinalDirection.NEbN);
                    buttonStyle.margin = new RectOffset(13, 0, 15, 0);

                    if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                    {
                        stateDirection = CardinalDirection.NEbN;
                        directionSelector.SetSelected(CardinalDirection.NEbN);
                    }
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal(GUILayout.Width(250f));

            if (mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("NWbW", "Northwest by West");

                var buttonStyle = GetButtonStyle(CardinalDirection.NWbW);
                buttonStyle.margin = new RectOffset(10, 0, 25, 0);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.NWbW;
                    directionSelector.SetSelected(CardinalDirection.NWbW);
                }
            }

            if (mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var left = mode == DirectionMode.ThirtyTwoWay ? 13 : 37;

                var buttonContent = new GUIContent("NW", "Northwest");

                var buttonStyle = GetButtonStyle(CardinalDirection.NW);
                buttonStyle.margin = new RectOffset(left, 0, 25, 0);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.NW;
                    directionSelector.SetSelected(CardinalDirection.NW);
                }
            }
            else
            {
                GUILayout.Label("", height, GUILayout.Width(70f));
            }

            if (mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("NNW", "North-Northwest");

                var buttonStyle = GetButtonStyle(CardinalDirection.NNW);
                buttonStyle.margin = new RectOffset(0, 0, 7, 0);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.NNW;
                    directionSelector.SetSelected(CardinalDirection.NNW);
                }
            }
            else
            {
                GUILayout.Label("", height, width);
            }

            if (mode == DirectionMode.FourWay || mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("N", "North");

                var buttonStyle = GetButtonStyle(CardinalDirection.N);
                buttonStyle.margin = new RectOffset(3, 3, 0, 0);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.N;
                    directionSelector.SetSelected(CardinalDirection.N);
                }
            }

            if (mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("NNE", "North-Northeast");

                var buttonStyle = GetButtonStyle(CardinalDirection.NNE);
                buttonStyle.margin = new RectOffset(0, 0, 7, 0);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.NNE;
                    directionSelector.SetSelected(CardinalDirection.NNE);
                }
            }
            else
            {
                GUILayout.Label("", height, width);
            }

            if (mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("NE", "Northeast");

                var buttonStyle = GetButtonStyle(CardinalDirection.NE);
                buttonStyle.margin = new RectOffset(0, 0, 25, 0);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.NE;
                    directionSelector.SetSelected(CardinalDirection.NE);
                }
            }
            else
            {
                GUILayout.Label("", height, GUILayout.Width(70f));
            }

            if (mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("NEbE", "Northeast by East");

                var buttonStyle = GetButtonStyle(CardinalDirection.NEbE);
                buttonStyle.margin = new RectOffset(13, 0, 25, 0);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.NEbE;
                    directionSelector.SetSelected(CardinalDirection.NEbE);
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        private void LeftButtons(DirectionMode mode, GUILayoutOption height, GUILayoutOption width)
        {
            if (mode == DirectionMode.ThirtyTwoWay)
            {
                EditorGUILayout.BeginVertical();

                {
                    var buttonContent = new GUIContent("WbN", "West by North");

                    var buttonStyle = GetButtonStyle(CardinalDirection.WbN);
                    buttonStyle.margin = new RectOffset(0, 0, 15, 0);

                    if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                    {
                        stateDirection = CardinalDirection.WbN;
                        directionSelector.SetSelected(CardinalDirection.WbN);
                    }
                }

                {
                    var buttonContent = new GUIContent("WbS", "West by South");

                    var buttonStyle = GetButtonStyle(CardinalDirection.WbS);
                    buttonStyle.margin = new RectOffset(0, 0, 20, 0);

                    if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                    {
                        stateDirection = CardinalDirection.WbS;
                        directionSelector.SetSelected(CardinalDirection.WbS);
                    }
                }

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.BeginVertical(GUILayout.Height(135f));
            GUILayout.FlexibleSpace();

            if (mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("WNW", "West-Northwest");

                var buttonStyle = GetButtonStyle(CardinalDirection.WNW);
                buttonStyle.margin = new RectOffset(7, 0, 0, 0);

                if(GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.WNW;
                    directionSelector.SetSelected(CardinalDirection.WNW);
                }
            }

            if (mode == DirectionMode.TwoWay || mode == DirectionMode.FourWay || mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("W", "West");

                var buttonStyle = GetButtonStyle(CardinalDirection.W);
                buttonStyle.margin = new RectOffset(0, 22, 5, 5);

                if(GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.W;
                    directionSelector.SetSelected(CardinalDirection.W);
                }
            }

            if (mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("WSW", "West-Southwest");

                var buttonStyle = GetButtonStyle(CardinalDirection.WSW);
                buttonStyle.margin = new RectOffset(7, 0, 0, 0);

                if(GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.WSW;
                    directionSelector.SetSelected(CardinalDirection.WSW);
                }
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();
        }

        private void RightButtons(DirectionMode mode, GUILayoutOption height, GUILayoutOption width)
        {
            EditorGUILayout.BeginVertical(GUILayout.Height(135f));
            GUILayout.FlexibleSpace();

            if (mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("ENE", "East-Northeast");

                var buttonStyle = GetButtonStyle(CardinalDirection.ENE);
                buttonStyle.margin = new RectOffset(15, 7, 0, 0);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.ENE;
                    directionSelector.SetSelected(CardinalDirection.ENE);
                }
            }

            if (mode == DirectionMode.TwoWay || mode == DirectionMode.FourWay || mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("E", "East");
                
                var buttonStyle = GetButtonStyle(CardinalDirection.E);
                buttonStyle.margin = new RectOffset(22, 0, 5, 5);

                if(GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.E;
                    directionSelector.SetSelected(CardinalDirection.E);
                }
            }

            if (mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("ESE", "East-Southeast");

                var buttonStyle = GetButtonStyle(CardinalDirection.ESE);
                buttonStyle.margin = new RectOffset(15, 7, 0, 0);

                if(GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.ESE;
                    directionSelector.SetSelected(CardinalDirection.ESE);
                }
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();

            if (mode == DirectionMode.ThirtyTwoWay)
            {
                EditorGUILayout.BeginVertical();

                {
                    var buttonContent = new GUIContent("EbN", "East by North");

                    var buttonStyle = GetButtonStyle(CardinalDirection.EbN);
                    buttonStyle.margin = new RectOffset(0, 0, 15, 0);

                    if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                    {
                        stateDirection = CardinalDirection.EbN;
                        directionSelector.SetSelected(CardinalDirection.EbN);
                    }
                }

                {
                    var buttonContent = new GUIContent("EbS", "East by South");

                    var buttonStyle = GetButtonStyle(CardinalDirection.EbS);
                    buttonStyle.margin = new RectOffset(0, 0, 20, 0);

                    if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                    {
                        stateDirection = CardinalDirection.EbS;
                        directionSelector.SetSelected(CardinalDirection.EbS);
                    }
                }

                EditorGUILayout.EndVertical();
            }
        }

        private void BottomButtons(DirectionMode mode, GUILayoutOption height, GUILayoutOption width)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Width(250f));

            if (mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("SWbW", "Southwest by West");

                var buttonStyle = GetButtonStyle(CardinalDirection.SWbW);
                buttonStyle.margin = new RectOffset(10, 0, 0, 0);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.SWbW;
                    directionSelector.SetSelected(CardinalDirection.SWbW);
                }
            }

            if (mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var left = mode == DirectionMode.ThirtyTwoWay ? 13 : 35;

                var buttonContent = new GUIContent("SW", "Southwest");

                var buttonStyle = GetButtonStyle(CardinalDirection.SW);
                buttonStyle.margin = new RectOffset(left, 0, 0, 0);

                if(GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.SW;
                    directionSelector.SetSelected(CardinalDirection.SW);
                }
            }
            else
            {
                GUILayout.Label("", height, GUILayout.Width(70f));
            }

            if (mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("SSW", "South-Southwest");

                var buttonStyle = GetButtonStyle(CardinalDirection.SSW);
                buttonStyle.margin = new RectOffset(0, 0, 18, 0);

                if(GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.SSW;
                    directionSelector.SetSelected(CardinalDirection.SSW);
                }
            }
            else
            {
                GUILayout.Label("", height, width);
            }

            if (mode == DirectionMode.FourWay || mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("S", "South");

                var buttonStyle = GetButtonStyle(CardinalDirection.S);
                buttonStyle.margin = new RectOffset(3, 3, 25, 0);

                if(GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.S;
                    directionSelector.SetSelected(CardinalDirection.S);
                }
            }

            if (mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("SSE", "South-Southeast");

                var buttonStyle = GetButtonStyle(CardinalDirection.SSE);
                buttonStyle.margin = new RectOffset(0, 0, 18, 0);

                if(GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.SSE;
                    directionSelector.SetSelected(CardinalDirection.SSE);
                }
            }
            else
            {
                GUILayout.Label("", height, width);
            }

            if (mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay || mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("SE", "Southeast");

                var buttonStyle = GetButtonStyle(CardinalDirection.SE);
                buttonStyle.margin = new RectOffset(0, 0, 0, 25);

                if(GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.SE;
                    directionSelector.SetSelected(CardinalDirection.SE);
                }
            }
            else
            {
                GUILayout.Label("", height, GUILayout.Width(70f));
            }

            if (mode == DirectionMode.ThirtyTwoWay)
            {
                var buttonContent = new GUIContent("SEbE", "Southeast by East");

                var buttonStyle = GetButtonStyle(CardinalDirection.SEbE);
                buttonStyle.margin = new RectOffset(13, 0, 0, 0);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.SEbE;
                    directionSelector.SetSelected(CardinalDirection.SEbE);
                }
            }

            EditorGUILayout.EndHorizontal();

            if (mode == DirectionMode.ThirtyTwoWay)
            {
                EditorGUILayout.BeginHorizontal();

                {
                    var buttonContent = new GUIContent("SWbS", "Southwest by South");

                    var buttonStyle = GetButtonStyle(CardinalDirection.SWbS);
                    buttonStyle.margin = new RectOffset(63, 0, 10, 0);

                    if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                    {
                        stateDirection = CardinalDirection.SWbS;
                        directionSelector.SetSelected(CardinalDirection.SWbS);
                    }
                }

                {
                    var buttonContent = new GUIContent("SbW", "South by West");

                    var buttonStyle = GetButtonStyle(CardinalDirection.SbW);
                    buttonStyle.margin = new RectOffset(13, 0, 25, 0);

                    if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                    {
                        stateDirection = CardinalDirection.SbW;
                        directionSelector.SetSelected(CardinalDirection.SbW);
                    }
                }

                {
                    var buttonContent = new GUIContent("SbE", "South by East");

                    var buttonStyle = GetButtonStyle(CardinalDirection.SbE);
                    buttonStyle.margin = new RectOffset(15, 0, 25, 0);

                    if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                    {
                        stateDirection = CardinalDirection.SbE;
                        directionSelector.SetSelected(CardinalDirection.SbE);
                    }
                }

                {
                    var buttonContent = new GUIContent("SEbS", "Southeast by South");

                    var buttonStyle = GetButtonStyle(CardinalDirection.SEbS);
                    buttonStyle.margin = new RectOffset(13, 0, 10, 0);

                    if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                    {
                        stateDirection = CardinalDirection.SEbS;
                        directionSelector.SetSelected(CardinalDirection.SEbS);
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
