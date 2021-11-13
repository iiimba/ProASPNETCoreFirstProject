namespace GOFPatternExamples.Prototype
{
    abstract class Employee
    {
        protected Employee(string name, decimal salary)
        {
            Name = name;
            Salary = salary;
        }

        public string Name { get; }

        public decimal Salary { get; }

        public abstract Employee Clone();

        public override string ToString()
        {
            return $"Name = {Name}, Salary = {Salary}";
        }
    }

    class Developer : Employee
    {
        public Developer(string name, decimal salary, string programingLanguage)
            : base(name, salary)
        {
            ProgramingLanguage = programingLanguage;
        }

        public string ProgramingLanguage { get; }

        public override Employee Clone()
        {
            return new Developer(this.Name, this.Salary, this.ProgramingLanguage);
        }

        public override string ToString()
        {
            return base.ToString() + $", ProgramingLanguage = {ProgramingLanguage}";
        }
    }
}
