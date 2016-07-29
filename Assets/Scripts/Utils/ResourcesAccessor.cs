using System.IO;
using UnityEngine;

public static class ResourcesAccessor
{
    public static void SaveTo(string fileName, string TextContent)
    {
        string resourcesPath = GetResourcesPath();
        File.WriteAllText(resourcesPath + fileName + ".txt", TextContent);

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    public static string LoadFrom(string filePath)
    {
        TextAsset data = Resources.Load<TextAsset>(filePath); 
        return data.text;
    }

    public static bool DoesResourcesFileExist(string resourcesFolderPath, string fileName)
    {
        string path = GetResourcesPath() + resourcesFolderPath;
        return Directory.Exists(path);
    }

    public static string GetResourcesPath()
    {
        return "Assets/Resources/";
    }
}