using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandQueryResponsibilitySegregation
{
    // CQRS => Command Query Responsibility Segregation
    // CQS => Command Query Separation
        // Command => Do/Change
    public class Person
    {
        private int age;
        EventBroker broker;

        public Person(EventBroker broker)
        {
            this.broker = broker;
            this.broker.Commands += BrokerOnCommands;
            this.broker.Queries += BrokerOnQueriese;
        }

        private void BrokerOnQueriese(object sender, Query query)
        {
            var ag = query as AgeQuery;
            if (ag != null && ag.Target == this)
                ag.Result = age;

        }

        private void BrokerOnCommands(object sender, Command command)
        {
            var cac = command as ChangeAgeCommand;
            if (cac != null && cac.Target == this)
            {
                if (cac.Reqister) broker.AllEvents.Add(new AgeChangedEvent(this, age, cac.NewAge));
                age = cac.NewAge;
            }
        }
    }

    public class EventBroker
    {
        // 1. All the events that happened
        public IList<Event> AllEvents = new List<Event>();

        // 2. Commands
        public event EventHandler<Command> Commands;

        // 3. Query
        public event EventHandler<Query> Queries;

        public void Command(Command command)
        {
            Commands?.Invoke(this, command);
        }

        public T Query<T> (Query q)
        {
            Queries?.Invoke(this, q);
            return (T)q.Result;
        }

        public void UndoLast()
        {
            Event e = AllEvents.LastOrDefault();
            AgeChangedEvent ac = e as AgeChangedEvent;
            if (ac != null)
            {
                Command(new ChangeAgeCommand(ac.Target, ac.OldValue) { Reqister = false }); 
                AllEvents.Remove(e);
            }
        }
    }

    class StartUp
    {
        static void Main(string[] args)
        {
            EventBroker broker = new EventBroker();
            Person person = new Person(broker);
            broker.Command(new ChangeAgeCommand(person, 123));

            foreach (var e in broker.AllEvents)
                Console.WriteLine(e);

            int age;
            age = broker.Query<int>(new AgeQuery { Target = person });

            Console.WriteLine(age);

            broker.UndoLast();
            age = broker.Query<int>(new AgeQuery { Target = person });

            Console.WriteLine(age);
        }
    }
}
