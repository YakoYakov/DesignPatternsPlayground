namespace CommandQueryResponsibilitySegregation
{
    public class ChangeAgeCommand : Command
    {
        public Person Target;
        public  int NewAge;
        public ChangeAgeCommand(Person target, int newAge)
        {
            this.Target = target;
            this.NewAge = newAge;
        }
    }
}