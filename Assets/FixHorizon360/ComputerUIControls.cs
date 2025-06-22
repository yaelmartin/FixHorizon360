using TMPro;
using UnityEngine;

public class ComputerUIControls : MonoBehaviour
{
    [SerializeField] private PanoHandler panoHandler;
    [SerializeField] private TextMeshProUGUI degreesPerIncrementText;
    
    private float _multiplicator = 1;
    private float _baseValue = 0.1f;

    public float MultiplicatorRotationIncrement => _multiplicator;

    public void SetMultiplicator(float value)
    {
        _multiplicator = value;
        if (degreesPerIncrementText != null)
        {
            degreesPerIncrementText.text = $"Degrees per increment {_baseValue*_multiplicator:F2}";
        }
    }
    public void IncrementRotation(bool positive)
    {
        float degree = _baseValue * _multiplicator;
        if (positive)
        {
            degree = -degree;
        }
        panoHandler.RotatingPivots.IncrementRotation(degree);
    }
}
