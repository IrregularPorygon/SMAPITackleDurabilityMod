using StardewModdingAPI;
using System.Collections.Generic;

namespace TackleDurabilityMod
{
    public class ModConfig
    {
        public float durabilityModifier { get; set; }

        public ModConfig()
        {
            durabilityModifier = 1.0f;
        }

    }
}
