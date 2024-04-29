using UnityEditor;
using UnityEngine;

namespace Gokboerue.Tools
{
    [CustomEditor(typeof(GameAssets))]
    public class GameAssetsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GameAssets gameAssetScript = (GameAssets)target;
            
            gameAssetScript.AssetFileName = EditorGUILayout.TextField("File Name", gameAssetScript.AssetFileName);
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("prefabAssets"));
            serializedObject.ApplyModifiedProperties();
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            if (GUILayout.Button("Generate " + gameAssetScript.AssetFileName))
            {
                gameAssetScript.GenerateAssetData();
            }
        }
    }
}