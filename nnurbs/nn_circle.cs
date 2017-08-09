using System;
using System.Runtime.InteropServices;

namespace NN.Geometry
{
    /// <summary>
    /// Represents a circle in 3D.
    /// <para>The values used are a radius and an orthonormal frame	of the plane containing the circle,
    /// with origin at the center.</para>
    /// <para>The circle is parameterized by radians from 0 to 2 Pi given by</para>
    /// <para>t -> center + cos(t)*radius*xaxis + sin(t)*radius*yaxis</para>
    /// <para>where center, xaxis and yaxis define the orthonormal frame of the circle plane.</para>
    /// </summary>
    /// <remarks>>An IsValid circle has positive radius and an IsValid plane defining the frame.</remarks>
    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 136)]
    [Serializable]
    public struct Circle : IEpsilonComparable<Circle>
    {

        internal Plane m_plane;
        internal double m_radius;


        /// <summary>
        /// Gets a circle with Unset components.
        /// </summary>
        static public Circle Unset
        {
            get
            {
                return new Circle(Plane.Unset, RhinoMath.UnsetValue);
            }
        }



        /// <summary>
        /// Initializes a circle with center (0,0,0) in the world XY plane.
        /// </summary>
        /// <param name="radius">Radius of circle, should be a positive number.</param>
        public Circle(double radius) : this(Plane.WorldXY, radius) { }

        public Circle(Point3d P, Point3d Q, Point3d R)
        {
            m_plane = Plane.WorldXY;
            m_radius = 0.01;
            /// TODO
        }

        /// <summary>
        /// Initializes a circle on a plane with a given radius.
        /// </summary>
        /// <param name="plane">Plane of circle. Plane origin defines the center of the circle.</param>
        /// <param name="radius">Radius of circle (should be a positive value).</param>
        /// <example>
        /// <code source='examples\vbnet\ex_addcircle.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_addcircle.cs' lang='cs'/>
        /// <code source='examples\py\ex_addcircle.py' lang='py'/>
        /// </example>
        public Circle(Plane plane, double radius)
        {
            m_plane = plane;
            m_radius = radius;
        }

        /// <summary>
        /// Initializes a circle parallel to the world XY plane with given center and radius.
        /// </summary>
        /// <param name="center">Center of circle.</param>
        /// <param name="radius">Radius of circle (should be a positive value).</param>
        /// <example>
        /// <code source='examples\vbnet\ex_addtruncatedcone.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_addtruncatedcone.cs' lang='cs'/>
        /// <code source='examples\py\ex_addtruncatedcone.py' lang='py'/>
        /// </example>
        public Circle(Point3d center, double radius)
        {
            m_plane = Plane.WorldXY;
            m_plane.Origin = center;
            m_radius = radius;
        }

        /// <summary>
        /// Initializes a circle from an arc.
        /// </summary>
        /// <param name="arc">Arc that defines the plane and radius.</param>
        public Circle(Arc arc)
        {
            m_plane = arc.m_plane;
            m_radius = arc.m_radius;
        }



        /// <summary>
        /// Initializes a circle parallel to a given plane with given center and radius.
        /// </summary>
        /// <param name="plane">Plane for circle.</param>
        /// <param name="center">Center point override.</param>
        /// <param name="radius">Radius of circle (should be a positive value).</param>
        public Circle(Plane plane, Point3d center, double radius)
        {
            m_plane = plane;
            m_radius = radius;
            m_plane.Origin = center;
        }



        /// <summary> 
        /// A valid circle has radius larger than 0.0 and a base plane which is must also be valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                bool rc = m_radius > 0.0 && RhinoMath.IsValidDouble(m_radius) && m_plane.IsValid;
                return rc;
            }
            set
            {


            }
        }

        public bool Transform(Transform xform)
        {
            return false; 
        }


        /// <summary>
        /// Gets or sets the radius of this circle. 
        /// Radii should be positive values.
        /// </summary>
        public double Radius
        {
            get { return m_radius; }
            set { m_radius = value; }
        }

        /// <summary>
        /// Gets or sets the diameter (radius * 2.0) of this circle. 
        /// Diameters should be positive values.
        /// </summary>
        public double Diameter
        {
            get { return m_radius * 2.0; }
            set { m_radius = 0.5 * value; }
        }

        /// <summary>
        /// Gets or sets the plane of the circle.
        /// </summary>
        public Plane Plane
        {
            get { return m_plane; }
            set { m_plane = value; }
        }

        /// <summary>
        /// Gets or sets the center point of this circle.
        /// </summary>
        public Point3d Center
        {
            // David asks : since Point3d is a value type, can't we just return the origin directly?
            get { return m_plane.Origin; }
            set { m_plane.Origin = value; }
        }

        /// <summary>
        /// Gets the normal vector for this circle.
        /// </summary>
        public Vector3d Normal
        {
            get { return m_plane.ZAxis; }
        }

        /// <summary>
        /// Gets or sets the circumference of this circle.
        /// </summary>
        public double Circumference
        {
            get
            {
                return Math.Abs(2.0 * Math.PI * m_radius);
            }
            set
            {
                m_radius = value / (2.0 * Math.PI);
            }
        }



        /// <summary>
        /// Gets the circle's 3d axis aligned bounding box.
        /// </summary>
        /// <returns>3d bounding box.</returns>
        public BoundingBox BoundingBox
        {
            // David changed this on april 16th 2010, we need to provide tight boundingboxes for atomic types.
            get
            {
                double rx = m_radius * Length2d(m_plane.m_zaxis.m_y, m_plane.m_zaxis.m_z);
                double ry = m_radius * Length2d(m_plane.m_zaxis.m_z, m_plane.m_zaxis.m_x);
                double rz = m_radius * Length2d(m_plane.m_zaxis.m_x, m_plane.m_zaxis.m_y);

                double x0 = m_plane.m_origin.m_x - rx;
                double x1 = m_plane.m_origin.m_x + rx;
                double y0 = m_plane.m_origin.m_y - ry;
                double y1 = m_plane.m_origin.m_y + ry;
                double z0 = m_plane.m_origin.m_z - rz;
                double z1 = m_plane.m_origin.m_z + rz;

                return new BoundingBox(x0, y0, z0, x1, y1, z1);
            }
            set
            {


            }
        }

        private static double Length2d(double x, double y)
        {
            double len;
            x = Math.Abs(x);
            y = Math.Abs(y);
            if (y > x)
            {
                len = x;
                x = y;
                y = len;
            }

            // 15 September 2003 Dale Lear
            //     For small denormalized doubles (positive but smaller
            //     than DBL_MIN), some compilers/FPUs set 1.0/fx to +INF.
            //     Without the ON_DBL_MIN test we end up with
            //     microscopic vectors that have infinite length!
            //
            //     This code is absolutely necessary.  It is a critical
            //     part of the fix for RR 11217.
            if (x > double.Epsilon)
            {
                len = 1.0 / x;
                y *= len;
                len = x * Math.Sqrt(1.0 + y * y);
            }
            else if (x > 0.0 && !double.IsInfinity(x))
            {
                len = x;
            }
            else
            {
                len = 0.0;
            }

            return len;
        }

        /// <summary>
        /// Determines the value of the Nth derivative at a parameter. 
        /// </summary>
        /// <param name="derivative">Which order of derivative is wanted.</param>
        /// <param name="t">Parameter to evaluate derivative. Valid values are 0, 1, 2 and 3.</param>
        /// <returns>The derivative of the circle at the given parameter.</returns>
        public Vector3d DerivativeAt(int derivative, double t)
        {
            double r0 = m_radius;
            double r1 = m_radius;

            switch (Math.Abs(derivative) % 4)
            {
                case 0:
                    r0 *= Math.Cos(t);
                    r1 *= Math.Sin(t);
                    break;
                case 1:
                    r0 *= -Math.Sin(t);
                    r1 *= Math.Cos(t);
                    break;
                case 2:
                    r0 *= -Math.Cos(t);
                    r1 *= -Math.Sin(t);
                    break;
                case 3:
                    r0 *= Math.Sin(t);
                    r1 *= -Math.Cos(t);
                    break;
            }

            return (r0 * m_plane.XAxis + r1 * m_plane.YAxis);
        }

        /// <summary>
        /// Circles use trigonometric parameterization: 
        /// t -> center + cos(t)*radius*xaxis + sin(t)*radius*yaxis.
        /// </summary>
        /// <param name="t">Parameter of point to evaluate.</param>
        /// <returns>The point on the circle at the given parameter.</returns>
        public Point3d PointAt(double t)
        {
            return m_plane.PointAt(Math.Cos(t) * m_radius, Math.Sin(t) * m_radius);
        }


        /// <summary>
        /// Rotates the circle around an axis that starts at the base plane origin.
        /// </summary>
        /// <param name="sinAngle">The value returned by Math.Sin(angle) to compose the rotation.</param>
        /// <param name="cosAngle">The value returned by Math.Cos(angle) to compose the rotation.</param>
        /// <param name="axis">A rotation axis.</param>
        /// <returns>true on success, false on failure.</returns>
        public bool Rotate(double sinAngle, double cosAngle, Vector3d axis)
        {
            return m_plane.Rotate(sinAngle, cosAngle, axis);
        }

        /// <summary>
        /// Rotates the circle around an axis that starts at the provided point.
        /// </summary>
        /// <param name="sinAngle">The value returned by Math.Sin(angle) to compose the rotation.</param>
        /// <param name="cosAngle">The value returned by Math.Cos(angle) to compose the rotation.</param>
        /// <param name="axis">A rotation direction.</param>
        /// <param name="point">A rotation base point.</param>
        /// <returns>true on success, false on failure.</returns>
        public bool Rotate(double sinAngle, double cosAngle, Vector3d axis, Point3d point)
        {
            return m_plane.Rotate(sinAngle, cosAngle, axis, point);
        }

        /// <summary>
        /// Rotates the circle through a given angle.
        /// </summary>
        /// <param name="angle">Angle (in radians) of the rotation.</param>
        /// <param name="axis">Rotation axis.</param>
        /// <returns>true on success, false on failure.</returns>
        public bool Rotate(double angle, Vector3d axis)
        {
            return m_plane.Rotate(angle, axis);
        }

        /// <summary>
        /// Rotates the circle through a given angle.
        /// </summary>
        /// <param name="angle">Angle (in radians) of the rotation.</param>
        /// <param name="axis">Rotation axis.</param>
        /// <param name="point">Rotation anchor point.</param>
        /// <returns>true on success, false on failure.</returns>
        public bool Rotate(double angle, Vector3d axis, Point3d point)
        {
            return m_plane.Rotate(angle, axis, point);
        }

        /// <summary>
        /// Moves the circle.
        /// </summary>
        /// <param name="delta">Translation vector.</param>
        /// <returns>true on success, false on failure.</returns>
        public bool Translate(Vector3d delta)
        {
            return m_plane.Translate(delta);
        }


        /// <summary>
        /// Reverse the orientation of the circle. Changes the domain from [a,b]
        /// to [-b,-a].
        /// </summary>
        public void Reverse()
        {
            m_plane.YAxis = -m_plane.YAxis;
            m_plane.ZAxis = -m_plane.ZAxis;
        }




        /// <summary>
        /// Check that all values in other are within epsilon of the values in this
        /// </summary>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool EpsilonEquals(Circle other, double epsilon)
        {
            return RhinoMath.EpsilonEquals(m_radius, other.m_radius, epsilon) &&
                   m_plane.EpsilonEquals(other.m_plane, epsilon);
        }
    }
}