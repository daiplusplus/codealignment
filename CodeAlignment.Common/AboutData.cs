using System;
using System.Linq;
using System.Drawing;
using System.Reflection;

namespace CMcG.CodeAlignment
{
    public class AboutData
    {
        public static Image Image { get; set; }

        static Assembly s_mainAssembly;
        public static Assembly MainAssembly
        {
            get { return s_mainAssembly ?? Assembly.GetExecutingAssembly(); }
            set { s_mainAssembly = value; }
        }

        public String AssemblyTitle
        {
            get
            {
                String title = this.GetAttributeProperty<AssemblyTitleAttribute>(x => x.Title);
                return title != String.Empty ? title : "Code alignment";
            }
        }

        public String AssemblyVersion
        {
            get { return MainAssembly.GetName().Version.ToString(); }
        }

        public String AssemblyCopyright
        {
            get { return this.GetAttributeProperty<AssemblyCopyrightAttribute>(x => x.Copyright); }
        }

        public String GetAttributeProperty<T>(Func<T, String> getProperty) where T : Attribute
        {
            T attribute = (T)MainAssembly.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            return attribute != null ? getProperty.Invoke(attribute) : String.Empty;
        }
    }
}
