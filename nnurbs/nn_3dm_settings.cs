using System;
using NN.Geometry;
using NN.Runtime.InteropWrappers;

// Most of these should not need to be wrapped. Some of their
// functionality is merged into other wrapper classes
namespace NN.DocObjects
{
    //public class ON_3dmUnitsAndTolerances{}
    //public class ON_3dmAnnotationSettings { }
    //public class ON_3dmConstructionPlaneGridDefaults { }

    // Can't add a cref to an XML comment here since the NamedConstructionPlaneTable
    // is not included in the OpenNURBS flavor build of RhinoCommon

    /// <summary>
    /// Represents a contruction plane inside the document.
    /// <para>Use NN.DocObjects.Tables.NamedConstructionPlaneTable
    /// methods and indexers to add and access a <see cref="ConstructionPlane"/>.</para>
    /// </summary>
    public class ConstructionPlane
    {

        /// <summary>
        /// Initializes a new instance of <see cref="ConstructionPlane"/>.
        /// </summary>
        public ConstructionPlane()
        {
            Plane = Plane.WorldXY;
            GridSpacing = 1.0;
            GridLineCount = 70;
            ThickLineFrequency = 5;
            DepthBuffered = true;
            ShowGrid = true;
            ShowAxes = true;
        }

        /// <summary>
        /// Gets or sets the geometric plane to use for construction.
        /// </summary>
        public Plane Plane { get; set; }

        /// <summary>
        /// Gets or sets the distance between grid lines.
        /// </summary>
        public double GridSpacing { get; set; }

        /// <summary>
        /// when "grid snap" is enabled, the distance between snap points.
        /// Typically this is the same distance as grid spacing.
        /// </summary>
        public double SnapSpacing { get; set; }


        /// <summary>
        /// Gets or sets the total amount of grid lines in each direction.
        /// </summary>
        public int GridLineCount { get; set; }


        /// <summary>
        /// Gets or sets the recurrence of a wider line on the grid.
        /// <para>0: No lines are thick, all are drawn thin.</para>
        /// <para>1: All lines are thick.</para>
        /// <para>2: Every other line is thick.</para>
        /// <para>3: One line in three lines is thick (and two are thin).</para>
        /// <para>4: ...</para>
        /// </summary>
        public int ThickLineFrequency { get; set; }


        /// <summary>
        /// Gets or sets whether the grid is drawn on top of geometry.
        /// <para>false=grid is always drawn behind 3d geometry</para>
        /// <para>true=grid is drawn at its depth as a 3d plane and grid lines obscure things behind the grid.</para>
        /// </summary>
        public bool DepthBuffered { get; set; }


        /// <summary>
        /// Gets or sets the name of the grid.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets whether the grid itself should be visible. 
        /// </summary>
        public bool ShowGrid { get; set; }


        /// <summary>
        /// Gets or sets whether the axes of the grid shuld be visible.
        /// </summary>
        public bool ShowAxes { get; set; }


        /// <summary>
        /// Gets or sets the color of the thinner, less prominent line.
        /// </summary>
        public ColorEx ThinLineColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the thicker, wider line.
        /// </summary>
        public ColorEx ThickLineColor { get; set; }


        /// <summary>
        /// Gets or sets the color of the grid X-axis mark.
        /// </summary>
        public ColorEx GridXColor { get; set; }


        /// <summary>
        /// Gets or sets the color of the grid Y-axis mark.
        /// </summary>
        public ColorEx GridYColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the grid Z-axis mark.
        /// </summary>
        public ColorEx GridZColor { get; set; }
    }


    /// <summary>
    /// Represents the name and orientation of a View (and named view).
    /// <para>views can be thought of as cameras.</para>
    /// </summary>
    public class ViewInfo
    {
        public ViewInfo() { }

        internal ViewInfo(Guid id, bool namedViewTable)
        {
            Id = id;
            NamedViewTable = namedViewTable;
        }

        /// 
        /// <summary>
        /// Gets or sets the name of the NamedView.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Gets the viewport, or viewing frustum, associated with this view.
        /// </summary>
        public ViewportInfo Viewport { get; set; }

        public Guid Id { get; set; }
        public bool NamedViewTable { get; set; }

    }

    /// <summary>
    /// Contains information about the model's position in latitude, longitude,
    /// and elevation for GIS mapping applications.
    /// </summary>
    public class EarthAnchorPoint
    {
        /// <summary>
        /// Gets or sets a point latitude on earth, in decimal degrees.
        /// +90 = north pole, 0 = equator, -90 = south pole.
        /// </summary>
        public double EarthBasepointLatitude { get; set; }

        /// <summary>
        /// Gets or sets the point longitude on earth, in decimal degrees.
        /// <para>0 = prime meridian (Greenwich meridian)</para>
        /// <para>Values increase towards West</para>
        /// </summary>
        public double EarthBasepointLongitude { get; set; }


        /// <summary>
        /// Gets or sets the point elevation on earth, in meters.
        /// </summary>
        public double EarthBasepointElevation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the zero level convention relating to a location on Earth.
        /// </summary>
        public BasepointZero EarthBasepointElevationZero { get; set; }

        /// <summary>Corresponding model point in model coordinates.</summary>
        public Point3d ModelBasePoint { get; set; }


        /// <summary>Earth directions in model coordinates.</summary>
        public Vector3d ModelNorth { get; set; }

        /// <summary>Earth directions in model coordinates.</summary>
        public Vector3d ModelEast { get; set; }

        public string Name { get; set; }


        /// <summary>
        /// Gets or sets the long form of the identifying information about this location.
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// Returns a plane in model coordinates whose X axis points East,
        /// Y axis points North and Z axis points Up. The origin
        /// is set to ModelBasepoint.
        /// </summary>
        /// <returns>A plane value. This might be invalid on error.</returns>
        public Plane ModelCompass { get; set; }
    }

    /// <summary>
    /// Specifies enumerated constants used to indicate the zero level convention relating to a location on Earth.
    /// <para>This is used in conjunction with the <see cref="EarthAnchorPoint"/> class.</para>
    /// </summary>
    public enum BasepointZero
    {
        /// <summary>
        /// The ground level is the convention for 0.
        /// </summary>
        GroundLevel = 0,

        /// <summary>
        /// The mean sea level is the convention for 0.
        /// </summary>
        MeanSeaLevel = 1,

        /// <summary>
        /// The center of the planet is the convention for 0.
        /// </summary>
        CenterOfEarth = 2,
    }
}

namespace NN.Render
{
    /// <summary>
    /// Contains settings used in rendering.
    /// </summary>
    public class RenderSettings
    {
        /// <summary>
        /// Gets or sets the ambient light color used in rendering.
        /// </summary>
        public ColorEx AmbientLight { get; set; }


        /// <summary>
        /// Gets or sets the background top color used in rendering.
        /// <para>Sets also the background color if a solid background color is set.</para>
        /// </summary>
        public ColorEx BackgroundColorTop { get; set; }

        /// <summary>
        /// Gets or sets the background bottom color used in rendering.
        /// </summary>
        public ColorEx BackgroundColorBottom { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to render using lights that are on layers that are off.
        /// </summary>
        public bool UseHiddenLights { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to render using depth cues.
        /// <para>These are clues to help the perception of position and orientation of objects in the image.</para>
        /// </summary>
        public bool DepthCue { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to render using flat shading.
        /// </summary>
        public bool FlatShade { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to render back faces.
        /// </summary>
        public bool RenderBackfaces { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to instruct the rendering engine to show points.
        /// </summary>
        public bool RenderPoints { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to instruct the rendering engine to show curves.
        /// </summary>
        public bool RenderCurves { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to instruct the rendering engine to show isocurves.
        /// </summary>
        public bool RenderIsoparams { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to instruct the rendering engine to show mesh edges.
        /// </summary>
        public bool RenderMeshEdges { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to instruct the rendering engine to show annotations,
        /// such as linear dimensions or angular dimensions.
        /// </summary>
        public bool RenderAnnotations { get; set; }


        public int AntialiasLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the resolution of the
        /// viewport being rendered or ImageSize when rendering
        /// </summary>
        public bool UseViewportSize { get; set; }

        /// <summary>
        /// unit system to use when converting image pixel size and dpi information
        /// into a print size.  Default = inches
        /// </summary>
        public UnitSystem ImageUnitSystem { get; set; }

        /// <summary>
        /// Number of dots/inch (dots=pixels) to use when printing and saving
        /// bitmaps. The default is 72.0 dots/inch.
        /// </summary>
        public double ImageDpi { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the size of the rendering result if
        /// UseViewportSize is set to false.  If UseViewportSize is set to true,
        /// then this value is ignored.
        /// </summary>
        public System.Drawing.Size ImageSize { get; set; }

        /// <summary>
        /// 0=none, 1=normal, 2=best.
        /// </summary>
        public int ShadowmapLevel { get; set; }


        /// <summary>
        /// How the viewport's backgroun should be filled.
        /// </summary>
        public Display.BackgroundStyle BackgroundStyle { get; set; }
    }
}

namespace NN.Display
{
    /// <summary>
    /// Contains enumerated constants that define how the background of
    /// a viewport should be filled.
    /// </summary>
    public enum BackgroundStyle : int
    {
        /// <summary>Single solid color fill.</summary>
        SolidColor = 0,
        /// <summary>Simple image background wallpaper.</summary>
        WallpaperImage = 1,
        /// <summary>Two color top/bottom color gradient.</summary>
        Gradient = 2,
        /// <summary>Using a special environment.</summary>
        Environment = 3
    }

}

namespace NN.FileIO
{
    /// <summary>
    /// Contains settings used within the whole 3dm file.
    /// </summary>
    public class File3dmSettings
    {

        public File3dmSettings() { }
#if RHINO3DMIO || RHINOCOMMON
        public File3dmSettings(Rhino.FileIO.File3dmSettings f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.FileIO.File3dmSettings from)
        {
            this.ModelUrl = from.ModelUrl;
            this.ModelBasepoint =  new NN.Geometry.Point3d(from.ModelBasepoint);
            this.ModelAbsoluteTolerance = from.ModelAbsoluteTolerance;
            this.ModelAngleToleranceRadians = from.ModelAngleToleranceRadians;
            this.ModelAngleToleranceDegrees = from.ModelAngleToleranceDegrees;
            this.ModelRelativeTolerance = from.ModelRelativeTolerance;
            this.PageAbsoluteTolerance = from.PageAbsoluteTolerance;
            this.PageAngleToleranceRadians = from.PageAngleToleranceRadians;
            this.PageAngleToleranceDegrees = from.PageAngleToleranceDegrees;
            this.PageRelativeTolerance = from.PageRelativeTolerance;
            this.ModelUnitSystem = (UnitSystem) from.ModelUnitSystem;
            this.PageUnitSystem = (UnitSystem) from.PageUnitSystem;
            return true;
        }


        public bool CopyTo(Rhino.FileIO.File3dmSettings to)
        {
            to.ModelUrl = this.ModelUrl;
            to.ModelBasepoint = this.ModelBasepoint.RhinoObject();
            to.ModelAbsoluteTolerance = this.ModelAbsoluteTolerance;
            to.ModelAngleToleranceRadians = this.ModelAngleToleranceRadians;
            to.ModelAngleToleranceDegrees = this.ModelAngleToleranceDegrees;
            to.ModelRelativeTolerance = this.ModelRelativeTolerance;
            to.PageAbsoluteTolerance = this.PageAbsoluteTolerance;
            to.PageAngleToleranceRadians = this.PageAngleToleranceRadians;
            to.PageAngleToleranceDegrees = this.PageAngleToleranceDegrees;
            to.PageRelativeTolerance = this.PageRelativeTolerance;
            to.ModelUnitSystem = (Rhino.UnitSystem) this.ModelUnitSystem;
            to.PageUnitSystem =  (Rhino.UnitSystem) this.PageUnitSystem;
            return true;
        }
#endif


            /// <summary>
            /// Gets or sets a Uniform Resource Locator (URL) direction for the model.
            /// </summary>
        public string ModelUrl { get; set; }


        /// <summary>
        /// Gets or sets the model basepoint that is used when the file is read as an instance definition.
        /// <para>This point is mapped to the origin in the instance definition.</para>
        /// </summary>
        public Point3d ModelBasepoint { get; set; }


        /// <summary>Gets or sets the model space absolute tolerance.</summary>
        public double ModelAbsoluteTolerance { get; set; }

        /// <summary>Gets or sets the model space angle tolerance.</summary>
        public double ModelAngleToleranceRadians { get; set; }

        /// <summary>Gets or sets the model space angle tolerance.</summary>
        public double ModelAngleToleranceDegrees { get; set; }

        /// <summary>Gets or sets the model space relative tolerance.</summary>
        public double ModelRelativeTolerance { get; set; }

        /// <summary>Gets or sets the page space absolute tolerance.</summary>
        public double PageAbsoluteTolerance { get; set; }

        /// <summary>Gets or sets the page space angle tolerance.</summary>
        public double PageAngleToleranceRadians { get; set; }

        /// <summary>Gets or sets the page space angle tolerance.</summary>
        public double PageAngleToleranceDegrees { get; set; }

        /// <summary>Gets or sets the page space relative tolerance.</summary>
        public double PageRelativeTolerance { get; set; }


        /// <summary>
        /// Gets or sets the model unit system, using <see cref="NN.UnitSystem"/> enumeration.
        /// </summary>
        public UnitSystem ModelUnitSystem { get; set; }

        /// <summary>
        /// Gets or sets the page unit system, using <see cref="NN.UnitSystem"/> enumeration.
        /// </summary>
        public UnitSystem PageUnitSystem { get; set; }
    }
}