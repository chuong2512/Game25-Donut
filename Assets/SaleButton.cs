using UnityEngine;
using UnityEngine.UI;

public class SaleButton : MonoBehaviour
{
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Click);
    }

    private void Click()
    {
        PurchasingManager.Instance.OnPressDown(0);
    }
}