using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Serializacao
{
    public class Serialize
    {
        public static XmlDocument SerializeMe<T>(T objeto, string encoding = "ISO-8859-1", XmlSerializerNamespaces namespaces = null) where T : class
        {
            if (string.IsNullOrEmpty(encoding))
                encoding = "ISO-8859-1";

            var construtorXml = new StringBuilder();
            var xmlSettings = new XmlWriterSettings
            {
                Indent = true,
                CheckCharacters = true,
                Encoding = Encoding.GetEncoding(encoding)
            };

            using (XmlWriter fs = XmlWriter.Create(construtorXml, xmlSettings))
            {
                var xmls = new XmlSerializer(typeof(T));

                if (namespaces != null)
                    xmls.Serialize(fs, objeto, namespaces);
                else
                    xmls.Serialize(fs, objeto);

                fs.Flush();
                fs.Close();
                var xmlRet = new XmlDocument { PreserveWhitespace = false };
                xmlRet.LoadXml(construtorXml.ToString());
                return xmlRet;
            }
        }
    }
}
