using System;
using System.Runtime.Serialization;
using NN.Runtime.InteropWrappers;

namespace NN.Geometry
{
    /// <summary>
    /// Represents a light that shines in the modeling space.
    /// </summary>
    [Serializable]
    public class Light : GeometryBase
    {
        public Light()
        { }


        /// <summary>
        /// Gets or sets a value that defines if the light is turned on (true) or off (false).
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a light style on this camera.
        /// </summary>
        public LightStyle LightStyle { get; set; }


        /// <summary>
        /// Gets a value indicating whether the light style
        /// is <see cref="LightStyle"/> CameraPoint or WorldPoint.
        /// </summary>
        public bool IsPointLight { get; set; }


        /// <summary>
        /// Gets a value indicating whether the light style
        /// is <see cref="LightStyle"/> CameraDirectional or WorldDirectional.
        /// </summary>
        public bool IsDirectionalLight { get; set; }


        /// <summary>
        /// Gets a value indicating whether the light style
        /// is <see cref="LightStyle"/> CameraSpot or WorldSpot.
        /// </summary>
        public bool IsSpotLight { get; set; }


        /// <summary>
        /// Gets a value indicating whether the light style
        /// is <see cref="LightStyle"/> WorldLinear.
        /// </summary>
        public bool IsLinearLight { get; set; }

        /// <summary>
        /// Gets a value indicating whether the light style
        /// is <see cref="LightStyle"/> WorldRectangular.
        /// </summary>
        public bool IsRectangularLight { get; set; }

        /// <summary>
        /// Gets a value, determined by LightStyle, that explains whether
        /// the camera diretions are relative to World or Camera spaces.
        /// </summary>
        public DocObjects.CoordinateSystem CoordinateSystem { get; set; }


        /// <summary>
        /// Gets or sets the light or 3D position or location.
        /// </summary>
        public Point3d Location { get; set; }

        /// <summary>
        /// Gets or sets the vector direction of the camera.
        /// </summary>
        public Vector3d Direction { get; set; }


        /// <summary>
        /// Gets a perpendicular vector to the camera direction.
        /// </summary>
        public Vector3d PerpendicularDirection { get; set; }

        /// <summary>
        /// Gets or sets the light intensity.
        /// </summary>
        public double Intensity { get; set; }


        /// <summary>
        /// Gets or sets the light power in watts (W).
        /// </summary>
        public double PowerWatts { get; set; }


        /// <summary>
        /// Gets or sets the light power in lumens (lm).
        /// </summary>
        public double PowerLumens { get; set; }

        /// <summary>
        /// Gets or sets the light power in candelas (cd).
        /// </summary>
        public double PowerCandela { get; set; }

        /// <summary>
        /// Gets or sets the ambient color.
        /// </summary>
        public ColorEx Ambient { get; set; }


        /// <summary>
        /// Gets or sets the diffuse color.
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_modifylightcolor.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_modifylightcolor.cs' lang='cs'/>
        /// <code source='examples\py\ex_modifylightcolor.py' lang='py'/>
        /// </example>
        public ColorEx Diffuse { get; set; }

        /// <summary>
        /// Gets or sets the specular color.
        /// </summary>
        public ColorEx Specular { get; set; }


        /// <summary>
        /// Gets or Sets the attenuation vector.
        /// </summary>
        public Vector3d AttenuationVector { get; set; }

        public double Attenuation { get; set; }

        public double SpotAngleRadians { get; set; }

        public double SpotExponent { get; set; }

        public double HotSpot { get; set; }

        public double SpotLightRadiusInner {get; set; }

        public double SpotLightRadiusOutter { get; set; }


        /// <summary>
        /// Gets or sets the height in linear and rectangular lights.
        /// <para>(ignored for non-linear/rectangular lights.)</para>
        /// </summary>
        public Vector3d Length { get; set; }
        

        /// <summary>
        /// Gets or sets the width in linear and rectangular lights.
        /// <para>(ignored for non-linear/rectangular lights.)</para>
        /// </summary>
        public Vector3d Width { get; set; }
        

        /// <summary>
        /// Gets or sets the spot light shadow intensity.
        /// <para>(ignored for non-spot lights.)</para>
        /// </summary>
        public double SpotLightShadowIntensity { get; set; }
        
        /// <summary>
        /// Gets or sets the spot light name.
        /// </summary>
        public string Name { get; set; }
    }
}
