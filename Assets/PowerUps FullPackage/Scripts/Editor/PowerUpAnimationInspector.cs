namespace VisCircle {

    using UnityEngine;
    using UnityEditor;
    using System.Collections;

    [CustomEditor(typeof(PowerUpAnimation))]
    public class PowerupAnimationInspector : Editor {
        public override void OnInspectorGUI() {
            PowerUpAnimation simpleAnimation = target as PowerUpAnimation;

            EditorGUI.BeginChangeCheck();
            bool newAnimateRotation = EditorGUILayout.Toggle("Animated Rotation", simpleAnimation.GetAnimateRotation());
            if (EditorGUI.EndChangeCheck()) {
                simpleAnimation.SetAnimateRotation(newAnimateRotation);
            }

            if (simpleAnimation.GetAnimateRotation()) {
                EditorGUI.indentLevel++;
                simpleAnimation.rotationSpeedsInDegreePerSecond = EditorGUILayout.Vector3Field("Rotation Speeds", simpleAnimation.rotationSpeedsInDegreePerSecond);
                simpleAnimation.rotationType = (PowerUpAnimation.RotationType)EditorGUILayout.EnumPopup("Rotation Axis", simpleAnimation.rotationType);
                EditorGUI.indentLevel--;
            }

            GUILayout.Space(10f);

            EditorGUI.BeginChangeCheck();
            bool newAnimateScale = EditorGUILayout.Toggle("Animated Scale", simpleAnimation.GetAnimateScale());
            if (EditorGUI.EndChangeCheck()) {
                simpleAnimation.SetAnimateScale(newAnimateScale);
            }

            if (simpleAnimation.GetAnimateScale()) {
                EditorGUI.indentLevel++;
                simpleAnimation.scaleMin = EditorGUILayout.FloatField("Min Scale", simpleAnimation.scaleMin);
                simpleAnimation.scaleMax = EditorGUILayout.FloatField("Max Scale", simpleAnimation.scaleMax);
                simpleAnimation.scaleCycleDuration = EditorGUILayout.FloatField("Scale Cycle Duration", simpleAnimation.scaleCycleDuration);
                EditorGUI.indentLevel--;
            }

            GUILayout.Space(10f);

            EditorGUI.BeginChangeCheck();
            bool newAnimateYOffset = EditorGUILayout.Toggle("Animated Y Offset", simpleAnimation.GetAnimateYOffset());
            if (EditorGUI.EndChangeCheck()) {
                simpleAnimation.SetAnimateYOffset(newAnimateYOffset);
            }

            if (simpleAnimation.GetAnimateYOffset()) {
                EditorGUI.indentLevel++;
                simpleAnimation.yOffsetAmplitude = EditorGUILayout.FloatField("Amplitude", simpleAnimation.yOffsetAmplitude);
                simpleAnimation.yOffsetCycleDuration = EditorGUILayout.FloatField("Y Offset Cycle Duration", simpleAnimation.yOffsetCycleDuration);
                EditorGUI.indentLevel--;
            }

            if (GUI.changed) {
                EditorUtility.SetDirty(target);
            }
        }
    }

}
