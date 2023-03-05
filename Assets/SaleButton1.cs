using UnityEngine;
using UnityEngine.UI;

public class SaleButton1 : MonoBehaviour
{
    public int index;
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() =>
            Click(index));
    }

    private void Click(int i)
    {
        PurchasingManager.Instance.OnPressDown(i);
    }
}