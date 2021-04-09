using SRML.Console;
using System.Collections.Generic;

namespace SlimesAndMarket
{
    class RegistredModsCommand : ConsoleCommand
    {
        // The command's command ID; how someone using the console will call your command.
        public override string ID => "slimes_and_market_registred_mods";

        // What will appear when the command is used incorrectly or viewed via the help command.
        // Remember, <> is a required argument while [] is an optional one.
        public override string Usage => "slimes_and_market_registred_mods";

        // A description of the command that will appear when using the help command.
        public override string Description => "Reveals what mods are registred and what not (what mods uses this mod to make their things sellable)";

        // The code that the command runs. Requires you to return a bool.
        public override bool Execute(string[] args)
        {
            // Checks if the code has enough arguments.
            if (args == null || args.Length > 0)
            {
                Console.LogError("Incorrect amount of arguments!", true);
                return false;
            }

            Console.Log("Mods registred are:");

            foreach(string mod in ModdedThings.modsRegistred)
            {
                Console.Log(mod);
            }

            return true;
        }

        // A list that the autocomplete references from. You must return a List<string>.
        public override List<string> GetAutoComplete(int argIndex, string argText)
        {
            // Checks which argument you're on.
            List<string> result;
            result = base.GetAutoComplete(argIndex, argText);

            return result;
        }
    }
}