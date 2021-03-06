using System;
using System.Runtime.InteropServices;

namespace NN.Geometry
{
    // look at System.Windows.Media.Media3d.Matrix3D structure to help in laying this structure out
    /// <summary>
    /// Represents the values in a 4x4 transform matrix.
    /// <para>This is parallel to C++ ON_Xform.</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 128)]
    [Serializable]
    public struct Transform : IComparable<Transform>, IEquatable<Transform>
    {
        #region members
        internal double m_00, m_01, m_02, m_03;
        internal double m_10, m_11, m_12, m_13;
        internal double m_20, m_21, m_22, m_23;
        internal double m_30, m_31, m_32, m_33;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new transform matrix with a specified value along the diagonal.
        /// </summary>
        /// <param name="diagonalValue">Value to assign to all diagonal cells except M33 which is set to 1.0.</param>
        public Transform(double diagonalValue)
          : this()
        {
            m_00 = diagonalValue;
            m_11 = diagonalValue;
            m_22 = diagonalValue;
            m_33 = 1.0;
        }

        /// <summary>
        /// Gets a new identity transform matrix. An identity matrix defines no transformation.
        /// </summary>
        public static Transform Identity
        {
            get
            {
                Transform xf = new Transform();
                xf.m_00 = 1.0;
                xf.m_11 = 1.0;
                xf.m_22 = 1.0;
                xf.m_33 = 1.0;
                return xf;
            }
        }

        /// <summary>
        /// Gets an XForm filled with RhinoMath.UnsetValue.
        /// </summary>
        public static Transform Unset
        {
            get
            {
                Transform xf = new Transform();
                xf.m_00 = RhinoMath.UnsetValue;
                xf.m_01 = RhinoMath.UnsetValue;
                xf.m_02 = RhinoMath.UnsetValue;
                xf.m_03 = RhinoMath.UnsetValue;
                xf.m_10 = RhinoMath.UnsetValue;
                xf.m_11 = RhinoMath.UnsetValue;
                xf.m_12 = RhinoMath.UnsetValue;
                xf.m_13 = RhinoMath.UnsetValue;
                xf.m_20 = RhinoMath.UnsetValue;
                xf.m_21 = RhinoMath.UnsetValue;
                xf.m_22 = RhinoMath.UnsetValue;
                xf.m_23 = RhinoMath.UnsetValue;
                xf.m_30 = RhinoMath.UnsetValue;
                xf.m_31 = RhinoMath.UnsetValue;
                xf.m_32 = RhinoMath.UnsetValue;
                xf.m_33 = RhinoMath.UnsetValue;
                return xf;
            }
        }
        #endregion

        #region static constructors
        /// <summary>
        /// Constructs a new translation (move) transformation. 
        /// </summary>
        /// <param name="motion">Translation (motion) vector.</param>
        /// <returns>A transform matrix which moves geometry along the motion vector.</returns>
        /// <example>
        /// <code source='examples\vbnet\ex_constrainedcopy.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_constrainedcopy.cs' lang='cs'/>
        /// <code source='examples\py\ex_constrainedcopy.py' lang='py'/>
        /// </example>
        public static Transform Translation(Vector3d motion)
        {
            return Translation(motion.m_x, motion.m_y, motion.m_z);
        }

        /// <summary>
        /// Constructs a new translation (move) tranformation. 
        /// Right column is (dx, dy, dz, 1.0).
        /// </summary>
        /// <param name="dx">Distance to translate (move) geometry along the world X axis.</param>
        /// <param name="dy">Distance to translate (move) geometry along the world Y axis.</param>
        /// <param name="dz">Distance to translate (move) geometry along the world Z axis.</param>
        /// <returns>A transform matrix which moves geometry with the specified distances.</returns>
        /// <example>
        /// <code source='examples\vbnet\ex_transformbrep.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_transformbrep.cs' lang='cs'/>
        /// <code source='examples\py\ex_transformbrep.py' lang='py'/>
        /// </example>
        public static Transform Translation(double dx, double dy, double dz)
        {
            Transform xf = Identity;
            xf.m_03 = dx;
            xf.m_13 = dy;
            xf.m_23 = dz;
            xf.m_33 = 1.0;
            return xf;
        }

        /// <summary>
        /// Constructs a new uniform scaling transformation with a specified scaling anchor point.
        /// </summary>
        /// <param name="anchor">Defines the anchor point of the scaling operation.</param>
        /// <param name="scaleFactor">Scaling factor in all directions.</param>
        /// <returns>A transform matrix which scales geometry uniformly around the anchor point.</returns>
        public static Transform Scale(Point3d anchor, double scaleFactor)
        {
            return Scale(new Plane(anchor, new Vector3d(1, 0, 0), new Vector3d(0, 1, 0)), scaleFactor, scaleFactor, scaleFactor);
        }

        /// <summary>
        /// Constructs a new non-uniform scaling transformation with a specified scaling anchor point.
        /// </summary>
        /// <param name="plane">Defines the center and orientation of the scaling operation.</param>
        /// <param name="xScaleFactor">Scaling factor along the anchor plane X-Axis direction.</param>
        /// <param name="yScaleFactor">Scaling factor along the anchor plane Y-Axis direction.</param>
        /// <param name="zScaleFactor">Scaling factor along the anchor plane Z-Axis direction.</param>
        /// <returns>A transformation matrix which scales geometry non-uniformly.</returns>
        public static Transform Scale(Plane plane, double xScaleFactor, double yScaleFactor, double zScaleFactor)
        {
            Transform xf = Identity;

            // TODO
            //UnsafeNativeMethods.ON_Xform_Scale(ref xf, ref plane, xScaleFactor, yScaleFactor, zScaleFactor);
            return xf;
        }

        /// <summary>
        /// Constructs a new rotation transformation with specified angle, rotation center and rotation axis.
        /// </summary>
        /// <param name="sinAngle">Sin of the rotation angle.</param>
        /// <param name="cosAngle">Cos of the rotation angle.</param>
        /// <param name="rotationAxis">Axis direction of rotation.</param>
        /// <param name="rotationCenter">Center point of rotation.</param>
        /// <returns>A transformation matrix which rotates geometry around an anchor point.</returns>
        public static Transform Rotation(double sinAngle, double cosAngle, Vector3d rotationAxis, Point3d rotationCenter)
        {
            Transform xf = Identity;
            // TODO
            // UnsafeNativeMethods.ON_Xform_Rotation(ref xf, sinAngle, cosAngle, rotationAxis, rotationCenter);
            return xf;
        }

        /// <summary>
        /// Constructs a new rotation transformation with specified angle and rotation center.
        /// </summary>
        /// <param name="angleRadians">Angle (in Radians) of the rotation.</param>
        /// <param name="rotationCenter">Center point of rotation. Rotation axis is vertical.</param>
        /// <returns>A transformation matrix which rotates geometry around an anchor point.</returns>
        public static Transform Rotation(double angleRadians, Point3d rotationCenter)
        {
            return Rotation(angleRadians, new Vector3d(0, 0, 1), rotationCenter);
        }

        /// <summary>
        /// Constructs a new rotation transformation with specified angle, rotation center and rotation axis.
        /// </summary>
        /// <param name="angleRadians">Angle (in Radians) of the rotation.</param>
        /// <param name="rotationAxis">Axis direction of rotation operation.</param>
        /// <param name="rotationCenter">Center point of rotation. Rotation axis is vertical.</param>
        /// <returns>A transformation matrix which rotates geometry around an anchor point.</returns>
        public static Transform Rotation(double angleRadians, Vector3d rotationAxis, Point3d rotationCenter)
        {
            return Rotation(Math.Sin(angleRadians), Math.Cos(angleRadians), rotationAxis, rotationCenter);
        }

        /// <summary>
        /// Constructs a new rotation transformation with start and end directions and rotation center.
        /// </summary>
        /// <param name="startDirection">A start direction.</param>
        /// <param name="endDirection">An end direction.</param>
        /// <param name="rotationCenter">A rotation center.</param>
        /// <returns>A transformation matrix which rotates geometry around an anchor point.</returns>
        public static Transform Rotation(Vector3d startDirection, Vector3d endDirection, Point3d rotationCenter)
        {
            if (Math.Abs(startDirection.Length - 1.0) > RhinoMath.SqrtEpsilon)
                startDirection.Unitize();
            if (Math.Abs(endDirection.Length - 1.0) > RhinoMath.SqrtEpsilon)
                endDirection.Unitize();
            double cos_angle = startDirection * endDirection;
            Vector3d axis = Vector3d.CrossProduct(startDirection, endDirection);
            double sin_angle = axis.Length;
            if (0.0 == sin_angle || !axis.Unitize())
            {
                axis.PerpendicularTo(startDirection);
                axis.Unitize();
                sin_angle = 0.0;
                cos_angle = (cos_angle < 0.0) ? -1.0 : 1.0;
            }
            return Rotation(sin_angle, cos_angle, axis, rotationCenter);
        }

        /// <summary>
        /// Constructs a transformation that maps X0 to X1, Y0 to Y1, Z0 to Z1.
        /// </summary>
        /// <param name="x0">First "from" vector.</param>
        /// <param name="y0">Second "from" vector.</param>
        /// <param name="z0">Third "from" vector.</param>
        /// <param name="x1">First "to" vector.</param>
        /// <param name="y1">Second "to" vector.</param>
        /// <param name="z1">Third "to" vector.</param>
        /// <returns>A rotation transformation value.</returns>
        public static Transform Rotation(Vector3d x0, Vector3d y0, Vector3d z0,
          Vector3d x1, Vector3d y1, Vector3d z1)
        {
            // F0 changes x0,y0,z0 to world X,Y,Z
            Transform F0 = new Transform();
            F0[0, 0] = x0.X; F0[0, 1] = x0.Y; F0[0, 2] = x0.Z;
            F0[1, 0] = y0.X; F0[1, 1] = y0.Y; F0[1, 2] = y0.Z;
            F0[2, 0] = z0.X; F0[2, 1] = z0.Y; F0[2, 2] = z0.Z;
            F0[3, 3] = 1.0;

            // F1 changes world X,Y,Z to x1,y1,z1
            Transform F1 = new Transform();
            F1[0, 0] = x1.X; F1[0, 1] = y1.X; F1[0, 2] = z1.X;
            F1[1, 0] = x1.Y; F1[1, 1] = y1.Y; F1[1, 2] = z1.Y;
            F1[2, 0] = x1.Z; F1[2, 1] = y1.Z; F1[2, 2] = z1.Z;
            F1[3, 3] = 1.0;

            return F1 * F0;
        }

        /// <summary>
        /// Create mirror transformation matrix
        /// The mirror transform maps a point Q to 
        /// Q - (2*(Q-P)oN)*N, where
        /// P = pointOnMirrorPlane and N = normalToMirrorPlane.
        /// </summary>
        /// <param name="pointOnMirrorPlane">Point on the mirror plane.</param>
        /// <param name="normalToMirrorPlane">Normal vector to the mirror plane.</param>
        /// <returns>A transformation matrix which mirrors geometry in a specified plane.</returns>
        public static Transform Mirror(Point3d pointOnMirrorPlane, Vector3d normalToMirrorPlane)
        {
            Transform xf = new Transform();
            // TODO
            //UnsafeNativeMethods.ON_Xform_Mirror(ref xf, pointOnMirrorPlane, normalToMirrorPlane);
            return xf;
        }

        /// <summary>
        /// Constructs a new Mirror transformation.
        /// </summary>
        /// <param name="mirrorPlane">Plane that defines the mirror orientation and position.</param>
        /// <returns>A transformation matrix which mirrors geometry in a specified plane.</returns>
        public static Transform Mirror(Plane mirrorPlane)
        {
            return Mirror(mirrorPlane.Origin, mirrorPlane.ZAxis);
        }

        /// <summary>
        /// Computes a change of basis transformation. A basis change is essentially a remapping 
        /// of geometry from one coordinate system to another.
        /// </summary>
        /// <param name="plane0">Coordinate system in which the geometry is currently described.</param>
        /// <param name="plane1">Target coordinate system in which we want the geometry to be described.</param>
        /// <returns>
        /// A transformation matrix which orients geometry from one coordinate system to another on success.
        /// Transform.Unset on failure.
        /// </returns>
        public static Transform ChangeBasis(Plane plane0, Plane plane1)
        {
            Transform rc = Transform.Identity;
            // TODO
            //bool success = UnsafeNativeMethods.ON_Xform_PlaneToPlane(ref rc, ref plane0, ref plane1, false);
            return Transform.Unset;
        }

        /// <example>
        /// <code source='examples\vbnet\ex_orientonsrf.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_orientonsrf.cs' lang='cs'/>
        /// <code source='examples\py\ex_orientonsrf.py' lang='py'/>
        /// </example>
        public static Transform PlaneToPlane(Plane plane0, Plane plane1)
        {
            Transform rc = Transform.Identity;
            // TODO
            //UnsafeNativeMethods.ON_Xform_PlaneToPlane(ref rc, ref plane0, ref plane1, true);
            return rc;
        }

        /// <summary>
        /// Computes a change of basis transformation. A basis change is essentially a remapping 
        /// of geometry from one coordinate system to another.
        /// </summary>
        /// <param name="initialBasisX">can be any 3d basis.</param>
        /// <param name="initialBasisY">can be any 3d basis.</param>
        /// <param name="initialBasisZ">can be any 3d basis.</param>
        /// <param name="finalBasisX">can be any 3d basis.</param>
        /// <param name="finalBasisY">can be any 3d basis.</param>
        /// <param name="finalBasisZ">can be any 3d basis.</param>
        /// <returns>
        /// A transformation matrix which orients geometry from one coordinate system to another on success.
        /// Transform.Unset on failure.
        /// </returns>
        public static Transform ChangeBasis(Vector3d initialBasisX, Vector3d initialBasisY, Vector3d initialBasisZ,
          Vector3d finalBasisX, Vector3d finalBasisY, Vector3d finalBasisZ)
        {
            Transform rc = Transform.Identity;
            // TODO
            //bool success = UnsafeNativeMethods.ON_Xform_ChangeBasis2(ref rc,
            //  initialBasisX, initialBasisY, initialBasisZ, finalBasisX, finalBasisY, finalBasisZ);
            //return success ? rc :k Transform.Unset;

            return Transform.Unset;
        }

        /// <summary>
        /// Constructs a projection transformation.
        /// </summary>
        /// <param name="plane">Plane onto which everything will be perpendicularly projected.</param>
        /// <returns>A transformation matrix which projects geometry onto a specified plane.</returns>
        public static Transform PlanarProjection(Plane plane)
        {
            Transform rc = Transform.Identity;
            // TODO
            //UnsafeNativeMethods.ON_Xform_PlanarProjection(ref rc, ref plane);
            return rc;
        }

        /// <summary>
        /// Constructs a Shear transformation.
        /// </summary>
        /// <param name="plane">Base plane for shear.</param>
        /// <param name="x">Shearing vector along plane x-axis.</param>
        /// <param name="y">Shearing vector along plane y-axis.</param>
        /// <param name="z">Shearing vector along plane z-axis.</param>
        /// <returns>A transformation matrix which shear geometry.</returns>
        public static Transform Shear(Plane plane, Vector3d x, Vector3d y, Vector3d z)
        {
            Transform rc = Transform.Identity;
            // TODO UnsafeNativeMethods.ON_Xform_Shear(ref rc, ref plane, x, y, z);
            return rc;
        }

        // TODO: taper.
        #endregion

        #region operators
        /// <summary>
        /// Determines if two transformations are equal in value.
        /// </summary>
        /// <param name="a">A tranform.</param>
        /// <param name="b">Another tranform.</param>
        /// <returns>true if transforms are equal; otherwise false.</returns>
        public static bool operator ==(Transform a, Transform b)
        {
            return a.m_00 == b.m_00 && a.m_01 == b.m_01 && a.m_02 == b.m_02 && a.m_03 == b.m_03 &&
              a.m_10 == b.m_10 && a.m_11 == b.m_11 && a.m_12 == b.m_12 && a.m_13 == b.m_13 &&
              a.m_20 == b.m_20 && a.m_21 == b.m_21 && a.m_22 == b.m_22 && a.m_23 == b.m_23 &&
              a.m_30 == b.m_30 && a.m_31 == b.m_31 && a.m_32 == b.m_32 && a.m_33 == b.m_33;
        }

        /// <summary>
        /// Determines if two transformations are different in value.
        /// </summary>
        /// <param name="a">A tranform.</param>
        /// <param name="b">Another tranform.</param>
        /// <returns>true if transforms are different; otherwise false.</returns>
        public static bool operator !=(Transform a, Transform b)
        {
            return a.m_00 != b.m_00 || a.m_01 != b.m_01 || a.m_02 != b.m_02 || a.m_03 != b.m_03 ||
              a.m_10 != b.m_10 || a.m_11 != b.m_11 || a.m_12 != b.m_12 || a.m_13 != b.m_13 ||
              a.m_20 != b.m_20 || a.m_21 != b.m_21 || a.m_22 != b.m_22 || a.m_23 != b.m_23 ||
              a.m_30 != b.m_30 || a.m_31 != b.m_31 || a.m_32 != b.m_32 || a.m_33 != b.m_33;
        }

        /// <summary>
        /// Multiplies (combines) two transformations.
        /// </summary>
        /// <param name="a">First transformation.</param>
        /// <param name="b">Second transformation.</param>
        /// <returns>A transformation matrix that combines the effect of both input transformations. 
        /// The resulting Transform gives the same result as though you'd first apply A then B.</returns>
        public static Transform operator *(Transform a, Transform b)
        {
            Transform xf = new Transform();
            xf.m_00 = a.m_00 * b.m_00 + a.m_01 * b.m_10 + a.m_02 * b.m_20 + a.m_03 * b.m_30;
            xf.m_01 = a.m_00 * b.m_01 + a.m_01 * b.m_11 + a.m_02 * b.m_21 + a.m_03 * b.m_31;
            xf.m_02 = a.m_00 * b.m_02 + a.m_01 * b.m_12 + a.m_02 * b.m_22 + a.m_03 * b.m_32;
            xf.m_03 = a.m_00 * b.m_03 + a.m_01 * b.m_13 + a.m_02 * b.m_23 + a.m_03 * b.m_33;

            xf.m_10 = a.m_10 * b.m_00 + a.m_11 * b.m_10 + a.m_12 * b.m_20 + a.m_13 * b.m_30;
            xf.m_11 = a.m_10 * b.m_01 + a.m_11 * b.m_11 + a.m_12 * b.m_21 + a.m_13 * b.m_31;
            xf.m_12 = a.m_10 * b.m_02 + a.m_11 * b.m_12 + a.m_12 * b.m_22 + a.m_13 * b.m_32;
            xf.m_13 = a.m_10 * b.m_03 + a.m_11 * b.m_13 + a.m_12 * b.m_23 + a.m_13 * b.m_33;

            xf.m_20 = a.m_20 * b.m_00 + a.m_21 * b.m_10 + a.m_22 * b.m_20 + a.m_23 * b.m_30;
            xf.m_21 = a.m_20 * b.m_01 + a.m_21 * b.m_11 + a.m_22 * b.m_21 + a.m_23 * b.m_31;
            xf.m_22 = a.m_20 * b.m_02 + a.m_21 * b.m_12 + a.m_22 * b.m_22 + a.m_23 * b.m_32;
            xf.m_23 = a.m_20 * b.m_03 + a.m_21 * b.m_13 + a.m_22 * b.m_23 + a.m_23 * b.m_33;

            xf.m_30 = a.m_30 * b.m_00 + a.m_31 * b.m_10 + a.m_32 * b.m_20 + a.m_33 * b.m_30;
            xf.m_31 = a.m_30 * b.m_01 + a.m_31 * b.m_11 + a.m_32 * b.m_21 + a.m_33 * b.m_31;
            xf.m_32 = a.m_30 * b.m_02 + a.m_31 * b.m_12 + a.m_32 * b.m_22 + a.m_33 * b.m_32;
            xf.m_33 = a.m_30 * b.m_03 + a.m_31 * b.m_13 + a.m_32 * b.m_23 + a.m_33 * b.m_33;
            return xf;
        }

        /// <summary>
        /// Multiplies a transformation by a point and gets a new point.
        /// </summary>
        /// <param name="m">A transformation.</param>
        /// <param name="p">A point.</param>
        /// <returns>The tranformed point.</returns>
        public static Point3d operator *(Transform m, Point3d p)
        {
            double x = p.m_x; // optimizer should put x,y,z in registers
            double y = p.m_y;
            double z = p.m_z;
            Point3d rc = new Point3d();
            rc.m_x = m.m_00 * x + m.m_01 * y + m.m_02 * z + m.m_03;
            rc.m_y = m.m_10 * x + m.m_11 * y + m.m_12 * z + m.m_13;
            rc.m_z = m.m_20 * x + m.m_21 * y + m.m_22 * z + m.m_23;
            double w = m.m_30 * x + m.m_31 * y + m.m_32 * z + m.m_33;
            if (w != 0.0)
            {
                w = 1.0 / w;
                rc.m_x *= w;
                rc.m_y *= w;
                rc.m_z *= w;
            }
            return rc;
        }

        /// <summary>
        /// Multiplies a transformation by a vector and gets a new vector.
        /// </summary>
        /// <param name="m">A transformation.</param>
        /// <param name="v">A vector.</param>
        /// <returns>The tranformed vector.</returns>
        public static Vector3d operator *(Transform m, Vector3d v)
        {
            double x = v.m_x; // optimizer should put x,y,z in registers
            double y = v.m_y;
            double z = v.m_z;
            Vector3d rc = new Vector3d();
            rc.m_x = m.m_00 * x + m.m_01 * y + m.m_02 * z;
            rc.m_y = m.m_10 * x + m.m_11 * y + m.m_12 * z;
            rc.m_z = m.m_20 * x + m.m_21 * y + m.m_22 * z;
            return rc;
        }

        /// <summary>
        /// Multiplies (combines) two transformations.
        /// <para>This is the same as the * operator between two transformations.</para>
        /// </summary>
        /// <param name="a">First transformation.</param>
        /// <param name="b">Second transformation.</param>
        /// <returns>A transformation matrix that combines the effect of both input transformations. 
        /// The resulting Transform gives the same result as though you'd first apply A then B.</returns>
        public static Transform Multiply(Transform a, Transform b)
        {
            return a * b;
        }
        #endregion

        #region properties
        #region accessor properties
        /// <summary>Gets or sets this[0,0].</summary>
        public double M00 { get { return m_00; } set { m_00 = value; } }
        /// <summary>Gets or sets this[0,1].</summary>
        public double M01 { get { return m_01; } set { m_01 = value; } }
        /// <summary>Gets or sets this[0,2].</summary>
        public double M02 { get { return m_02; } set { m_02 = value; } }
        /// <summary>Gets or sets this[0,3].</summary>
        public double M03 { get { return m_03; } set { m_03 = value; } }

        /// <summary>Gets or sets this[1,0].</summary>
        public double M10 { get { return m_10; } set { m_10 = value; } }
        /// <summary>Gets or sets this[1,1].</summary>
        public double M11 { get { return m_11; } set { m_11 = value; } }
        /// <summary>Gets or sets this[1,2].</summary>
        public double M12 { get { return m_12; } set { m_12 = value; } }
        /// <summary>Gets or sets this[1,3].</summary>
        public double M13 { get { return m_13; } set { m_13 = value; } }

        /// <summary>Gets or sets this[2,0].</summary>
        public double M20 { get { return m_20; } set { m_20 = value; } }
        /// <summary>Gets or sets this[2,1].</summary>
        public double M21 { get { return m_21; } set { m_21 = value; } }
        /// <summary>Gets or sets this[2,2].</summary>
        public double M22 { get { return m_22; } set { m_22 = value; } }
        /// <summary>Gets or sets this[2,3].</summary>
        public double M23 { get { return m_23; } set { m_23 = value; } }

        /// <summary>Gets or sets this[3,0].</summary>
        public double M30 { get { return m_30; } set { m_30 = value; } }
        /// <summary>Gets or sets this[3,1].</summary>
        public double M31 { get { return m_31; } set { m_31 = value; } }
        /// <summary>Gets or sets this[3,2].</summary>
        public double M32 { get { return m_32; } set { m_32 = value; } }
        /// <summary>Gets or sets this[3,3].</summary>
        public double M33 { get { return m_33; } set { m_33 = value; } }

        /// <summary>
        /// Gets or sets the matrix value at the given row and column indixes.
        /// </summary>
        /// <param name="row">Index of row to access, must be 0, 1, 2 or 3.</param>
        /// <param name="column">Index of column to access, must be 0, 1, 2 or 3.</param>
        /// <returns>The value at [row, column]</returns>
        /// <value>The new value at [row, column]</value>
        public double this[int row, int column]
        {
            get
            {
                if (row < 0) { throw new IndexOutOfRangeException("Negative row indices are not allowed when accessing a Transform matrix"); }
                if (row > 3) { throw new IndexOutOfRangeException("Row indices higher than 3 are not allowed when accessing a Transform matrix"); }
                if (column < 0) { throw new IndexOutOfRangeException("Negative column indices are not allowed when accessing a Transform matrix"); }
                if (column > 3) { throw new IndexOutOfRangeException("Column indices higher than 3 are not allowed when accessing a Transform matrix"); }

                if (row == 0)
                {
                    if (column == 0) { return m_00; }
                    if (column == 1) { return m_01; }
                    if (column == 2) { return m_02; }
                    if (column == 3) { return m_03; }
                }
                else if (row == 1)
                {
                    if (column == 0) { return m_10; }
                    if (column == 1) { return m_11; }
                    if (column == 2) { return m_12; }
                    if (column == 3) { return m_13; }
                }
                else if (row == 2)
                {
                    if (column == 0) { return m_20; }
                    if (column == 1) { return m_21; }
                    if (column == 2) { return m_22; }
                    if (column == 3) { return m_23; }
                }
                else if (row == 3)
                {
                    if (column == 0) { return m_30; }
                    if (column == 1) { return m_31; }
                    if (column == 2) { return m_32; }
                    if (column == 3) { return m_33; }
                }

                throw new IndexOutOfRangeException("One of the cross beams has gone out askew on the treadle.");
            }
            set
            {
                if (row < 0) { throw new IndexOutOfRangeException("Negative row indices are not allowed when accessing a Transform matrix"); }
                if (row > 3) { throw new IndexOutOfRangeException("Row indices higher than 3 are not allowed when accessing a Transform matrix"); }
                if (column < 0) { throw new IndexOutOfRangeException("Negative column indices are not allowed when accessing a Transform matrix"); }
                if (column > 3) { throw new IndexOutOfRangeException("Column indices higher than 3 are not allowed when accessing a Transform matrix"); }

                if (row == 0)
                {
                    if (column == 0)
                    { m_00 = value; }
                    else if (column == 1)
                    { m_01 = value; }
                    else if (column == 2)
                    { m_02 = value; }
                    else if (column == 3)
                    { m_03 = value; }
                }
                else if (row == 1)
                {
                    if (column == 0)
                    { m_10 = value; }
                    else if (column == 1)
                    { m_11 = value; }
                    else if (column == 2)
                    { m_12 = value; }
                    else if (column == 3)
                    { m_13 = value; }
                }
                else if (row == 2)
                {
                    if (column == 0)
                    { m_20 = value; }
                    else if (column == 1)
                    { m_21 = value; }
                    else if (column == 2)
                    { m_22 = value; }
                    else if (column == 3)
                    { m_23 = value; }
                }
                else if (row == 3)
                {
                    if (column == 0)
                    { m_30 = value; }
                    else if (column == 1)
                    { m_31 = value; }
                    else if (column == 2)
                    { m_32 = value; }
                    else if (column == 3)
                    { m_33 = value; }
                }
            }
        }
        #endregion

        /// <summary>
        /// Gets a value indicating whether or not this Transform is a valid matrix. 
        /// A valid transform matrix is not allowed to have any invalid numbers.
        /// </summary>
        public bool IsValid
        {
            get
            {
                bool rc = RhinoMath.IsValidDouble(m_00) && RhinoMath.IsValidDouble(m_01) && RhinoMath.IsValidDouble(m_02) && RhinoMath.IsValidDouble(m_03) &&
                          RhinoMath.IsValidDouble(m_10) && RhinoMath.IsValidDouble(m_11) && RhinoMath.IsValidDouble(m_12) && RhinoMath.IsValidDouble(m_13) &&
                          RhinoMath.IsValidDouble(m_20) && RhinoMath.IsValidDouble(m_21) && RhinoMath.IsValidDouble(m_22) && RhinoMath.IsValidDouble(m_23) &&
                          RhinoMath.IsValidDouble(m_30) && RhinoMath.IsValidDouble(m_31) && RhinoMath.IsValidDouble(m_22) && RhinoMath.IsValidDouble(m_33);
                return rc;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the Transform maintains similarity. 
        /// The easiest way to think of Similarity is that any circle, when transformed, 
        /// remains a circle. Whereas a non-similarity Transform deforms circles into ellipses.
        /// </summary>
        public TransformSimilarityType SimilarityType { get { return TransformSimilarityType.NotSimilarity; } }

        /// <summary>
        /// The determinant of this 4x4 matrix.
        /// </summary>
        public double Determinant { get { return 0; } }

        #endregion

        #region methods
        /// <summary>
        /// Computes a new boundingbox that is the smallest axis aligned
        /// boundingbox that contains the transformed result of its 8 original corner
        /// points.
        /// </summary>
        /// <returns>A new bounding box.</returns>
        public BoundingBox TransformBoundingBox(BoundingBox bbox)
        {
            BoundingBox rc = bbox;
            rc.Transform(this);
            return rc;
        }

        /// <summary>
        /// Given a list, an array or any enumerable set of points, computes a new array of tranformed points.
        /// </summary>
        /// <param name="points">A list, an array or any enumerable set of points to be left untouched and copied.</param>
        /// <returns>A new array.</returns>
        public Point3d[] TransformList(System.Collections.Generic.IEnumerable<Point3d> points)
        {
            System.Collections.Generic.List<Point3d> rc = new System.Collections.Generic.List<Point3d>(points);
            for (int i = 0; i < rc.Count; i++)
            {
                Point3d pt = rc[i];
                pt.Transform(this);
                rc[i] = pt;
            }
            return rc.ToArray();
        }

        /// <summary>
        /// Determines if another object is a transform and its value equals this transform value.
        /// </summary>
        /// <param name="obj">Another object.</param>
        /// <returns>true if obj is a transform and has the same value as this transform; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is Transform && Equals((Transform)obj);
        }

        /// <summary>
        /// Determines if another transform equals this transform value.
        /// </summary>
        /// <param name="other">Another transform.</param>
        /// <returns>true if other has the same value as this transform; otherwise, false.</returns>
        public bool Equals(Transform other)
        {
            return this == other;
        }

        /// <summary>
        /// Gets a non-unique hashing code for this transform.
        /// </summary>
        /// <returns>A number that can be used to hash this transform in a dictionary.</returns>
        public override int GetHashCode()
        {
            // MSDN docs recommend XOR'ing the internal values to get a hash code
            return m_00.GetHashCode() ^ m_01.GetHashCode() ^ m_02.GetHashCode() ^ m_03.GetHashCode() ^
                   m_10.GetHashCode() ^ m_11.GetHashCode() ^ m_12.GetHashCode() ^ m_13.GetHashCode() ^
                   m_20.GetHashCode() ^ m_21.GetHashCode() ^ m_22.GetHashCode() ^ m_23.GetHashCode() ^
                   m_30.GetHashCode() ^ m_31.GetHashCode() ^ m_32.GetHashCode() ^ m_33.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of this transform.
        /// </summary>
        /// <returns>A textual representation.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            IFormatProvider provider = System.Globalization.CultureInfo.InvariantCulture;
            sb.AppendFormat("R0=({0},{1},{2},{3}),", m_00.ToString(provider), m_01.ToString(provider), m_02.ToString(provider), m_03.ToString(provider));
            sb.AppendFormat(" R1=({0},{1},{2},{3}),", m_10.ToString(provider), m_11.ToString(provider), m_12.ToString(provider), m_13.ToString(provider));
            sb.AppendFormat(" R2=({0},{1},{2},{3}),", m_20.ToString(provider), m_21.ToString(provider), m_22.ToString(provider), m_23.ToString(provider));
            sb.AppendFormat(" R3=({0},{1},{2},{3})", m_30.ToString(provider), m_31.ToString(provider), m_32.ToString(provider), m_33.ToString(provider));
            return sb.ToString();
        }
        /// <summary>
        /// Attempts to get the inverse transform of this transform.
        /// </summary>
        /// <param name="inverseTransform">The inverse transform. This out reference will be assigned during this call.</param>
        /// <returns>
        /// true on success. 
        /// If false is returned and this Transform is Invalid, inserveTransform will be set to this Transform. 
        /// If false is returned and this Transform is Valid, inverseTransform will be set to a pseudo inverse.
        /// </returns>
        public bool TryGetInverse(out Transform inverseTransform)
        {
            inverseTransform = this;
            bool rc = false;
            if (IsValid)
            { // TODO
            }
            //    rc = UnsafeNativeMethods.ON_Xform_Invert(ref inverseTransform);
            return rc;
        }

        /// <summary>
        /// Flip row/column values
        /// </summary>
        /// <returns></returns>
        public Transform Transpose()
        {
            Transform rc = new Transform();
            for (int r = 0; r < 4; ++r)
            {
                for (int c = 0; c < 4; ++c)
                {
                    rc[r, c] = this[c, r];
                }
            }
            return rc;
        }

        /// <summary>
        /// Return the matrix as a linear array of 16 float values
        /// </summary>
        /// <param name="rowDominant"></param>
        /// <returns></returns>
        public float[] ToFloatArray(bool rowDominant)
        {
            var rc = new float[16];

            if (rowDominant)
            {
                rc[0] = (float)m_00; rc[1] = (float)m_01; rc[2] = (float)m_02; rc[3] = (float)m_03;
                rc[4] = (float)m_10; rc[5] = (float)m_11; rc[6] = (float)m_12; rc[7] = (float)m_13;
                rc[8] = (float)m_20; rc[9] = (float)m_21; rc[10] = (float)m_22; rc[11] = (float)m_23;
                rc[12] = (float)m_30; rc[13] = (float)m_31; rc[14] = (float)m_32; rc[15] = (float)m_33;
            }
            else
            {
                rc[0] = (float)m_00; rc[1] = (float)m_10; rc[2] = (float)m_20; rc[3] = (float)m_30;
                rc[4] = (float)m_01; rc[5] = (float)m_11; rc[6] = (float)m_21; rc[7] = (float)m_31;
                rc[8] = (float)m_02; rc[9] = (float)m_12; rc[10] = (float)m_22; rc[11] = (float)m_32;
                rc[12] = (float)m_03; rc[13] = (float)m_13; rc[14] = (float)m_23; rc[15] = (float)m_33;
            }

            return rc;
        }

        #endregion

        /// <summary>
        /// Compares this transform with another transform.
        /// <para>M33 has highest value, then M32, etc..</para>
        /// </summary>
        /// <param name="other">Another transform.</param>
        /// <returns>-1 if this &lt; other; 0 if both are equal; 1 otherwise.</returns>
        public int CompareTo(Transform other)
        {
            for (int i = 3; i >= 0; --i)
            {
                for (int j = 3; j >= 0; --j)
                {
                    if (this[i, j] < other[i, j]) return -1;
                    if (this[i, j] < other[i, j]) return 1;
                }
            }
            return 0;
        }
    }

    /// <summary>
    /// Lists all possible outcomes for transform similarity.
    /// </summary>
    public enum TransformSimilarityType : int
    {
        /// <summary>
        /// Similarity is preserved, but orientation is flipped.
        /// </summary>
        OrientationReversing = -1,

        /// <summary>
        /// Similarity is not preserved. Geometry needs to be deformable for this Transform to operate correctly.
        /// </summary>
        NotSimilarity = 0,

        /// <summary>
        /// Similarity and orientation are preserved.
        /// </summary>
        OrientationPreserving = 1
    }

    //public class ON_ClippingRegion { }
    //public class ON_Localizer { }

    /// <summary>
    /// Represents a spacial, Euclidean morph.
    /// </summary>
    public abstract class SpaceMorph
    {
        private double m_tolerance;
        private bool m_bQuickPreview;
        private bool m_bPreserveStructure;
        
        
        /// <summary>Morphs an Euclidean point. <para>This method is abstract.</para></summary>
        /// <param name="point">A point that will be morphed by this function.</param>
        /// <returns>Resulting morphed point.</returns>
        public abstract Point3d MorphPoint(Point3d point);

        /// <summary>
        /// The desired accuracy of the morph. This value is primarily used for deforming
        /// surfaces and breps. The default is 0.0 and any value &lt;= 0.0 is ignored by
        /// morphing functions. The Tolerance value does not affect the way meshes and points
        /// are morphed.
        /// </summary>
        public double Tolerance
        {
            get { return m_tolerance; }
            set { m_tolerance = value; }
        }

        /// <summary>
        /// true if the morph should be done as quickly as possible because the result
        /// is being used for some type of dynamic preview. If QuickPreview is true,
        /// the tolerance may be ignored.
        /// The QuickPreview value does not affect the way meshes and points are morphed.
        /// The default is false.
        /// </summary>
        public bool QuickPreview
        {
            get { return m_bQuickPreview; }
            set { m_bQuickPreview = value; }
        }

        /// <summary>
        /// true if the morph should be done in a way that preserves the structure of the geometry.
        /// In particular, for NURBS objects, true means that only the control points are moved.
        /// The PreserveStructure value does not affect the way meshes and points are morphed.
        /// The default is false.
        /// </summary>
        public bool PreserveStructure
        {
            get { return m_bPreserveStructure; }
            set { m_bPreserveStructure = value; }
        }

    }
}