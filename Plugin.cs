using System;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BATaxiMod;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    public override void Load()
    {
        Log = base.Log;
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        ClassInjector.RegisterTypeInIl2Cpp<TaxiButtonChecker>();
        SceneManager.add_sceneLoaded(new Action<Scene, LoadSceneMode>(OnSceneLoaded));
    }

    internal static void OnSceneLoaded(Scene scene, LoadSceneMode _)
    {
        if (scene.name != "LowerManhattan") return;
        
        GameObject.Find("GameManager").AddComponent<TaxiButtonChecker>();
    }
}
