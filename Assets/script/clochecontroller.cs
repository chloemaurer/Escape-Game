using System.Collections.Generic;
using UnityEngine;

public class BellSoundController : MonoBehaviour
{
    public static BellSoundController Instance { get; private set; }

    // List of logs to verify what was played
    private List<string> soundLogs = new List<string>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Called by each bell when it plays a sound
    public void RegisterBellSound(string bellName, string soundName)
    {
        string log = $"[{Time.time:F2}] Bell '{bellName}' played sound '{soundName}'";
        soundLogs.Add(log);
        Debug.Log(log);
    }

    // Make logs accessible
    public List<string> GetSoundLogs()
    {
        return new List<string>(soundLogs);
    }

    // Optional: verify if a specific bell played the correct sound
    public bool DidBellPlaySound(string bellName, string soundName)
    {
        foreach (var log in soundLogs)
        {
            if (log.Contains($"Bell '{bellName}'") && log.Contains($"'{soundName}'"))
                return true;
        }
        return false;
    }
}
