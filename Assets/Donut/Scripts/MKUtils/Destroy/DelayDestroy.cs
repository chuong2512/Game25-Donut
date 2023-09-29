using UnityEngine;

/*
    27.08.2020 - first
 */
namespace DonutMatach
{
    public class DelayDestroy : MonoBehaviour
    {
        [SerializeField]
        private float time = 0.0f;

        void Awake()
        {
            Destroy(gameObject, time);
        }
    }
}
