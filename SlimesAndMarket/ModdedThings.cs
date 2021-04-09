using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimesAndMarket
{
    class ModdedThings
    {
        public static List<string> modsRegistred = new List<string>();
        public static List<Identifiable.Id> VANILLA_SLIMES = new List<Identifiable.Id>() 
        { 
            Identifiable.Id.PINK_SLIME,
            Identifiable.Id.TABBY_SLIME,
            Identifiable.Id.ROCK_SLIME,
            Identifiable.Id.PHOSPHOR_SLIME,
            Identifiable.Id.RAD_SLIME,
            Identifiable.Id.BOOM_SLIME,
            Identifiable.Id.PUDDLE_SLIME,
            Identifiable.Id.CRYSTAL_SLIME,
            Identifiable.Id.HUNTER_SLIME,
            Identifiable.Id.HONEY_SLIME,
            Identifiable.Id.QUANTUM_SLIME,
            Identifiable.Id.DERVISH_SLIME,
            Identifiable.Id.TANGLE_SLIME,
            Identifiable.Id.MOSAIC_SLIME,
            Identifiable.Id.FIRE_SLIME,
            Identifiable.Id.SABER_SLIME,
            Identifiable.Id.GOLD_SLIME,
        };

    }

    public class ModdedSellable
    {
        public string nameOfMod;
        public float price;
        public float saturation;
        public Identifiable.Id itemToSell;
        public bool droneTake;

        public static List<ModdedSellable> moddedItemsToSell = new List<ModdedSellable>();

        public ModdedSellable(string _nameOfMod, float _price, float _saturation, Identifiable.Id _itemToSell, bool _droneTake)
        {
            this.nameOfMod = _nameOfMod;
            this.price = _price;
            this.saturation = _saturation;
            this.itemToSell = _itemToSell;
            this.droneTake = _droneTake;
        }


    }
}
