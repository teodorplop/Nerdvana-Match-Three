using UnityEngine;

public class Fade : MonoBehaviour
{
    public float duration;

    int direction;
    float remaining;
    SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        remaining = duration;
        direction = -1;
    }

    private void Update()
    {
        remaining += Time.deltaTime * direction;
        if (remaining < 0) direction *= -1;
        else if (remaining > duration) direction *= -1;

        Color color = sprite.color;
        color.a = Mathf.Clamp01(remaining / duration);
        sprite.color = color;
    }
}
