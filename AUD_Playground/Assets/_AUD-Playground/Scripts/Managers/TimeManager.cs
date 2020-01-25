using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public KeyCode key;
    public float slowdownFactor = 0.05f;            // How much to slow down
    public float slowdownLength = 2f;               // how long the slow-down effect will last

    float actualSlowDownLength = 0;
    float fixedDeltaTime;

    private void Awake()
    {
        actualSlowDownLength = slowdownLength;
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        Time.timeScale += (1f / actualSlowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);            // clamp it to never go below 0 or larger than 14

        if (Time.timeScale < 1)
        {
            Time.fixedDeltaTime = Time.timeScale * .02f;
        } else
        {
            Time.fixedDeltaTime = fixedDeltaTime;
        }

        if (Input.GetKeyDown(key))
        {
            DoSlowmotion();
        }
    }

    public void DoSlowmotion()
    {
        actualSlowDownLength = slowdownLength;
        Time.timeScale = slowdownFactor;
    }

}
