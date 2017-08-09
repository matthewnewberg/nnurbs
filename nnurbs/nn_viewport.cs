using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using NN.Geometry;

namespace NN.DocObjects
{
    /// <summary>
    /// Represents a viewing frustum.
    /// </summary>
    [Serializable]
    public sealed class ViewportInfo
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ViewportInfo()
        {

        }

        /// <summary>
        /// Gets a value that indicates whether the camera is valid.
        /// </summary>
        public bool IsValidCamera { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the frustum is valid.
        /// </summary>
        public bool IsValidFrustum { get; set; }

        /// <summary>
        /// Gets a value that indicates whether this complete object is valid.
        /// </summary>
        public bool IsValid { get; set; }


        /// <summary>
        /// Get or set whether this projection is perspective.
        /// </summary>
        public bool IsPerspectiveProjection { get; set; }


        /// <summary>
        /// Get or set whether this projection is parallel.
        /// </summary>
        public bool IsParallelProjection { get; set; }

        /// <summary>
        /// Gets a value that indicates whether this projection is a two-point perspective.
        /// </summary>
        public bool IsTwoPointPerspectiveProjection { get; set; }

        /// <summary>
        /// Gets the camera location (position) point.
        /// </summary>
        public NN.Geometry.Point3d CameraLocation { get; set; }


        /// <summary>
        /// Gets the direction that the camera faces.
        /// </summary>
        public NN.Geometry.Vector3d CameraDirection { get; set; }

        /// <summary>
        /// Gets the camera up vector.
        /// </summary>
        public NN.Geometry.Vector3d CameraUp { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the camera location is unmodifiable.
        /// </summary>
        public bool IsCameraLocationLocked { get; set; }


        /// <summary>
        /// Gets or sets a value that indicates whether the direction that the camera faces is unmodifiable.
        /// </summary>
        public bool IsCameraDirectionLocked { get; set; }


        /// <summary>
        /// Gets or sets a value that indicates whether the camera up vector is unmodifiable.
        /// </summary>
        public bool IsCameraUpLocked { get; set; }


        /// <summary>
        /// Gets or sets a value that indicates whether the camera frustum has a vertical symmetry axis.
        /// </summary>
        public bool IsFrustumLeftRightSymmetric { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the camera frustum has a horizontal symmetry axis.
        /// </summary>
        public bool IsFrustumTopBottomSymmetric { get; set; }


        /// <summary>
        /// Gets the unit "to the right" vector.
        /// </summary>
        public NN.Geometry.Vector3d CameraX { get; set; }


        /// <summary>
        /// Gets the unit "up" vector.
        /// </summary>
        public NN.Geometry.Vector3d CameraY { get; set; }


        /// <summary>
        /// Gets the unit vector in -CameraDirection.
        /// </summary>
        public NN.Geometry.Vector3d CameraZ { get; set; }




        /// <summary>
        /// Setting FrustumAspect changes the larger of the frustum's width/height
        /// so that the resulting value of width/height matches the requested
        /// aspect.  The camera angle is not changed.  If you change the shape
        /// of the view port with a call SetScreenPort(), then you generally 
        /// want to call SetFrustumAspect() with the value returned by 
        /// GetScreenPortAspect().
        /// </summary>
        public double FrustumAspect { get; set; }

        /// <summary>
        /// Gets the frustum center point.
        /// </summary>
        public NN.Geometry.Point3d FrustumCenter { get; set; }


        /// <summary>
        /// Gets the frustum left value. This is -right if the frustum has a vertical symmetry axis.
        /// <para>This number is usually negative.</para>
        /// </summary>
        public double FrustumLeft { get; set; }

        /// <summary>
        /// Gets the frustum right value. This is -left if the frustum has a vertical symmetry axis.
        /// <para>This number is usually positive.</para>
        /// </summary>
        public double FrustumRight { get; set; }

        /// <summary>
        /// Gets the frustum bottom value. This is -top if the frustum has a horizontal symmetry axis.
        /// <para>This number is usually negative.</para>
        /// </summary>
        public double FrustumBottom { get; set; }

        /// <summary>
        /// Gets the frustum top value. This is -bottom if the frustum has a horizontal symmetry axis.
        /// <para>This number is usually positive.</para>
        /// </summary>
        public double FrustumTop { get; set; }

        /// <summary>
        /// Gets the frustum near-cutting value.
        /// </summary>
        public double FrustumNear { get; set; }

        /// <summary>
        /// Gets the frustum far-cutting value.
        /// </summary>
        public double FrustumFar { get; set; }

        /// <summary>
        /// Gets the frustum width. This is <see cref="FrustumRight"/> - <see cref="FrustumLeft"/>.
        /// </summary>
        public double FrustumWidth { get; set; }

        /// <summary>
        /// Gets the frustum height. This is <see cref="FrustumTop"/> - <see cref="FrustumBottom"/>.
        /// </summary>
        public double FrustumHeight { get; set; }

        /// <summary>
        /// Gets the frustum minimum diameter, or the minimum between <see cref="FrustumWidth"/> and <see cref="FrustumHeight"/>.
        /// </summary>
        public double FrustumMinimumDiameter { get; set; }

        /// <summary>
        /// Gets the frustum maximum diameter, or the maximum between <see cref="FrustumWidth"/> and <see cref="FrustumHeight"/>.
        /// </summary>
        public double FrustumMaximumDiameter { get; set; }


        /// <summary>
        /// Gets near clipping plane if camera and frustum
        /// are valid.  The plane's frame is the same as the camera's
        /// frame.  The origin is located at the intersection of the
        /// camera direction ray and the near clipping plane. The plane's
        /// normal points out of the frustum towards the camera
        /// location.
        /// </summary>
        public NN.Geometry.Plane FrustumNearPlane { get; set; }

        /// <summary>
        /// Gets far clipping plane if camera and frustum
        /// are valid.  The plane's frame is the same as the camera's
        /// frame.  The origin is located at the intersection of the
        /// camera direction ray and the far clipping plane. The plane's
        /// normal points into the frustum towards the camera location.
        /// </summary>
        public NN.Geometry.Plane FrustumFarPlane { get; set; }


        /// <summary>
        /// Gets the frustum left plane that separates visibile from off-screen.
        /// </summary>
        public NN.Geometry.Plane FrustumLeftPlane { get; set; }


        /// <summary>
        /// Gets the frustum right plane that separates visibile from off-screen.
        /// </summary>
        public NN.Geometry.Plane FrustumRightPlane { get; set; }


        /// <summary>
        /// Gets the frustum bottom plane that separates visibile from off-screen.
        /// </summary>
        public NN.Geometry.Plane FrustumBottomPlane { get; set; }

        /// <summary>
        /// Gets the frustum top plane that separates visibile from off-screen.
        /// </summary>
        public NN.Geometry.Plane FrustumTopPlane { get; set; }



        /// <summary>
        /// Gets the location of viewport in pixels.
        /// See documentation for <see cref="SetScreenPort(int, int, int, int, int, int)">SetScreenPort</see>.
        /// </summary>
        /// <returns>The rectangle, or <see cref="System.Drawing.Rectangle.Empty">Empty</see> rectangle on error.</returns>
        public System.Drawing.Rectangle ScreenPort { get; set; }


        /// <summary>
        /// Gets the sceen aspect ratio.
        /// <para>This is width / height.</para>
        /// </summary>
        public double ScreenPortAspect { get; set; }


        /// <summary>
        /// Gets or sets the 1/2 smallest angle. See <see cref="GetCameraAngles"/> for more information.
        /// </summary>
        public double CameraAngle { get; set; }

        /// <summary>
        /// This property assumes the camera is horizontal and crop the
        /// film rather than the image when the aspect of the frustum
        /// is not 36/24.  (35mm film is 36mm wide and 24mm high.)
        /// Setting preserves camera location,
        /// changes the frustum, but maintains the frustum's aspect.
        /// </summary>
        public double Camera35mmLensLength { get; set; }



        /// <summary>
        /// Applies scaling factors to parallel projection clipping coordinates
        /// by setting the m_clip_mod transformation. 
        /// If you want to compress the view projection across the viewing
        /// plane, then set x = 0.5, y = 1.0, and z = 1.0.
        /// </summary>
        public System.Drawing.SizeF ViewScale { get; set; }


        /// <summary>
        /// The current value of the target point.  This point does not play
        /// a role in the view projection calculations.  It can be used as a 
        /// fixed point when changing the camera so the visible regions of the
        /// before and after frustums both contain the region of interest.
        /// The default constructor sets this point on ON_3dPoint::UnsetPoint.
        /// You must explicitly call one SetTargetPoint() functions to set
        /// the target point.
        /// </summary>
        public NN.Geometry.Point3d TargetPoint { get; set; }

        /// <summary>
        /// Sets the viewport's id to the value used to 
        /// uniquely identify this viewport.
        /// There is no approved way to change the viewport 
        /// id once it is set in order to maintain consistency
        /// across multiple viewports and those routines that 
        /// manage them.
        /// </summary>
        public Guid Id { get; set; }

    }
}



