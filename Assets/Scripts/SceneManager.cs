using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.IO;

public class SceneManager : MonoBehaviour {

    public GameObject roomPrefab;

    public Material wallMaterial;
    public Material floorMaterial;
    public Material ceilingMaterial;

    public Material skybox;
    public SceneData data;
    public string fileName = null;

    void Start()
    {
        ReloadSkybox();
    }

    public void ReloadRoom()
    {
        var currentRoom = GameObject.FindGameObjectWithTag("Room");
        if (currentRoom!= null){
            DestroyImmediate(currentRoom.gameObject);
        }
        Instantiate(roomPrefab, roomPrefab.transform.position, roomPrefab.transform.rotation);
    }

    public void ReloadSkybox()
    {
        RenderSettings.skybox = skybox;
    }
    /*
    public void ColorAllSurfaces()
    {
        ColorWall();
        ColorFloor();
        ColorCeiling();
    }

    public void ColorWall()
    {
        var walls = GameObject.FindGameObjectsWithTag("Wall");

        foreach (GameObject x in walls) {
            var renderer = x.GetComponent<MeshRenderer>();
            renderer.sharedMaterial = wallMaterial;
        }
    }

    public void ColorFloor()
    {
        var walls = GameObject.FindGameObjectsWithTag("Floor");

        foreach (GameObject x in walls) {
            var renderer = x.GetComponent<MeshRenderer>();
            renderer.sharedMaterial = floorMaterial;
        }
    }

    public void ColorCeiling()
    {
        var walls = GameObject.FindGameObjectsWithTag("Ceiling");

        foreach (GameObject x in walls) {
            var renderer = x.GetComponent<MeshRenderer>();
            renderer.sharedMaterial = ceilingMaterial;
        }
    }
    */
    public void Save()
    {
        var path = EditorUtility.SaveFilePanel("Save Preset", "", "newPreset", "dat");

        if(path.Length != 0){
            data.roomPrefabName = roomPrefab.name;
            data.wallColorName = wallMaterial.name;
            data.floorColorName = floorMaterial.name;
            data.ceilingColorName = ceilingMaterial.name;
            data.skyboxName = skybox.name;

            Serializer.Save<SceneData>(data, path);
        }

        #region //old save code
        //BinaryFormatter bf = new BinaryFormatter();


        //if (path.Length != 0)
        //{
        //    FileStream file = File.Create(path);
        //    //FileStream file = File.Create(Application.persistentDataPath + "/sceneInfo.dat");

        //    SceneData data = new SceneData();
        //    #region ////All data being saved
        //    data.roomPrefabName = roomPrefab.name;

        //        data.wallColor_r = wallColor.r;
        //        data.wallColor_g = wallColor.g;
        //        data.wallColor_b = wallColor.b;
        //        data.wallColor_a = wallColor.a;

        //        data.floorColor_r = floorColor.r;
        //        data.floorColor_g = floorColor.g;
        //        data.floorColor_b = floorColor.b;
        //        data.floorColor_a = floorColor.a;

        //        data.ceilingColor_r = ceilingColor.r;
        //        data.ceilingColor_g = ceilingColor.g;
        //        data.ceilingColor_b = ceilingColor.b;
        //        data.ceilingColor_a = ceilingColor.a;

        //        data.skyboxName = skybox.name;
        //    #endregion
        //    bf.Serialize(file, data);
        //    file.Close();
        //}
        #endregion
    }

    public void Load()
    {
        var path = EditorUtility.OpenFilePanel("Select Preset to Load", "", "dat");

        if (path.Length != 0){
            data = Serializer.Load<SceneData>(path);

            fileName = Path.GetFileName(path);

            roomPrefab = Resources.Load("Rooms/" + data.roomPrefabName) as GameObject;
            wallMaterial = Resources.Load("Materials/" + data.wallColorName) as Material;
            floorMaterial = Resources.Load("Materials/" + data.floorColorName) as Material;
            ceilingMaterial = Resources.Load("Materials/" + data.ceilingColorName) as Material;
            skybox = Resources.Load("Skyboxes/" + data.skyboxName) as Material;

            ReloadRoom();
            ReloadSkybox();
            //ColorAllSurfaces();
        }

        #region //old load code
        //if (File.Exists(Application.persistentDataPath + "/sceneInfo.dat"))
        //{
        //    BinaryFormatter bf = new BinaryFormatter();

        //    var path = EditorUtility.OpenFilePanel(
        //    "Select Preset to Load",
        //    "",
        //    "dat");

        //    if (path.Length != 0)
        //    {
        //        FileStream file = File.Open(path, FileMode.Open);
        //        fileName = file.Name;

        //        SceneData data = (SceneData)bf.Deserialize(file);
        //        file.Close();

        //        roomPrefab = Resources.Load(data.roomPrefabName) as GameObject;

        //        wallColor.r = data.wallColor_r;
        //        wallColor.g = data.wallColor_g;
        //        wallColor.b = data.wallColor_b;
        //        wallColor.a = data.wallColor_a;

        //        floorColor.r = data.floorColor_r;
        //        floorColor.g = data.floorColor_g;
        //        floorColor.b = data.floorColor_b;
        //        floorColor.a = data.floorColor_a;

        //        ceilingColor.r = data.ceilingColor_r;
        //        ceilingColor.g = data.ceilingColor_g;
        //        ceilingColor.b = data.ceilingColor_b;
        //        ceilingColor.a = data.ceilingColor_a;

        //        skybox = Resources.Load(data.skyboxName) as Material;
        //    }

        //    

        //    //UPDATE SKYBOX, SHOW CURRENT SAVE NAME
        //}
        #endregion
    }
}

[Serializable]
public class SceneData
{
    public string roomPrefabName;

    public string wallColorName;
    public string floorColorName;
    public string ceilingColorName;

    public string skyboxName;
}