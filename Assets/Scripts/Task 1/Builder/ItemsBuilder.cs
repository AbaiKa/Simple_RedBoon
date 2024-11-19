using UnityEngine;

public class ItemsBuilder : MonoBehaviour, IService
{
    [SerializeField] private ItemUI itemPrefab;
    [SerializeField] private ItemSO[] itemProperties;

    public ItemUI Build(string id)
    {
        ItemUI item = null;

        var props = GetProperties(id);
        if (props != null)
        {
            item = Instantiate(itemPrefab);
            item.Set(props.ID, props.Price, props.Icon);
        }

        return item;
    }

    public ItemUI[] BuildAll()
    {
        ItemUI[] items = new ItemUI[itemProperties.Length];
        for (int i = 0; i < itemProperties.Length; i++)
        {
            items[i] = Build(itemProperties[i].ID);
        }

        return items;
    }

    private ItemSO GetProperties(string id)
    {
        for (int i = 0; i < itemProperties.Length; i++)
        {
            if (itemProperties[i].ID == id)
            {
                return itemProperties[i];
            }
        }

        return null;
    }
}
