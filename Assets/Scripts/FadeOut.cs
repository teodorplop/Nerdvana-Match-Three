using UnityEngine;

/// <summary>
/// Animation for an object to fade out
/// </summary>
public class FadeOut : MonoBehaviour
{
    private float m_Duration = 1;

    private Vector3 m_Start;
    private Vector3 m_End;

    private float m_Timer;

    /// <summary>
    /// Fades the object out, in duration seconds
    /// </summary>
    public void PlayFadeOut(float duration)
    {
        // Start is the current scale
        m_Start = transform.localScale;
        // End is 0
        m_End = Vector3.zero;

        m_Timer = 0;
        m_Duration = duration;

        // Enable the component (to use Update method)
        enabled = true;
    }

    private void Update()
    {
        // Timer increases
        m_Timer += Time.deltaTime;
        // On each frame, we modify the scale based on the progress
        transform.localScale = GetScale(Mathf.Clamp01(m_Timer / m_Duration));

        // If the progress reached 1, we disable the component
        if (m_Timer >= m_Duration)
            enabled = false;
    }

    /// <summary>
    /// Gets the interpolated scale from start to end, based on a progress
    /// </summary>
    private Vector3 GetScale(float progress)
    {
        return m_Start * (1 - progress) + m_End * progress;
    }
}
