using UnityEngine;

namespace Utils
{
    public static class GameFlow
    {
        public static bool IsPaused => Time.timeScale == 0;

        public static bool PauseMenuAllowed = true;
    
        public static void Pause()
        {
            Time.timeScale = 0;
        }
        
        public static void Unpause()
        {
            Time.timeScale = 1;
        }
    }
}