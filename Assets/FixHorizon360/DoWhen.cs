using UnityEngine;
using UnityEngine.Events;

public class DoWhen : MonoBehaviour
{
    public UnityEvent doOnAwake;
    public UnityEvent doOnStart;
    public UnityEvent doOnEnable;
    public UnityEvent doOnDisable;

    private void Awake()
    {
        doOnAwake?.Invoke();
    }

    private void Start()
    {
        doOnStart?.Invoke();
    }

    private void OnEnable()
    {
        doOnEnable?.Invoke();
    }

    private void OnDisable()
    {
        doOnDisable?.Invoke();
    }
}