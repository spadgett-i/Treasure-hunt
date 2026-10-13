using UnityEngine;
using UnityEditor;
// Importamos la librería del XR Interaction Toolkit para poder acceder al TeleportationArea


public class AsignadorTeleport : EditorWindow
{
    [MenuItem("Herramientas/Configurar Teleportation Area")]
    public static void ConfigurarTeleportationEnSeleccionados()
    {
        // 1. Validar que tengamos objetos seleccionados en la jerarquía
        if (Selection.gameObjects.Length == 0)
        {
            Debug.LogWarning("Por favor, selecciona al menos un objeto en la Jerarquía.");
            return;
        }

        // Registrar la operación para poder deshacer con Ctrl+Z en Unity
        Undo.RecordObjects(Selection.gameObjects, "Configurar Teleportation Areas");

        int contadorModificados = 0;

        foreach (GameObject obj in Selection.gameObjects)
        {
            // 2. Obtener o añadir el MeshCollider del objeto
            MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
            if (meshCollider == null)
            {
                Debug.LogWarning($"El objeto '{obj.name}' no tiene un MeshCollider. Se ignorará para evitar errores.");
                continue; // Pasa al siguiente objeto si no tiene malla de colisión
            }

            // 3. Obtener o añadir el componente Teleportation Area de forma segura
            UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationArea teleportArea = obj.GetComponent<UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationArea>();
            if (teleportArea == null)
            {
                teleportArea = Undo.AddComponent<UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationArea>(obj);
            }

            // 4. Configurar el array de Colliders internamente mediante SerializedProperties
            // Usamos SerializedObject para asegurarnos de que Unity guarde los cambios correctamente en el Inspector
            SerializedObject serializedTeleport = new SerializedObject(teleportArea);
            SerializedProperty collidersProperty = serializedTeleport.FindProperty("m_Colliders");

            if (collidersProperty != null && collidersProperty.isArray)
            {
                // Cambiar el tamaño del array de 0 a 1 de forma dinámica
                collidersProperty.ClearArray();
                collidersProperty.InsertArrayElementAtIndex(0);

                // Obtener la casilla indexada [0] recién creada y asignarle el MeshCollider encontrado
                SerializedProperty primeraCasilla = collidersProperty.GetArrayElementAtIndex(0);
                primeraCasilla.objectReferenceValue = meshCollider;

                // Aplicar los cambios de manera persistente en la interfaz del inspector
                serializedTeleport.ApplyModifiedProperties();
            }

            // Marcar el objeto modificado como "sucio" para que Unity sepa que debe guardar los cambios en la escena
            EditorUtility.SetDirty(obj);
            contadorModificados++;
        }

        Debug.Log($"¡Automatización Completada! Se configuró el área de teletransporte en {contadorModificados} objetos.");
    }
}