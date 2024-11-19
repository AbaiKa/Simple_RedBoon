using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ItemsBuilder itemsBuilder;
    [SerializeField] private PlayerAssistance playerAssistance;
    [SerializeField] private ShopAssistance shopAssistance;

    private Player player;
    private ServicesAssistance services;

    private void Awake()
    {
        player = new Player();
        services = new ServicesAssistance();

        services.Register(itemsBuilder);
        services.Register(shopAssistance);
        services.Register(playerAssistance);

        player.Init();
        services.Init();
        playerAssistance.Init(player);
        shopAssistance.Init();
    }
}
