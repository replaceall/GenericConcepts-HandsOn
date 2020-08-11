using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    internal class ConcurrentDictionaryDemo
    {
        private static ConcurrentDictionary<string, string> capitals
      = new ConcurrentDictionary<string, string>();

        public static void MainCall()
        {
            AddParis();
            Task.Factory.StartNew(AddParis).Wait();

            //capitals["Russia"] = "Leningrad";
            // AddOrUpdate has the old value as well for the given key.
            var s = capitals.AddOrUpdate("Russia", "Moscow", (k, old) => old + " --> Moscow");
            Console.WriteLine("The capital of Russia is " + capitals["Russia"]);

            capitals["Sweden"] = "Uppsala";
            var capOfNorway = capitals.GetOrAdd("Sweden", "Stockholm"); // GetOrAdd - it first fetch the keyValue pair, if key is not present value will be added.
            Console.WriteLine($"The capital of Sweden is {capOfNorway}.");

            // removal
            const string toRemove = "Russia"; // make a mistake here
            string removed;
            var didRemove = capitals.TryRemove(toRemove, out removed);
            if (didRemove)
            {
                Console.WriteLine($"We just removed {removed}");
            }
            else
            {
                Console.WriteLine($"Failed to remove capital of {toRemove}");
            }

            // some operations are slow, e.g., Count, Empty
            //......................................................
            Console.WriteLine($"The ");

            foreach (var kv in capitals)
            {
                Console.WriteLine($" - {kv.Value} is the capital of {kv.Key}");
            }

            Console.ReadKey();
        }

        public static void AddParis()
        {
            // there is no add, since you don't know if it will succeed
            bool success = capitals.TryAdd("France", "Paris");

            string who = Task.CurrentId.HasValue ? ("Task " + Task.CurrentId) : "Main thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element.");
        }
    }
}
