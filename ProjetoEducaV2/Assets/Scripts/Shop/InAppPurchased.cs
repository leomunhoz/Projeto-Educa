using UnityEngine;
using UnityEngine.Purchasing;


public class InAppPurchased : MonoBehaviour
{
    // Start is called before the first frame update
   // public string environment = "production";

    //async void Start()
    //{
    //    try
    //    {
    //        var options = new InitializationOptions()
    //            .SetEnvironmentName(environment);

    //        await UnityServices.InitializeAsync(options);
    //    }
    //    catch (Exception exception)
    //    {
    //        // An error occurred during initialization.
    //    }
    //}

    public void OnPurchaseCompled(Product product)
    {
        Debug.Log("PURCHASED ITEM: " + product.definition.id);

        if (product.definition.type == ProductType.Consumable)
        {
            int count = PlayerPrefs.GetInt(product.definition.payout.subtype);
            count += (int)product.definition.payout.quantity;
            PlayerPrefs.SetInt(product.definition.payout.subtype, count);
            Debug.Log("PURCHASED QUANTITY: " + product.definition.payout.quantity);
            Debug.Log("TOTAL ["+ product.definition.payout.subtype + "] QUANTITY: " + count);
        }
        else
        {
            PlayerPrefs.SetInt(product.definition.id, 1);
        }
        
        PlayerPrefs.Save();

        //loop all buttons on scene to update button state
        CheckPurchase[] purchaseUIs = FindObjectsOfType<CheckPurchase>();
        foreach (CheckPurchase ui in purchaseUIs)
        {
            ui.UpdatePurchaseUI();
        }

    }
}
