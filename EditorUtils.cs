﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class EditorUtils
{
    // click command-0 to go to the prelaunch scene and then play
    // will search for the first scene prefixed with _
    // prefixing with _ will also make the prelaunch scene sort to the top

    [MenuItem("Cauldron/Play From Prelaunch Scene %0")]
    public static void PlayFromPrelaunchScene()
    {
        if (EditorApplication.isPlaying) return;
        string prelaunchScene = null;
        foreach (string guid in AssetDatabase.FindAssets("t:scene"))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var name = Path.GetFileNameWithoutExtension(path);
            if (string.IsNullOrEmpty(name)) continue;
            if (name.StartsWith("_"))
            {
                prelaunchScene = path;
                break;
            }
        }
        if (string.IsNullOrEmpty(prelaunchScene)) return;
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(prelaunchScene);
            EditorApplication.isPlaying = true;
        }
    }

    [MenuItem("Cauldron/List Scenes")]
    static void ListScenes()
    {
        var sb  = new StringBuilder();
        sb.AppendLine("Scenes in project:");
        foreach (string guid in AssetDatabase.FindAssets("t:scene"))
        {
            sb.AppendLine(AssetDatabase.GUIDToAssetPath(guid));
        }
        Debug.Log(sb.ToString());
    }

    [MenuItem("Cauldron/List Assets")]
    static void ListAssets()
    {
        string[] guids = AssetDatabase.GetAllAssetPaths();
        Debug.Log(string.Join(Environment.NewLine, guids));
    }

}

