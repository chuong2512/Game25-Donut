using UnityEngine;

namespace DonutMatach
{
    public class OpenURLButton : MonoBehaviour
    {
        public void Click()
        {
            Application.OpenURL("market://details?id=" + Application.identifier);
        }
    }
}