using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CharacterInventory : MonoBehaviour
{
    #region Variable Declarations
    public static CharacterInventory instance;

    private HotBar hotBar;
    Sprite defaultSprite;
    private Dictionary<int, ItemPickupSO> storedItems = new Dictionary<int, ItemPickupSO>();
    int itemSlot, amount;
    bool isRunning;
    #endregion

    #region Initializations
    private void Awake()
    {
        instance = this;
        isRunning = true;
    }

    private void Start()
    {
        hotBar = HotBar.instance;
        defaultSprite = hotBar.defaultSprite;
        for (int i = 0; i < hotBar.buttons.Length; i++)
        {
            hotBar.buttons[i].image.sprite = defaultSprite;
            hotBar.buttons[i].GetComponentInChildren<Text>().text = "";
        }

        StoreItem(GetComponent<PlayerController>().startingWeapon);

        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }
    #endregion

    private KeyCode[] keyCodes =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4
    };

    private void Update()
    {
        if (!isRunning)
            return;

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                UseStoredItem(i);
            }
        }
    }

    public void StoreItem(ItemPickupSO itemToStore)
    {
        //Check if the item is already stored
        if (storedItems.ContainsValue(itemToStore))
        {
            IncreaseStoredItemAmount(itemToStore);
        }
        else
        {
            StoreNewItem(itemToStore);
        }
    }

    private void UseStoredItem(int i)
    {
        if (storedItems.TryGetValue(i, out ItemPickupSO itemToUse))
        {
            //Try and use the item then check if it has been used && is destroy on use
            if (itemToUse.UseItem() && itemToUse.destroyOnUse)
            {
                amount = int.Parse(hotBar.buttons[i].GetComponentInChildren<Text>().text);

                if (hotBar.buttons[i].GetComponentInChildren<Text>().text == "" || amount == 1)
                {
                    storedItems.Remove(i);
                    hotBar.buttons[i].image.sprite = defaultSprite;
                    hotBar.buttons[i].GetComponentInChildren<Text>().text = "";
                }
                else if (amount > 1)
                {
                    amount--;
                    hotBar.buttons[i].GetComponentInChildren<Text>().text = amount.ToString();
                }
            }
        }
    }

    private void IncreaseStoredItemAmount(ItemPickupSO itemToStore)
    {
        //Get the item slot of the stored item
        itemSlot = storedItems.FirstOrDefault(x => x.Value == itemToStore).Key;

        if (itemToStore.isStackable)
        {
            amount = int.Parse(hotBar.buttons[itemSlot].GetComponentInChildren<Text>().text);
            amount++;
            hotBar.buttons[itemSlot].GetComponentInChildren<Text>().text = amount.ToString();
        }
        else
        {
            Debug.Log("You are already carrying " + itemToStore.name);
        }
    }

    private void StoreNewItem(ItemPickupSO itemToStore)
    {
        for (int i = 0; i < hotBar.buttons.Length; i++)
        {
            //Check if the slot is empty
            if (!storedItems.ContainsKey(i))
            {
                storedItems.Add(i, itemToStore);
                hotBar.buttons[i].image.sprite = itemToStore.icon;

                if (itemToStore.isStackable)
                {
                    hotBar.buttons[i].GetComponentInChildren<Text>().text = "1";
                }

                break;
            }
        }
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        isRunning = currentState == GameManager.GameState.RUNNING;
    }
}
