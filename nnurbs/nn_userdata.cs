using System;

namespace NN.DocObjects.Custom
{
    /// <summary>
    /// Provides a base class for custom classes of information which may be attached to
    /// geometry or attribute classes.
    /// </summary>
    public abstract class UserData
    {
        //static int g_next_serial_number = 1;
        //int m_serial_number = -1;


        /// <summary>Descriptive name of the user data.</summary>
        public virtual string Description { get { return "User Data"; } set { } }

        /// <summary>
        /// If you want to save this user data in a 3dm file, override
        /// ShouldWrite and return true.  If you do support serialization,
        /// you must also override the Read and Write functions.
        /// </summary>
        public virtual bool ShouldWrite { get { return false; } }

        /// override OnTransform() and want Transform to be updated, then call the 
        /// base class OnTransform() in your override.
        /// The default constructor sets Transform to the identity.
        /// </summary>
        public Geometry.Transform Transform { get; set; }
    }

    /// <summary>
    /// Represents user data with unknown origin.
    /// </summary>
    public class UnknownUserData : UserData
    {
        /// <summary>
        /// Constructs a new unknown data entity.
        /// </summary>
        public UnknownUserData()
        {
        }
    }

    /// <summary>Represents a collection of user data.</summary>
    public class UserDataList : System.Collections.Generic.List<UserData>
    {
        public UserDataList()
        {
        }

        public UserData Find(System.Type type)
        {
            foreach (var u in this)
            {
                if (u.GetType() == type)
                    return u;
            }

            return null;
        }
    }

    /// <summary>
    /// Defines the storage data class for a <see cref="NN.Collections.ArchivableDictionary">user dictionary</see>.
    /// </summary>
    [System.Runtime.InteropServices.Guid("171E831F-7FEF-40E2-9857-E5CCD39446F0")]
    public class UserDictionary : UserData
    {
        Collections.ArchivableDictionary m_dictionary;
        /// <summary>
        /// Gets the dictionary that is associated with this class.
        /// <para>This dictionary is unique.</para>
        /// </summary>
        public Collections.ArchivableDictionary Dictionary
        {
            get { return m_dictionary ?? (m_dictionary = new Collections.ArchivableDictionary(this)); }
        }

        /// <summary>
        /// Gets the text "RhinoCommon UserDictionary".
        /// </summary>
        public override string Description
        {
            get
            {
                return "RhinoCommon UserDictionary";
            }
        }

        /// <summary>
        /// Writes this entity if the count is larger than 0.
        /// </summary>
        public override bool ShouldWrite
        {
            get { return false; }
        }
    }

    [System.Runtime.InteropServices.Guid("2544A64E-220D-4D65-B8D4-611BB57B46C7")]
    class SharedUserDictionary : UserDictionary
    {
    }
}