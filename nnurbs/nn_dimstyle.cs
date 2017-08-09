#pragma warning disable 1591
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace NN.DocObjects
{
    public enum DimensionStyleArrowType : int
    {
        /// <summary>2:1</summary>
        SolidTriangle = 0,
        /// <summary></summary>
        Dot = 1,
        /// <summary></summary>
        Tick = 2,
        /// <summary>1:1</summary>
        ShortTriangle = 3,
        /// <summary></summary>
        Arrow = 4,
        /// <summary></summary>
        Rectangle = 5,
        /// <summary>4:1</summary>
        LongTriangle = 6,
        /// <summary>6:1</summary>
        LongerTriangle = 7
    }

    public class DimensionStyle 
    {
        // Represents both a CRhinoDimStyle and an ON_DimStyle. When m_ptr
        // is null, the object uses m_doc and m_id to look up the const
        // CRhinoDimStyle in the dimstyle table.

        readonly Guid m_id = Guid.Empty;

        public DimensionStyle()
        {
        }

        public Guid Id;
        public string Name;


        public int Index;

        public double ExtensionLineExtension;

        public double ExtensionLineOffset;

        public double ArrowLength;

        public double LeaderArrowLength;

        public double CenterMarkSize;

        public double TextGap;

        public double TextHeight;
        public double LengthFactor;

        public double AlternateLengthFactor;
        

        public TextDisplayAlignment TextAlignment;

        public int AngleResolution;


        /// <summary>Linear display precision.</summary>
        public int LengthResolution;

        public int FontIndex;

        public DistanceDisplayMode LengthFormat;

        public DimensionStyleArrowType ArrowType;

        public DimensionStyleArrowType LeaderArrowType;

        public string Prefix;
        public string Suffix;

        public bool IsReference;
    }
}