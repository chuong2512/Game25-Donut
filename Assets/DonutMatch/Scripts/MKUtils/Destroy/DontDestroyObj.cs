using UnityEngine;
using System.Collections;

/*
   11112019 - first
 */

namespace DonutMatach
{
    public class DontDestroyObj : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}