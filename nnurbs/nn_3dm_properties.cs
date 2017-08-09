using System;
using NN.Runtime.InteropWrappers;

namespace NN.FileIO
{
    // skip for now, these are typically only used for OpenNURBS File I/O
    //  public class File3dmRevisionHistory { }
    //  public class File3dmApplication { }
    //  public class File3dmProperties { }

    /// <summary>
    /// Represents the notes information stored in a 3dm file.
    /// </summary>
    public class File3dmNotes
    {
        File3dm m_parent;
        string m_notes;
        bool m_visible;
        bool m_html;
        System.Drawing.Rectangle m_winrect;

        /// <summary>
        /// Creates empty default notes
        /// </summary>
        public File3dmNotes() { }

        internal File3dmNotes(File3dm parent)
        {
            m_parent = parent;
        }

        internal void SetParent(File3dm parent)
        {
            if (m_parent != parent)
            {
                m_notes = Notes;
                m_visible = IsVisible;
                m_html = IsHtml;
                m_winrect = WindowRectangle;

                m_parent = parent;
                if (parent != null)
                {
                    Notes = m_notes;
                    IsVisible = m_visible;
                    IsHtml = m_html;
                    WindowRectangle = m_winrect;
                }
            }
        }

        /// <summary>
        /// Gets or sets the text content of the notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the notes visibility. If the notes are visible, true; false otherwise.
        /// </summary>
        public bool IsVisible { get; set; }
        

        /// <summary>
        /// Gets or sets the text format. If the format is HTML, true; false otherwise.
        /// </summary>
        public bool IsHtml { get; set; }
        

        /// <summary>
        /// Gets or sets the position of the Notes when they were saved.
        /// </summary>
        public System.Drawing.Rectangle WindowRectangle { get; set; }
    }
}