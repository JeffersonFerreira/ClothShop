using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Automatically populate game database using every equipment ScriptableObject available in the _GameSource directory
    /// </summary>
    public class GameDatabasePopulator : IPreprocessBuildWithReport
    {
        int IOrderedCallback.callbackOrder { get; }

        void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport report) => Internal_Execute();

        [InitializeOnEnterPlayMode]
        private static void Internal_Execute()
        {
            GameDatabase gameDatabase = LoadAsset<GameDatabase>().First();
            EquipmentSO[] equipment = LoadAsset<EquipmentSO>().ToArray();

            gameDatabase.SetItems(equipment);
        }

        private static IEnumerable<T> LoadAsset<T>() where T : Object
        {
            string typeName = typeof(T).Name;
            foreach (string assetGuid in AssetDatabase.FindAssets($"t: {typeName}", new[] { "Assets/_GameSource" }))
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                yield return AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }
        }
    }
}
#endif