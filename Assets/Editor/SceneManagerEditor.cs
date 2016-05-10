using UnityEngine;
using UnityEditor;

using System.Collections;
[CustomEditor(typeof(SceneManager))]
public class SceneManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        SceneManager sm = (SceneManager)target;

        #region //Room Prefab
        GUILayout.Label("Room Prefab", EditorStyles.boldLabel);
        sm.roomPrefab = (GameObject)EditorGUILayout.ObjectField("Room Prefab", sm.roomPrefab, typeof(GameObject), true);
        if (GUILayout.Button("Reload Prefab"))
            sm.ReloadRoom();

        GUILayout.Space(10f);
        #endregion
        
        #region //Room Colors

        /*
        GUILayout.Label("Room Colors", EditorStyles.boldLabel);

        sm.wallMaterial = (Material)EditorGUILayout.ObjectField("Wall Material", sm.wallMaterial, typeof(Material), true);
        sm.floorMaterial = (Material)EditorGUILayout.ObjectField("Floor Material", sm.floorMaterial, typeof(Material), true);
        sm.ceilingMaterial = (Material)EditorGUILayout.ObjectField("Ceiling Material", sm.ceilingMaterial, typeof(Material), true);


        //sm.wallColor = EditorGUILayout.ColorField("Wall Color", sm.wallColor);
        //sm.floorColor = EditorGUILayout.ColorField("Floor Color", sm.floorColor);
        //sm.ceilingColor = EditorGUILayout.ColorField("Ceiling Color", sm.ceilingColor);

        GUILayout.Space(10f);

        if (GUILayout.Button("Color All Surfaces"))
            sm.ColorAllSurfaces();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Color Walls"))
            sm.ColorWall();
        if (GUILayout.Button("Color Floor"))
            sm.ColorFloor();
        if (GUILayout.Button("Color Ceiling"))
            sm.ColorCeiling();
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

    */
        #endregion  
        
        #region //Skybox
        GUILayout.Label("Skybox", EditorStyles.boldLabel);
        sm.skybox = (Material)EditorGUILayout.ObjectField("Skybox", sm.skybox, typeof(Material), true);
        if (GUILayout.Button("Reload Skybox"))
            sm.ReloadSkybox();

        GUILayout.Space(10f);
        #endregion

        #region //Save and Load
        GUILayout.Label("Save/Load Presets", EditorStyles.boldLabel);

        if (sm.fileName == null) {
            GUILayout.Label("No Save Selected");
        }
        else {
            GUILayout.Label("Current Save File: " + sm.fileName);
        }

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
            sm.Save();
        if (GUILayout.Button("Load"))
            sm.Load();
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
        #endregion

        #region //Description

        GUILayout.Label("IMPORTANT:", EditorStyles.boldLabel);

        GUIStyle italicStyle = new GUIStyle(GUI.skin.label);
        italicStyle.fontStyle = FontStyle.Italic;
        italicStyle.wordWrap = true;
        GUILayout.TextArea("You can save and load presets as .dat files using this script. In order for this to work, the prefab containing the room as well as the skybox must be placed in the Resources folder." + 
            " \n\nAll room prefabs must be placed in Resources/Rooms/ and MUST have the tag \"Room\"" + 
           /* " \nAll surface materials must be placed in Resources/Materials/ " + */
            " \nAll skyboxes must be placed in Resources/Skyboxes/", italicStyle);
        GUILayout.Space(5f);
        #endregion
    }
}