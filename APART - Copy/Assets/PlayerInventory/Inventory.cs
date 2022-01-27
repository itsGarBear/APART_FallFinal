using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //Inventory Slots
    public int currentEquipped;
    public List<Image> inventorySlots;
    public List<Image> inventoryItems;
    public List<Slider> reloadSliders;
    public List<GameObject> itemPrefabs;
    public Image selectedSlot;
    public Image itemSlot1;
    public SpriteRenderer armImage;

    [SerializeField]
    public int currentSlot;
    public Sprite selectedSlotImage;
    public Sprite defaultSlotImage;
    public bool selected;

    //Ammo
    public int pistolAmmo;
    public int tranqAmmo;

    private Player player;
    private Transform dropTransform;

    public bool hasLevel1 = false;
    public bool hasLevel2 = false;
    public bool hasLevel3 = false;

    public Image level1Card;
    public Image level2Card;
    public Image level3Card;

    private void Awake()
    {
        int i = 0;
        foreach (Image im in inventorySlots)
        {
            //Debug.Log(ItemDatabase.instance.GetItem(GetSlotItem(i)));
            i++;
        }

        player = FindObjectOfType<Player>();
    }

    public void DropItem()
    {
        if (!selected)
        {
            return;
        }
        if (currentEquipped == 0)
        {
            return;
        }

        int iD = GetSlotItem(currentSlot);
        Instantiate(itemPrefabs[iD-1], new Vector3(player.transform.localPosition.x + 2f, player.transform.localPosition.y + 1f, player.transform.localPosition.z), Quaternion.identity);
        //inventoryItems[currentSlot].sprite = Resources.Load<Sprite>("Items/EmptyHand");
        UpdatePlayerEquipped(0, currentSlot);
    }

    public void UpdatePlayerEquipped(int itemID, int slotNdx)
    {
        Item i;
        currentEquipped = itemID;
        i = ItemDatabase.instance.GetItem(itemID);
        inventoryItems[slotNdx].sprite = Resources.Load<Sprite>("Items/" + i.name);
        if(PlayerController.myPlayer.isCara)
            armImage.sprite = Resources.Load<Sprite>("Arms/Cara/" + i.name);
        else
            armImage.sprite = Resources.Load<Sprite>("Arms/Alice/" + i.name);
    }


    public void ResetInventory()
    {
        foreach (Image iS in inventorySlots)
        {
            iS.sprite = defaultSlotImage;
        }

        foreach (Image iI in inventoryItems)
        {
            iI.sprite = Resources.Load<Sprite>("Items/EmptyHand");
        }

        UpdatePlayerEquipped(5, 0);
        UpdatePlayerEquipped(0, 1);

    }
    public void UpdateSlotSelection(int index)
    {

        foreach (Image iS in inventorySlots)
        {
            iS.sprite = defaultSlotImage;
        }

        if (currentSlot == index && selected == true)
        {

            inventorySlots[currentSlot].sprite = defaultSlotImage;
            selected = false;
            currentSlot = index;
            selectedSlot = inventorySlots[index];
            if (PlayerController.myPlayer.isCara)
                armImage.sprite = Resources.Load<Sprite>("Arms/Cara/EmptyHand");
            else
                armImage.sprite = Resources.Load<Sprite>("Arms/Alice/EmptyHand");
        }
        else if (currentSlot == index && selected == false)
        {
            inventorySlots[currentSlot].sprite = selectedSlotImage;
            selected = true;
            currentSlot = index;
            selectedSlot = inventorySlots[index];
            UpdatePlayerEquipped(GetSlotItem(index), index);
        }
        else
        {
            inventorySlots[index].sprite = selectedSlotImage;
            selected = true;
            currentSlot = index;
            selectedSlot = inventorySlots[index];
            UpdatePlayerEquipped(GetSlotItem(index), index);
        }
    }

    public int GetSlotItem(int index)
    {
        GameObject go;
        Item i;
        string goName;

        if (index == 0)
        {
            go = GameObject.FindGameObjectWithTag("Slot1");
            goName = go.GetComponent<Image>().sprite.name;
            //Debug.Log(goName);
            i = ItemDatabase.instance.GetItem(goName);
            //Debug.Log(i.id);
            return i.id;
        }
        else if (index == 1)
        {
            go = GameObject.FindGameObjectWithTag("Slot2");
            goName = go.GetComponent<Image>().sprite.name;
            //Debug.Log(goName);
            i = ItemDatabase.instance.GetItem(goName);
            //Debug.Log(i.id);
            return i.id;
        }

        return 0;
    }

    public void ResetCards()
    {
        hasLevel1 = false;
        hasLevel2 = false;
        hasLevel3 = false;
    }
}
