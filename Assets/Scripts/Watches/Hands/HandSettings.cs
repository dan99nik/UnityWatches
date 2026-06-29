using UnityEngine;

namespace App.Watches.Hands
{
    public class HandSettings : MonoBehaviour
    {
        [SerializeField] private HandType handType;
        [SerializeField] private Transform handTransform;

        public HandType HandType => handType;
        public Transform HandTransform => handTransform;
    }
}
