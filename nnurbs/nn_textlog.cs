using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace NN.FileIO
{
    /// <summary>
    /// Used for collecting text data
    /// </summary>
    public class TextLog
    {
        string m_pTextLog = "";
        StringBuilder m_pString = new StringBuilder();
        string m_indent = "";

        /// <summary>
        /// Creates a text log that stores all text in memory.  Use ToString on this
        /// version of the TextLog to get the text that we written
        /// </summary>
        public TextLog()
        {

        }

        /// <summary>
        /// Creates a text log that writes all text to a file. If no filename is
        /// provided, then text is written to StdOut
        /// </summary>
        /// <param name="filename">
        /// Name of file to create and write to. If null, then text output
        /// is sent to StdOut
        /// </param>
        public TextLog(string filename)
        {
            m_pTextLog = filename;
        }

        /// <summary>
        /// If the TextLog was constructed using the empty constructor, then the text
        /// information is stored in a runtime string.  The contents of this string
        /// is retrieved using ToString for this case
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (null != m_pString)
            {
                m_pString.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Increase the indentation level
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_printinstancedefinitiontree.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_printinstancedefinitiontree.cs' lang='cs'/>
        /// <code source='examples\py\ex_printinstancedefinitiontree.py' lang='py'/>
        /// </example>
        public void PushIndent()
        {
            m_indent += "\t";
        }

        /// <summary>
        /// Decrease the indentation level
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_printinstancedefinitiontree.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_printinstancedefinitiontree.cs' lang='cs'/>
        /// <code source='examples\py\ex_printinstancedefinitiontree.py' lang='py'/>
        /// </example>
        public void PopIndent()
        {
            int length = m_indent.Length;
            if (length >= 1)
            {
                //m_indent = indent.S(length - 1);
                m_indent = m_indent.Substring(0, length - 1);
            }
            else
            {
                m_indent = "";
            }
        }

        /// <summary>
        /// 0: one tab per indent. &gt;0: number of spaces per indent
        /// </summary>
        public int IndentSize { get; set; }
        

        /// <summary>
        /// Send text wrapped at a set line length
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lineLength"></param>
        public void PrintWrappedText(string text, int lineLength)
        {
            // TODO
            m_pString.Append(text);
        }

        /// <summary>
        /// Send text to the textlog
        /// </summary>
        /// <param name="text"></param>
        /// <example>
        /// <code source='examples\vbnet\ex_printinstancedefinitiontree.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_printinstancedefinitiontree.cs' lang='cs'/>
        /// <code source='examples\py\ex_printinstancedefinitiontree.py' lang='py'/>
        /// </example>
        public void Print(string text)
        {
            m_pString.Append(text);
        }

        /// <summary>
        /// Send formatted text to the textlog
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        public void Print(string format, object arg0)
        {
            Print(string.Format(format, arg0));
        }
        /// <summary>
        /// Send formatted text to the textlog
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        public void Print(string format, object arg0, object arg1)
        {
            Print(string.Format(format, arg0, arg1));
        }
    }
}