using UnityEngine;

/// <summary>
/// Animation for an object to move
/// </summary>
public class Move : MonoBehaviour
{
    private float m_Duration = 1;

    private Vector3 m_Start;
    private Vector3 m_End;

    private float m_Timer;

    /// <summary>
    /// Moves object to destination, in duration seconds
    /// </summary>
    public void PlayMove(Vector3 destination, float duration)
    {
        // Start is the current position
        m_Start = transform.position;
        // Destination is end
        m_End = destination;

        m_Timer = 0;
        m_Duration = duration;

        // Enable the component (to use Update method)
        enabled = true;
    }

    private void Update()
    {
        // Timer increases
        m_Timer += Time.deltaTime;
        // On each frame, we modify the position based on the progress
        transform.position = GetPosition(Mathf.Clamp01(m_Timer / m_Duration));

        // If the progress reached 1, we disable the component
        if (m_Timer >= m_Duration)
            enabled = false;
    }

    /// <summary>
    /// Gets the interpolated position from start to end, based on a progress
    /// </summary>
    private Vector3 GetPosition(float progress)
    {
        return m_Start * (1 - progress) + m_End * progress;
    }
}
