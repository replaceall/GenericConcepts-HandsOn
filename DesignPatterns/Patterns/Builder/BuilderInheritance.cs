using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Patterns
{
    class BuilderInheritance
    {
        public void Maincall()
        {
            var me = Person.New
                    .Called("Dmitri")
                    .WorksAsA("Quant")
                    .Born(DateTime.UtcNow)
                    .Build();

            // type of var is different for the following methdos based on queries. 
            PersonJobBuilder<PersonBirthDateBuilder<Person.Builder>> x = Person.New
                    .Called("Dmitri");

            PersonBirthDateBuilder<Person.Builder> y = Person.New
                    .Called("Dmitri")
                    .WorksAsA("Quant");
            Console.WriteLine(me);
        }
    }
    public class Person
    {
        public string Name;

        public string Position;

        public DateTime DateOfBirth;

        /*
         * Should be inherited by last class, recursively, to access all the methods inside.
         * PersonBirthDateBuilder will be used , since its last in hierarchy (1. PersonInfoBuilder used by 2.PersonJobBuilder used by  3. PersonBirthDateBuilder)
         */
        public class Builder : PersonBirthDateBuilder<Builder> 
        {
            internal Builder() { }
        }

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<SELF> : PersonBuilder
      where SELF : PersonInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    public class PersonJobBuilder<SELF>
      : PersonInfoBuilder<PersonJobBuilder<SELF>>
      where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF)this;
        }
    }

    // here's another inheritance level
    // note there's no PersonInfoBuilder<PersonJobBuilder<PersonBirthDateBuilder<SELF>>>!

    public class PersonBirthDateBuilder<SELF>
      : PersonJobBuilder<PersonBirthDateBuilder<SELF>>
      where SELF : PersonBirthDateBuilder<SELF>
    {
        public SELF Born(DateTime dateOfBirth)
        {
            person.DateOfBirth = dateOfBirth;
            return (SELF)this;
        }
    }
}
