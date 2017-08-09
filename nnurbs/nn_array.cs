using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NN.Geometry;

namespace NN.Runtime.InteropWrappers
{
    class INTERNAL_ComponentIndexArray : System.Collections.Generic.List<ComponentIndex>
    {
    }


    /// <summary>
    /// Wrapper for ON_SimpleArray&lt;int&gt;. If you are not writing C++ code
    /// then this class is not for you.
    /// </summary>
    public class SimpleArrayInt : System.Collections.Generic.List<int>
    {

    }

    /// <summary>
    /// Wrapper for ON_SimpleArray&lt;ON_UUID&gt;. If you are not writing C++ code
    /// then this class is not for you.
    /// </summary>
    public class SimpleArrayGuid : System.Collections.Generic.List<Guid>
    {

    }

    /// <summary>
    /// Wrapper for ON_SimpleArray&lt;ON_Imterval&gt;. If you are not writing C++ code
    /// then this class is not for you.
    /// </summary>
    public class SimpleArrayInterval : System.Collections.Generic.List<Interval>
    {

    }

    /// <summary>
    /// Wrapper for ON_SimpleArray&lt;double&gt;. If you are not writing C++ code,
    /// then this class is not for you.
    /// </summary>
    public class SimpleArrayDouble : System.Collections.Generic.List<Double>
    {

    }

    /// <summary>
    /// ON_SimpleArray&lt;ON_2dPoint&gt; class wrapper.  If you are not writing
    /// C++ code then this class is not for you.
    /// </summary>
    public class SimpleArrayPoint2d : System.Collections.Generic.List<Point2d>
    {

    }

    /// <summary>
    /// ON_SimpleArray&lt;ON_3dPoint&gt;, ON_3dPointArray, ON_PolyLine all have the same size
    /// This class wraps all of these C++ versions.  If you are not writing C++ code then this
    /// class is not for you.
    /// </summary>
    public class SimpleArrayPoint3d : System.Collections.Generic.List<Point3d>
    {

    }

    /// <summary>
    /// Wrapper for ON_SimpleArray&lt;ON_Line&gt;. If you are not writing C++ code
    /// then this class is not for you.
    /// </summary>
    public class SimpleArrayLine : System.Collections.Generic.List<Line>
    {

    }

    /// <summary>
    /// Wrapper for a C++ ON_SimpleArray of ON_Surface* or const ON_Surface*.  If
    /// you are not writing C++ code then this class is not for you.
    /// </summary>
    public class SimpleArraySurfacePointer : System.Collections.Generic.List<Surface>
    {
    }

    /// <summary>
    /// Wrapper for a C++ ON_SimpleArray of ON_Curve* or const ON_Curve*.  If you are not
    /// writing C++ code, then you can ignore this class.
    /// </summary>
    public class SimpleArrayCurvePointer : System.Collections.Generic.List<Curve>
    {

    }

    /// <summary>
    /// Wrapper for a C++ ON_SimpleArray&lt;ON_Geometry*&gt;* or ON_SimpleArray&lt;const ON_Geometry*&gt;.
    /// If you are not writing C++ code, then this class is not for you.
    /// </summary>
    public class SimpleArrayGeometryPointer : System.Collections.Generic.List<GeometryBase>
    {

    }

    /// <summary>
    /// Represents a wrapper to an unmanaged array of mesh pointers.
    /// <para>Wrapper for a C++ ON_SimpleArray of ON_Mesh* or const ON_Mesh*. If you are not
    /// writing C++ code then this class is not for you.</para>
    /// </summary>
    public class SimpleArrayMeshPointer : System.Collections.Generic.List<Geometry.Mesh>
    {

    }

    /// <summary>
    /// Wrapper for a C++ ON_SimpleArray&lt;ON_Brep*&gt; or ON_SimpleArray&lt;const ON_Brep*&gt;
    /// If you are not writing C++ code then this class is not for you.
    /// </summary>
    public class SimpleArrayBrepPointer : System.Collections.Generic.List<Geometry.Brep>
    {

    }
}