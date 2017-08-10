using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using NN.Runtime;
using NN.Runtime.InteropWrappers;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;

namespace NN
{
    [Serializable]
    public struct ColorEx
    {
        private Color m_color;

        public ColorEx(Color color)
        {
            m_color = color;
        }

        [XmlIgnore]
        public Color Color
        { get { return m_color; } set { m_color = value; } }

        [XmlAttribute]
        public string ColorHtml
        {
            get { return ColorTranslator.ToHtml(this.Color); }
            set { this.Color = ColorTranslator.FromHtml(value); }
        }

        public static implicit operator Color(ColorEx colorEx)
        {
            return colorEx.Color;
        }

        public static implicit operator ColorEx(Color color)
        {
            return new ColorEx(color);
        }
    }
}