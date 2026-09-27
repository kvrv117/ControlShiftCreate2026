using UnityEngine;

namespace CSC2026
{
    public class Ghost : Item
    {
        public override void Use(UserType user)
        {
            UnitType a, b;
            (a, b) = Units.GetTwoRandomUnit();
            Rules.Replace(a, b);
        }
    }
}