using UnityEngine;

namespace CSC2026
{
    public abstract class Item : MonoBehaviour
    {
        public string Name;
        public string Description;
        public Sprite Sprite;

        public abstract void Use(UserType user);
    }

    public enum UserType { Player, Enemy }
}
