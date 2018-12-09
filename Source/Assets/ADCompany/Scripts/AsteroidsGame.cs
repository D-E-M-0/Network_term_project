using UnityEngine;

namespace Photon.Pun.Demo.Asteroids
{
    public class AsteroidsGame
    {
        public const string PLAYER_READY = "IsPlayerReady";
        public const string PLAYER_LOADED_LEVEL = "PlayerLoadedLevel";

		public const int PLAYER_PROGRESS = 0;

        public static Color GetColor(int colorChoice)
        {
            switch (colorChoice)
            {
                case 0: return Color.red;
                case 1: return Color.green;
            }

            return Color.black;
        }
    }
}