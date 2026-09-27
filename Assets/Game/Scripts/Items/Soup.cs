using UnityEngine;

namespace CSC2026
{
    public class Soup : Item
    {
        public override void Use(UserType user)
        {
            if (user == UserType.Player)
            {
                HealthManager.ChangePlayerHealth(1);
            }
            else
            {
                HealthManager.ChangeEnemyHealth(1);
            }
        }
    }
}