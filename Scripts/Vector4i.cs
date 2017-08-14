using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FGDKit.Base {

    [StructLayout( LayoutKind.Sequential )]
    public struct Vector4i {

        public int x;
        public int y;
        public int z;
        public int w;

        public int this[ int index ] {
            get {
                int result;
                switch ( index ) {
                case 0:
                    result = this.x;
                    break;
                case 1:
                    result = this.y;
                    break;
                case 2:
                    result = this.z;
                    break;
                case 3:
                    result = this.w;
                    break;
                default:
                    throw new IndexOutOfRangeException( "Invalid Vector4 index!" );
                }
                return result;
            }
            set {
                switch ( index ) {
                case 0:
                    this.x = value;
                    break;
                case 1:
                    this.y = value;
                    break;
                case 2:
                    this.z = value;
                    break;
                case 3:
                    this.w = value;
                    break;
                default:
                    throw new IndexOutOfRangeException( "Invalid Vector4 index!" );
                }
            }
        }

        public float sqrMagnitude {
            get {
                return Vector4i.Dot( this, this );
            }
        }

        public static Vector4i zero {
            get {
                return new Vector4i( 0, 0, 0, 0 );
            }
        }

        public static Vector4i one {
            get {
                return new Vector4i( 1, 1, 1, 1 );
            }
        }

        public Vector4i( int x, int y, int z, int w ) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4i( int x, int y, int z ) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 0;
        }

        public Vector4i( int x, int y ) {
            this.x = x;
            this.y = y;
            this.z = 0;
            this.w = 0;
        }

        public void Set( int new_x, int new_y, int new_z, int new_w ) {
            this.x = new_x;
            this.y = new_y;
            this.z = new_z;
            this.w = new_w;
        }

        public static Vector4i Scale( Vector4i a, Vector4i b ) {
            return new Vector4i( a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w );
        }

        public void Scale( Vector4i scale ) {
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
            this.w *= scale.w;
        }

        public override int GetHashCode() {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
        }

        public override bool Equals( object other ) {
            bool result;
            if ( !( other is Vector4i ) ) {
                result = false;
            } else {
                Vector4i vector = ( Vector4i )other;
                result = ( this.x.Equals( vector.x ) && this.y.Equals( vector.y ) && this.z.Equals( vector.z ) && this.w.Equals( vector.w ) );
            }
            return result;
        }

        public static int Dot( Vector4i a, Vector4i b ) {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static Vector4i operator +( Vector4i a, Vector4i b ) {
            return new Vector4i( a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w );
        }

        public static Vector4i operator -( Vector4i a, Vector4i b ) {
            return new Vector4i( a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w );
        }

        public static Vector4i operator -( Vector4i a ) {
            return new Vector4i( -a.x, -a.y, -a.z, -a.w );
        }

        public static Vector4i operator *( Vector4i a, int d ) {
            return new Vector4i( a.x * d, a.y * d, a.z * d, a.w * d );
        }

        public static Vector4i operator *( int d, Vector4i a ) {
            return new Vector4i( a.x * d, a.y * d, a.z * d, a.w * d );
        }

        public static Vector4i operator /( Vector4i a, int d ) {
            return new Vector4i( a.x / d, a.y / d, a.z / d, a.w / d );
        }

        public static bool operator ==( Vector4i lhs, Vector4i rhs ) {
            return Vector4i.SqrMagnitude( lhs - rhs ) == 0;
        }

        public static bool operator !=( Vector4i lhs, Vector4i rhs ) {
            return Vector4i.SqrMagnitude( lhs - rhs ) != 0;
        }

        public override string ToString() {
            return String.Format( "({0}, {1}, {2}, {3})",
                this.x,
                this.y,
                this.z,
                this.w
            );
        }

        public string ToString( string format ) {
            return String.Format( "({0}, {1}, {2}, {3})",
                this.x.ToString( format ),
                this.y.ToString( format ),
                this.z.ToString( format ),
                this.w.ToString( format )
            );
        }

        public static float SqrMagnitude( Vector4i a ) {
            return Vector4i.Dot( a, a );
        }

        public float SqrMagnitude() {
            return Vector4i.Dot( this, this );
        }
    }
}
