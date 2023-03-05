using System;
using Donut;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PurchasingManager : PersistentSingleton<PurchasingManager>
{
    void Start()
    {
        InitializePurchasing();
    }
    
    [Header("Consumables: ", order = 1)]
    public ShopThingData[] consumable;

    [Header("Non consumables: ", order = 1)]
    public ShopThingData[] nonConsumable;

    [Header("Subscriptions: ", order = 1)]
    public ShopThingData[] subscriptions;

    public void InitializePurchasing()
    {
        // Create a builder, first passing in a suite of Unity provided stores.

        // Add a product to sell / restore by way of its identifier, associating the general identifier with its store-specific identifiers.

        #region build

        if (consumable != null && consumable.Length > 0)
        {
            for (int i = 0; i < consumable.Length; i++)
            {
                if (consumable[i] != null && !string.IsNullOrEmpty(consumable[i].kProductID))
                {
                    string prodID = consumable[i].kProductID;
                    consumable[i].clickEvent.RemoveAllListeners();
                    consumable[i].clickEvent.AddListener(() => { BuyProductID(prodID); });
                }
            }
        }

        if (nonConsumable != null && nonConsumable.Length > 0)
        {
            for (int i = 0; i < nonConsumable.Length; i++)
            {
                if (nonConsumable[i] != null && !string.IsNullOrEmpty(nonConsumable[i].kProductID))
                {
                    string prodID = nonConsumable[i].kProductID;
                    nonConsumable[i].clickEvent.RemoveAllListeners();
                    nonConsumable[i].clickEvent.AddListener(() => { BuyProductID(prodID); });
                }
            }
        }

        if (subscriptions != null && subscriptions.Length > 0)
        {
            for (int i = 0; i < subscriptions.Length; i++)
            {
                if (subscriptions[i] != null && !string.IsNullOrEmpty(subscriptions[i].kProductID))
                {
                    string prodID = subscriptions[i].kProductID;
                    nonConsumable[i].clickEvent.RemoveAllListeners();
                    nonConsumable[i].clickEvent.AddListener(() => { BuyProductID(prodID); });
                }
            }
        }
    }

    #endregion build

    public void OnPressDown(int i)
    {
        switch (i)
        {
            case 0:
                IAPManager.OnPurchaseSuccess = () =>
                    BuyProductID("starter");
                IAPManager.Instance.BuyProductID(IAPKey.PACK0);
                break;
            case 1:
                IAPManager.OnPurchaseSuccess = () =>
                    BuyProductID("sale_1");
                IAPManager.Instance.BuyProductID(IAPKey.PACK1);
                break;
            case 2:
                IAPManager.OnPurchaseSuccess = () =>
                    BuyProductID("coins_50");
                IAPManager.Instance.BuyProductID(IAPKey.PACK2);
                break;
            case 3:
                IAPManager.OnPurchaseSuccess = () =>
                    BuyProductID("coins_100");
                IAPManager.Instance.BuyProductID(IAPKey.PACK3);
                break;
            case 4:
                IAPManager.OnPurchaseSuccess = () =>
                    BuyProductID("coins_200");
                IAPManager.Instance.BuyProductID(IAPKey.PACK4);
                break;
            case 5:
                IAPManager.OnPurchaseSuccess = () =>
                    BuyProductID("life_5");
                IAPManager.Instance.BuyProductID(IAPKey.PACK5);
                break;
        }
    }

    public void Sub(int i)
    {
        GameDataManager.Instance.playerData.SubDiamond(i);
    }

    public Action <string, string> GoodPurchaseEvent;   // <id, name>
    public Action <string, string> FailedPurchaseEvent;
    
    public void BuyProductID(string productId)
    {
        ShopThingData prod = GetProductById(productId);
        if (prod != null)
        {
            prod.PurchaseEvent?.Invoke();
            GoodPurchaseEvent?.Invoke(productId, prod.name);
        }
        else
        {
            FailedPurchaseEvent?.Invoke(productId, "Unknown product");
        }
    }

    private ShopThingData GetProductById(string id)
    {
        if (consumable != null && consumable.Length > 0)
            for (int i = 0; i < consumable.Length; i++)
            {
                if (consumable[i] != null)
                    if (String.Equals(id, consumable[i].kProductID, StringComparison.Ordinal))
                        return consumable[i];
            }

        if (nonConsumable != null && nonConsumable.Length > 0)
            for (int i = 0; i < nonConsumable.Length; i++)
            {
                if (nonConsumable[i] != null)
                    if (String.Equals(id, nonConsumable[i].kProductID, StringComparison.Ordinal))
                        return nonConsumable[i];
            }

        if (subscriptions != null && subscriptions.Length > 0)
            for (int i = 0; i < subscriptions.Length; i++)
            {
                if (subscriptions[i] != null)
                    if (String.Equals(id, subscriptions[i].kProductID, StringComparison.Ordinal))
                        return subscriptions[i];
            }

        return null;
    }
}

[System.Serializable]
public class ShopThingData
{
    public string name;

    public string kProductID;

    [Space(8, order = 0)] [Header("Purchase Event: ", order = 1)]
    public UnityEvent PurchaseEvent;

    [HideInInspector] public Button.ButtonClickedEvent clickEvent;

    public ShopThingData(ShopThingData prod)
    {
        if (prod == null) return;
        name = prod.name;
        clickEvent = prod.clickEvent;
        kProductID = prod.kProductID;
        PurchaseEvent = prod.PurchaseEvent;
    }
}