using UnityEngine;

/// <summary>
/// Animation that changes the sprite of a SpriteRenderer
/// </summary>
public class SpriteAnimation : MonoBehaviour
{
    /// <summary>
    /// Speed of the animation
    /// </summary>
    [SerializeField] private int m_FramesPerSecond;
    /// <summary>
    /// Sprites to cycle through
    /// </summary>
    [SerializeField] private Sprite[] m_Sprites;

    private SpriteRenderer m_SpriteRenderer;

    private float m_Interval;
    private float m_Timer;
    private int m_CurrentSpriteIndex;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        // Interval between sprite changes
        m_Interval = 1.0f / m_FramesPerSecond;
        m_Timer = m_Interval;

        // We set the initial sprite
        m_SpriteRenderer.sprite = m_Sprites[m_CurrentSpriteIndex];
    }

    private void Update()
    {
        // Time passes
        m_Timer -= Time.deltaTime;
        // If there's no more time left
        if (m_Timer <= 0)
        {
            // We go to the next sprite
            ++m_CurrentSpriteIndex;
            if (m_CurrentSpriteIndex == m_Sprites.Length)
                m_CurrentSpriteIndex = 0;

            // Change it
            m_SpriteRenderer.sprite = m_Sprites[m_CurrentSpriteIndex];
            // Reset the time
            m_Timer = m_Interval;
        }
    }

}
