using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NN.Geometry
{
    /// <summary> constansts used for CreatePipe functions </summary>
    public enum PipeCapMode : int
    {
        /// <summary> No cap </summary>
        None = 0,
        /// <summary> Cap with planar surface </summary>
        Flat = 1,
        /// <summary> Cap with hemispherical surface </summary>
        Round = 2
    }

    /// <summary>
    /// Specifies enumerated constants for all supported loft types.
    /// </summary>
    public enum LoftType : int
    {
        /// <summary>
        /// Uses chord-length parameterization in the loft direction.
        /// </summary>
        Normal = 0,
        /// <summary>
        /// The surface is allowed to move away from the original curves to make a smoother surface.
        /// The surface control points are created at the same locations as the control points
        /// of the loft input curves.
        /// </summary>
        Loose = 1,
        /// <summary>
        /// The surface sticks closely to the original curves. Uses square root of chord-length
        /// parameterization in the loft direction.
        /// </summary>
        Tight = 2,
        /// <summary>
        /// The sections between the curves are straight. This is also known as a ruled surface.
        /// </summary>
        Straight = 3,
        /// <summary>
        /// Constructs a separate developable surface or polysurface from each pair of curves.
        /// </summary>
        Developable = 4,

        /// <summary>
        /// Constructs a uniform loft. The object knot vectors will be uniform.
        /// </summary>
        Uniform = 5
    }

    /// <summary>
    /// Corner types used for creating a tapered extrusion
    /// </summary>
    public enum ExtrudeCornerType : int
    {
        /// <summary>No Corner</summary>
        None = 0,
        /// <summary></summary>
        Sharp = 1,
        /// <summary></summary>
        Round = 2,
        /// <summary></summary>
        Smooth = 3,
        /// <summary></summary>
        Chamfer = 4
    }
    /// <summary>
    /// Boundary Representation. A surface or polysurface along with trim curve information.
    /// </summary>
    [Serializable]
    public class Brep : GeometryBase
    {
        /// <summary>Initializes a new empty brep</summary>
        public Brep()
        {

        }

#if RHINO3DMIO
        public Brep(Rhino.Geometry.Brep f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.Brep from)
        {

            if (from == null)
                return false;

            this.Vertices = new NN.Geometry.Collections.BrepVertexList(from.Vertices);
            this.Surfaces = new NN.Geometry.Collections.BrepSurfaceList(from.Surfaces);
            this.Edges =  new NN.Geometry.Collections.BrepEdgeList(from.Edges);
            //    this.Trims =  new NN.Geometry.Collections.BrepTrimList(from.Trims);
            //		this.Loops =  new NN.Geometry.Collections.BrepLoopList(from.Loops);
            this.Faces = new NN.Geometry.Collections.BrepFaceList(from.Faces);
            this.Curves2D = new NN.Geometry.Collections.BrepCurveList(from.Curves2D);
            this.Curves3D = new NN.Geometry.Collections.BrepCurveList(from.Curves3D);
            this.IsSolid = from.IsSolid;
            this.SolidOrientation = (NN.Geometry.BrepSolidOrientation)from.SolidOrientation;
            this.IsManifold = from.IsManifold;
            this.IsSurface = from.IsSurface;
            //		this.IsDocumentControlled = from.IsDocumentControlled;// Missing This Prop
            this.ObjectType = (NN.DocObjects.ObjectType)from.ObjectType;
            //		this.IsDeformable = from.IsDeformable;// Missing This Prop
            //		this.HasBrepForm = from.HasBrepForm;// Missing This Prop
            //		this.UserStringCount = from.UserStringCount;// Missing This Prop
            //		this.IsValid = from.IsValid;// Missing This Prop
            //		this.HasUserData = from.HasUserData;// Missing This Prop
            //		this.UserData = from.UserData;// Missing This Prop
            return true;
        }


        public bool CopyTo(Rhino.Geometry.Brep to)
        {
            foreach (var s in Surfaces)
            {
                if (s != null)
                {
                    var surface = s.RhinoSurfaceObject();

                    if (surface != null)
                        to.AddSurface(surface);
                }
            }
            
            foreach (var c in Curves2D.Curves)
                to.Curves2D.Add(c.RhinoCurveObject());

            foreach (var c in Curves3D.Curves)
                to.Curves3D.Add(c.RhinoCurveObject());

            foreach (var v in Vertices)
                to.Vertices.Add(v.Location.RhinoObject(), 0); // TODO how to get Tolerance

            /*
            foreach (var e in Edges)
            {
                if (e.EdgeCurve != null)
                {
                    to.AddEdgeCurve(e.EdgeCurve.RhinoCurveObject());
                }
            }

            foreach (var t in Trims)
            {
                if (t.TrimCurve != null)
                {
                    to.AddTrimCurve(t.TrimCurve.RhinoCurveObject());
                }
            }
            */

            // TODO Finish

            to.Standardize();
            
            return true;
        }

        public Rhino.Geometry.Brep RhinoObject()
        {
            var rhinoBrep = new Rhino.Geometry.Brep();

            CopyTo(rhinoBrep);

            return rhinoBrep;
        }

#endif

        /// <summary>
        /// </summary>
        public Collections.BrepVertexList Vertices { get; set; }

        /// <summary> Parametric surfaces used by faces </summary>
        public Collections.BrepSurfaceList Surfaces { get; set; }

        public Collections.BrepEdgeList Edges { get; set; }

        public Collections.BrepTrimList Trims { get; set; }

        public Collections.BrepLoopList Loops { get; set; }

        public Collections.BrepFaceList Faces { get; set; }

        public Collections.BrepCurveList Curves2D { get; set; }
 
        public Collections.BrepCurveList Curves3D { get; set; }

        public bool IsSolid { get; set; }

        /// <summary>
        /// Gets the solid orientation state of this Brep.
        /// </summary>
        public BrepSolidOrientation SolidOrientation { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the Brep is manifold. 
        /// Non-Manifold breps have at least one edge that is shared among three or more faces.
        /// </summary>
        public bool IsManifold { get; set; }

        /// <summary>
        /// Returns true if the Brep has a single face and that face is geometrically the same
        /// as the underlying surface.  I.e., the face has trivial trimming.
        /// <para>In this case, the surface is the first face surface. The flag
        /// Brep.Faces[0].OrientationIsReversed records the correspondence between the surface's
        /// natural parametric orientation and the orientation of the Brep.</para>
        /// <para>trivial trimming here means that there is only one loop curve in the brep
        /// and that loop curve is the same as the underlying surface boundary.</para>
        /// </summary>
        public bool IsSurface { get; set; }

    }

    /// <summary>
    /// Enumerates the possible point/BrepFace spatial relationships.
    /// </summary>
    public enum PointFaceRelation : int
    {
        /// <summary>
        /// Point is on the exterior (the trimmed part) of the face.
        /// </summary>
        Exterior = 0,

        /// <summary>
        /// Point is on the interior (the existing part) of the face.
        /// </summary>
        Interior = 1,

        /// <summary>
        /// Point is in limbo.
        /// </summary>
        Boundary = 2
    }

    /// <summary>
    /// Enumerates all possible Solid Orientations for a Brep.
    /// </summary>
    public enum BrepSolidOrientation : int
    {
        /// <summary>
        /// Brep is not a Solid.
        /// </summary>
        None = 0,

        /// <summary>
        /// Brep is a Solid with inward facing normals.
        /// </summary>
        Inward = -1,

        /// <summary>
        /// Brep is a Solid with outward facing normals.
        /// </summary>
        Outward = 1,

        /// <summary>
        /// Breps is a Solid but no orientation could be computed.
        /// </summary>
        Unknown = 2
    }

    /// <summary>
    /// Enumerates all possible Topological Edge adjacency types.
    /// </summary>
    public enum EdgeAdjacency : int
    {
        /// <summary>
        /// Edge is not used by any faces and is therefore superfluous.
        /// </summary>
        None = 0,

        /// <summary>
        /// Edge is used by a single face.
        /// </summary>
        Naked = 1,

        /// <summary>
        /// Edge is used by two adjacent faces.
        /// </summary>
        Interior = 2,

        /// <summary>
        /// Edge is used by three or more adjacent faces.
        /// </summary>
        NonManifold = 3
    }

    /// <summary>
    /// Brep vertex information
    /// </summary>
    public class BrepVertex : Point
    {
        int m_index;

        BrepVertex()
        {
            m_index = -1;
        }


#if RHINO3DMIO
        public BrepVertex(Rhino.Geometry.BrepVertex f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.BrepVertex from)
        {
            if (from == null)
                return false;

            //		this.Brep = from.Brep;// Missing This Prop
            this.VertexIndex = from.VertexIndex;
            //		this.Location =  new NN.Geometry.Point3d(from.Location);
            //		this.IsDocumentControlled = from.IsDocumentControlled;// Missing This Prop
            this.ObjectType = (NN.DocObjects.ObjectType)from.ObjectType;

            var l = from.Location;

            this.Location.X = l.X;
            this.Location.Y = l.Y;
            this.Location.Z = l.Z;

            this.ComponentIndex = new ComponentIndex(from.ComponentIndex());

            return true;
        }


        public bool CopyTo(Rhino.Geometry.BrepVertex to)
        {
            return true;
        }
#endif

        internal BrepVertex(int index, Brep owner)
        {
            m_index = index;
        }

        #region properties

        /// <summary>
        /// Gets the index of this vertex in the Brep.Vertices collection.
        /// </summary>
        public int VertexIndex
        {
            get { return m_index; }
            set { m_index = value; }
        }
        #endregion

    }

    /// <summary>
    /// Represents a single edge curve in a Brep object.
    /// </summary>
    public class BrepEdge : CurveProxy
    {

#if RHINO3DMIO
        public BrepEdge(Rhino.Geometry.BrepEdge f)
        {
            CopyFrom(f);
        }
        public bool CopyFrom(Rhino.Geometry.BrepEdge from)
        {

            if (from == null)
                return false;

            this.EdgeCurve = Curve.CreateCurve(from.DuplicateCurve());

            this.Tolerance = from.Tolerance;
            this.TrimCount = from.TrimCount;
            this.Valence = (NN.Geometry.EdgeAdjacency)from.Valence;

            this.EdgeIndex = from.EdgeIndex;
            this.StartVertex = new NN.Geometry.BrepVertex(from.StartVertex);
            this.EndVertex = new NN.Geometry.BrepVertex(from.EndVertex);
            this.ProxyCurveIsReversed = from.ProxyCurveIsReversed;
            this.Domain = new NN.Geometry.Interval(from.Domain);
            this.Dimension = from.Dimension;
            this.SpanCount = from.SpanCount;
            this.Degree = from.Degree;
            this.IsClosed = from.IsClosed;
            this.IsPeriodic = from.IsPeriodic;
            this.ObjectType = (NN.DocObjects.ObjectType)from.ObjectType;

            this.ComponentIndex = new ComponentIndex(from.ComponentIndex());

            return true;
        }


        public bool CopyTo(Rhino.Geometry.BrepEdge to)
        {
            return true;
        }
#endif

        public BrepEdge()
        {
        }

        internal BrepEdge(int index, Brep owner)
        {
        }

        //    ON_U m_edge_user;
        //    ON_BrepTrim* Trim( int eti ) const;
        //    ON_BrepVertex* Vertex(int evi) const;
        //    int EdgeCurveIndexOf() const;
        //    const ON_Curve* EdgeCurveOf() const;
        //    bool ChangeEdgeCurve( int c3i );
        //    void UnsetPlineEdgeParameters();
        //    int m_c3i;
        //    int m_vi[2];
        //    ON_SimpleArray<int> m_ti;

        /// <summary>
        /// Gets or sets the accuracy of the edge curve (>=0.0 or RhinoMath.UnsetValue)
        /// A value of UnsetValue indicates that the tolerance should be computed.
        ///
        /// The maximum distance from the edge's 3d curve to any surface of a face
        /// that has this edge as a portion of its boundary must be &lt;= this tolerance.
        /// </summary>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets the number of trim-curves that use this edge.
        /// </summary>
        public int TrimCount { get; set; }


        /// <summary>
        /// Gets the topological valency of this edge. The topological valency 
        /// is defined by how many adjacent faces share this edge.
        /// </summary>
        public EdgeAdjacency Valence { get; set; }


        /// <summary>
        /// Gets the index of this edge in the Brep.Edges collection.
        /// </summary>
        public int EdgeIndex
        {
            get;
            set;
        }

        /// <summary>
        /// BrepVertex at start of edge
        /// </summary>
        public BrepVertex StartVertex { get; set; }

        /// <summary>
        /// BrepVertex at end of edge
        /// </summary>
        public BrepVertex EndVertex { get; set; }

        public Curve EdgeCurve { get; set; }
    }

    /// <summary>
    /// Each brep trim has a defined type.
    /// </summary>
    public enum BrepTrimType : int
    {
        /// <summary>Unknown type</summary>
        Unknown = 0,
        /// <summary>
        /// Trim is connected to an edge, is part of an outer, inner or
        /// slit loop, and is the only trim connected to the edge.
        /// </summary>
        Boundary = 1,
        /// <summary>
        /// Trim is connected to an edge, is part of an outer, inner or slit loop,
        /// no other trim from the same loop is connected to the edge, and at least
        /// one trim from a different loop is connected to the edge.
        /// </summary>
        Mated = 2,
        /// <summary>
        /// trim is connected to an edge, is part of an outer, inner or slit loop,
        /// and one other trim from the same loop is connected to the edge.
        /// (There can be other mated trims that are also connected to the edge.
        /// For example, the non-mainfold edge that results when a surface edge lies
        /// in the middle of another surface.)  Non-mainfold "cuts" have seam trims too.
        /// </summary>
        Seam = 3,
        /// <summary>
        /// Trim is part of an outer loop, the trim's 2d curve runs along the singular
        /// side of a surface, and the trim is NOT connected to an edge. (There is
        /// no 3d edge because the surface side is singular.)
        /// </summary>
        Singular = 4,
        /// <summary>
        /// Trim is connected to an edge, is the only trim in a crfonsrf loop, and
        /// is the only trim connected to the edge.
        /// </summary>
        CurveOnSurface = 5,
        /// <summary>
        /// Trim is a point on a surface, trim.m_pbox is records surface parameters,
        /// and is the only trim in a ptonsrf loop.  This trim is not connected to
        /// an edge and has no 2d curve.
        /// </summary>
        PointOnSurface = 6,
        /// <summary></summary>
        Slit = 7
    }


    /// <summary>
    /// Brep trim information is stored in BrepTrim classes. Brep.Trims is an
    /// array of all the trims in the brep. A BrepTrim is derived from CurveProxy
    /// so the trim can supply easy to use evaluation tools via the Curve virtual
    /// member functions.
    /// Note well that the domains and orientations of the curve m_C2[trim.m_c2i]
    /// and the trim as a curve may not agree.
    /// </summary>
    public class BrepTrim : CurveProxy
    {
        public BrepTrim() { }


#if RHINO3DMIO
        public BrepTrim(Rhino.Geometry.BrepTrim f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.BrepTrim from)
        {
            this.ComponentIndex = new ComponentIndex(from.ComponentIndex());

            this.TrimCurve = Curve.CreateCurve(from.DuplicateCurve());

            return true;
        }


        public bool CopyTo(Rhino.Geometry.BrepTrim to)
        {
            return true;
        }
#endif


        public BrepLoop Loop { get; set; }

        /// <summary>
        /// Brep face this trim belongs to
        /// </summary>
        public int Face { get; set; }

        /// <summary>
        /// Brep edge this trim belongs to. This will be null for singular trims
        /// </summary>
        public int Edge { get; set; }

        /// <summary>
        /// Gets the index of this trim in the Brep.Trims collection.
        /// </summary>
        public int TrimIndex { get; set; }

        /// <summary>Type of trim</summary>
        public BrepTrimType TrimType { get; set; }


        /// <summary></summary>
        public IsoStatus IsoStatus { get; set; }

        /// <summary>
        /// The values in tolerance[] record the accuracy of the parameter space
        /// trimming curves.
        /// </summary>
        /// <remarks>
        /// <para>tolerance[0] = accuracy of parameter space curve in first ("u") parameter</para>
        /// <para>tolerance[1] = accuracy of parameter space curve in second ("v") parameter</para>
        /// <para>
        /// A value of RhinoMath.UnsetValue indicates that the tolerance should be computed.
        /// If the value &gt;= 0.0, then the tolerance is set.
        /// If the value is RhinoMath.UnsetValue, then the tolerance needs to be computed.
        /// </para>
        /// <para>
        /// If the trim is not singular, then the trim must have an edge. If P is a
        /// 3d point on the edge's curve and surface(u,v) = Q is the point on the
        /// surface that is closest to P, then there must be a parameter t in the
        /// interval [m_t[0], m_t[1]] such that
        ///  |u - curve2d(t)[0]| &lt;= tolerance[0]
        /// and
        ///  |v - curve2d(t)[1]| &lt;= tolerance[1]
        ///
        /// If P is the 3d point for the vertex brep.m_V[m_vi[k]] and (uk,vk) is the
        /// corresponding end of the trim's parameter space curve, then there must be
        /// a surface parameter (u,v) such that:
        /// </para>
        /// <list type="bullet">
        /// <item><description>
        /// the distance from the 3d point surface(u,v) to P is &lt;= brep.m_V[m_vi[k]].m_tolerance,
        /// </description></item>
        /// <item><description>|u-uk| &lt;= tolerance[0].</description></item>
        /// <item><description>|v-vk| &lt;= tolerance[1].</description></item>
        /// </list>
        /// </remarks>
        /// <param name="toleranceU"></param>
        /// <param name="toleranceV"></param>
        /// 
        double ToleranceU { get; set; }
        double ToleranceV { get; set; }

        public Curve TrimCurve { get; set; }
    }

    /// <summary>
    /// Each brep loop has a defined type, e.g. outer, inner or point on surface.
    /// </summary>
    public enum BrepLoopType : int
    {
        /// <summary>
        /// Unknown loop type.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 2d loop curves form a simple closed curve with a counterclockwise orientation.
        /// </summary>
        Outer = 1,
        /// <summary>
        /// 2d loop curves form a simple closed curve with a clockwise orientation.
        /// </summary>
        Inner = 2,
        /// <summary>
        /// Always closed - used internally during splitting operations.
        /// </summary>
        Slit = 3,
        /// <summary>
        /// "loop" is a curveonsrf made from a single (open or closed) trim that
        /// has type TrimType.CurveOnSurface.
        /// </summary>
        CurveOnSurface = 4,
        /// <summary>
        /// "loop" is a PointOnSurface made from a single trim that has
        /// type TrimType.PointOnSurface.
        /// </summary>
        PointOnSurface = 5
    }

    /// <summary>
    /// Represent a single loop in a Brep object. A loop is composed
    /// of a list of trim curves.
    /// </summary>
    public class BrepLoop : GeometryBase
    {
        public BrepLoop()
        {
        }

#if RHINO3DMIO
        public BrepLoop(Rhino.Geometry.BrepLoop f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.BrepLoop from)
        {
            if (from == null)
                return false;

            //		this.Brep = from.Brep;
            this.LoopIndex = from.LoopIndex;
            //		this.Face = from.Face;
            this.LoopType = (NN.Geometry.BrepLoopType)from.LoopType;
            this.FaceIndex = from.Face.FaceIndex;

            //		this.Trims =  new NN.Geometry.Collections.BrepTrimList(from.Trims);
            //		this.IsDocumentControlled = from.IsDocumentControlled;
            this.ObjectType = (NN.DocObjects.ObjectType)from.ObjectType;
            

            this.ComponentIndex = new ComponentIndex(from.ComponentIndex());

            return true;
        }


        public bool CopyTo(Rhino.Geometry.BrepLoop to)
        {
            return true;
        }
#endif



        /// <summary>
        /// Gets the index of this loop in the Brep.Loops collection.
        /// </summary>
        public int LoopIndex { get; set; }


        /// <summary>
        /// BrepFace this loop belongs to.
        /// </summary>
        public int FaceIndex { get; set; }

        /// <summary>
        /// type of loop.
        /// </summary>
        public BrepLoopType LoopType { get; set; }

        /// <summary>
        /// List of trims for this loop
        /// </summary>
        public NN.Geometry.Collections.BrepTrimList Trims { get; set; }
    }

    /// <summary>
    /// Provides strongly-typed access to brep faces.
    /// <para>A Brep face is composed of one surface and trimming curves.</para>
    /// </summary>
    public class BrepFace : SurfaceProxy
    {
        public BrepFace() { }

#if RHINO3DMIO
        public BrepFace(Rhino.Geometry.BrepFace f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.BrepFace from)
        {
            FaceIndex = from.FaceIndex;
            SurfaceIndex = from.SurfaceIndex;

            return true;
        }


        public bool CopyTo(Rhino.Geometry.BrepFace to)
        {
            return true;
        }

        public Rhino.Geometry.BrepFace RhinoObject()
        {
            return null;
        }

        public override Rhino.Geometry.Surface RhinoSurfaceObject()
        {
            return RhinoObject();
        }
#endif

        /// <summary>
        /// true if face orientation is opposite of natural surface orientation.
        public bool OrientationIsReversed { get; set; }


        /// <summary>
        /// Gets a value indicating whether the face is synonymous with the underlying surface. 
        /// If a Face has no trimming curves then it is considered a Surface.
        /// </summary>
        public bool IsSurface { get; set; }

        /// <summary>Index of face in Brep.Faces array.</summary>
        public int FaceIndex { get; set; }

        /// <summary>
        /// Surface index of the 3d surface geometry used by this face or -1
        /// </summary>
        public int SurfaceIndex { get; set; }


        public NN.Geometry.Collections.BrepLoopList Loops { get; set; }


        /// <summary>
        /// Every face has a single outer loop.
        /// </summary>
        public BrepLoop OuterLoop { get; set; }

        public Mesh PreviewMesh { get; set; }
        public Mesh RenderMesh { get; set; }
    }
}

namespace NN.Geometry.Collections
{
    /// <summary>
    /// Provides access to all the Vertices in a Brep object
    /// </summary>
    public class BrepVertexList : System.Collections.Generic.List<BrepVertex>, NN.Collections.IRhinoTable<BrepVertex>
    {
        public BrepVertexList()
        { }


#if RHINO3DMIO
        public BrepVertexList(Rhino.Geometry.Collections.BrepVertexList f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.Collections.BrepVertexList from)
        {


            this.Clear();

            foreach (var o in from)
            {
                this.Add(new BrepVertex(o));
            }

            return true;
        }


        public bool CopyTo(Rhino.Geometry.Collections.BrepVertexList to)
        {
            return true;
        }
#endif

    }

    /// <summary>
    /// Provides access to all the Faces in a Brep object.
    /// </summary>
    public class BrepFaceList : System.Collections.Generic.List<BrepFace>, NN.Collections.IRhinoTable<BrepFace>
    {
        public BrepFaceList()
        {
        }

#if RHINO3DMIO
        public BrepFaceList(Rhino.Geometry.Collections.BrepFaceList f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.Collections.BrepFaceList from)
        {


            this.Clear();

            foreach (var o in from)
            {
                this.Add(new BrepFace(o));
            }

            return true;
        }


        public bool CopyTo(Rhino.Geometry.Collections.BrepFaceList to)
        {
            return true;
        }
#endif
    }

    /// <summary>
    /// Provides access to all the underlying surfaces in a Brep object.
    /// </summary>
    public class BrepSurfaceList : System.Collections.Generic.List<Surface>, NN.Collections.IRhinoTable<Surface>
    {
        public BrepSurfaceList()
        { }

#if RHINO3DMIO
        public BrepSurfaceList(Rhino.Geometry.Collections.BrepSurfaceList f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.Collections.BrepSurfaceList from)
        {
            this.Clear();

            foreach (var s in from)
            {
                this.Add(Surface.CreateSurface(s));
            }

            return true;
        }


        public bool CopyTo(Rhino.Geometry.Collections.BrepSurfaceList to)
        {
            return true;
        }
#endif
    }

    /// <summary>
    /// Provides access to all the underlying curves in a Brep object.
    /// </summary>
    public class BrepCurveList
    { 
        public bool m_c2list; // if false, then m_c3

        
        [XmlElement(typeof(NN.Geometry.NurbsCurve))]
        [XmlElement(typeof(NN.Geometry.PolyCurve))]
        [XmlElement(typeof(NN.Geometry.PolylineCurve))]
        [XmlElement(typeof(NN.Geometry.LineCurve))]
        [XmlElement(typeof(NN.Geometry.ArcCurve))]
        [XmlElement(typeof(NN.Geometry.BrepEdge))]
        [XmlElement(typeof(NN.Geometry.BrepTrim))]
        public System.Collections.Generic.List<Curve> Curves { get; set; }

        public BrepCurveList() { Curves = new List<Curve>(); }

#if RHINO3DMIO
        public BrepCurveList(Rhino.Geometry.Collections.BrepCurveList f)
        {
            Curves = new List<Curve>();

            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.Collections.BrepCurveList from)
        {
            this.Curves.Clear();

            foreach (var s in from)
            {
                Curves.Add(Curve.CreateCurve(s));
            }

            return true;
        }


        public bool CopyTo(Rhino.Geometry.Collections.BrepSurfaceList to)
        {
            return true;
        }
#endif
    }

    /// <summary>
    /// Provides access to all the Edges in a Brep object.
    /// </summary>
    public class BrepEdgeList : System.Collections.Generic.List<BrepEdge>, NN.Collections.IRhinoTable<BrepEdge>
    {
        public BrepEdgeList()
        {
        }

#if RHINO3DMIO
        public BrepEdgeList(Rhino.Geometry.Collections.BrepEdgeList f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.Collections.BrepEdgeList from)
        {
            Clear();

            foreach (var e in from)
            {
                Add(new BrepEdge(e));
            }

            return true;
        }


        public bool CopyTo(Rhino.Geometry.Collections.BrepEdgeList to)
        {
            return true;
        }
#endif
    }
    
    /// <summary>
    /// Provides access to all the Trims in a Brep object
    /// </summary>
    public class BrepTrimList : System.Collections.Generic.List<BrepTrim>, NN.Collections.IRhinoTable<BrepTrim>
    {
        public BrepTrimList()
        {

        }
    }

    /// <summary>
    /// Provides access to all the Loops in a Brep object.
    /// </summary>
    public class BrepLoopList : System.Collections.Generic.List<BrepLoop>, NN.Collections.IRhinoTable<BrepLoop>
    {
        public BrepLoopList()
        { }
    }
}