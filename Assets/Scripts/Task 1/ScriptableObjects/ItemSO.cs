using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    [field: SerializeField] public string ID { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}