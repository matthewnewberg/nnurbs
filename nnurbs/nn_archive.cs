#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using NN.Runtime.InteropWrappers;

namespace NN.Collections
{
  /// <summary>
  /// <para>Represents a dictionary class that can be attached to objects and
  /// can be serialized (saved) at necessity.</para>
  /// <para>See remarks for layout.</para>
  /// </summary>
  /// <remarks>
  /// <para>This is the layout of this object:</para>
  /// <para>.</para>
  /// <para>BEGINCHUNK (TCODE_ANONYMOUS_CHUNK)</para>
  /// <para>|- version (int)</para>
  /// <para>|- entry count (int)</para>
  /// <para>   for entry count entries</para>
  /// <para>   |- BEGINCHUNK (TCODE_ANONYMOUS_CHUNK)</para>
  /// <para>   |- key (string)</para>
  /// <para>   |- entry contents</para>
  /// <para>   |- ENDCHUNK (TCODE_ANONYMOUS_CHUNK)</para>
  /// <para>ENDCHUNK (TCODE_ANONYMOUS_CHUNK)</para>
  /// </remarks>
  public class ArchivableDictionary
  {
    private enum ItemType : int
    {
      // values <= 0 are considered bogus
      // each supported object type has an associated ItemType enum value
      // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      // NEVER EVER Change ItemType values as this will break I/O code
      // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      Undefined = 0,
      // some basic types
      Bool = 1, // bool
      Byte = 2, // unsigned char
      SByte = 3, // char
      Short = 4, // short
      UShort = 5, // unsigned short
      Int32 = 6, // int
      UInt32 = 7, // unsigned int
      Int64 = 8, // time_t
      Single = 9, // float
      Double = 10, // double
      Guid = 11,
      String = 12,

      // array of basic .NET data types
      ArrayBool = 13,
      ArrayByte = 14,
      ArraySByte = 15,
      ArrayShort = 16,
      ArrayInt32 = 17,
      ArraySingle = 18,
      ArrayDouble = 19,
      ArrayGuid = 20,
      ArrayString = 21,

      // System::Drawing structs
      Color = 22,
      Point = 23,
      PointF = 24,
      Rectangle = 25,
      RectangleF = 26,
      Size = 27,
      SizeF = 28,
      Font = 29,

      // RMA::OpenNURBS::ValueTypes structs
      Interval = 30,
      Point2d = 31,
      Point3d = 32,
      Point4d = 33,
      Vector2d = 34,
      Vector3d = 35,
      BoundingBox = 36,
      Ray3d = 37,
      PlaneEquation = 38,
      Xform = 39,
      Plane = 40,
      Line = 41,
      Point3f = 42,
      Vector3f = 43,

      // RMA::OpenNURBS classes
      OnBinaryArchiveDictionary = 44,
      OnObject = 45, // don't use this anymore
      OnMeshParameters = 46,
      OnGeometry = 47,
      OnObjRef = 48,
      ArrayObjRef = 49,
      MAXVALUE = 49
    }

    int m_version;
    string m_name;
    //private Dictionary<string, DictionaryItem> m_items = new Dictionary<string, DictionaryItem>();
    DocObjects.Custom.UserData m_parent_userdata;

    /// <summary>
    /// Gets or sets the version of this <see cref="ArchivableDictionary"/>.
    /// </summary>
    public int Version
    {
      get { return m_version; }
      set { m_version = value; }
    }

    /// <summary>
    /// Gets or sets the name string of this <see cref="ArchivableDictionary"/>.
    /// </summary>
    public string Name
    {
      get { return m_name; }
      set { m_name = value; }
    }

    // I don't think this needs to be public
    static Guid RhinoDotNetDictionaryId
    {
      get
      {
        // matches id used by old NN.NET
        return new Guid("21EE7933-1E2D-4047-869E-6BDBF986EA11");
      }
    }

    /// <summary>Initializes an instance of a dictionary for writing to a 3dm archive.</summary>
    public ArchivableDictionary()
    {
      m_version = 0;
      m_name = String.Empty;
    }

    /// <summary>Initializes an instance of a dictionary for writing to a 3dm archive</summary>
    /// <param name="parentUserData">
    /// parent user data if this dictionary is associated with user data
    /// </param>
    public ArchivableDictionary(DocObjects.Custom.UserData parentUserData)
    {
      m_parent_userdata = parentUserData;
      m_version = 0;
      m_name = String.Empty;
    }

    /// <summary>Initializes an instance of a dictionary for writing to a 3dm archive.</summary>
    /// <param name="version">
    /// Custom version used to help the plug-in developer determine which version of
    /// a dictionary is being written. One good way to write version information is to
    /// use a date style integer (YYYYMMDD)
    /// </param>
    public ArchivableDictionary(int version)
    {
      m_version = version;
      m_name = String.Empty;
    }

    ///<summary>Initializes an instance of a dictionary for writing to a 3dm archive.</summary>
    ///<param name="version">
    /// custom version used to help the plug-in developer determine which version of
    /// a dictionary is being written. One good way to write version information is to
    /// use a date style integer (YYYYMMDD)
    ///</param>
    ///<param name="name">
    /// Optional name to associate with this dictionary.
    /// NOTE: if this dictionary is set as a subdictionary, the name will be changed to
    /// the subdictionary key entry
    ///</param>
    public ArchivableDictionary(int version, string name)
    {
      m_version = version;
      m_name = String.IsNullOrEmpty(name) ? String.Empty : name;
    }

    /// <summary>
    /// If this dictionary is part of userdata (or is a UserDictionary), then
    /// this is the parent user data. null if this dictionary is not part of
    /// userdata
    /// </summary>
    public DocObjects.Custom.UserData ParentUserData
    {
      get { return m_parent_userdata; }
    }

  }
}
