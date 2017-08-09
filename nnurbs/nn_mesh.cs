using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using NN.Collections;
using System.Runtime.Serialization;
using NN.Geometry;
using NN.Runtime.InteropWrappers;
using NN.Render;

namespace NN.Render
{
    /// <summary>
    /// Used for cached texture coordinates
    /// </summary>
    public class CachedTextureCoordinates : System.Collections.Generic.List<Point3d>
    {
        /// <summary>
        /// Coordinate dimension: 2 = UV, 3 = UVW
        /// </summary>
        public int Dim { get; set; }

        /// <summary>
        /// The texture mapping Id.
        /// </summary>
        public Guid MappingId { get; set; }

        /// <summary>
        /// This collection is always read-only
        /// </summary>
        public bool IsReadOnly { get { return true; } }
    }

    /// <summary>
    /// Internal class used to enumerate a list of CachedTextureCoordinates
    /// </summary>
    class CachedTextureCoordinatesEnumerator : System.Collections.Generic.List<Point3d>
    {
        public CachedTextureCoordinatesEnumerator()
        {
        }
    }


    /// <summary>
    /// Holds texture mapping information.
    /// </summary>
    public class MappingTag
    {
        /// <summary>
        ///  Gets or sets a map globally unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  Gets or sets a texture mapping type: linear, cylinder, etc...
        /// </summary>
        public TextureMappingType MappingType { get; set; }

        /// <summary>
        /// Gets or sets the cyclic redundancy check on the mapping.
        /// See also <see cref="RhinoMath.CRC32(uint,byte[])" />.
        /// </summary>

        public uint MappingCRC { get; set; }

        /// <summary>
        ///  Gets or sets a 4x4 matrix tranform.
        /// </summary>
        public NN.Geometry.Transform MeshTransform { get; set; }
    }
}

namespace NN.Geometry
{
    /// <summary>
    /// Type of Mesh Parameters used by the RhinoDoc for meshing objects
    /// </summary>
    public enum MeshingParameterStyle : int
    {
        /// <summary>No style</summary>
        None = 0,
        /// <summary></summary>
        Fast = 1,
        /// <summary></summary>
        Quality = 2,
        /// <summary></summary>
        Custom = 9,
        /// <summary></summary>
        PerObject = 10
    }

    /// <summary>
    /// Represents settings used for creating a mesh representation of a brep or surface.
    /// </summary>
    public class MeshingParameters
    {
        /// <summary>
        /// Initializes a new instance with default values.
        /// <para>Initial values are same as <see cref="Default"/>.</para>
        /// </summary>
        public MeshingParameters()
        {

        }

        /// <summary>Gets minimal meshing parameters.</summary>
        /// <example>
        /// <code source='examples\vbnet\ex_createmeshfrombrep.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_createmeshfrombrep.cs' lang='cs'/>
        /// <code source='examples\py\ex_createmeshfrombrep.py' lang='py'/>
        /// </example>
        public static MeshingParameters Minimal
        {
            get
            {
                MeshingParameters mp = new MeshingParameters();
                mp.JaggedSeams = true;
                mp.RefineGrid = false;
                mp.SimplePlanes = false;
                mp.ComputeCurvature = false;

                //mp.Facetype = 0;
                mp.GridMinCount = 16;
                mp.GridMaxCount = 0;

                mp.GridAmplification = 1.0;
                mp.GridAngle = 0.0;
                mp.GridAspectRatio = 6.0;

                mp.Tolerance = 0.0;
                mp.MinimumTolerance = 0.0;

                mp.MinimumEdgeLength = 0.0001;
                mp.MaximumEdgeLength = 0.0;

                mp.RefineAngle = 0.0;
                mp.RelativeTolerance = 0.0;

                return mp;
            }
        }

        /// <summary>
        /// Get default meshing parameters.
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_createmeshfrombrep.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_createmeshfrombrep.cs' lang='cs'/>
        /// <code source='examples\py\ex_createmeshfrombrep.py' lang='py'/>
        /// </example>
        public static MeshingParameters Default
        {
            get
            {
                MeshingParameters mp = new MeshingParameters();
                /*
                mp.JaggedSeams = false;
                mp.RefineGrid = true;
                mp.SimplePlanes = false;
                mp.ComputeCurvature = false;

                mp.Facetype = 0;
                mp.GridMinCount = 0;
                mp.GridMaxCount = 0;

                mp.GridAmplification = 1.0;
                mp.GridAngle = (20.0 * Math.PI) / 180.0;
                mp.GridAspectRatio = 6.0;

                mp.Tolerance = 0.0;
                mp.MinimumTolerance = 0.0;

                mp.MinimumEdgeLength = 0.0001;
                mp.MaximumEdgeLength = 0.0;

                mp.RefineAngle = (20.0 * Math.PI) / 180.0;
                mp.RelativeTolerance = 0.0;
                */
                return mp;
            }
        }

        /// <summary>
        /// Gets meshing parameters for coarse meshing. 
        /// <para>This corresponds with the "Jagged and Faster" default in NN.</para>
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_createmeshfrombrep.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_createmeshfrombrep.cs' lang='cs'/>
        /// <code source='examples\py\ex_createmeshfrombrep.py' lang='py'/>
        /// </example>
        public static MeshingParameters Coarse
        {
            get
            {
                MeshingParameters mp = new MeshingParameters();

                mp.GridAmplification = 0.0;
                mp.GridAngle = 0.0;
                mp.GridAspectRatio = 0.0;
                mp.RefineAngle = 0.0;

                mp.RelativeTolerance = 0.65;
                mp.GridMinCount = 16;
                mp.MinimumEdgeLength = 0.0001;
                mp.SimplePlanes = true;

                return mp;
            }
        }
        /// <summary>
        /// Gets meshing parameters for smooth meshing. 
        /// <para>This corresponds with the "Smooth and Slower" default in NN.</para>
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_createmeshfrombrep.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_createmeshfrombrep.cs' lang='cs'/>
        /// <code source='examples\py\ex_createmeshfrombrep.py' lang='py'/>
        /// </example>
        public static MeshingParameters Smooth
        {
            get
            {
                MeshingParameters mp = new MeshingParameters();

                mp.GridAmplification = 0.0;
                mp.GridAngle = 0.0;
                mp.GridAspectRatio = 0.0;

                mp.RelativeTolerance = 0.8;
                mp.GridMinCount = 16;
                mp.MinimumEdgeLength = 0.0001;
                mp.SimplePlanes = true;
                mp.RefineAngle = (20.0 * Math.PI) / 180.0;

                return mp;
            }
        }

        /// <summary>
        /// Gets or sets whether or not the mesh is allowed to have jagged seams. 
        /// When this flag is set to true, meshes on either side of a Brep Edge will not match up.
        /// </summary>
        public bool JaggedSeams { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the sampling grid can be refined 
        /// when certain tolerances are not met.
        /// </summary>
        public bool RefineGrid { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether or not planar areas are allowed 
        /// to be meshed in a simplified manner.
        /// </summary>
        public bool SimplePlanes { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether or not surface curvature 
        /// data will be embedded in the mesh.
        /// </summary>
        public bool ComputeCurvature { get; set; }

        /// <summary>
        /// Gets or sets the minimum number of grid quads in the initial sampling grid.
        /// </summary>
        public int GridMinCount { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of grid quads in the initial sampling grid.
        /// </summary>
        public int GridMaxCount { get; set; }


        /// <summary>
        /// Gets or sets the maximum allowed angle difference (in radians) 
        /// for a single sampling quad. The angle pertains to the surface normals.
        /// </summary>
        public double GridAngle { get; set; }

        /// <summary>
        /// Gets or sets the maximum allowed aspect ratio of sampling quads.
        /// </summary>
        public double GridAspectRatio { get; set; }

        /// <summary>
        /// Gets or sets the grid amplification factor. 
        /// Values lower than 1.0 will decrease the number of initial quads, 
        /// values higher than 1.0 will increase the number of initial quads.
        /// </summary>
        public double GridAmplification { get; set; }

        /// <summary>
        /// Gets or sets the maximum allowed edge deviation. 
        /// This tolerance is measured between the center of the mesh edge and the surface.
        /// </summary>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets or sets the minimum tolerance.
        /// </summary>
        public double MinimumTolerance { get; set; }

        /// <summary>
        /// Gets or sets the relative tolerance.
        /// </summary>
        public double RelativeTolerance { get; set; }

        /// <summary>
        /// Gets or sets the minimum allowed mesh edge length.
        /// </summary>
        public double MinimumEdgeLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum allowed mesh edge length.
        /// </summary>
        public double MaximumEdgeLength { get; set; }

        /// <summary>
        /// Gets or sets the mesh parameter refine angle.
        /// </summary>
        public double RefineAngle { get; set; }

    }

    /// <summary>
    /// Represents a portion of a mesh for partitioning
    /// </summary>
    /*
    public class MeshPart
    {
        private int m_vi0;
        private int m_vi1;
        private int m_fi0;
        private int m_fi1;
        private int m_vertex_count;
        private int m_triangle_count;

        internal MeshPart(int vertexStart, int vertexEnd, int faceStart, int faceEnd, int vertexCount, int triangleCount)
        {
            m_vi0 = vertexStart;
            m_vi1 = vertexEnd;
            m_fi0 = faceStart;
            m_fi1 = faceEnd;
            m_vertex_count = vertexCount;
            m_triangle_count = triangleCount;
        }

        /// <summary>Start of subinterval of parent mesh vertex array</summary>
        public int StartVertexIndex { get { return m_vi0; } }
        /// <summary>End of subinterval of parent mesh vertex array</summary>
        public int EndVertexIndex { get { return m_vi1; } }
        /// <summary>Start of subinterval of parent mesh face array</summary>
        public int StartFaceIndex { get { return m_fi0; } }
        /// <summary>End of subinterval of parent mesh face array</summary>
        public int EndFaceIndex { get { return m_fi1; } }

        /// <summary>EndVertexIndex - StartVertexIndex</summary>
        public int VertexCount { get { return m_vertex_count; } }
        /// <summary></summary>
        public int TriangleCount { get { return m_triangle_count; } }
    } */

    /// <summary>
    /// Represents a geometry type that is defined by vertices and faces.
    /// <para>This is often called a face-vertex mesh.</para>
    /// </summary>
    [Serializable]
    public class Mesh : GeometryBase
    {
        public Mesh()
        {

        }


#if RHINO3DMIO
        public Mesh(Rhino.Geometry.Mesh f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.Mesh from)
        {

            if (from == null)
                return false;

            this.DisjointMeshCount = from.DisjointMeshCount;
            this.IsClosed = from.IsClosed;
            this.Vertices = new NN.Geometry.Collections.MeshVertexList(from.Vertices);
            this.TopologyVertices = new NN.Geometry.Collections.MeshTopologyVertexList(from.TopologyVertices);
            this.TopologyEdges = new NN.Geometry.Collections.MeshTopologyEdgeList(from.TopologyEdges);
            this.Normals = new NN.Geometry.Collections.MeshVertexNormalList(from.Normals);
            this.Faces = new NN.Geometry.Collections.MeshFaceList(from.Faces);
            this.FaceNormals = new NN.Geometry.Collections.MeshFaceNormalList(from.FaceNormals);
            this.VertexColors = new NN.Geometry.Collections.MeshVertexColorList(from.VertexColors);
            this.TextureCoordinates = new NN.Geometry.Collections.MeshTextureCoordinateList(from.TextureCoordinates);
            this.PartitionCount = from.PartitionCount;
            this.ObjectType = (NN.DocObjects.ObjectType)from.ObjectType;

            return true;
        }


        public bool CopyTo(Rhino.Geometry.Mesh to)
        {
            to.Vertices.Clear();

            foreach (var v in this.Vertices)
                to.Vertices.Add(v.RhinoObject());

            to.Normals.Clear();

            foreach (var n in Normals)
                to.Normals.Add(n.RhinoObject());

            to.Faces.Clear();

            foreach (var f in Faces)
                to.Faces.AddFace(f.RhinoObject());

            to.FaceNormals.Clear();

            foreach (var f in FaceNormals)
                to.FaceNormals.AddFaceNormal(f.RhinoObject());

            to.VertexColors.Clear();

            foreach (var c in VertexColors)
                to.VertexColors.Add(c);

            to.TextureCoordinates.Clear();

            foreach (var tc in TextureCoordinates)
                to.TextureCoordinates.Add(tc.X, tc.Y);

            return true;
        }

        public Rhino.Geometry.Mesh RhinoObject()
        {
            var rhinoMesh = new Rhino.Geometry.Mesh();

            CopyTo(rhinoMesh);

            return rhinoMesh;
        }

#endif

        /// <summary>
        /// Gets the number of disjoint (topologically unconnected) pieces in this mesh.
        /// </summary>
        public int DisjointMeshCount { get; set; }


        /// <summary>
        /// Gets a value indicating whether a mesh is considered to be closed (solid).
        /// A mesh is considered solid when every mesh edge borders two or more faces.
        /// </summary>
        /// <returns>true if the mesh is closed, false if it is not.</returns>
        public bool IsClosed { get; set; }


        public NN.Geometry.Collections.MeshVertexList Vertices { get; set; }


        public NN.Geometry.Collections.MeshTopologyVertexList TopologyVertices { get; set; }

        public NN.Geometry.Collections.MeshTopologyEdgeList TopologyEdges { get; set; }

        public NN.Geometry.Collections.MeshVertexNormalList Normals { get; set; }


        public NN.Geometry.Collections.MeshFaceList Faces { get; set; }

        public NN.Geometry.Collections.MeshFaceNormalList FaceNormals { get; set; }

        public NN.Geometry.Collections.MeshVertexColorList VertexColors { get; set; }


        public NN.Geometry.Collections.MeshTextureCoordinateList TextureCoordinates { get; set; }


        /// <summary>
        /// Determines orientation of a "solid" mesh.
        /// </summary>
        /// <returns>
        /// <para>+1 = mesh is solid with outward facing normals.</para>
        /// <para>-1 = mesh is solid with inward facing normals.</para>
        /// <para>0 = mesh is not solid.</para>
        /// </returns>
        public int SolidOrientation { get; set; }


        /// <summary>
        /// Number of partition information chunks stored on this mesh based
        /// on the last call to CreatePartitions
        /// </summary>
        public int PartitionCount { set; get; }

        public Point3f GetClosestVertex(Point3f p)
        {
            

            Point3f cloesestVertex = Geometry.Point3f.Unset;
            float distance = float.MaxValue;

            if (Vertices == null)
                return cloesestVertex;

            foreach (var v in Vertices)
            {
                float d = (float)v.DistanceTo(p);

                if (d < distance) {
                    distance = d;
                    cloesestVertex = v;
                }
            }

            return cloesestVertex;
        }

    }
}

namespace NN.Geometry.Collections
{
    /// <summary>
    /// Provides access to the vertices and vertex-related functionality of a mesh.
    /// </summary>
    ///
    public class MeshVertexList : System.Collections.Generic.List<Point3f>, NN.Collections.IRhinoTable<Point3f>
    {
        public MeshVertexList()
        {
        }

#if RHINO3DMIO
        public MeshVertexList(Rhino.Geometry.Collections.MeshVertexList rhinoVerts)
        {
            this.Clear();

            foreach (var v in rhinoVerts)
            {
                this.Add(new Geometry.Point3f(v));
            }
        }
#endif
    }

    /// <summary>
    /// Provides access to the mesh topology vertices of a mesh. Topology vertices are
    /// sets of vertices in the MeshVertexList that can topologically be considered the
    /// same vertex.
    /// </summary>
    public class MeshTopologyVertexList : System.Collections.Generic.List<Point3f>, NN.Collections.IRhinoTable<Point3f>
    {
        public MeshTopologyVertexList()
        {
        }

#if RHINO3DMIO
        public MeshTopologyVertexList(Rhino.Geometry.Collections.MeshTopologyVertexList rhinoVerts)
        {
            this.Clear();

            foreach (var v in rhinoVerts)
            {
                this.Add(new Geometry.Point3f(v));
            }
        }
#endif

    }

    /// <summary>
    /// Represents an entry point to the list of edges in a mesh topology.
    /// </summary>
    public class MeshTopologyEdgeList
    {
        public MeshTopologyEdgeList()
        {
        }

#if RHINO3DMIO
        public MeshTopologyEdgeList(Rhino.Geometry.Collections.MeshTopologyEdgeList rhinoVerts)
        {
            // TODO
        }
#endif
    }

    /// <summary>
    /// Provides access to the Vertex Normals of a Mesh.
    /// </summary>
    public class MeshVertexNormalList : System.Collections.Generic.List<Vector3f>, NN.Collections.IRhinoTable<Vector3f>
    {
        public MeshVertexNormalList()
        {
        }

#if RHINO3DMIO
        public MeshVertexNormalList(Rhino.Geometry.Collections.MeshVertexNormalList rhinoNormals)
        {
            this.Clear();

            this.Capacity = rhinoNormals.Count;

            foreach (var v in rhinoNormals)
            {
                this.Add(new Geometry.Vector3f(v));
            }
        }
#endif
    }

    /// <summary>
    /// Provides access to the faces and Face related functionality of a Mesh.
    /// </summary>
    public class MeshFaceList : System.Collections.Generic.List<MeshFace>, NN.Collections.IRhinoTable<MeshFace>
    {
        public MeshFaceList()
        {
        }

#if RHINO3DMIO
        public MeshFaceList(Rhino.Geometry.Collections.MeshFaceList rhinoFaces)
        {
            this.Clear();

            this.Capacity = rhinoFaces.Count;

            foreach (var v in rhinoFaces)
            {
                this.Add(new Geometry.MeshFace(v));
            }
        }
#endif

        /// <summary>
        /// Gets the number of faces that are quads (4 corners).
        /// </summary>
        public int QuadCount
        {
            get
            {
                int quad = 0;

                foreach (MeshFace m in this)
                {
                    if (m.IsQuad)
                        quad++;
                }

                return quad;
            }
        }

        /// <summary>
        /// Gets the number of faces that are triangles (3 corners).
        /// </summary>
        public int TriangleCount
        {
            get
            {
                int tri = 0;

                foreach (MeshFace m in this)
                {
                    if (m.IsTriangle)
                        tri++;
                }

                return tri;
            }
        }
    }

    /// <summary>
    /// Provides access to the Face normals of a Mesh.
    /// </summary>
    public class MeshFaceNormalList : System.Collections.Generic.List<Vector3f>, NN.Collections.IRhinoTable<Vector3f>
    {

#if RHINO3DMIO
        public MeshFaceNormalList(Rhino.Geometry.Collections.MeshFaceNormalList rhinoNormals)
        {
            this.Clear();

            this.Capacity = rhinoNormals.Count;

            foreach (var v in rhinoNormals)
            {
                this.Add(new Geometry.Vector3f(v));
            }
        }
#endif

        public MeshFaceNormalList()
        {
        }
    }

    /// <summary>
    /// Provides access to the vertex colors of a mesh object.
    /// </summary>
    public class MeshVertexColorList : System.Collections.Generic.List<Color>, NN.Collections.IRhinoTable<Color>
    {
#if RHINO3DMIO
        public MeshVertexColorList(Rhino.Geometry.Collections.MeshVertexColorList rhinoColors)
        {
            this.Clear();

            this.Capacity = rhinoColors.Count;

            this.AddRange(rhinoColors);
        }
#endif

        public MeshVertexColorList()
        {
        }

        /// <example>
        /// <code source='examples\vbnet\ex_analysismode.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_analysismode.cs' lang='cs'/>
        /// </example>
        /// <summary>
        /// Gets or sets a mapping information for the mesh associated with these vertex colors.
        /// </summary>
        public NN.Render.MappingTag Tag { get; set; }

    }

    /// <summary>
    /// Provides access to the Vertex Texture coordinates of a Mesh.
    /// </summary>
    public class MeshTextureCoordinateList : System.Collections.Generic.List<Point2f>, NN.Collections.IRhinoTable<Point2f>
    {
#if RHINO3DMIO
        public MeshTextureCoordinateList(Rhino.Geometry.Collections.MeshTextureCoordinateList rhinoTexCords)
        {
            this.Clear();

            this.Capacity = rhinoTexCords.Count;

            foreach (var v in rhinoTexCords)
            {
                this.Add(new Geometry.Point2f(v));
            }
        }
#endif

        public MeshTextureCoordinateList()
        {
        }
    }
}

namespace NN.Geometry
{
    /// <summary>
    /// Represents the values of the four indices of a mesh face quad.
    /// <para>If the third and fourth values are the same, this face represents a
    /// triangle.</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 16)]
    [DebuggerDisplay("{DebuggerDisplayUtil}")]
    [Serializable]
    public struct MeshFace
    {
        #region members
        internal int m_a;
        internal int m_b;
        internal int m_c;
        internal int m_d;
        #endregion

#if RHINO3DMIO
        public MeshFace(Rhino.Geometry.MeshFace rhinoFace)
        {
            m_a = rhinoFace.A;
            m_b = rhinoFace.B;
            m_c = rhinoFace.C;
            m_d = rhinoFace.D;
        }

        public Rhino.Geometry.MeshFace RhinoObject()
        {
            return new Rhino.Geometry.MeshFace(m_a, m_b, m_c, m_d);
        }
#endif

        /// <summary>
        /// Constructs a new triangular Mesh face.
        /// </summary>
        /// <param name="a">Index of first corner.</param>
        /// <param name="b">Index of second corner.</param>
        /// <param name="c">Index of third corner.</param>
        public MeshFace(int a, int b, int c)
        {
            m_a = a;
            m_b = b;
            m_c = c;
            m_d = c;
        }
        /// <summary>
        /// Constructs a new quadrangular Mesh face.
        /// </summary>
        /// <param name="a">Index of first corner.</param>
        /// <param name="b">Index of second corner.</param>
        /// <param name="c">Index of third corner.</param>
        /// <param name="d">Index of fourth corner.</param>
        public MeshFace(int a, int b, int c, int d)
        {
            m_a = a;
            m_b = b;
            m_c = c;
            m_d = d;
        }

        /// <summary>
        /// Gets an Unset MeshFace. Unset faces have Int32.MinValue for all corner indices.
        /// </summary>
        public static MeshFace Unset
        {
            get { return new MeshFace(int.MinValue, int.MinValue, int.MinValue); }
        }


        /// <summary>
        /// Internal property that figures out the debugger display for Mesh Faces.
        /// </summary>
        internal string DebuggerDisplayUtil
        {
            get
            {
                if (IsTriangle)
                {
                    return string.Format(System.Globalization.CultureInfo.InvariantCulture, "T({0}, {1}, {2})", m_a, m_b, m_c);
                }
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Q({0}, {1}, {2}, {3})", m_a, m_b, m_c, m_d);
            }
        }

        /// <summary>
        /// Gets or sets the first corner index of the mesh face.
        /// </summary>
        public int A
        {
            get { return m_a; }
            set { m_a = value; }
        }
        /// <summary>
        /// Gets or sets the second corner index of the mesh face.
        /// </summary>
        public int B
        {
            get { return m_b; }
            set { m_b = value; }
        }
        /// <summary>
        /// Gets or sets the third corner index of the mesh face.
        /// </summary>
        public int C
        {
            get { return m_c; }
            set { m_c = value; }
        }
        /// <summary>
        /// Gets or sets the fourth corner index of the mesh face. 
        /// If D equals C, the mesh face is considered to be a triangle 
        /// rather than a quad.
        /// </summary>
        public int D
        {
            get { return m_d; }
            set { m_d = value; }
        }

        /// <summary>
        /// Gets or sets the vertex index associated with an entry in this face.
        /// </summary>
        /// <param name="index">A number in interval [0-3] that refers to an index of a vertex in this face.</param>
        /// <returns>The vertex index associated with this mesh face.</returns>
        public int this[int index]
        {
            get
            {
                if (index == 0) return m_a;
                if (index == 1) return m_b;
                if (index == 2) return m_c;
                if (index == 3) return m_d;
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index == 0) m_a = value;
                else if (index == 1) m_b = value;
                else if (index == 2) m_c = value;
                else if (index == 3) m_d = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not this mesh face 
        /// is considered to be valid. Note that even valid mesh faces 
        /// could potentially be invalid in the context of a specific Mesh, 
        /// if one or more of the corner indices exceeds the number of 
        /// vertices on the mesh. If you want to perform a complete 
        /// validity check, use IsValid(int) instead.
        /// </summary>
        public bool IsValid()
        {
            if (m_a < 0) { return false; }
            if (m_b < 0) { return false; }
            if (m_c < 0) { return false; }
            if (m_d < 0) { return false; }

            if (m_a == m_b) { return false; }
            if (m_a == m_c) { return false; }
            if (m_a == m_d) { return false; }
            if (m_b == m_c) { return false; }
            if (m_b == m_d) { return false; }

            return true;
        }
        /// <summary>
        /// Gets a value indicating whether or not this mesh face 
        /// is considered to be valid. Unlike the simple IsValid function, 
        /// this function takes upper bound indices into account.
        /// </summary>
        /// <param name="vertexCount">Number of vertices in the mesh that this face is a part of.</param>
        /// <returns>true if the face is considered valid, false if not.</returns>
        public bool IsValid(int vertexCount)
        {
            if (!IsValid()) { return false; }

            if (m_a >= vertexCount) { return false; }
            if (m_b >= vertexCount) { return false; }
            if (m_c >= vertexCount) { return false; }
            if (m_d >= vertexCount) { return false; }

            return true;
        }

        /// <summary>
        /// Gets a value indicating whether or not this mesh face is a triangle. 
        /// A mesh face is considered to be a triangle when C equals D, thus it is 
        /// possible for an Invalid mesh face to also be a triangle.
        /// </summary>
        public bool IsTriangle { get { return m_c == m_d; } }
        /// <summary>
        /// Gets a value indicating whether or not this mesh face is a quad. 
        /// A mesh face is considered to be a triangle when C does not equal D, 
        /// thus it is possible for an Invalid mesh face to also be a quad.
        /// </summary>
        public bool IsQuad { get { return m_c != m_d; } }

        /// <summary>
        /// Sets all the corners for this face as a triangle.
        /// </summary>
        /// <param name="a">Index of first corner.</param>
        /// <param name="b">Index of second corner.</param>
        /// <param name="c">Index of third corner.</param>
        public void Set(int a, int b, int c)
        {
            m_a = a;
            m_b = b;
            m_c = c;
            m_d = c;
        }
        /// <summary>
        /// Sets all the corners for this face as a quad.
        /// </summary>
        /// <param name="a">Index of first corner.</param>
        /// <param name="b">Index of second corner.</param>
        /// <param name="c">Index of third corner.</param>
        /// <param name="d">Index of fourth corner.</param>
        public void Set(int a, int b, int c, int d)
        {
            m_a = a;
            m_b = b;
            m_c = c;
            m_d = d;
        }

        /// <summary>
        /// Reverses the orientation of the face by swapping corners. 
        /// The first corner is always maintained.
        /// </summary>
        public MeshFace Flip()
        {
            if (m_c == m_d)
                return new MeshFace(m_a, m_c, m_b, m_b);
            return new MeshFace(m_a, m_d, m_c, m_a);
        }
    }
}
