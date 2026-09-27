using UnityEngine;

namespace CSC2026
{
    public class UpsideDown : Item
    {
        public override void Use(UserType user)
        {
            Rules.UpsideDown();
        }
    }
}