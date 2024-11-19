using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image imageComponent;
    [SerializeField] private TextMeshProUGUI priceTextComponent;

    public string ID {  get; private set; }
    public int Price { get; private set; }
    public void Set(string id, int price, Sprite icon)
    {
        ID = id;
        Price = price;
        imageComponent.sprite = icon;
        priceTextComponent.text = price.ToString();
    }
}
