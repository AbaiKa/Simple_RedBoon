using Newtonsoft.Json;
using UnityEngine;

public class Player
{
    public PlayerData Data {  get; private set; }
    private readonly string saveKey = "player_data";

    public void Init()
    {
        Data = Load();
    }

    public void Save()
    {
        string data = JsonConvert.SerializeObject(Data);
        Debug.Log($"Saved data: {data}");
        PlayerPrefs.SetString(saveKey, data);
        PlayerPrefs.Save();
    }

    public PlayerData Load()
    {
        Data = new PlayerData();

        string loadedData = PlayerPrefs.GetString(saveKey, null);

        if (!string.IsNullOrEmpty(loadedData))
        {
            Data = JsonConvert.DeserializeObject<PlayerData>(loadedData);
        }

        return Data;
    }
}
