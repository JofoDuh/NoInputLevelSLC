using HarmonyLib;

namespace NoInputLevelSLC
{
    public static class InputBlocker
    {
        public static bool IsActive { get; set; } = false;
    }
    public static class Patch
    {
        [HarmonyPatch(typeof(scrController), "CountValidKeysPressed")]
        [HarmonyAfter("KeyboardChatterBlocker")]
        public static class Patch_CountValidKeysPressed
        {
            static void Postfix(ref int __result)
            {

                InputBlocker.IsActive = ADOBase.isLevelSelect ? true : false;

                if (InputBlocker.IsActive)
                {
                    __result = 0; // Prevents any key presses from being counted
                }
            }
        }
    }
}