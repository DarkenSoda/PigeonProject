using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

namespace PigeonProject.Editor
{
    [InitializeOnLoad]
    public static class HierarchyIconDisplay 
    {
        static bool _hierarchyHasFocus = false;
        static EditorWindow _hierarchyEditorWindow;

        static HierarchyIconDisplay(){
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGui;
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate()
        {
            if(_hierarchyEditorWindow == null)
                _hierarchyEditorWindow = EditorWindow.GetWindow(System.Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor"));

            _hierarchyHasFocus = EditorWindow.focusedWindow != null &&
                EditorWindow.focusedWindow == _hierarchyEditorWindow;
        }

        private static void OnHierarchyWindowItemOnGui(int instanceID, Rect selectionRect)
        {
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if(obj == null)
                return;

            if(PrefabUtility.GetCorrespondingObjectFromSource(obj) != null)
                return;

            Component[] components = obj.GetComponents<Component>();

            if(components == null || components.Length == 0)
                return;

            Component component = components.Length > 1 ? components[1] : null;

            if(component == null)
                return;

            if(component.GetComponent<CanvasRenderer>() != null){
                component = components.Length > 2 ? components[2] : components[0];
            }

            Type type = component.GetType();

            GUIContent content = EditorGUIUtility.ObjectContent(component , type);
            content.text = null;
            content.tooltip = type.Name;

            if(content.image == null)
                return;
            
            bool isSelected = Selection.instanceIDs.Contains(instanceID);
            bool isHovering = selectionRect.Contains(Event.current.mousePosition);

            Color color = HierarchyColorGUIUtility.GetColor(isSelected, isHovering , _hierarchyHasFocus);
            Rect backgroundRect = selectionRect;
            backgroundRect.width = 18.5f;
            EditorGUI.DrawRect(backgroundRect, color);

            EditorGUI.LabelField(selectionRect, content);

        }
    }
}
