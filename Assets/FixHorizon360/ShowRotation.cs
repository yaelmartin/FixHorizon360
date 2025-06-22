using UnityEngine;
using TMPro;

public class ShowRotation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI textComputer;
    [SerializeField] private PanoHandler panoHandler;

    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        // === ROTATION DATA USING ConvertRotation UTILITY ===

        Vector3 unityEuler = ConvertRotation.GetUnityDeltaEuler(panoHandler.ReferenceZero, panoHandler.MainPanoPivot);
        ConvertRotation.UptaleRotation uptaleEuler = ConvertRotation.GetUptaleDeltaEuler(panoHandler.ReferenceZero, panoHandler.MainPanoPivot);

        // === TEXT OUTPUT ===
        string output = $"<b>Unity Euler (normalized):</b><br>" +
                        $"X: {unityEuler.x:F2}°<br>" +
                        $"Y: {unityEuler.y:F2}°<br>" +
                        $"Z: {unityEuler.z:F2}°<br><br>" +

                        $"<b>Uptale Format:</b><br>" +
                        $"Vertical Axis (Y): {uptaleEuler.verticalAxis:F2}°<br>" +
                        $"Horizontal Axis (X): {uptaleEuler.horizontalAxis:F2}°<br>" +
                        $"Frontal Axis (Z): {uptaleEuler.frontalAxis:F2}°<br><br>";

        text.text = output;

        string outputComputer = $"<b>Uptale Format:</b><br>" +
                                $"Vertical Axis (Y): {uptaleEuler.verticalAxis:F2}°<br>" +
                                $"Horizontal Axis (X): {uptaleEuler.horizontalAxis:F2}°<br>" +
                                $"Frontal Axis (Z): {uptaleEuler.frontalAxis:F2}°<br><br>";

        textComputer.text = outputComputer;
    }
}
