using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Assignments
{
    /*
     * Question:
     * var cb = new CodeBuilder("person").AddField("Name", "string").AddField("Age", "int");
     * */
    class CodeBuilder
    {
        List<Tuple<string, string>> ListOfTuples;
        private string name;

        public CodeBuilder(string name)
        {
            this.name = name;
        }

        public CodeBuilder AddField(string field, string type)
        {
            ListOfTuples.Add(Tuple.Create(field, type));
            return this;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
