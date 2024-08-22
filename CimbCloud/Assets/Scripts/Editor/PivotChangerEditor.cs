using UnityEngine;
using UnityEditor;

public class PivotChangerEditor : EditorWindow
{
    private Texture2D texture;
    private Vector2 newPivot = new Vector2(0.5f, 0.5f);

    [MenuItem("Tools/Pivot Changer")]
    public static void ShowWindow()
    {
        GetWindow<PivotChangerEditor>("Pivot Changer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Change Pivot of Sprites", EditorStyles.boldLabel);

        texture = (Texture2D)EditorGUILayout.ObjectField("Texture", texture, typeof(Texture2D), false);
        newPivot = EditorGUILayout.Vector2Field("New Pivot", newPivot);

        if (GUILayout.Button("Change Pivot"))
        {
            ChangePivot();
        }
    }

    private void ChangePivot()
    {
        if (texture == null)
        {
            Debug.LogError("Please select a Texture2D object.");
            return;
        }

        string path = AssetDatabase.GetAssetPath(texture);
        TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

        if (textureImporter == null)
        {
            Debug.LogError("Could not get the TextureImporter.");
            return;
        }

        // Ensure the texture is readable
        bool wasReadable = textureImporter.isReadable;
        if (!textureImporter.isReadable)
        {
            textureImporter.isReadable = true;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        // Modify the sprite sheet pivot points
        SpriteMetaData[] sprites = textureImporter.spritesheet;

        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogError("No sprites found in the texture.");
            return;
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            Debug.Log($"Changing pivot for sprite {i} to {newPivot}");
            sprites[i].pivot = newPivot;
        }

        // Apply the modified sprite sheet back to the texture importer
        textureImporter.spritesheet = sprites;

        // Mark the TextureImporter as dirty to ensure Unity recognizes the change
        EditorUtility.SetDirty(textureImporter);

        // Save the changes and reimport the texture
        textureImporter.SaveAndReimport();

        // Optionally, revert the readable state
        if (!wasReadable)
        {
            textureImporter.isReadable = false;
            EditorUtility.SetDirty(textureImporter);
            textureImporter.SaveAndReimport();
        }

        // Ensure the AssetDatabase is up to date with the changes
        AssetDatabase.WriteImportSettingsIfDirty(path);
        AssetDatabase.Refresh();

        Debug.Log($"Pivot of all sprites in '{texture.name}' changed to {newPivot} and saved.");
    }
}