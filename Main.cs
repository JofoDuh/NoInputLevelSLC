using System.Reflection;
using HarmonyLib;
using UnityModManagerNet;

namespace NoInputLevelSLC
{
    public static class Main
    {
        public static bool IsEnabled = false;
        public static UnityModManager.ModEntry.ModLogger Logger;
        public static Harmony harmony;
        public static Setting setting;
       
        internal static void Setup(UnityModManager.ModEntry modEntry)
        {
            setting = new Setting();
            setting = UnityModManager.ModSettings.Load<Setting>(modEntry);
            
            Logger = modEntry.Logger;
            modEntry.OnToggle = OnToggle;
            
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            IsEnabled = value;
            if (value)
            {
                harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                
                modEntry.OnSaveGUI = OnSaveGUI;
            }
            else
            {
                harmony.UnpatchAll(modEntry.Info.Id);
            }
            return true;
        }
        
        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            setting.Save(modEntry);
        }
    }
}