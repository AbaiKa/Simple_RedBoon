using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAssistance : MonoBehaviour, IService
{
    public PlayerData Data => player.Data;
    private Player player;

    /// <summary>
    /// Key & value
    /// </summary>
    public UnityEvent<string, int> onResourcesChanged = new UnityEvent<string, int>();

    public void Init(Player player)
    {
        this.player = player;

        AddResources("gold", 0);

        foreach (var resource in Data.Resources)
        {
            onResourcesChanged?.Invoke(resource.Key, resource.Value);
        }
    }

    public void AddResources(string key, int value)
    {
        if (!player.Data.Resources.ContainsKey(key))
        {
            player.Data.Resources.Add(key, 0);
        }

        player.Data.Resources[key] += value;
        player.Save();

        onResourcesChanged?.Invoke(key, player.Data.Resources[key]);
    }

    public bool SubstractResources(string key, int value)
    {
        if (player.Data.Resources.ContainsKey(key))
        {
            int currentValue = player.Data.Resources[key];

            if (currentValue >= value)
            {
                player.Data.Resources[key] = Mathf.Max(0, currentValue - value);
                player.Save();

                onResourcesChanged?.Invoke(key, player.Data.Resources[key]);
                return true;
            }
        }

        return false;
    }

    public void AddItem(string key, int price)
    {
        if (!player.Data.Items.ContainsKey(key))
        {
            player.Data.Items.Add(key, new Item(key, price));
            player.Save();
        }
        else
        {
            player.Data.Items[key].count += 1;
        }
    }
    public bool SubstractItem(string key, int value)
    {
        if (player.Data.Items.ContainsKey(key))
        {
            int currentValue = player.Data.Items[key].count;
            int result = Mathf.Max(0, currentValue - value);

            if (result == 0)
            {
                player.Data.Items.Remove(key);
            }
            else
            {
                player.Data.Items[key].count = result;
            }

            player.Save();

            return true;
        }

        return false;
    }
}
