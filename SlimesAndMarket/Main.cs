using System;
using System.Collections.Generic;
using System.Linq;

using SRML;
using SRML.SR;

using Console = SRML.Console.Console;

namespace SlimesAndMarket
{
    public class Main : ModEntryPoint
    {
        public override void PreLoad()
        {
            HarmonyInstance.PatchAll();
        }

        public override void Load()
        {
            Console.Log("Loading SlimesAndMarket");

            try //Try the code there, if some error arises then it calls the "catch" function 
            {
                foreach (Identifiable.Id slime in ExtraSlimes.VANILLA_SLIMES) //For each slime int he slime class
                {
                    Console.Log("Printing Slime: " + slime.ToString());

                    EconomyDirector.ValueMap valueMap = Extension.GetValueMap(slime);
                    
                    //If the slime is a puddle/fire/quicksilver slime then add the plort id, else just add the id of the slime (true/false argumeter tells the programm if it's a puddle/fire/quicksilver slime or regular slimes, except tarrs and lucky slimes)
                    //We are getting the informations of the plort of the correct slime (to make normal prices)
                    //But if we find nothing then return null
                    Console.Log("   valueMap not found...");
                    if (valueMap == null)
                        continue;
                    Console.Log("   ...or maybe yes? Now registering slimes");

                    PlortRegistry.RegisterPlort(slime, valueMap.value * 4, valueMap.fullSaturation); //Else, if the slime is a regular slime, then register the slime (make so that you can sell it) for 4 times the original price more.

                    DroneRegistry.RegisterBasicTarget(slime); //And make so that the drones can grab slimes

                    Console.Log("Slime: " + slime + " registred perfectly");
                }
            }
            catch (Exception e) //But if an error was found
            {
                Console.Log("Slimes and Market mod error (Vanilla): " + e.Message); //Then log the error into the SRML.log file
            }

            ExtraSlimes.RegisterSaberSlimes();
            ExtraSlimes.RegisterGoldSlimes();
            ExtraSlimes.RegisterFireSlimes();
            ExtraSlimes.RegisterPuddleSlimes();
            ExtraSlimes.RegisterQuicksilverSlimes();
            ExtraSlimes.RegisterLuckySlimes();
        }

    }

    public static class Extension
    {
        static List<Identifiable.Id> registredSlimes = new List<Identifiable.Id>(); //A List used to mark what slimes already got registred and what not
        static List<Identifiable.Id> largoSlimes = LargoLibrary.LargoGenerator.generatedIds;

        public static bool FindContent(this Array obj, Identifiable.Id comparator, string print = "") //This is an extension (it's called so in C#) for arrays that allows me to search if something is in that array or not (without erroring).
        {
            if (print != "")
                //Console.Log("Printing Array: " + print);
                foreach (Identifiable.Id content in obj) //For each element in our array
                {
                    if (print != "")
                        //Console.Log("Printing Id: " + content);
                        if (content == comparator) //If the ID is there
                        {
                            //Console.Log("   Something got found in the comparator: " + content);
                            return true; //Then return true
                        }
                }
            //Console.Log("   Nothing got found in the comparator");
            return false; //Else return false
        }

        public static bool IsNullOrEmpty<T>(this T[] array) //To see if an array exists and/or has 
        {
            if (array == null || array.Length == 0)
                return true;
            else
                return array.All(item => item == null);
        }

        public static EconomyDirector.ValueMap FindById(this Array obj, string puddleSlime)
        {
            Identifiable.Id acceptId = Identifiable.Id.NONE;
            Identifiable.Id acceptIdPlort = Identifiable.Id.NONE;

            foreach (EconomyDirector.ValueMap valueMap in obj)
            {
                if (puddleSlime == "PUDDLE_SLIME")
                {
                    acceptId = Identifiable.Id.PUDDLE_SLIME;
                    acceptIdPlort = Identifiable.Id.PUDDLE_PLORT;
                }
                else
                {
                    acceptId = Identifiable.Id.FIRE_SLIME;
                    acceptIdPlort = Identifiable.Id.FIRE_PLORT;
                }

                if (!registredSlimes.ToArray().FindContent(acceptId))
                { //If the slime wasn't registred before, then continue, else dont continue

                    if (valueMap.accept.id == acceptIdPlort) //If the Identifiable.Id matches with the plort already registred
                    {
                        registredSlimes.Add(acceptId); //Add this slime to the eatmap: now it's registred, and it cannot be registred again
                        return valueMap; //Then we found a matching slime, so return the correct valueMap (informations about the plort that you normally sell)
                    }
                }
            }
            return null;
        }

        public static EconomyDirector.ValueMap FindById(this Array obj, Identifiable.Id id)
        {
            foreach (EconomyDirector.ValueMap valueMap in obj) //For each vanilla sellable plorts
            {
                bool check = true;
                Identifiable.Id acceptId = Identifiable.Id.NONE;

                if (check)
                {
                    if (SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(id).Diet.Produces.IsNullOrEmpty()) //If the "plort" parameter is not a slime and the slime produces no plorts, avoid the error and just pass on the next slime
                        break;
                }

                if (check)
                    acceptId = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(id).Diet.Produces[0];

                //Identifiable.Id acceptId = (plort ? id : SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(id).Diet.Produces[0]); //Ternary Operator: If Parameter 3, "boolean plort", [Continue below]
                //is true, then just make the control with Parameter 2 (it is a plort), else get the plort of the slime (use Parameter 2 as the id of the current slime)

                if (!registredSlimes.ToArray().FindContent(acceptId))
                { //If the slime wasn't registred before, then continue, else dont continue

                    if (valueMap.accept.id == acceptId) //If the Identifiable.Id matches with the plort already registred
                    {
                        registredSlimes.Add(acceptId); //Add this slime to the eatmap: now it's registred, and it cannot be registred again
                        return valueMap; //Then we found a matching slime, so return the correct valueMap (informations about the plort that you normally sell)
                    }
                }
            }

            return null; //If nothing was found then return null
        }
        
        public static EconomyDirector.ValueMap GetValueMap(Identifiable.Id id)
        {
            EconomyDirector.ValueMap[] valueMaps = SRSingleton<SceneContext>.Instance.EconomyDirector.baseValueMap; //Everything that can be sold

            foreach (EconomyDirector.ValueMap valueMap in valueMaps)
            {
                Console.Log("GetValueMap: " + valueMap.accept.id);
                if (SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(id).Diet.Produces[0] == valueMap.accept.id)
                {
                    return valueMap;
                }
            }

            return null;
        }

    }
}
