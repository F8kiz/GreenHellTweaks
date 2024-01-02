using System.Xml.Serialization;
using UnityEngine;

namespace GHTweaks.Models
{
    public class KeyBinding
    {
        [XmlAttribute]
        public KeyCode Key { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
