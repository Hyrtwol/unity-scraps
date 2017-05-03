using UnityEngine;

// http://www.madpixelmachine.com/2013/07/05/monobehaviour-lifecycle-and-leaks-explained/

// When new component (this script) is added: Awake(), OnEnable(), Reset(), Start()
// When scene is saved (CTRL+S): nothing happens but here you may see those irritating info messages
// When scene is unloaded: OnDisable(), OnDestroy()
// When scene is loaded: Awake(), OnEnable(), Start()
// When script is modified: OnDisable(), OnEnable()

[ExecuteInEditMode]
public class LifecycleTest : MonoBehaviour
{
    public bool LogToConsole = true;

    public void Awake()
    {
        Log("Awake");
    }

    public void Reset()
    {
        Log("Reset");
    }

    public void OnEnable()
    {
        Log("OnEnable");
    }

    public void OnDisable()
    {
        Log("OnDisable");
    }

    public void OnDestroy()
    {
        Log("OnDestroy");
    }

    public void Start()
    {
        Log("Start");
    }

    private void Log(string action)
    {
        if (LogToConsole) Debug.LogFormat("{0} :: {1}", GetType().Name, action);
    }
}