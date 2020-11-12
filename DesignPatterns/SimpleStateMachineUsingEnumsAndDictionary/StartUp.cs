using System;
using System.Collections.Generic;

namespace SimpleStateMachineUsingEnumsAndDictionary
{
    public enum State
    {
        OffHook,
        Connecting,
        Connected,
        OnHold
    }

    public enum Trigger
    {
        CallDialed,
        HangUp,
        CallConnected,
        PlacedOnHold,
        TakenOffHold,
        LeftMessage
    }

    class StartUp
    {
        private static Dictionary<State, List<(Trigger, State)>> rules
            = new Dictionary<State, List<(Trigger, State)>>
            {
                [State.OffHook] = new List<(Trigger, State)>
                {
                    (Trigger.CallDialed, State.Connecting)
                },
                [State.Connecting] = new List<(Trigger, State)>
                {
                    (Trigger.HangUp, State.OffHook),
                    (Trigger.CallConnected, State.Connected)
                },
                [State.Connected] = new List<(Trigger, State)>
                {
                    (Trigger.LeftMessage, State.OffHook),
                    (Trigger.HangUp, State.OffHook),
                    (Trigger.PlacedOnHold, State.OnHold)
                },
                [State.OnHold] = new List<(Trigger, State)>
                {
                  (Trigger.TakenOffHold, State.Connected),
                  (Trigger.HangUp, State.OffHook)
                }
            };

        static void Main(string[] args)
        {
            State state = State.OffHook;

            while (true)
            {
                Console.WriteLine($"The phone is currently {state}");
                Console.WriteLine("Select an action");

                for (int i = 0; i < rules[state].Count; i++)
                {
                    (Trigger t, State _) = rules[state][i];
                    Console.WriteLine($"{i}. {t}");
                }

                if (!int.TryParse(Console.ReadLine(), out int input))
                    continue;

                if (input >= 0 && input <= rules[state].Count - 1)
                {
                    (Trigger _, State s) = rules[state][input];
                    state = s;
                    continue;
                }

                Console.WriteLine("Choose a valid action number!");
            }
        }
    }
}
