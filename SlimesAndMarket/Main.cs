using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SRML;
using SRML.SR;

namespace SlimesAndMarket
{
    public static class Extension {
        public static EconomyDirector.ValueMap FindById(this Array obj, Identifiable.Id id, bool plort) //This is an extension (it's called so in C#) for arrays (EconomyDirector.ValueMap inherits from the class Array). Continue below
            //Parameter 1: The target type for the extension and our array
            //Parameter 2: The id of the slime or the plort
            //Parameter 3: It tells us if Parameter 2 is the id of a slime or of a plort
        {
            foreach (EconomyDirector.ValueMap valueMap in obj) //For each vanilla sellable plorts
            {
                if (valueMap.accept.id == (plort ? id : SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(id).Diet.Produces[0])) //Ternary Operator: If Parameter 3, "boolean plort", [Continue below]
                    //is true, then just make the control with Parameter 2 (it is a plort), else get the plort of the slime (use Parameter 2 as the id of the current slime)
                {
                    return valueMap; //If we found the matching slime then return th correct valueMap (informations about the plort that you normally sell)
                }
            }
            return null; //If nothing was found then return null and there will be an error in the mod
        }
    }

    public class Main : ModEntryPoint
    {
        public override void PostLoad()
        {
            EconomyDirector.ValueMap[] valueMaps = SRSingleton<SceneContext>.Instance.EconomyDirector.baseValueMap; //Everything that can be sold
            Identifiable.Id idPlort = Identifiable.Id.NONE; //The plort used for puddle/fire slimes

            try //Try the code there, if some error arises then it calls the "catch" function 
            {
                foreach (Identifiable.Id slime in Identifiable.SLIME_CLASS) //For each slime int he slime class
                {
                    bool puddleFlag = (slime == Identifiable.Id.PUDDLE_SLIME) ? true : false; //Ternary Operator: If the id is the id of a puddle slime then return true, else false
                    bool fireFlag = (slime == Identifiable.Id.FIRE_SLIME) ? true : false; //Same as above, only with fire slimes
                    bool flag = puddleFlag || fireFlag; //If the slime is a puddle slime or a fire slime, the this variable will be true, else they will be false
                    if (flag)
                        idPlort = (puddleFlag) ? Identifiable.Id.PUDDLE_PLORT : Identifiable.Id.FIRE_PLORT; // If the id (the slime variable) is a puddle or fire slime then assign the variable "idPlort" the puddle/fire plort id

                    if (slime != Identifiable.Id.TARR_SLIME && slime != Identifiable.Id.LUCKY_SLIME) //Avoid the code for tarrs and lucky slimes (they dont have plorts in vanilla and currently this mod dosen't support modded slimes)
                    {
                        EconomyDirector.ValueMap valueMap = (flag) ? valueMaps.FindById(idPlort, true) : valueMaps.FindById(slime, false); //Read below
                        //If the slime is a puddle/fire slime then add the plort id, else just add the id of the slime (true/false argumeter tells the programm if it's a puddle/fire slime or regular slimes, except tarrs and lucky slimes)
                        //We are getting the informations of the plort of the correct slime (to make normal prices)
                        PlortRegistry.RegisterPlort(slime, valueMap.value * 4, valueMap.fullSaturation); //Then register the slime (make so that you can sell it) for 4 times the original price more.

                    }
                }
            }
            catch (Exception e) //But if an error was found
            {
                SRML.Console.Console.Log("Sellable Slimes Mod error: " + e.Message); //Then log the error into the SRML.log file
            }
        }

    }
}
