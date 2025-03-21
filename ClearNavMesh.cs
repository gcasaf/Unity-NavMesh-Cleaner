using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Editor window tool to clean up baked NavMesh data from a Unity project
/// </summary>
public class NavMeshCleaner : EditorWindow
{
    private bool cleanScenes = true;
    private bool cleanPrefabs = true;
    private bool cleanNavMeshDataAssets = true;
    private Vector2 scrollPosition;
    private List<string> operationLog = new List<string>();
    private bool isProcessing = false;

    [MenuItem("Tools/NavMesh Cleaner")]
    public static void ShowWindow()
    {
        GetWindow<NavMeshCleaner>("NavMesh Cleaner");
    }

    private void OnGUI()
    {
        GUILayout.Label("NavMesh Data Cleaner", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("This tool removes baked NavMesh data from your project. Make a backup before proceeding!", MessageType.Warning);
        EditorGUILayout.Space();

        cleanScenes = EditorGUILayout.Toggle("Clean Scenes", cleanScenes);
        cleanPrefabs = EditorGUILayout.Toggle("Clean Prefabs", cleanPrefabs);
        cleanNavMeshDataAssets = EditorGUILayout.Toggle("Clean NavMesh Data Assets", cleanNavMeshDataAssets);

        EditorGUILayout.Space();
        GUI.enabled = !isProcessing && (cleanScenes || cleanPrefabs || cleanNavMeshDataAssets);

        if (GUILayout.Button("Clean NavMesh Data"))
        {
            if (EditorUtility.DisplayDialog("Confirm NavMesh Data Removal",
                "This will remove all baked NavMesh data from your project. This operation cannot be undone. Continue?",
                "Yes, proceed", "Cancel"))
            {
                isProcessing = true;
                operationLog.Clear();
                LogOperation("Starting NavMesh cleanup process...");

                EditorApplication.delayCall += () => {
                    CleanNavMeshData();
                    isProcessing = false;
                };
            }
        }

        GUI.enabled = true;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Operation Log", EditorStyles.boldLabel);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
        foreach (string logEntry in operationLog)
        {
            EditorGUILayout.HelpBox(logEntry, MessageType.Info);
        }
        EditorGUILayout.EndScrollView();
    }

    private void CleanNavMeshData()
    {
        int totalCleaned = 0;

        // Clean scenes
        if (cleanScenes)
        {
            int scenesCleaned = CleanScenesNavMeshData();
            totalCleaned += scenesCleaned;
            LogOperation($"Cleaned NavMesh data from {scenesCleaned} scenes.");
        }

        // Clean prefabs
        if (cleanPrefabs)
        {
            int prefabsCleaned = CleanPrefabsNavMeshData();
            totalCleaned += prefabsCleaned;
            LogOperation($"Cleaned NavMesh data from {prefabsCleaned} prefabs.");
        }

        // Clean NavMesh data assets
        if (cleanNavMeshDataAssets)
        {
            int assetsCleaned = CleanNavMeshDataAssets();
            totalCleaned += assetsCleaned;
            LogOperation($"Removed {assetsCleaned} NavMesh data assets.");
        }

        LogOperation($"NavMesh cleanup complete. Processed {totalCleaned} items in total.");
        AssetDatabase.Refresh();
    }

    private int CleanScenesNavMeshData()
    {
        int count = 0;
        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene");

        foreach (string guid in sceneGuids)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(guid);
            LogOperation($"Processing scene: {scenePath}");

            // Load the scene asset
            Object[] objects = AssetDatabase.LoadAllAssetsAtPath(scenePath);
            bool modified = false;

            foreach (Object obj in objects)
            {
                // Look for NavMesh components or objects
                if (obj != null && obj.name.Contains("NavMesh"))
                {
                    // Attempt to destroy NavMesh data
                    DestroyImmediate(obj, true);
                    modified = true;
                    count++;
                }
            }

            if (modified)
            {
                EditorUtility.SetDirty(AssetDatabase.LoadAssetAtPath<Object>(scenePath));
                AssetDatabase.SaveAssets();
            }
        }

        return count;
    }

    private int CleanPrefabsNavMeshData()
    {
        int count = 0;
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");

        foreach (string guid in prefabGuids)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(guid);
            LogOperation($"Processing prefab: {prefabPath}");

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            bool modified = false;

            // Check for NavMesh components
            Component[] components = prefab.GetComponentsInChildren<Component>(true);
            foreach (Component component in components)
            {
                if (component != null && component.GetType().Name.Contains("NavMesh"))
                {
                    DestroyImmediate(component, true);
                    modified = true;
                    count++;
                }
            }

            if (modified)
            {
                EditorUtility.SetDirty(prefab);
                AssetDatabase.SaveAssets();
            }
        }

        return count;
    }

    private int CleanNavMeshDataAssets()
    {
        int count = 0;
        string[] navMeshDataGuids = AssetDatabase.FindAssets("t:NavMeshData");

        foreach (string guid in navMeshDataGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            LogOperation($"Removing NavMesh data asset: {assetPath}");

            AssetDatabase.DeleteAsset(assetPath);
            count++;
        }

        return count;
    }

    private void LogOperation(string message)
    {
        operationLog.Add($"[{System.DateTime.Now.ToString("HH:mm:ss")}] {message}");
        Repaint();
        Debug.Log($"[NavMesh Cleaner] {message}");
    }
}