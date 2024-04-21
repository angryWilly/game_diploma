﻿using CodeBase.Enemy.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker marker, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(marker.transform.position, 0.5f);
        }
    }
}