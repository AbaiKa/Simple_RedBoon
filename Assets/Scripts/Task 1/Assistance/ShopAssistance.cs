using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class ShopAssistance : MonoBehaviour, IService
{
    [SerializeField] private DropComponent inventoryDropComponent;
    [SerializeField] private DropComponent shopDropComponent;

    private ItemsBuilder builder;
    private PlayerAssistance playerAssistance;
    public void Init()
    {
        shopDropComponent.onDrop.AddListener(OnShopZoneDrop);
        inventoryDropComponent.onDrop.AddListener(OnInventoryZoneDrop);

        builder = ServicesAssistance.Main.Get<ItemsBuilder>();
        playerAssistance = ServicesAssistance.Main.Get<PlayerAssistance>();

        var items = builder.BuildAll();

        for (int i = 0; i < items.Length; i++)
        {
            Transform container = shopDropComponent.Container;
            bool isPlayerItem = playerAssistance.Data.Items.ContainsKey(items[i].ID);

            if (isPlayerItem)
            {
                container = inventoryDropComponent.Container;
            }

            items[i].transform.SetParent(container);
            items[i].transform.localScale = Vector3.one;
        }
    }

    public void AddGold()
    {
        playerAssistance.AddResources("gold", 100);
    }

    private void OnInventoryZoneDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemUI>();

        if (playerAssistance.SubstractResources("gold", item.Price))
        {
            playerAssistance.AddItem(item.ID, item.Price);
            eventData.pointerDrag.transform.SetParent(inventoryDropComponent.Container);
        }
    }

    private void OnShopZoneDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemUI>();

        if (playerAssistance.SubstractItem(item.ID, 1))
        {
            playerAssistance.AddResources("gold", item.Price / 2);
            eventData.pointerDrag.transform.SetParent(shopDropComponent.Container);
        }
    }
}
