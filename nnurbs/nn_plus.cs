using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NN.Runtime.InteropWrappers;

namespace NN.Runtime.InteropWrappers
{
  /// <summary>
  /// This is only needed when passing values to the Rhino C++ core, ignore
  /// for .NET plug-ins.
  /// </summary>
  
  [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 88)]
  public struct MeshPointDataStruct
  {
    /// <summary>
    /// This is only needed when passing values to the Rhino C++ core, ignore
    /// for .NET plug-ins.
    /// </summary>
    public double m_et;

    //ON_COMPONENT_INDEX m_ci;
    /// <summary>
    /// This is only needed when passing values to the Rhino C++ core, ignore
    /// for .NET plug-ins.
    /// </summary>
    public uint m_ci_type;
    /// <summary>
    /// This is only needed when passing values to the Rhino C++ core, ignore
    /// for .NET plug-ins.
    /// </summary>
    public int m_ci_index;

    /// <summary>
    /// This is only needed when passing values to the Rhino C++ core, ignore
    /// for .NET plug-ins.
    /// </summary>
    public int m_edge_index;
    /// <summary>
    /// This is only needed when passing values to the Rhino C++ core, ignore
    /// for .NET plug-ins.
    /// </summary>
    public int m_face_index;
    /// <summary>
    /// This is only needed when passing values to the Rhino C++ core, ignore
    /// for .NET plug-ins.
    /// </summary>
    public char m_Triangle;
    /// <summary>
    /// This is only needed when passing values to the Rhino C++ core, ignore
    /// for .NET plug-ins.
    /// </summary>
    public double m_t0;
    /// <summary>
    /// This is only needed when passing values to the Rhino C++ core, ignore
    /// for .NET plug-ins.
    /// </summary>
    public double m_t1;
    /// <summary>
    /// This is only needed when passing values to the Rhino C++ core, ignore
    /// for .NET plug-ins.
    /// </summary>
    public double m_t2;
    /// <summary>
    /// This is only needed when passing values to the Rhino C++ core, ignore
    /// for .NET plug-ins.
    /// </summary>
    public double m_t3;

    //ON_3dPoint m_P;
    /// <summary>
    /// This is only needed when passing values to the Rhino C++ core, ignore
    /// for .NET plug-ins.
    /// </summary>
    public double m_Px;
    /// <summary>
    /// This is only needed when passing values to the Rhino C++ core, ignore
    /// for .NET plug-ins.
    /// </summary>
    public double m_Py;
    /// <summary>
    /// This is only needed when passing values to the Rhino C++ core, ignore
    /// for .NET plug-ins.
    /// </summary>
    public double m_Pz;
  }
}

namespace NN.Geometry
{
  /// <summary>
  /// Represents a point that is found on a mesh.
  /// </summary>
  public class MeshPoint
  {
    internal Mesh m_parent;
    internal MeshPointDataStruct m_data;
    internal MeshPoint(Mesh parent, MeshPointDataStruct ds)
    {
      m_parent = parent;
      m_data = ds;
    }

    /// <summary>
    /// The mesh that is ralated to this point.
    /// </summary>
    public Mesh Mesh
    {
      get { return m_parent; }
    }

    /// <summary>
    /// Edge parameter when found.
    /// </summary>
    public double EdgeParameter
    {
      get { return m_data.m_et; }
    }

    /// <summary>
    /// Gets the component index of the intersecting element in the mesh.
    /// </summary>
    public ComponentIndex ComponentIndex
    {
      get
      {
        return new ComponentIndex((ComponentIndexType)m_data.m_ci_type, m_data.m_ci_index);
      }
    }

    /// <summary>
    /// When set, EdgeIndex is an index of an edge in the mesh's edge list.
    /// </summary>
    public int EdgeIndex
    {
      get { return m_data.m_edge_index; }
    }

    /// <summary>
    /// FaceIndex is an index of a face in mesh.Faces.
    /// When ComponentIndex refers to a vertex, any face that uses the vertex
    /// may appear as FaceIndex.  When ComponenctIndex refers to an Edge or
    /// EdgeIndex is set, then any face that uses that edge may appear as FaceIndex.
    /// </summary>
    public int FaceIndex
    {
      get { return m_data.m_face_index; }
    }

    //bool IsValid( ON_TextLog* text_log ) const;


    /// <summary>
    /// Face triangle where the intersection takes place:
    /// <para>0 is unset</para>
    /// <para>A is 0,1,2</para>
    /// <para>B is 0,2,3</para>
    /// <para>C is 0,1,3</para>
    /// <para>D is 1,2,3</para>
    /// </summary>
    public char Triangle
    {
      get { return m_data.m_Triangle; }
    }


    /// <summary>
    /// Barycentric quad coordinates for the point on the mesh
    /// face mesh.Faces[FaceIndex].  If the face is a triangle
    /// disregard T[3] (it should be set to 0.0). If the face is
    /// a quad and is split between vertexes 0 and 2, then T[3]
    /// will be 0.0 when point is on the triangle defined by vi[0],
    /// vi[1], vi[2] and T[1] will be 0.0 when point is on the
    /// triangle defined by vi[0], vi[2], vi[3]. If the face is a
    /// quad and is split between vertexes 1 and 3, then T[2] will
    /// be -1 when point is on the triangle defined by vi[0],
    /// vi[1], vi[3] and m_t[0] will be -1 when point is on the
    /// triangle defined by vi[1], vi[2], vi[3].
    /// </summary>
    public double[] T
    {
      get { return m_t ?? (m_t = new double[] { m_data.m_t0, m_data.m_t1, m_data.m_t2, m_data.m_t3 }); }
    }
    double[] m_t;

    /// <summary>
    /// Gets the location (position) of this point.
    /// </summary>
    public Point3d Point
    {
      get { return new Point3d(m_data.m_Px, m_data.m_Py, m_data.m_Pz); }
    }
  }
}

namespace NN.Geometry.Intersect
{
  
}
