using UnityEditor;
using UnityEngine;

namespace PigeonProject.Editor
{
    public static class HierarchyColorGUIUtility 
    {
        // Default Colors
        static readonly Color k_defaultColor = new Color(0.7843f, 0.7843f, 0.7843f);
        static readonly Color k_defaultDarkColor = new Color(0.2196f, 0.2196f, 0.2196f);

        // Selected Colors
        static readonly Color k_selectedColor = new Color(0.22745f, 0.447f, 0.6902f);
        static readonly Color k_selectedDarkColor = new Color(0.1725f, 0.3647f, 0.5294f);

        // Selected UnFocused Colors
        static readonly Color k_selectedUnFocusedColor = new Color(0.68f, 0.68f, 0.68f);
        static readonly Color k_selectedUnFocusedDarkColor = new Color(0.3f, 0.3f, 0.3f);

        // Hovered Colors
        static readonly Color k_hoveredColor = new Color(0.698f, 0.698f, 0.698f);
        static readonly Color k_hoveredDarkColor = new Color(0.2706f, 0.2706f, 0.2706f);

        public static Color GetColor(bool isSelected, bool isHovered, bool isWindowFocused){
            if(isSelected){
                if(isWindowFocused)
                {
                    return EditorGUIUtility.isProSkin ? k_selectedDarkColor : k_selectedColor;
                }else{
                    return EditorGUIUtility.isProSkin ? k_selectedUnFocusedDarkColor : k_selectedUnFocusedColor;
                }

            }
            else if(isHovered){
                return EditorGUIUtility.isProSkin ? k_hoveredDarkColor : k_hoveredColor;
            }else{
                return EditorGUIUtility.isProSkin ? k_defaultDarkColor : k_defaultColor;
            }
        }
    }
}
