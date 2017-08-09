using System;
using System.Runtime.InteropServices;

namespace NN
{

    /// <summary>
    /// Represents two indices: I and J.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 8)]
    [System.Diagnostics.DebuggerDisplay("{m_i}, {m_j}")]
    public struct IndexPair
    {
        int m_i, m_j;

        /// <summary>
        /// Initializes a new instance of <see cref="IndexPair"/> with two indices.
        /// </summary>
        /// <param name="i">A first index.</param>
        /// <param name="j">A second index.</param>
        public IndexPair(int i, int j)
        {
            m_i = i;
            m_j = j;
        }

        /// <summary>
        /// Gets or sets the first, I index.
        /// </summary>
        public int I
        {
            get { return m_i; }
            set { m_i = value; }
        }

        /// <summary>
        /// Gets or sets the second, J index.
        /// </summary>
        public int J
        {
            get { return m_j; }
            set { m_j = value; }
        }
    }

    /// <summary>
    /// Provides constants and static methods that are additional to
    /// <see cref="System.Math"/>.
    /// </summary>
    public static class RhinoMath
    {
        // Only used inside of this class. Not exposed since there is already
        // a Math.PI that people can use
        const double PI = 3.141592653589793238462643;

        /// <summary>
        /// Gets the Zero Tolerance constant (1.0e-12).
        /// </summary>
        public const double ZeroTolerance = 1.0e-12;

        /// <summary>
        /// Gets the Rhino standard Unset value. Use this value rather than Double.NaN when 
        /// a bogus floating point value is required.
        /// </summary>
        public const double UnsetValue = -1.23432101234321e+308;

        /// <summary>
        /// Represents a default value that is used when comparing square roots.
        /// <para>This value is several orders of magnitude larger than <see cref="RhinoMath.ZeroTolerance"/>.</para>
        /// </summary>
        public const double SqrtEpsilon = 1.490116119385000000e-8;

        /// <summary>
        /// Represents the default angle tolerance, used when no other values are provided.
        /// <para>This is one degree, expressed in radians.</para>
        /// </summary>
        public const double DefaultAngleTolerance = PI / 180.0;

        /// <summary>
        /// Gets the single precision floating point number that is considered 'unset' in NN.
        /// </summary>
        public const float UnsetSingle = -1.234321e+38f;

        /// <summary>
        /// Convert an angle from degrees to radians.
        /// </summary>
        /// <param name="degrees">Degrees to convert (180 degrees equals pi radians).</param>
        public static double ToRadians(double degrees)
        {
            return degrees * PI / 180.0;
        }

        /// <summary>
        /// Convert an angle from radians to degrees.
        /// </summary>
        /// <param name="radians">Radians to convert (180 degrees equals pi radians).</param>
        public static double ToDegrees(double radians)
        {
            return radians * 180.0 / PI;
        }

        /// <summary>
        /// Determines whether a <see cref="double"/> value is valid within the RhinoCommon context.
        /// <para>Rhino does not use Double.NaN by convention, so this test evaluates to true if:</para>
        /// <para>x is not equal to RhinoMath.UnsetValue</para>
        /// <para>System.Double.IsNaN(x) evaluates to false</para>
        /// <para>System.Double.IsInfinity(x) evaluates to false</para>
        /// </summary>
        /// <param name="x"><see cref="double"/> number to test for validity.</param>
        /// <returns>true if the number if valid, false if the number is NaN, Infinity or Unset.</returns>
        public static bool IsValidDouble(double x)
        {
            return (x != UnsetValue) && (!double.IsInfinity(x)) && (!double.IsNaN(x));
        }

        /// <summary>
        /// Determines whether a <see cref="float"/> value is valid within the RhinoCommon context.
        /// <para>Rhino does not use Single.NaN by convention, so this test evaluates to true if:</para>
        /// <para>x is not equal to RhinoMath.UnsetValue,</para>
        /// <para>System.Single.IsNaN(x) evaluates to false</para>
        /// <para>System.Single.IsInfinity(x) evaluates to false</para>
        /// </summary>
        /// <param name="x"><see cref="float"/> number to test for validity.</param>
        /// <returns>true if the number if valid, false if the number is NaN, Infinity or Unset.</returns>
        public static bool IsValidSingle(float x)
        {
            return (x != UnsetSingle) && (!float.IsInfinity(x)) && (!float.IsNaN(x));
        }


        /// <summary>
        /// Restricts a <see cref="int"/> to be specified within an interval of two integers.
        /// </summary>
        /// <param name="value">An integer.</param>
        /// <param name="bound1">A first bound.</param>
        /// <param name="bound2">A second bound. This does not necessarily need to be larger or smaller than bound1.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value, int bound1, int bound2)
        {
            int min = bound1;
            int max = bound2;

            if (bound1 > bound2)
            {
                min = bound2;
                max = bound1;
            }
            if (value > max)
                value = max;
            if (value < min)
                value = min;
            return value;
        }

        /// <summary>
        /// Restricts a <see cref="double"/> to be specified within an interval of two numbers.
        /// </summary>
        /// <param name="value">A number.</param>
        /// <param name="bound1">A first bound.</param>
        /// <param name="bound2">A second bound. This does not necessarily need to be larger or smaller than bound1.</param>
        /// <returns>The clamped value.</returns>
        public static double Clamp(double value, double bound1, double bound2)
        {
            double min = bound1;
            double max = bound2;

            if (bound1 > bound2)
            {
                min = bound2;
                max = bound1;
            }
            if (value > max)
                value = max;
            if (value < min)
                value = min;
            return value;
        }

        static uint[] ON_CRC32_ZLIB_TABLE = new uint[] {
                0x00000000, 0x77073096, 0xee0e612c, 0x990951ba, 0x076dc419,
                0x706af48f, 0xe963a535, 0x9e6495a3, 0x0edb8832, 0x79dcb8a4,
                0xe0d5e91e, 0x97d2d988, 0x09b64c2b, 0x7eb17cbd, 0xe7b82d07,
                0x90bf1d91, 0x1db71064, 0x6ab020f2, 0xf3b97148, 0x84be41de,
                0x1adad47d, 0x6ddde4eb, 0xf4d4b551, 0x83d385c7, 0x136c9856,
                0x646ba8c0, 0xfd62f97a, 0x8a65c9ec, 0x14015c4f, 0x63066cd9,
                0xfa0f3d63, 0x8d080df5, 0x3b6e20c8, 0x4c69105e, 0xd56041e4,
                0xa2677172, 0x3c03e4d1, 0x4b04d447, 0xd20d85fd, 0xa50ab56b,
                0x35b5a8fa, 0x42b2986c, 0xdbbbc9d6, 0xacbcf940, 0x32d86ce3,
                0x45df5c75, 0xdcd60dcf, 0xabd13d59, 0x26d930ac, 0x51de003a,
                0xc8d75180, 0xbfd06116, 0x21b4f4b5, 0x56b3c423, 0xcfba9599,
                0xb8bda50f, 0x2802b89e, 0x5f058808, 0xc60cd9b2, 0xb10be924,
                0x2f6f7c87, 0x58684c11, 0xc1611dab, 0xb6662d3d, 0x76dc4190,
                0x01db7106, 0x98d220bc, 0xefd5102a, 0x71b18589, 0x06b6b51f,
                0x9fbfe4a5, 0xe8b8d433, 0x7807c9a2, 0x0f00f934, 0x9609a88e,
                0xe10e9818, 0x7f6a0dbb, 0x086d3d2d, 0x91646c97, 0xe6635c01,
                0x6b6b51f4, 0x1c6c6162, 0x856530d8, 0xf262004e, 0x6c0695ed,
                0x1b01a57b, 0x8208f4c1, 0xf50fc457, 0x65b0d9c6, 0x12b7e950,
                0x8bbeb8ea, 0xfcb9887c, 0x62dd1ddf, 0x15da2d49, 0x8cd37cf3,
                0xfbd44c65, 0x4db26158, 0x3ab551ce, 0xa3bc0074, 0xd4bb30e2,
                0x4adfa541, 0x3dd895d7, 0xa4d1c46d, 0xd3d6f4fb, 0x4369e96a,
                0x346ed9fc, 0xad678846, 0xda60b8d0, 0x44042d73, 0x33031de5,
                0xaa0a4c5f, 0xdd0d7cc9, 0x5005713c, 0x270241aa, 0xbe0b1010,
                0xc90c2086, 0x5768b525, 0x206f85b3, 0xb966d409, 0xce61e49f,
                0x5edef90e, 0x29d9c998, 0xb0d09822, 0xc7d7a8b4, 0x59b33d17,
                0x2eb40d81, 0xb7bd5c3b, 0xc0ba6cad, 0xedb88320, 0x9abfb3b6,
                0x03b6e20c, 0x74b1d29a, 0xead54739, 0x9dd277af, 0x04db2615,
                0x73dc1683, 0xe3630b12, 0x94643b84, 0x0d6d6a3e, 0x7a6a5aa8,
                0xe40ecf0b, 0x9309ff9d, 0x0a00ae27, 0x7d079eb1, 0xf00f9344,
                0x8708a3d2, 0x1e01f268, 0x6906c2fe, 0xf762575d, 0x806567cb,
                0x196c3671, 0x6e6b06e7, 0xfed41b76, 0x89d32be0, 0x10da7a5a,
                0x67dd4acc, 0xf9b9df6f, 0x8ebeeff9, 0x17b7be43, 0x60b08ed5,
                0xd6d6a3e8, 0xa1d1937e, 0x38d8c2c4, 0x4fdff252, 0xd1bb67f1,
                0xa6bc5767, 0x3fb506dd, 0x48b2364b, 0xd80d2bda, 0xaf0a1b4c,
                0x36034af6, 0x41047a60, 0xdf60efc3, 0xa867df55, 0x316e8eef,
                0x4669be79, 0xcb61b38c, 0xbc66831a, 0x256fd2a0, 0x5268e236,
                0xcc0c7795, 0xbb0b4703, 0x220216b9, 0x5505262f, 0xc5ba3bbe,
                0xb2bd0b28, 0x2bb45a92, 0x5cb36a04, 0xc2d7ffa7, 0xb5d0cf31,
                0x2cd99e8b, 0x5bdeae1d, 0x9b64c2b0, 0xec63f226, 0x756aa39c,
                0x026d930a, 0x9c0906a9, 0xeb0e363f, 0x72076785, 0x05005713,
                0x95bf4a82, 0xe2b87a14, 0x7bb12bae, 0x0cb61b38, 0x92d28e9b,
                0xe5d5be0d, 0x7cdcefb7, 0x0bdbdf21, 0x86d3d2d4, 0xf1d4e242,
                0x68ddb3f8, 0x1fda836e, 0x81be16cd, 0xf6b9265b, 0x6fb077e1,
                0x18b74777, 0x88085ae6, 0xff0f6a70, 0x66063bca, 0x11010b5c,
                0x8f659eff, 0xf862ae69, 0x616bffd3, 0x166ccf45, 0xa00ae278,
                0xd70dd2ee, 0x4e048354, 0x3903b3c2, 0xa7672661, 0xd06016f7,
                0x4969474d, 0x3e6e77db, 0xaed16a4a, 0xd9d65adc, 0x40df0b66,
                0x37d83bf0, 0xa9bcae53, 0xdebb9ec5, 0x47b2cf7f, 0x30b5ffe9,
                0xbdbdf21c, 0xcabac28a, 0x53b39330, 0x24b4a3a6, 0xbad03605,
                0xcdd70693, 0x54de5729, 0x23d967bf, 0xb3667a2e, 0xc4614ab8,
                0x5d681b02, 0x2a6f2b94, 0xb40bbe37, 0xc30c8ea1, 0x5a05df1b,
                0x2d02ef8d
        };

        
        public static uint CRC32(uint current_remainder, byte[] buffer)
        {
            int count = buffer.Length;

            /*
            //////////////////////////////////////////////////////////////////////////////////////////
            //
            // ON_CRC32() is a slightly altered version of zlib 1.3.3's crc32()
            // and the zlib "legal stuff" is reproduced below.
            //
            // ON_CRC32() and crc32() compute the same values.  ON_CRC32() was renamed
            // so it wouldn't clash with the other crc32()'s that are out there and the
            // argument order was switched to match that used by the legacy ON_CRC16().
            //
            //////////////////////////////////////////////////////////////////////////////////////////

            zlib.h -- interface of the 'zlib' general purpose compression library
            version 1.1.3, July 9th, 1998

            Copyright (C) 1995-1998 Jean-loup Gailly and Mark Adler

            This software is provided 'as-is', without any express or implied
            warranty.  In no event will the authors be held liable for any damages
            arising from the use of this software.

            Permission is granted to anyone to use this software for any purpose,
            including commercial applications, and to alter it and redistribute it
            freely, subject to the following restrictions:

            1. The origin of this software must not be misrepresented; you must not
               claim that you wrote the original software. If you use this software
               in a product, an acknowledgment in the product documentation would be
               appreciated but is not required.
            2. Altered source versions must be plainly marked as such, and must not be
               misrepresented as being the original software.
            3. This notice may not be removed or altered from any source distribution.

            Jean-loup Gailly        Mark Adler
            jloup@gzip.org          madler@alumni.caltech.edu


            The data format used by the zlib library is described by RFCs (Request for
            Comments) 1950 to 1952 in the files ftp://ds.internic.net/rfc/rfc1950.txt
            (zlib format), rfc1951.txt (deflate format) and rfc1952.txt (gzip format).

            */


            /*
            ON_CRC32_ZLIB_TABLE[] is a table for a byte-wise 32-bit CRC calculation 
            using the generator polynomial:
            x^32+x^26+x^23+x^22+x^16+x^12+x^11+x^10+x^8+x^7+x^5+x^4+x^2+x+1.
            */
            int bufferCount = 0;

            if (count > 0 && buffer.Length >= count)
            {

                // The trailing L was needed long ago when "int" was often a 16 bit integers.
                // Today it is more common for "int" to be a 32 bit integer and
                // L to be a 32 or 64 bit integer.  So, the L is causing more
                // problems that it is fixing.
                // current_remainder ^= 0xffffffffL; 
                current_remainder ^= 0xffffffff;
                while (count >= 8)
                {
                    // while() loop unrolled for speed
                    current_remainder = ON_CRC32_ZLIB_TABLE[((int)current_remainder ^ buffer[bufferCount++]) & 0xff] ^ (current_remainder >> 8);
                    current_remainder = ON_CRC32_ZLIB_TABLE[((int)current_remainder ^ buffer[bufferCount++]) & 0xff] ^ (current_remainder >> 8);
                    current_remainder = ON_CRC32_ZLIB_TABLE[((int)current_remainder ^ buffer[bufferCount++]) & 0xff] ^ (current_remainder >> 8);
                    current_remainder = ON_CRC32_ZLIB_TABLE[((int)current_remainder ^ buffer[bufferCount++]) & 0xff] ^ (current_remainder >> 8);
                    current_remainder = ON_CRC32_ZLIB_TABLE[((int)current_remainder ^ buffer[bufferCount++]) & 0xff] ^ (current_remainder >> 8);
                    current_remainder = ON_CRC32_ZLIB_TABLE[((int)current_remainder ^ buffer[bufferCount++]) & 0xff] ^ (current_remainder >> 8);
                    current_remainder = ON_CRC32_ZLIB_TABLE[((int)current_remainder ^ buffer[bufferCount++]) & 0xff] ^ (current_remainder >> 8);
                    current_remainder = ON_CRC32_ZLIB_TABLE[((int)current_remainder ^ buffer[bufferCount++]) & 0xff] ^ (current_remainder >> 8);
                    count -= 8;
                }
                while (count-- > 0)
                {
                    current_remainder = ON_CRC32_ZLIB_TABLE[((int)current_remainder ^ buffer[bufferCount++]) & 0xff] ^ (current_remainder >> 8);
                }
                // The trailing L was needed long ago when "int" was often a 16 bit integers.
                // Today it is more common for "int" to be a 32 bit integer and
                // L to be a 32 or 64 bit integer.  So, the L is causing more
                // problems than it is fixing.
                //current_remainder ^= 0xffffffffL;
                current_remainder ^= 0xffffffff;
            }

            return current_remainder;
        }

        /// <summary>
        /// Advances the cyclic redundancy check value remainder given a <see cref="double"/>.
        /// http://en.wikipedia.org/wiki/Cyclic_redundancy_check.
        /// </summary>
        /// <param name="currentRemainder">The remainder from which to start.</param>
        /// <param name="value">The value to add to the current remainder.</param>
        /// <returns>The new current remainder.</returns>
        /// <example>
        /// <code source='examples\vbnet\ex_analysismode.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_analysismode.cs' lang='cs'/>
        /// </example>
        
        public static uint CRC32(uint currentRemainder, double value)
        {
            return CRC32(currentRemainder, BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Advances the cyclic redundancy check value remainder given a <see cref="int"/>.
        /// http://en.wikipedia.org/wiki/Cyclic_redundancy_check.
        /// </summary>
        /// <param name="currentRemainder">The remainder from which to start.</param>
        /// <param name="value">The value to add to the current remainder.</param>
        /// <returns>The new current remainder.</returns>
        
        public static uint CRC32(uint currentRemainder, int value)
        {
            return CRC32(currentRemainder, BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Compare two doubles for equality within some "epsilon" range
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static bool EpsilonEquals(double x, double y, double epsilon)
        {
            // IEEE standard says that any comparison between NaN should return false;
            if (double.IsNaN(x) || double.IsNaN(y))
                return false;
            if (double.IsPositiveInfinity(x))
                return double.IsPositiveInfinity(y);
            if (double.IsNegativeInfinity(x))
                return double.IsNegativeInfinity(y);

            // if both are smaller than epsilon, their difference may not be.
            // therefore compare in absolute sense
            if (Math.Abs(x) < epsilon && Math.Abs(y) < epsilon)
            {
                bool result = Math.Abs(x - y) < epsilon;
                return result;
            }

            return (x >= y - epsilon && x <= y + epsilon);
        }

        /// <summary>
        /// Compare to floats for equality within some "epsilon" range
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static bool EpsilonEquals(float x, float y, float epsilon)
        {
            // IEEE standard says that any comparison between NaN should return false;
            if (float.IsNaN(x) || float.IsNaN(y))
                return false;
            if (float.IsPositiveInfinity(x))
                return float.IsPositiveInfinity(y);
            if (float.IsNegativeInfinity(x))
                return float.IsNegativeInfinity(y);

            // if both are smaller than epsilon, their difference may not be.
            // therefore compare in absolute sense
            if (Math.Abs(x) < epsilon && Math.Abs(y) < epsilon)
            {
                bool result = Math.Abs(x - y) < epsilon;
                return result;
            }

            return (x >= y - epsilon && x <= y + epsilon);
        }

    }

    /// <summary>
    /// Provides enumerated values for several unit systems.
    /// </summary>
    public enum UnitSystem : int
    {
        /// <summary>No unit system is specified.</summary>
        None = 0,
        /// <summary>1.0e-10 meters.</summary>
        Angstroms = 12,
        /// <summary>1.0e-9 meters.</summary>
        Nanometers = 13,
        /// <summary>1.0e-6 meters.</summary>
        Microns = 1,
        /// <summary>1.0e-3 meters.</summary>
        Millimeters = 2,
        /// <summary>1.0e-2 meters.</summary>
        Centimeters = 3,
        /// <summary>1.0e-1 meters.</summary>
        Decimeters = 14,
        /// <summary>The base unit in the International System of Units.</summary>
        Meters = 4,
        /// <summary>1.0e+1 meters.</summary>
        Dekameters = 15,
        /// <summary>1.0e+2 meters.</summary>
        Hectometers = 16,
        /// <summary>1.0e+3 meters.</summary>
        Kilometers = 5,
        /// <summary>1.0e+6 meters.</summary>
        Megameters = 17,
        /// <summary>1.0e+9 meters.</summary>
        Gigameters = 18,
        /// <summary>2.54e-8 meters (1.0e-6 inches).</summary>
        Microinches = 6,
        /// <summary>2.54e-5 meters (0.001 inches).</summary>
        Mils = 7,
        /// <summary>0.0254 meters.</summary>
        Inches = 8,
        /// <summary>0.3048 meters (12 inches).</summary>
        Feet = 9,
        /// <summary>0.9144 meters (36 inches).</summary>
        Yards = 19,
        /// <summary>1609.344 meters (5280 feet).</summary>
        Miles = 10,
        /// <summary>Printer distance 1/72 inches (computer points).</summary>
        PrinterPoint = 20,
        /// <summary>Printer distance 1/6 inches (computer picas).</summary>
        PrinterPica = 21,
        /// <summary>
        /// Terrestrial distance, 1852 meters.
        /// <para>Approximately 1 minute of arc on a terrestrial great circle.
        /// See http://en.wikipedia.org/wiki/Nautical_mile .</para>
        /// </summary>
        NauticalMile = 22,
        // astronomical distances
        /// <summary>
        /// Astronomical unit distance.
        /// http://en.wikipedia.org/wiki/Astronomical_unit
        /// 1.495979e+11  // http://units.nist.gov/Pubs/SP811/appenB9.htm
        /// An astronomical unit (au) is the mean distance from the
        /// center of the earth to the center of the sun.
        /// </summary>
        Astronomical = 23,
        /// <summary>
        /// Light Year
        /// <para>http://en.wikipedia.org/wiki/Light_year
        /// 9.46073e+15 meters   http://units.nist.gov/Pubs/SP811/appenB9.htm </para>
        /// <para>A light year is the distance light travels in one Julian year.
        /// The speed of light is exactly 299792458 meters/second.
        /// A Julian year is exactly 365.25 * 86400 seconds and is
        /// approximately the time it takes for one earth orbit.</para>
        /// </summary>
        Lightyears = 24,
        /// <summary>
        /// Parallax Second
        /// http://en.wikipedia.org/wiki/Parsec
        /// 3.085678e+16 meters   http://units.nist.gov/Pubs/SP811/appenB9.htm.
        /// </summary>
        Parsecs = 25,
        /// <summary>
        /// Custom unit systems
        /// x meters with x defined in ON_3dmUnitsAndTolerances.m_custom_unit_scale.
        /// </summary>
        CustomUnitSystem = 11
    }

    namespace Geometry
    {
        /// <summary>
        /// Provides enumerated values for continuity along geometry,
        /// such as continuous first derivative or continuous unit tangent and curvature.
        /// </summary>
        public enum Continuity : int
        {
            /// <summary>
            /// There is no continuity.
            /// </summary>
            None = 0,

            /// <summary>
            /// Continuous Function : Test for parametric continuity. In particular, all types of curves
            /// are considered infinitely continuous at the start/end of the evaluation domain.
            /// </summary>
            C0_continuous = 1,

            /// <summary>
            /// Continuous first derivative : Test for parametric continuity. In particular,
            /// all types of curves are considered infinitely continuous at the start/end
            /// of the evaluation domain.
            /// </summary>
            C1_continuous = 2,

            /// <summary>
            /// Continuous first derivative and second derivative : Test for parametric continuity.
            /// In particular, all types of curves are considered infinitely continuous at the
            /// start/end of the evaluation domain.
            /// </summary>
            C2_continuous = 3,

            /// <summary>
            /// Continuous unit tangent : Test for parametric continuity. In particular, all types of
            /// curves are considered infinitely continuous at the start/end of the evaluation domain.
            /// </summary>
            G1_continuous = 4,

            /// <summary>
            /// Continuous unit tangent and curvature : Test for parametric continuity. In particular,
            /// all types of curves are considered infinitely continuous at the start/end of the
            /// evaluation domain.
            /// </summary>
            G2_continuous = 5,


            /// <summary>
            /// Locus continuous function :
            /// Continuity tests using the following enum values are identical to tests using the
            /// preceding enum values on the INTERIOR of a curve's domain. At the END of a curve
            /// a "locus" test is performed in place of a parametric test. In particular, at the
            /// END of a domain, all open curves are locus discontinuous. At the END of a domain,
            /// all closed curves are at least C0_locus_continuous. By convention all Curves
            /// are considered locus continuous at the START of the evaluation domain. This
            /// convention is not strictly correct, but it was adopted to make iterative kink
            /// finding tools easier to use and so that locus discontinuities are reported once
            /// at the end parameter of a curve rather than twice.
            /// </summary>
            C0_locus_continuous = 6,

            /// <summary>
            /// Locus continuous first derivative :
            /// Continuity tests using the following enum values are identical to tests using the
            /// preceding enum values on the INTERIOR of a curve's domain. At the END of a curve
            /// a "locus" test is performed in place of a parametric test. In particular, at the
            /// END of a domain, all open curves are locus discontinuous. At the END of a domain,
            /// all closed curves are at least C0_locus_continuous. By convention all Curves
            /// are considered locus continuous at the START of the evaluation domain. This
            /// convention is not strictly correct, but it was adopted to make iterative kink
            /// finding tools easier to use and so that locus discontinuities are reported once
            /// at the end parameter of a curve rather than twice.
            /// </summary>
            C1_locus_continuous = 7,

            /// <summary>
            /// Locus continuous first and second derivative :
            /// Continuity tests using the following enum values are identical to tests using the
            /// preceding enum values on the INTERIOR of a curve's domain. At the END of a curve
            /// a "locus" test is performed in place of a parametric test. In particular, at the
            /// END of a domain, all open curves are locus discontinuous. At the END of a domain,
            /// all closed curves are at least C0_locus_continuous. By convention all Curves
            /// are considered locus continuous at the START of the evaluation domain. This
            /// convention is not strictly correct, but it was adopted to make iterative kink
            /// finding tools easier to use and so that locus discontinuities are reported once
            /// at the end parameter of a curve rather than twice.
            /// </summary>
            C2_locus_continuous = 8,

            /// <summary>
            /// Locus continuous unit tangent :
            /// Continuity tests using the following enum values are identical to tests using the
            /// preceding enum values on the INTERIOR of a curve's domain. At the END of a curve
            /// a "locus" test is performed in place of a parametric test. In particular, at the
            /// END of a domain, all open curves are locus discontinuous. At the END of a domain,
            /// all closed curves are at least C0_locus_continuous. By convention all Curves
            /// are considered locus continuous at the START of the evaluation domain. This
            /// convention is not strictly correct, but it was adopted to make iterative kink
            /// finding tools easier to use and so that locus discontinuities are reported once
            /// at the end parameter of a curve rather than twice.
            /// </summary>
            G1_locus_continuous = 9,

            /// <summary>
            /// Locus continuous unit tangent and curvature :
            /// Continuity tests using the following enum values are identical to tests using the
            /// preceding enum values on the INTERIOR of a curve's domain. At the END of a curve
            /// a "locus" test is performed in place of a parametric test. In particular, at the
            /// END of a domain, all open curves are locus discontinuous. At the END of a domain,
            /// all closed curves are at least C0_locus_continuous. By convention all Curves
            /// are considered locus continuous at the START of the evaluation domain. This
            /// convention is not strictly correct, but it was adopted to make iterative kink
            /// finding tools easier to use and so that locus discontinuities are reported once
            /// at the end parameter of a curve rather than twice.
            /// </summary>
            G2_locus_continuous = 10,

            /// <summary>
            /// Analytic discontinuity.
            /// </summary>
            Cinfinity_continuous = 11,
        }

        //public enum ControlPointStyle : int
        //{
        //  None = 0,
        //  NotRational = 1,
        //  HomogeneousRational = 2,
        //  EuclideanRational = 3,
        //  IntrinsicPointStyle = 4,
        //}

        /// <summary>
        /// Defines enumerated values for various mesh types.
        /// </summary>
        public enum MeshType : int
        {
            /// <summary>
            /// The default mesh.
            /// </summary>
            Default = 0,

            /// <summary>
            /// The render mesh.
            /// </summary>
            Render = 1,

            /// <summary>
            /// The analysis mesh.
            /// </summary>
            Analysis = 2,

            /// <summary>
            /// The preview mesh.
            /// </summary>
            Preview = 3,

            /// <summary>
            /// Any mesh that is available.
            /// </summary>
            Any = 4
        }
    }

    namespace DocObjects
    {
        /// <summary>Defines the current working space.</summary>
        public enum ActiveSpace : int
        {
            /// <summary>There is no working space.</summary>
            None = 0,
            /// <summary>3d modeling or "world" space.</summary>
            ModelSpace = 1,
            /// <summary>page/layout/paper/printing space.</summary>
            PageSpace = 2
        }

        /// <summary>
        /// Defines enumerated values for coordinate systems to use as references.
        /// </summary>
        public enum CoordinateSystem : int
        {
            /// <summary>
            /// The world coordinate system. This has origin (0,0,0),
            /// X unit axis is (1, 0, 0) and Y unit axis is (0, 1, 0).
            /// </summary>
            World = 0,

            /// <summary>
            /// The camera coordinate system.
            /// </summary>
            Camera = 1,

            /// <summary>
            /// The clip coordinate system.
            /// </summary>
            Clip = 2,

            /// <summary>
            /// The screen coordinate system.
            /// </summary>
            Screen = 3
        }

        /// <summary>
        /// Defines enumerated values for the display and behavior of single objects.
        /// </summary>
        public enum ObjectMode : int
        {
            ///<summary>Object mode comes from layer.</summary>
            Normal = 0,
            ///<summary>Not visible, object cannot be selected or changed.</summary>
            Hidden = 1,
            ///<summary>Visible, object cannot be selected or changed.</summary>
            Locked = 2,
            ///<summary>
            ///Object is part of an InstanceDefinition. The InstanceDefinition
            ///m_object_uuid[] array will contain this object attribute's uuid.
            ///</summary>
            InstanceDefinitionObject = 3
            //ObjectModeCount = 4
        }

        /// <summary>
        /// Defines enumerated values for the source of display color of single objects.
        /// </summary>
        public enum ObjectColorSource : int
        {
            /// <summary>use color assigned to layer.</summary>
            ColorFromLayer = 0,
            /// <summary>use color assigned to object.</summary>
            ColorFromObject = 1,
            /// <summary>use diffuse render material color.</summary>
            ColorFromMaterial = 2,
            /// <summary>
            /// for objects with parents (like objects in instance references, use parent linetype)
            /// if no parent, treat as color_from_layer.
            /// </summary>
            ColorFromParent = 3
        }

        /// <summary>
        /// Defines enumerated values for the source of plotting/printing color of single objects.
        /// </summary>
        public enum ObjectPlotColorSource : int
        {
            /// <summary>use plot color assigned to layer.</summary>
            PlotColorFromLayer = 0,
            /// <summary>use plot color assigned to object.</summary>
            PlotColorFromObject = 1,
            /// <summary>use display color.</summary>
            PlotColorFromDisplay = 2,
            /// <summary>
            /// for objects with parents (like objects in instance references, use parent plot color)
            /// if no parent, treat as plot_color_from_layer.
            /// </summary>
            PlotColorFromParent = 3
        }

        /// <summary>
        /// Defines enumerated values for the source of plotting/printing weight of single objects.
        /// </summary>
        public enum ObjectPlotWeightSource : int
        {
            /// <summary>use plot color assigned to layer.</summary>
            PlotWeightFromLayer = 0,
            /// <summary>use plot color assigned to object.</summary>
            PlotWeightFromObject = 1,
            /// <summary>
            /// for objects with parents (like objects in instance references, use parent plot color)
            /// if no parent, treat as plot_color_from_layer.
            /// </summary>
            PlotWeightFromParent = 3
        }

        /// <summary>
        /// Defines enumerated values for the source of linetype of single objects.
        /// </summary>
        public enum ObjectLinetypeSource : int
        {
            /// <summary>use line style assigned to layer.</summary>
            LinetypeFromLayer = 0,
            /// <summary>use line style assigned to object.</summary>
            LinetypeFromObject = 1,
            /// <summary>
            /// for objects with parents (like objects in instance references, use parent linetype)
            /// if not parent, treat as linetype_from_layer.
            /// </summary>
            LinetypeFromParent = 3
        }

        /// <summary>
        /// Defines enumerated values for the source of material of single objects.
        /// </summary>
        public enum ObjectMaterialSource : int
        {
            /// <summary>use material assigned to layer.</summary>
            MaterialFromLayer = 0,
            /// <summary>use material assigned to object.</summary>
            MaterialFromObject = 1,
            /// <summary>
            /// for objects with parents, like definition geometry in instance
            /// references and faces in polysurfaces, this value indicates the
            /// material definition should come from the parent. If the object
            /// does not have an obvious "parent", then treat it the same as
            /// material_from_layer.
            /// </summary>
            MaterialFromParent = 3
        }

        /// <summary>
        /// Defines enumerated values for display modes, such as wireframe or shaded.
        /// </summary>
        public enum DisplayMode : int
        {
            /// <summary>
            /// The default display mode.
            /// </summary>
            Default = 0,

            /// <summary>
            /// The wireframe display mode.
            /// <para>Objects are generally only outlined by their corresponding isocurves and edges.</para>
            /// </summary>
            Wireframe = 1,

            /// <summary>
            /// The shaded display mode.
            /// <para>Objects are generally displayed with their corresponding isocurves and edges,
            /// and are filled with their diplay colors.</para>
            /// </summary>
            Shaded = 2,

            /// <summary>
            /// The render display mode.
            /// <para>Objects are generally displayed in a similar way to the one that will be resulting
            /// from rendering.</para>
            /// </summary>
            RenderPreview = 3
        }

        /// <summary>
        /// Defines enumerated values for the display of distances in US customary and Imperial units.
        /// </summary>
        public enum DistanceDisplayMode : int
        {
            /// <summary>
            /// Shows distance decimals.
            /// </summary>
            Decimal = 0,

            /// <summary>
            /// Show feet.
            /// </summary>
            Feet = 1,

            /// <summary>
            /// Show feet and inches.
            /// </summary>
            FeetAndInches = 2
        }

        /// <summary>
        /// Defines enumerated values for the line alignment of text.
        /// </summary>
        public enum TextDisplayAlignment : int
        {
            /// <summary>
            /// Normal alignment.
            /// </summary>
            Normal = 0,

            /// <summary>
            /// Horizontal alignment.
            /// </summary>
            Horizontal = 1,

            /// <summary>
            /// Above line alignment.
            /// </summary>
            AboveLine = 2,

            /// <summary>
            /// In line alignment.
            /// </summary>
            InLine = 3
        }

        /// <summary>
        /// Defines binary mask values for each object type that can be found in a document.
        /// </summary>
        [Flags]
        public enum ObjectType : uint
        {
            /// <summary>
            /// Nothing.
            /// </summary>
            None = 0,

            /// <summary>
            /// A point.
            /// </summary>
            Point = 1,

            /// <summary>
            /// A point set or cloud.
            /// </summary>
            PointSet = 2,

            /// <summary>
            /// A curve.
            /// </summary>
            Curve = 4,

            /// <summary>
            /// A surface.
            /// </summary>
            Surface = 8,

            /// <summary>
            /// A brep.
            /// </summary>
            Brep = 0x10,

            /// <summary>
            /// A mesh.
            /// </summary>
            Mesh = 0x20,
            //Layer = 0x40,
            //Material = 0x80,

            /// <summary>
            /// A rendering light.
            /// </summary>
            Light = 0x100,

            /// <summary>
            /// An annotation.
            /// </summary>
            Annotation = 0x200,
            //UserData = 0x400,

            /// <summary>
            /// A block definition.
            /// </summary>
            InstanceDefinition = 0x800,

            /// <summary>
            /// A block reference.
            /// </summary>
            InstanceReference = 0x1000,

            /// <summary>
            /// A text dot.
            /// </summary>
            TextDot = 0x2000,

            /// <summary>Selection filter value - not a real object type.</summary>
            Grip = 0x4000,

            /// <summary>
            /// A detail.
            /// </summary>
            Detail = 0x8000,

            /// <summary>
            /// A hatch.
            /// </summary>
            Hatch = 0x10000,

            /// <summary>
            /// A morph control.
            /// </summary>
            MorphControl = 0x20000,

            /// <summary>
            /// A brep loop.
            /// </summary>
            BrepLoop = 0x80000,
            /// <summary>Selection filter value - not a real object type.</summary>
            PolysrfFilter = 0x200000,
            /// <summary>Selection filter value - not a real object type.</summary>
            EdgeFilter = 0x400000,
            /// <summary>Selection filter value - not a real object type.</summary>
            PolyedgeFilter = 0x800000,

            /// <summary>
            /// A mesh vertex.
            /// </summary>
            MeshVertex = 0x01000000,

            /// <summary>
            /// A mesh edge.
            /// </summary>
            MeshEdge = 0x02000000,

            /// <summary>
            /// A mesh face.
            /// </summary>
            MeshFace = 0x04000000,

            /// <summary>
            /// A cage.
            /// </summary>
            Cage = 0x08000000,

            /// <summary>
            /// A phantom object.
            /// </summary>
            Phantom = 0x10000000,

            /// <summary>
            /// A clipping plane.
            /// </summary>
            ClipPlane = 0x20000000,

            /// <summary>
            /// An extrusion.
            /// </summary>
            Extrusion = 0x40000000,

            /// <summary>
            /// All bits set.
            /// </summary>
            AnyObject = 0xFFFFFFFF
        }

        /// <summary>
        /// Defines bit mask values to represent object decorations.
        /// </summary>
        [Flags]
        public enum ObjectDecoration : int
        {
            /// <summary>There are no object decorations.</summary>
            None = 0,
            /// <summary>Arrow head at start.</summary>
            StartArrowhead = 0x08,
            /// <summary>Arrow head at end.</summary>
            EndArrowhead = 0x10,
            /// <summary>Arrow head at start and end.</summary>
            BothArrowhead = 0x18
        }
    }
}

namespace NN.Geometry
{
    /// <summary>
    /// Defines enumerated values to represent light styles or types, such as directional or spotlight.
    /// </summary>
    public enum LightStyle : int
    {
        /// <summary>
        /// No light type. This is the default value of the enumeration type.
        /// </summary>
        None = 0,
        /// <summary>
        /// Light location and direction in camera coordinates.
        /// +x points to right, +y points up, +z points towards camera.
        /// </summary>
        CameraDirectional = 4,
        /// <summary>
        /// Light location and direction in camera coordinates.
        /// +x points to right, +y points up, +z points towards camera.
        /// </summary>
        CameraPoint = 5,
        /// <summary>
        /// Light location and direction in camera coordinates.
        /// +x points to right, +y points up, +z points towards camera.
        /// </summary>
        CameraSpot = 6,
        /// <summary>Light location and direction in world coordinates.</summary>
        WorldDirectional = 7,
        /// <summary>Light location and direction in world coordinates.</summary>
        WorldPoint = 8,
        /// <summary>Light location and direction in world coordinates.</summary>
        WorldSpot = 9,
        /// <summary>Ambient light.</summary>
        Ambient = 10,
        /// <summary>Linear light in world coordinates.</summary>
        WorldLinear = 11,
        /// <summary>Rectangular light in world coordinates.</summary>
        WorldRectangular = 12
    }

    /// <summary>
    /// Defines enumerated values to represent component index types.
    /// </summary>
    public enum ComponentIndexType : int
    {
        /// <summary>
        /// Not used. This is the default value of the enumeration type.
        /// </summary>
        InvalidType = 0,

        /// <summary>
        /// Targets a brep vertex index.
        /// </summary>
        BrepVertex = 1,

        /// <summary>
        /// Targets a brep edge index.
        /// </summary>
        BrepEdge = 2,

        /// <summary>
        /// Targets a brep face index.
        /// </summary>
        BrepFace = 3,

        /// <summary>
        /// Targets a brep trim index.
        /// </summary>
        BrepTrim = 4,

        /// <summary>
        /// Targets a brep loop index.
        /// </summary>
        BrepLoop = 5,

        /// <summary>
        /// Targets a mesh vertex index.
        /// </summary>
        MeshVertex = 11,

        /// <summary>
        /// Targets a mesh topology vertex index.
        /// </summary>
        MeshTopologyVertex = 12,

        /// <summary>
        /// Targets a mesh topology edge index.
        /// </summary>
        MeshTopologyEdge = 13,

        /// <summary>
        /// Targets a mesh face index.
        /// </summary>
        MeshFace = 14,

        /// <summary>
        /// Targets an instance definition part index.
        /// </summary>
        InstanceDefinitionPart = 21,

        /// <summary>
        /// Targets a polycurve segment index.
        /// </summary>
        PolycurveSegment = 31,

        /// <summary>
        /// Targets a pointcloud point index.
        /// </summary>
        PointCloudPoint = 41,

        /// <summary>
        /// Targets a group member index.
        /// </summary>
        GroupMember = 51,

        /// <summary>
        /// Targets a linear dimension point index.
        /// </summary>
        DimLinearPoint = 100,

        /// <summary>
        /// Targets a radial dimension point index.
        /// </summary>
        DimRadialPoint = 101,

        /// <summary>
        /// Targets an angular dimension point index.
        /// </summary>
        DimAngularPoint = 102,

        /// <summary>
        /// Targets an ordinate dimension point index.
        /// </summary>
        DimOrdinatePoint = 103,

        /// <summary>
        /// Targets a text point index.
        /// </summary>
        DimTextPoint = 104,

        /// <summary>
        /// Targets no specific type.
        /// </summary>
        NoType = 0x0FFFFFFF // switched to 0fffffff from 0xffffffff in order to maintain cls compliance
    }

    /// <summary>
    /// Represents an index of an element contained in another object.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 8)]
    public struct ComponentIndex  { 
        private uint m_type;
        private int m_index;

#if RHINO3DMIO || RHINOCOMMON
        public ComponentIndex(Rhino.Geometry.ComponentIndex f)
        {

            m_index = f.Index;
            m_type = (uint)f.ComponentIndexType;
        }

        public bool CopyFrom(Rhino.Geometry.ComponentIndex from)
        {
            m_index = from.Index;
            m_type = (uint)from.ComponentIndexType;

            return true;
        }


        public bool CopyTo(Rhino.Geometry.BrepTrim to)
        {
            return true;
        }
#endif
         


        /// <summary>
        /// Construct component index with a specific type/index combination
        /// </summary>
        /// <param name="type"></param>
        /// <param name="index"></param>
        public ComponentIndex(ComponentIndexType type, int index)
        {
            m_type = (uint)type;
            m_index = index;
        }

        /// <summary>
        /// The interpretation of Index depends on the Type value.
        /// Type             m_index interpretation (0 based indices)
        /// no_type            used when context makes it clear what array is being index
        /// brep_vertex        Brep.m_V[] array index
        /// brep_edge          Brep.m_E[] array index
        /// brep_face          Brep.m_F[] array index
        /// brep_trim          Brep.m_T[] array index
        /// brep_loop          Brep.m_L[] array index
        /// mesh_vertex        Mesh.m_V[] array index
        /// meshtop_vertex     MeshTopology.m_topv[] array index
        /// meshtop_edge       MeshTopology.m_tope[] array index
        /// mesh_face          Mesh.m_F[] array index
        /// idef_part          InstanceDefinition.m_object_uuid[] array index
        /// polycurve_segment  PolyCurve::m_segment[] array index
        /// dim_linear_point   LinearDimension2::POINT_INDEX
        /// dim_radial_point   RadialDimension2::POINT_INDEX
        /// dim_angular_point  AngularDimension2::POINT_INDEX
        /// dim_ordinate_point OrdinateDimension2::POINT_INDEX
        /// dim_text_point     TextEntity2 origin point.
        /// </summary>
        public ComponentIndexType ComponentIndexType
        {
            get
            {
                if (0xFFFFFFFF == m_type)
                    return ComponentIndexType.NoType;
                int t = (int)m_type;
                return (ComponentIndexType)t;
            }
        }
        /// <summary>
        /// The interpretation of m_index depends on the m_type value.
        /// m_type             m_index interpretation (0 based indices)
        /// no_type            used when context makes it clear what array is being index
        /// brep_vertex        Brep.m_V[] array index
        /// brep_edge          Brep.m_E[] array index
        /// brep_face          Brep.m_F[] array index
        /// brep_trim          Brep.m_T[] array index
        /// brep_loop          Brep.m_L[] array index
        /// mesh_vertex        Mesh.m_V[] array index
        /// meshtop_vertex     MeshTopology.m_topv[] array index
        /// meshtop_edge       MeshTopology.m_tope[] array index
        /// mesh_face          Mesh.m_F[] array index
        /// idef_part          InstanceDefinition.m_object_uuid[] array index
        /// polycurve_segment  PolyCurve::m_segment[] array index
        /// dim_linear_point   LinearDimension2::POINT_INDEX
        /// dim_radial_point   RadialDimension2::POINT_INDEX
        /// dim_angular_point  AngularDimension2::POINT_INDEX
        /// dim_ordinate_point OrdinateDimension2::POINT_INDEX
        /// dim_text_point     TextEntity2 origin point.
        /// </summary>
        public int Index
        {
            get { return m_index; }
            set { m_index = value; }
        }

        public uint TypeU
        {
            get { return m_type; }
            set { m_type = value; }
        }

        /// <summary>
        /// The unset value of component index.
        /// </summary>
        public static ComponentIndex Unset
        {
            get { return new ComponentIndex(ComponentIndexType.InvalidType, -1); }
        }

    }
}
