using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public static ItemDatabase instance;

    private void Awake()
    {
        BuildDatabase();
        instance = this;
    }

    public Item GetItem(int id)
    {
        //Debug.Log(items.Find(item => item.id == id).id);
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName)
    {
        //Debug.Log(items.Find(item => item.name == itemName));
        return items.Find(item => item.name == itemName);
    }

    void BuildDatabase()
    {
        items = new List<Item>()
        {
            new Item(0, "EmptyHand", "You're not holding anything"),
            new Item(1, "Pistol", "A standard sidearm given to security guards"),
            new Item(2, "Tranq", "A tranquilizer pistol"),
            new Item(3, "Wrench", "A monkey wrench"),
            new Item(4, "Scalpel", "A scalpel"),
            new Item(5, "Umbrella", "An orange umbrella")
        };
        foreach (Item i in items)
        {
            //Debug.Log(i.id + " " + i.name + " " + i.description);
        }
    }
}
