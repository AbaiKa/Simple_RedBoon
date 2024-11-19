using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public Dictionary<string, int> Resources = new Dictionary<string, int>();
    public Dictionary<string, Item> Items = new Dictionary<string, Item>();
}
[Serializable]
public class Item
{
    public string id;
    public int price;
    public int count;

    public Item(string id, int price) 
    {
        this.id = id;
        this.price = price;
        count = 1;
    }
}
