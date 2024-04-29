using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Gokboerue.Gameplay
{
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            //Create a reference to our script
            MapGenerator levelGen = (MapGenerator)target;

            //List of editors to only show if we have elements in the map settings list
            List<Editor> mapEditors = new List<Editor>();

            for (int i = 0; i < levelGen.mapSettings.Count; i++)
            {
                if (levelGen.mapSettings[i] != null)
                {
                    Editor mapLayerEditor = CreateEditor(levelGen.mapSettings[i]);
                    mapEditors.Add(mapLayerEditor);
                }
            }
            //If we have more than one editor in our editor list, draw them out. Also draw the buttons
            if (mapEditors.Count > 0)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                for (int i = 0; i < mapEditors.Count; i++)
                {
                    mapEditors[i].OnInspectorGUI();
                }

                if (GUILayout.Button("Generate"))
                {
                    levelGen.GenerateMap();
                }


                if (GUILayout.Button("Clear"))
                {
                    levelGen.ClearMap();
                }
            }
        }
    }
}
