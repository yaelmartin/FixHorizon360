using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class ImageLoad : MonoBehaviour
{
    [SerializeField] private PanoHandler panoHandler;
    [SerializeField] private Material targetMaterial;
    [SerializeField] private string imageFolderName = "360";

    private string imagePath;
    private string imageFilename;

    private void Start()
    {
        LoadFirstImage();
    }

    public void LoadFirstImage()
    {
        string folderPath = Path.Combine(Application.streamingAssetsPath, imageFolderName);

        if (!Directory.Exists(folderPath))
        {
            Debug.LogWarning($"ImageLoad: Folder does not exist at path: {folderPath}");
            return;
        }

        string[] imageFiles = Directory.GetFiles(folderPath)
            .Where(f => !Path.GetFileName(f).StartsWith(".") &&
                        (f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                         f.EndsWith(".png", StringComparison.OrdinalIgnoreCase)))
            .OrderBy(f => f)
            .ToArray();

        if (imageFiles.Length == 0)
        {
            Debug.LogWarning("ImageLoad: No image files found.");
            return;
        }

        imagePath = imageFiles[0];
        imageFilename = Path.GetFileName(imagePath);
        Debug.Log($"ImageLoad: Loading image {imageFilename}");

        LoadTexture(imagePath);
    }

    private void LoadTexture(string path)
    {
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        texture.name = imageFilename;

        if (targetMaterial != null)
        {
            targetMaterial.mainTexture = texture;
        }
    }

    public void SaveRotationToDisk()
    {
        if (string.IsNullOrEmpty(imageFilename))
        {
            Debug.LogWarning("ImageLoad: No image loaded, cannot save rotation.");
            return;
        }

        // Get the rotations
        Vector3 unityEuler = ConvertRotation.GetUnityDeltaEuler(panoHandler.ReferenceZero, panoHandler.MainPanoPivot);
        ConvertRotation.UptaleRotation uptaleEuler = ConvertRotation.GetUptaleDeltaEuler(panoHandler.ReferenceZero, panoHandler.MainPanoPivot);

        // Create the data object
        RotationData rotationData = new RotationData
        {
            unityEuler = unityEuler,
            uptaleEuler = uptaleEuler
        };

        // Serialize to JSON
        string json = JsonUtility.ToJson(rotationData, true);

        // Save next to the image
        string folderPath = Path.Combine(Application.streamingAssetsPath, imageFolderName);
        string jsonFilename = Path.ChangeExtension(imageFilename, ".json");
        string jsonPath = Path.Combine(folderPath, jsonFilename);

        File.WriteAllText(jsonPath, json);
        Debug.Log($"ImageLoad: Saved rotation data to {jsonFilename}");
    }

    // Optional accessors
    public string GetImageFilename() => imageFilename;
    public string GetImagePath() => imagePath;
}

// Serializable class for saving
[Serializable]
public class RotationData
{
    public Vector3 unityEuler;
    public ConvertRotation.UptaleRotation uptaleEuler;
}
