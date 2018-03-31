using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Simulator_of_Light.Simulator.Utilities
{
    public static class XmlExtension
    {
        public static string Serialize<T>(this T value)
        {
            if (value == null) return string.Empty;

            var xmlSerializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                xmlSerializer.Serialize(writer, value);
                return stringWriter.ToString();
            }
        }
    }
}
