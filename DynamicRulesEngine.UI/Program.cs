using DynamicRulesEngine.Configuration;
using DynamicRulesEngine.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DynamicRulesEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var rulesEngine = ConfigurationManager.GetSection("RulesEngine") as RulesEngineSection;
                if (rulesEngine == null)
                    Console.WriteLine("Invalid Rules Engine setup.");

                var events = new List<Event>()
                {
                        new Event() { Timestamp = "NOREAD", Count = "-1" },
                        new Event() { Count = "10" }
                };

                Engine<Event>
                    .Initialize(rulesEngine)
                    .ForEach(rule =>
                    {
                        events.ForEach(evnt =>
                        {
                            rule.Invoke(evnt);
                            Console.WriteLine($"Event Processed: {evnt.Processed}");
                        });
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }
    }
}
