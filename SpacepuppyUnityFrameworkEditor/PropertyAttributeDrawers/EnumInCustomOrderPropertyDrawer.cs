﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

using com.spacepuppy;
using com.spacepuppy.Utils;

namespace com.spacepuppyeditor.PropertyAttributeDrawers
{

    [CustomPropertyDrawer(typeof(EnumInCustomOrderAttribute))]
    public class EnumInCustomOrderPropertyDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var enumType = this.fieldInfo.FieldType;
            if (!enumType.IsEnum)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            System.Enum evalue = property.GetEnumValue(enumType);
            var attrib = this.attribute as EnumInCustomOrderAttribute;
            if (attrib != null && attrib.customOrder != null)
            {
                var values = (from i in attrib.customOrder select EditorHelper.TempContent(System.Enum.GetName(enumType, i))).ToArray();
                int index = System.Array.IndexOf(attrib.customOrder, property.intValue);
                index = EditorGUI.Popup(position, label, index, values);
                property.intValue = index >= 0 && index < attrib.customOrder.Length ? attrib.customOrder[index] : -1;
            }
            else
            {
                property.SetEnumValue(EditorGUI.EnumPopup(position, label, evalue));
            }
        }

    }

}
