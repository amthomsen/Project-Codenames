using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FullSerializer;
using UnityEngine;
using UnityEditor;

public class Serializer
{
    private static readonly fsSerializer _serializer = new fsSerializer();
    private static readonly string _fileExtenstion = ".halal";

    public static string Serialize<T>(T value)
    {
        var type = typeof (T);
        fsData data;
        var success = _serializer.TrySerialize(type, value, out data);
        return fsJsonPrinter.PrettyJson(data);
    }

    public static T Deserialize<T>(string serializedState)
    {
        var type = typeof (T);
        fsData data;
        var success = fsJsonParser.Parse(serializedState, out data);
        object deserialized = null;
        success = _serializer.TryDeserialize(data, type, ref deserialized);
        return (T) deserialized;
    }

    //public static T Load<T>(string fileName)
    public static T Load<T>(string path)
    {
        //var finalPath = Path.Combine(Application.persistentDataPath, fileName + _fileExtenstion);
        var finalPath = path;
        try
        {
            if (File.Exists(finalPath))
            {
                var type = typeof (T);
                var bf = new BinaryFormatter();
                var file = File.ReadAllText(finalPath);
                var temp = (T) Deserialize(type, file);
                return temp;
            }
            return default(T);
        }
        catch (Exception e)
        {
            //Debug.Log("Malformed file brah: " + fileName);
            Debug.Log("Malformed file brah: " + path);
            Debug.Log(e.Message);
            return default(T);
        }
    }

    public static string GetFileInternal(string fileName)
    {
        var finalPath = Path.Combine(Application.streamingAssetsPath, fileName + _fileExtenstion);
        try
        {
            if (File.Exists(finalPath))
            {
                var file = File.ReadAllText(finalPath);
                return file;
            }
            return "";
        }
        catch (Exception)
        {
            Debug.Log("Malformed file brah: " + fileName);
            return "";
        }
    }

    public static T LoadInternal<T>(string fileName)
    {
        var finalPath = Path.Combine(Application.streamingAssetsPath, fileName + _fileExtenstion);

        try
        {
            if (File.Exists(finalPath))
            {
                var type = typeof (T);
                var bf = new BinaryFormatter();
                var file = File.ReadAllText(finalPath);
                var temp = (T) Deserialize(type, file);
                return temp;
            }
            return default(T);
        }
        catch (Exception exception)
        {
            Debug.Log("Malformed file brah: " + fileName);
            return default(T);
        }
    }

    //public static void Save<T>(T item, string fileName, string comments = "")
    public static void Save<T>(T item, string path, string comments = "")
    {
        var type = typeof (T);
        //var finalPath = Path.Combine(Application.persistentDataPath, fileName + _fileExtenstion);
        var finalPath = path;

        var serializedData = Serialize(type, item);
         //Debug.Log(finalPath);
        File.WriteAllText(finalPath, serializedData + comments);
    }

    public static void SaveInternal<T>(T item, string fileName)
    {
        var type = typeof (T);
        var finalPath = Path.Combine(Application.streamingAssetsPath, fileName + _fileExtenstion);
        var serializedData = Serialize(type, item);
        Debug.Log(finalPath);
        File.WriteAllText(finalPath, serializedData);
    }

    public static string Serialize(Type type, object value)
    {
        // serialize the data
        fsData data;
        var success = _serializer.TrySerialize(type, value, out data);
        //if (success.Failed) throw new Exception(success.FailureReason);

        // emit the data via JSON
        return fsJsonPrinter.PrettyJson(data);
    }

    public static object Deserialize(Type type, string serializedState)
    {
        // step 1: parse the JSON data
        fsData data;
        var success = fsJsonParser.Parse(serializedState, out data);
        //if (success.Failed) throw new Exception(success.FailureReason);

        // step 2: deserialize the data
        object deserialized = null;
        success = _serializer.TryDeserialize(data, type, ref deserialized);
        // if (success.Failed) throw new Exception(success.FailureReason);

        return deserialized;
    }
}
