using System;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEditor;
#endif

namespace Mamont.Data.Train
{
	[CreateAssetMenu(menuName = "Data/TrainDataConfig" , fileName = "TrainDataConfig.asset")]
	public class TrainDataConfigTable:ScriptableObject
	{
		public List<RowItem> ItemList = new();

		[Serializable]
		public class RowItem
		{
			public RowItem( TrainType _selfType )
			{
				this._selfType = _selfType;
				_movementSpeed = 10.0f;
				_extractionTime = 10.0f;
			}

			[SerializeField]
			private TrainType _selfType;
			public TrainType SelfType => _selfType;

			[SerializeField]
			private float _movementSpeed;
			public float MovementSpeed => _movementSpeed;

			[SerializeField]
			private float _extractionTime;
			public float ExtractionTime => _extractionTime;

		}

#if UNITY_EDITOR

		public void CheckState()
		{
			foreach( TrainType type in Enum.GetValues(typeof(TrainType)) )
			{
				bool contain = false;
				foreach( var item in ItemList )
				{
					if( item.SelfType == type )
					{
						contain = true;
						break;
					}
				}
				if( contain == false )
				{
					ItemList.Add(new RowItem(type));
				}
			}

		}

		[CustomEditor(typeof(TrainDataConfigTable))]
		class TrainDataConfigCustomizer:Editor
		{
			public override void OnInspectorGUI()
			{
				if( GUI.changed )
				{
					EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
				}
				DrawDefaultInspector();
				TrainDataConfigTable mt = (TrainDataConfigTable)target;
				Undo.RegisterCompleteObjectUndo(mt , "TrainDataConfigCustomizer");


				if( GUILayout.Button("CheckState" , GUILayout.ExpandWidth(false)) )
				{
					mt.CheckState();
				}
			}
		}

		[CustomPropertyDrawer(typeof(RowItem))]
		class RowItemDrawer:PropertyDrawer
		{
			public override void OnGUI( Rect position , SerializedProperty property , GUIContent label )
			{
				float x = position.x;
				float y = position.y;
				float w = position.width;
				float h = position.height;
				Rect r = new Rect(x , y , w , h);

				EditorGUI.indentLevel = 1;

				float px = position.x;
				float py = position.y;
				float width = 100.0f;
				float height = position.height;

				width = 120.0f;
				Rect valueRect = new Rect(px , py , width , height);
				int k = property.FindPropertyRelative("_selfType").enumValueIndex;
				EditorGUI.LabelField(valueRect , $"{(TrainType)k}");
				px += width;

				width = 120.0f;
				valueRect = new Rect(px , py , width , height);
				EditorGUI.LabelField(valueRect , "MovementSpeed - ") ;
				px += width;

				width = 120.0f;
				valueRect = new Rect(px , py , width , height);
				EditorGUI.PropertyField(valueRect , property.FindPropertyRelative("_movementSpeed") , GUIContent.none);
				px += width;

				width = 120.0f;
				valueRect = new Rect(px , py , width , height);
				EditorGUI.LabelField(valueRect , "ExtractionSpeed - ");
				px += width;

				width = 120.0f;
				valueRect = new Rect(px , py , width , height);
				EditorGUI.PropertyField(valueRect , property.FindPropertyRelative("_extractionTime") , GUIContent.none);
				px += width;
			}
		}


#endif
	}
}





