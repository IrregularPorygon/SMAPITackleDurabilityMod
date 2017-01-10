using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewValley.Menus;
using StardewValley.Tools;

namespace TackleDurabilityMod
{
    public class ModEntry : Mod
    {

        private ModConfig config;

        /*********
        ** Public methods
        *********/
        /// <summary>Initialise the mod.</summary>
        /// <param name="helper">Provides methods for interacting with the mod directory, such as read/writing a config file or custom JSON files.</param>
        public override void Entry(IModHelper helper)
        {
            MenuEvents.MenuClosed += Events_MenuClosed;

            config = this.Helper.ReadConfig<ModConfig>();
        }

        void Events_MenuClosed(object sender, EventArgsClickableMenuClosed e)
        {
            //We do nothing unless the menu that closed was the fishing game
            if (e.PriorMenu is BobberBar)
            {
                //Alright, they just finished fishing, now we can get to work.
                //First, we need to make sure they even have a rod that can support a tackle:
                FishingRod rod = (FishingRod)Game1.player.getToolFromName("Iridium Rod");
                if (rod != null)
                {
                    //We now have a reference to the iridium rod, but do they have any tackle?
                    if (rod.attachments.Length >= 2 && rod.attachments[1] != null)
                    {
                        //The code for durability is stored in a scale variable (FishingRod.cs Line# 992)
                        if(config.durabilityModifier >= 1.0f)
                        {
                            //So let's set that back to full
                            rod.attachments[1].scale.Y = 1.0f;
                        }
                        else
                        {
                            //Lets not set it to full, lets just add back a percentage of what it lost.
                            //The game removes 0.05f from it after every catch, so if they set it to .5f, 
                            //then we'll add back 0.025f, effectively doubling the durability of the tool
                            rod.attachments[1].scale.Y += 0.05f * config.durabilityModifier;
                        }
                    }
                    else
                    {
                        //No fishing tackle found, abort!
                    }
                }
                else
                {
                    //They don't have an iridium rod, so they can't have tackle equipped, do nothing.
                }
            }
        }
    }
}