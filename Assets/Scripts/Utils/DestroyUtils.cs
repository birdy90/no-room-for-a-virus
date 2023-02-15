using UnityEngine;

namespace Utils
{
    public class DestroyUtils : MonoBehaviour
    {
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}