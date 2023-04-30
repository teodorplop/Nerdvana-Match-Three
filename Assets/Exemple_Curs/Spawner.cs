using System;
using UnityEngine;

[Serializable]
public class Settings
{
    [SerializeField]
    public float volume;

    [SerializeField]
    public int level;

    [SerializeField]
    public int coins;
}

public class Spawner : MonoBehaviour
{
    public int numberOfObjects;
    public GameObject obj;

    Settings settings = new Settings();

    private void Start()
    {
        for (int i = 0; i < numberOfObjects; ++i)
        {
            GameObject clone = Instantiate(obj);
            clone.GetComponent<Transform>().position = new Vector3(i, 10);
        }

        if (PlayerPrefs.HasKey("settings"))
        {
            settings = JsonUtility.FromJson<Settings>(PlayerPrefs.GetString("settings"));
        }

        Debug.Log($"volume = {settings.volume} level = {settings.level}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            settings.level++;

            PlayerPrefs.SetString("settings", JsonUtility.ToJson(settings));
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            settings.volume += 0.1f;

            PlayerPrefs.SetString("settings", JsonUtility.ToJson(settings));
        }
    }
}
