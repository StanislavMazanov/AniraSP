using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AniraSP.DAL.Handles {
    public sealed class XmlSingletonSerializer<T> {
        private static readonly object Locker = new object();
        private static XmlSingletonSerializer<T> _instance;

        private readonly XmlSerializer _serializer;
        private readonly XmlReaderSettings _readerSettings;
        private readonly XmlWriterSettings _writerSettings;
        private readonly XmlSerializerNamespaces _serializerNamespaces;

        private XmlSingletonSerializer() {
            _serializer = new XmlSerializer(typeof(T));

            _readerSettings = new XmlReaderSettings();
            _writerSettings = new XmlWriterSettings {
                Encoding = new UnicodeEncoding(false, false),
                Indent = false,
                OmitXmlDeclaration = false
            };

            _serializerNamespaces = new XmlSerializerNamespaces();
            _serializerNamespaces.Add("", "");
        }

        public static XmlSingletonSerializer<T> Instance {
            get {
                if (_instance == null) {
                    lock (Locker) {
                        if (_instance == null)
                            _instance = new XmlSingletonSerializer<T>();
                    }
                }

                return _instance;
            }
        }

        public T Deserialize(string xml, Encoding encoding = null) {
            if (string.IsNullOrEmpty(xml))
                return default;

            if (encoding != null) {
                byte[] bytes = Encoding.Default.GetBytes(xml);
                xml = encoding.GetString(bytes);
            }

            using (var textReader = new StringReader(xml)) {
                using (var xmlReader = XmlReader.Create(textReader, _readerSettings)) {
                    return (T) _serializer.Deserialize(xmlReader);
                }
            }
        }

        public string Serialize(T value, bool useUtf8 = true) {
            if (value == null)
                return null;

            using StringWriter textWriter = useUtf8 ? new Utf8StringWriter() : new StringWriter();
            using var xmlWriter = XmlWriter.Create(textWriter, _writerSettings);
            _serializer.Serialize(xmlWriter, value, _serializerNamespaces);

            return textWriter.ToString();
        }
    }

    public sealed class Utf8StringWriter : StringWriter {
        public override Encoding Encoding => Encoding.UTF8;
    }
}