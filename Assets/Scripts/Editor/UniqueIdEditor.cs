using System;
using System.Linq;
using Game_logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor: UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (UniqueId) target;

            if (string.IsNullOrEmpty(uniqueId.id))
                Generate(uniqueId);
            else
            {
                UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();

                if (uniqueIds.Any(other => other != uniqueId && other.id == uniqueId.id))
                    Generate(uniqueId);
            }
        }

        private void Generate(UniqueId uniqueId)
        {
            uniqueId.id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

            if (Application.isPlaying is false)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }
    }
}