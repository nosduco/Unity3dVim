using System;
using System.IO;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Plugins.Editor.Vim
{
  [InitializeOnLoad]
  public static class VimPlugin
  {
    private static string projectDirectory;
    private static string DefaultApp
    {
      get { return EditorPrefs.GetString("kScriptsDefaultApp"); }
    }

    internal static bool Enabled
    {
      get
      {
        return DefaultApp.Contains("VimRunner");
      }
    }

    static VimPlugin()
    {
      if (Enabled)
      {
        InitVimPlugin();
      }
    }

    private static void InitVimPlugin()
    {
      projectDirectory = Directory.GetParent(Application.dataPath).FullName;
      UpdateUnitySettings();
    }

    private static void UpdateUnitySettings()
    {
      try
      {
        EditorPrefs.SetString("kScriptEditorArgs", "$(File) $(Line)");
      }
      catch (Exception e)
      {
          Debug.Log("[VIM] " + e.ToString());
      }
    }

    private static bool CallVim(string args)
    {
      ProcessStartInfo psi = new ProcessStartInfo();
      psi.FileName = "/bin/bash";
      psi.UseShellExecute = false;
      psi.RedirectStandardOutput = true;
      psi.Arguments = DefaultApp + " " + args;
      Process.Start(psi);
      return true;
    }

    [UnityEditor.Callbacks.OnOpenAssetAttribute()]
    static bool OnOpenedAsset(int instanceID, int line)
    {
      if (Enabled)
      {
        string appPath = Path.GetDirectoryName(Application.dataPath);
        var selected = EditorUtility.InstanceIDToObject(instanceID);

        if (selected.GetType().ToString() == "UnityEditor.MonoScript")
        {
          SyncSolution(); 
          var assetFilePath = Path.Combine(appPath, AssetDatabase.GetAssetPath(selected));
          var args = string.Format("{1} {0}", line, assetFilePath);
          return CallVim(args);
        }
      }
      return false;
    }

    [MenuItem("Assets/Open C# Project Directory in Vim", false, 1000)]
    static void MenuOpenProject()
    {
      SyncSolution();
      CallVim(projectDirectory);
    }

    [MenuItem("Assets/Open C# Project Directory in Vim", true, 1000)]
    static bool ValidateMenuOpenProject()
    {
      return Enabled;
    }

    private static void SyncSolution()
    {
      System.Type T = System.Type.GetType("UnityEditor.SyncVS,UnityEditor");
      System.Reflection.MethodInfo SyncSolution = T.GetMethod("SyncSolution",
        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
      SyncSolution.Invoke(null, null);
    }
  }
}
