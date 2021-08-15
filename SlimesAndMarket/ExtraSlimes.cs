using SRML.SR;
using SRML.Console;
using System.Collections.Generic;

namespace SlimesAndMarket
{
    class ExtraSlimes
    {
        public static List<Identifiable.Id> VANILLA_SLIMES = new List<Identifiable.Id>()
        {
            Identifiable.Id.PINK_SLIME,
            Identifiable.Id.TABBY_SLIME,
            Identifiable.Id.ROCK_SLIME,
            Identifiable.Id.PHOSPHOR_SLIME,
            Identifiable.Id.RAD_SLIME,
            Identifiable.Id.BOOM_SLIME,
            Identifiable.Id.CRYSTAL_SLIME,
            Identifiable.Id.HUNTER_SLIME,
            Identifiable.Id.HONEY_SLIME,
            Identifiable.Id.QUANTUM_SLIME,
            Identifiable.Id.DERVISH_SLIME,
            Identifiable.Id.TANGLE_SLIME,
            Identifiable.Id.MOSAIC_SLIME,
        };

        public static EconomyDirector.ValueMap GetValueMap(Identifiable.Id id)
        {
            EconomyDirector.ValueMap[] valueMaps = SRSingleton<SceneContext>.Instance.EconomyDirector.baseValueMap; //Everything that can be sold

            foreach (EconomyDirector.ValueMap valueMap in valueMaps)
            {
                Console.Log("GetValueMap: " + valueMap.accept.id);
                if (id == valueMap.accept.id)
                {
                    return valueMap;
                }
            }

            return null;
        }
        public static void RegisterSaberSlimes()
        {
            Identifiable.Id id = Identifiable.Id.SABER_PLORT;
            Identifiable.Id slimeId = Identifiable.Id.SABER_SLIME;

            EconomyDirector.ValueMap valueMap = GetValueMap(id);

            PlortRegistry.RegisterPlort(slimeId, valueMap.value * 50, valueMap.fullSaturation);

            DroneRegistry.RegisterBasicTarget(slimeId); //And make so that the drones can grab slimes

            Console.Log("Slime:" + slimeId + " registred perfectly");
        }

        public static void RegisterGoldSlimes()
        {
            Identifiable.Id id = Identifiable.Id.GOLD_PLORT;
            Identifiable.Id slimeId = Identifiable.Id.GOLD_SLIME;

            EconomyDirector.ValueMap valueMap = GetValueMap(id);

            PlortRegistry.RegisterPlort(slimeId, valueMap.value * 8, valueMap.fullSaturation);

            DroneRegistry.RegisterBasicTarget(slimeId); //And make so that the drones can grab slimes

            Console.Log("Slime:" + slimeId + " registred perfectly");
        }

        public static void RegisterPuddleSlimes()
        {
            Identifiable.Id id = Identifiable.Id.PUDDLE_PLORT;
            Identifiable.Id slimeId = Identifiable.Id.PUDDLE_SLIME;

            EconomyDirector.ValueMap valueMap = GetValueMap(id);

            PlortRegistry.RegisterPlort(slimeId, valueMap.value * 4, valueMap.fullSaturation);

            DroneRegistry.RegisterBasicTarget(slimeId); //And make so that the drones can grab slimes

            Console.Log("Slime:" + slimeId + " registred perfectly");
        }

        public static void RegisterFireSlimes()
        {
            Identifiable.Id id = Identifiable.Id.FIRE_PLORT;
            Identifiable.Id slimeId = Identifiable.Id.FIRE_SLIME;

            EconomyDirector.ValueMap valueMap = GetValueMap(id);

            PlortRegistry.RegisterPlort(slimeId, valueMap.value * 4, valueMap.fullSaturation);

            DroneRegistry.RegisterBasicTarget(slimeId); //And make so that the drones can grab slimes

            Console.Log("Slime:" + slimeId + " registred perfectly");
        }

        public static void RegisterQuicksilverSlimes()
        {
            Identifiable.Id slimeId = Identifiable.Id.QUICKSILVER_SLIME;

            PlortRegistry.RegisterPlort(slimeId, 200f*5, 100f);

            DroneRegistry.RegisterBasicTarget(slimeId); //And make so that the drones can grab slimes

            Console.Log("Slime:" + slimeId + " registred perfectly");
        }

        public static void RegisterLuckySlimes()
        {
            Identifiable.Id slimeId = Identifiable.Id.LUCKY_SLIME;

            PlortRegistry.RegisterPlort(slimeId, 250f*20, 125f);

            DroneRegistry.RegisterBasicTarget(slimeId); //And make so that the drones can grab slimes

            Console.Log("Slime:" + slimeId + " registred perfectly");
        }
    }
}
