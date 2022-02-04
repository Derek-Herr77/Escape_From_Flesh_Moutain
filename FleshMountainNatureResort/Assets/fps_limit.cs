using UnityEngine;

public class fps_limit : MonoBehaviour
{
    public int targetFrameRate = 15;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }
}