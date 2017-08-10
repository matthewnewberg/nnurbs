using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using NN.Runtime;
using NN.Runtime.InteropWrappers;

namespace NN.Geometry
{
    /// <summary>
    /// Specifies enumerated constants used to indicate the internal alignment and justification of text.
    /// </summary>
    [Flags]
    public enum TextJustification : int
    {
        /// <summary>
        /// The default justification.
        /// </summary>
        None = 0,

        /// <summary>
        /// Left justification.
        /// </summary>
        Left = 1 << 0,

        /// <summary>
        /// Center justification.
        /// </summary>
        Center = 1 << 1,

        /// <summary>
        /// Right justification.
        /// </summary>
        Right = 1 << 2,

        /// <summary>
        /// Bottom inner alignment.
        /// </summary>
        Bottom = 1 << 16,

        /// <summary>
        /// Middle inner alignment.
        /// </summary>
        Middle = 1 << 17,

        /// <summary>
        /// Top inner alignment.
        /// </summary>
        Top = 1 << 18,

        /// <summary>
        /// Combination of left justification and bottom alignment.
        /// </summary>
        BottomLeft = Bottom | Left,

        /// <summary>
        /// Combination of center justification and bottom alignment.
        /// </summary>
        BottomCenter = Bottom | Center,

        /// <summary>
        /// Combination of right justification and bottom alignment.
        /// </summary>
        BottomRight = Bottom | Right,

        /// <summary>
        /// Combination of left justification and middle alignment.
        /// </summary>
        MiddleLeft = Middle | Left,

        /// <summary>
        /// Combination of center justification and middle alignment.
        /// </summary>
        MiddleCenter = Middle | Center,

        /// <summary>
        /// Combination of right justification and middle alignment.
        /// </summary>
        MiddleRight = Middle | Right,

        /// <summary>
        /// Combination of left justification and top alignment.
        /// </summary>
        TopLeft = Top | Left,

        /// <summary>
        /// Combination of center justification and top alignment.
        /// </summary>
        TopCenter = Top | Center,

        /// <summary>
        /// Combination of right justification and top alignment.
        /// </summary>
        TopRight = Top | Right
    }

    /// <summary>
    /// Provides a common base class to all annotation geometry.
    /// <para>This class refers to the geometric element that is independent from the document.</para>
    /// </summary>
    public class AnnotationBase : GeometryBase
    {

        /// <summary>
        /// Protected constructor for internal use.
        /// </summary>
        public AnnotationBase() { }
        

        /// <summary>
        /// Gets the numeric value, depending on geometry type.
        /// <para>LinearDimension: distance between arrow tips</para>
        /// <para>RadialDimension: radius or diamater depending on type</para>
        /// <para>AngularDimension: angle in degrees</para>
        /// <para>Leader or Text: UnsetValue</para>
        /// </summary>
        public double NumericValue { get; set; }


        /// <summary>
        /// Gets or sets the text for this annotation.
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_edittext.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_edittext.cs' lang='cs'/>
        /// <code source='examples\py\ex_edittext.py' lang='py'/>
        /// </example>
        public string Text { get; set; }


        /// <summary>
        /// Gets or sets a formula for this annotation.
        /// </summary>
        public string TextFormula { get; set; }

        /// <summary>
        /// Gets or sets the text height in model units.
        /// </summary>
        public double TextHeight { get; set; }

        /// <summary>
        /// Gets or sets the plane containing the annotation.
        /// </summary>
        public Plane Plane { get; set; }


        /// <summary>
        /// Index of DimensionStyle in document DimStyle table used by the dimension.
        /// </summary>
        public int Index { get; set; }

    }

    /// <summary>
    /// Represents a linear dimension.
    /// <para>This entity refers to the geometric element that is independent from the document.</para>
    /// </summary>
    public class LinearDimension : AnnotationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearDimension"/> class.
        /// </summary>
        public LinearDimension()
        { }

        /// <summary>
        /// Gets the distance between arrow tips.
        /// </summary>
        public double DistanceBetweenArrowTips { get; set; }

        /// <summary>
        /// Index of DimensionStyle in document DimStyle table used by the dimension.
        /// </summary>
        public int DimensionStyleIndex { get; set; }

        const int ext0_pt_index = 0;   // end of first extension line
        const int arrow0_pt_index = 1; // arrowhead tip on first extension line
        const int ext1_pt_index = 2;   // end of second extension line
        const int arrow1_pt_index = 3; // arrowhead tip on second extension line
        const int userpositionedtext_pt_index = 4;

        /// <summary>
        /// Gets the end of the first extension line.
        /// </summary>
        public Point2d ExtensionLine1End { get; set; }


        /// <summary>
        /// Gets the end of the second extension line.
        /// </summary>
        public Point2d ExtensionLine2End { get; set; }


        /// <summary>
        /// Gets the arrow head end of the first extension line.
        /// </summary>
        public Point2d Arrowhead1End { get; set; }

        /// <summary>
        /// Gets the arrow head end of the second extension line.
        /// </summary>
        public Point2d Arrowhead2End { get; set; }

        /// <summary>
        /// Gets and sets the position of text on the plane.
        /// </summary>
        public Point2d TextPosition { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this annotation is aligned.
        /// </summary>
        public bool Aligned { get; set; }

    }

    /// <summary>
    /// Represents a dimension of a circular entity that can be measured with radius or diameter.
    /// <para>This entity refers to the geometric element that is independent from the document.</para>
    /// </summary>
    public class RadialDimension : AnnotationBase
    {
        public bool IsDiameterDimension { get; set; }
    }

    /// <summary>
    /// Represents a dimension of an entity that can be measured with an angle.
    /// <para>This entity refers to the geometric element that is independent from the document.</para>
    /// </summary>
    public class AngularDimension : AnnotationBase
    {
        public AngularDimension() { }
    }

    /// <summary>
    /// Represents the geometry of a dimension that displays a coordinate of a point.
    /// <para>This class refers to the geometric element that is independent from the document.</para>
    /// </summary>
    public class OrdinateDimension : AnnotationBase
    {

        public OrdinateDimension() { }
    }

    /// <summary>
    /// Represents text geometry.
    /// <para>This class refers to the geometric element that is independent from the document.</para>
    /// </summary>
    [Serializable]
    public class TextEntity : AnnotationBase
    {
        public TextEntity()
        {
        }

        public int FontIndex { get; set; }
        
        /// <summary>
        /// Gets or sets the justification of text in relation to its base point.
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_textjustify.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_textjustify.cs' lang='cs'/>
        /// <code source='examples\py\ex_textjustify.py' lang='py'/>
        /// </example>
        public TextJustification Justification { get; set; }
        
        /// <summary>
        /// Determines whether or not to draw a Text Mask
        /// </summary>
        public bool MaskEnabled { get; set; }
        

        /// <summary>
        /// If true, the viewport's color is used for the mask color. If
        /// false, the color defined by MaskColor is used
        /// </summary>
        public bool MaskUsesViewportColor { get; set; }
        
        /// <summary>
        /// Color to use for drawing a text mask when it is enabled. If the mask is
        /// enabled and MaskColor is ColorEx.Transparent, then the
        /// viewport's color will be used for the MaskColor
        /// </summary>
        public ColorEx MaskColor { get; set; }
        

        /// <summary>
        /// distance around text to display mask
        /// </summary>
        public double MaskOffset { get; set; }
        
        /// <summary>
        /// Scale annotation according to detail scale factor in paperspace
        /// or by 1.0 in paperspace and not in a detail
        /// Otherwise, dimscale or text scale is used
        /// </summary>
        public bool AnnotativeScalingEnabled { get; set; }
    }

    /// <summary>
    /// Represents a leader, or an annotation entity with text that resembles an arrow pointing to something.
    /// <para>This class refers to the geometric element that is independent from the document.</para>
    /// </summary>
    [Serializable]
    public class Leader : AnnotationBase
    {
        public Leader() { }
    }

    /// <summary>
    /// Represents a text dot, or an annotation entity with text that always faces the camera and always has the same size.
    /// <para>This class refers to the geometric element that is independent from the document.</para>
    /// </summary>
    [Serializable]
    public class TextDot : GeometryBase
    {
        /// <summary>
        /// Protected constructor used in serialization.
        /// </summary>
        public TextDot() { }

        
        /// <summary>
        /// Gets or sets the position of the textdot.
        /// </summary>
        public Point3d Point { get; set; }
        
        /// <summary>
        /// Gets or sets the text of the textdot.
        /// </summary>
        public string Text { get; set; }
        

        /// <summary>
        /// Height of font used for displaying the dot
        /// </summary>
        public int FontHeight { get; set; }
        
        /// <summary>Font face used for displaying the dot</summary>
        public string FontFace { get; set; }
    }
}
