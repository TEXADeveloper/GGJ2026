using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace TEXADev.Tools
{
    [InitializeOnLoad]
    public static class AutoRenamePatrolPoint
    {
        static AutoRenamePatrolPoint()
        {
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
        }

        private static void OnHierarchyChanged()
        {
            GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

            foreach (GameObject obj in allObjects)
            {
                Vector3 pos = obj.transform.position;
                string newName = "";
                bool rename = false;
                
                if (obj.name.ToLower().Equals("patrol") && obj.transform.parent.name.Equals("PatrolPoints"))
                {
                    newName = "PatrolPoint";
                    rename = true;
                } else if (obj.name.ToLower().Equals("object") && obj.transform.parent.name.Equals("ObjectPositions"))
                {
                    newName = "ObjectPoint";
                    rename = true;
                } else if (obj.name.ToLower().Equals("mask") && obj.transform.parent.name.Equals("MaskPositions"))
                {
                    newName = "MaskPoint";
                    rename = true;
                }

                if (rename)
                {
                    newName += $"({pos.x:F1}, {pos.z:F1})";

                    // Prevent infinite rename loops
                    if (obj.name != newName)
                    {
                        Undo.RecordObject(obj, "Auto Rename Point");
                        obj.name = newName;
                        EditorUtility.SetDirty(obj);
                    }
                }
            }
        }
    }
}
