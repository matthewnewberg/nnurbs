#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace NN.DocObjects
{
    [Serializable]
    public class Linetype 
    {
        #region members
        // Represents both a CRhinoLinetype and an ON_Linetype. When m_ptr is
        // null, the object uses m_doc and m_id to look up the const
        // CRhinoLinetype in the linetype table.

        Guid m_id = Guid.Empty;
        #endregion

        #region constructors
        public Linetype()
        {
        }

        internal Linetype(Guid id)
        {
            m_id = id;
        }
        #endregion


        #region properties
        /// <summary>The name of this linetype.</summary>
        public string Name;


        /// <summary>The index of this linetype.</summary>
        public int LinetypeIndex;

        /// <summary>Total length of one repeat of the pattern.</summary>
        public double PatternLength;

        /// <summary>Number of segments in the pattern.</summary>
        public int SegmentCount;
        

        /// <summary>
        /// Gets the ID of this linetype object.
        /// </summary>
        public Guid Id { get { return m_id; } set { m_id = value; } }

        /// <summary>
        /// Gets a value indicating whether this linetype has been deleted and is 
        /// currently in the Undo buffer.
        /// </summary>
        public bool IsDeleted;
        /// <summary>
        /// Gets a value indicting whether this linetype is a referenced linetype. 
        /// Referenced linetypes are part of referenced documents.
        /// </summary>
        public bool IsReference;
        /// <summary>
        /// true if this linetype has been modified by LinetypeTable.ModifyLinetype()
        /// and the modifications can be undone.
        /// </summary>
        public bool IsModified;

        int GetInt(int which)
        {
            return 0;
        }
        void SetInt(int which, int val)
        {

        }
        #endregion



        /// <summary>Adds a segment to the pattern.</summary>
        /// <param name="length">The length of the segment to be added.</param>
        /// <param name="isSolid">
        /// If true, the length is interpreted as a line. If false,
        /// then the length is interpreted as a space.
        /// </param>
        /// <returns>Index of the added segment.</returns>
        public int AppendSegment(double length, bool isSolid)
        {
            return 0;   
        }

        /// <summary>Removes a segment in the linetype.</summary>
        /// <param name="index">Zero based index of the segment to remove.</param>
        /// <returns>true if the segment index was removed.</returns>
        public bool RemoveSegment(int index)
        {
            return false;
        }

        /// <summary>Sets the length and type of the segment at index.</summary>
        /// <param name="index">Zero based index of the segment.</param>
        /// <param name="length">The length of the segment to be added in millimeters.</param>
        /// <param name="isSolid">
        /// If true, the length is interpreted as a line. If false,
        /// then the length is interpreted as a space.
        /// </param>
        /// <returns>true if the operation was successful; otherwise false.</returns>
        public bool SetSegment(int index, double length, bool isSolid)
        {
            return false;
        }

        /// <summary>
        /// Gets the segment information at a index.
        /// </summary>
        /// <param name="index">Zero based index of the segment.</param>
        /// <param name="length">The length of the segment in millimeters.</param>
        /// <param name="isSolid">
        /// If the length is interpreted as a line, true is assigned during the call to this out parameter.
        /// <para>If the length is interpreted as a space, then false is assigned during the call to this out parameter.</para>
        /// </param>
        /// <exception cref="IndexOutOfRangeException">If the index is unacceptable.</exception>
        public void GetSegment(int index, out double length, out bool isSolid)
        {
            length = 0;
            isSolid = false;
            return;
        }
    }
}