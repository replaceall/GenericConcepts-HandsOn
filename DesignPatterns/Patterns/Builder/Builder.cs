using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Patterns
{
    class Builder
    {
        /*
         * Having a constructor with 10 arguments is a not a good option and not prodictive
         * Instead, Opt for pieceWise construction
         * BUILDER provides an API for constructing an object step-by-step
         * BUILDER: when piece wise construction is complicated, provide an API for doing it succinctly 
         */

        public void MainCall()
        {
            //.................... Creating Html structure with out any object oriented way-----------
            LifeWithOutBuilder();

            // ................... Using Builder pattern, creating HTML structure.....................

            NonFluentAndFluentBuilder();
        }

        private void NonFluentAndFluentBuilder()
        {
            // ordinary non-fluent builder
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");
            Console.WriteLine(builder.ToString());

            // fluent builder
            builder.Clear(); // disengage builder from the object it's building, then...
            builder.AddChildFluent("li", "hello").AddChildFluent("li", "world");
            Console.WriteLine(builder);
        }

        /// <summary>
        /// Create HTML template usind StringBuilder
        /// </summary>
        private void LifeWithOutBuilder()
        {
            string hello = "Hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            Console.WriteLine(sb);
            // ^^ Simpler

            // vv Below is something fishy to create un-orderd list, for-each loop is using to create an object (not good)
            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach(var word in words)
            {
                sb.AppendFormat("<li>{0}</li>",word);
            }
            sb.Append("</p>");

            Console.WriteLine(sb);
        }

        class HtmlElement
        {
            public string Name, Text;
            public List<HtmlElement> Elements = new List<HtmlElement>();
            private const int indentSize = 2;

            public HtmlElement()
            {

            }

            public HtmlElement(string name, string text)
            {
                Name = name;
                Text = text;
            }

            private string ToStringImpl(int indent)
            {
                var sb = new StringBuilder();
                var i = new string(' ', indentSize * indent);
                sb.Append($"{i}<{Name}>\n");
                if (!string.IsNullOrWhiteSpace(Text))
                {
                    sb.Append(new string(' ', indentSize * (indent + 1)));
                    sb.Append(Text);
                    sb.Append("\n");
                }

                foreach (var e in Elements)
                    sb.Append(e.ToStringImpl(indent + 1));

                sb.Append($"{i}</{Name}>\n");
                return sb.ToString();
            }

            public override string ToString()
            {
                return ToStringImpl(0);
            }
        }

        class HtmlBuilder
        {
            private readonly string rootName;

            public HtmlBuilder(string rootName)
            {
                this.rootName = rootName;
                root.Name = rootName;
            }

            // not fluent
            public void AddChild(string childName, string childText)
            {
                var e = new HtmlElement(childName, childText);
                root.Elements.Add(e);
            }

            public HtmlBuilder AddChildFluent(string childName, string childText)
            {
                var e = new HtmlElement(childName, childText);
                root.Elements.Add(e);
                return this;
            }

            public override string ToString()
            {
                return root.ToString();
            }

            public void Clear()
            {
                root = new HtmlElement { Name = rootName };
            }

            HtmlElement root = new HtmlElement();
        }
    }
}
