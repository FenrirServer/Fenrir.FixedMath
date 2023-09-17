using FixedMath;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FixedMath.Tests
{

    public class MatrixComparer : IEqualityComparer<Matrix>
    {
        static public MatrixComparer Epsilon = new MatrixComparer((Fixed)0.000001M);

        private readonly Fixed _epsilon;

        private MatrixComparer(Fixed epsilon)
        {
            _epsilon = epsilon;
        }

        public bool Equals(Matrix x, Matrix y)
        {
            return Fixed.Abs(x.M11 - y.M11) < _epsilon &&
                    Fixed.Abs(x.M12 - y.M12) < _epsilon &&
                    Fixed.Abs(x.M13 - y.M13) < _epsilon &&
                    Fixed.Abs(x.M14 - y.M14) < _epsilon &&
                    Fixed.Abs(x.M21 - y.M21) < _epsilon &&
                    Fixed.Abs(x.M22 - y.M22) < _epsilon &&
                    Fixed.Abs(x.M23 - y.M23) < _epsilon &&
                    Fixed.Abs(x.M24 - y.M24) < _epsilon &&
                    Fixed.Abs(x.M31 - y.M31) < _epsilon &&
                    Fixed.Abs(x.M32 - y.M32) < _epsilon &&
                    Fixed.Abs(x.M33 - y.M33) < _epsilon &&
                    Fixed.Abs(x.M34 - y.M34) < _epsilon &&
                    Fixed.Abs(x.M41 - y.M41) < _epsilon &&
                    Fixed.Abs(x.M42 - y.M42) < _epsilon &&
                    Fixed.Abs(x.M43 - y.M43) < _epsilon &&
                    Fixed.Abs(x.M44 - y.M44) < _epsilon;
        }

        public int GetHashCode(Matrix obj)
        {
            throw new NotImplementedException();
        }
    }

    public class ByteComparer : IEqualityComparer<byte>
    {
        static public ByteComparer Equal = new ByteComparer();

        private ByteComparer()
        {
        }

        public bool Equals(byte x, byte y)
        {
            return x == y;
        }

        public int GetHashCode(byte obj)
        {
            throw new NotImplementedException();
        }
    }

    public class FixedComparer : IEqualityComparer<Fixed>
    {
        static public FixedComparer Epsilon = new FixedComparer((Fixed)0.000001M);

        private readonly Fixed _epsilon;

        private FixedComparer(Fixed epsilon)
        {
            _epsilon = epsilon;
        }

        public bool Equals(Fixed x, Fixed y)
        {
            return Fixed.Abs(x - y) < _epsilon;
        }

        public int GetHashCode(Fixed obj)
        {
            throw new NotImplementedException();
        }
    }

    public class BoundingSphereComparer : IEqualityComparer<BoundingSphere>
    {
        static public BoundingSphereComparer Epsilon = new BoundingSphereComparer((Fixed)0.000001M);

        private readonly Fixed _epsilon;

        private BoundingSphereComparer(Fixed epsilon)
        {
            _epsilon = epsilon;
        }

        public bool Equals(BoundingSphere x, BoundingSphere y)
        {
            return Fixed.Abs(x.Center.X - y.Center.X) < _epsilon &&
                    Fixed.Abs(x.Center.Y - y.Center.Y) < _epsilon &&
                    Fixed.Abs(x.Center.Z - y.Center.Z) < _epsilon &&
                    Fixed.Abs(x.Radius - y.Radius) < _epsilon;
        }

        public int GetHashCode(BoundingSphere obj)
        {
            throw new NotImplementedException();
        }
    }

    public class Vector2Comparer : IEqualityComparer<Vector2>
    {
        static public Vector2Comparer Epsilon = new Vector2Comparer((Fixed)0.000001M);

        private readonly Fixed _epsilon;

        private Vector2Comparer(Fixed epsilon)
        {
            _epsilon = epsilon;
        }

        public bool Equals(Vector2 x, Vector2 y)
        {
            return Fixed.Abs(x.X - y.X) < _epsilon &&
                   Fixed.Abs(x.Y - y.Y) < _epsilon;
        }

        public int GetHashCode(Vector2 obj)
        {
            throw new NotImplementedException();
        }
    }

    public class Vector3Comparer : IEqualityComparer<Vector3>
    {
        static public Vector3Comparer Epsilon = new Vector3Comparer((Fixed)0.000001M);

        private readonly Fixed _epsilon;

        private Vector3Comparer(Fixed epsilon)
        {
            _epsilon = epsilon;
        }

        public bool Equals(Vector3 x, Vector3 y)
        {
            return Fixed.Abs(x.X - y.X) < _epsilon &&
                   Fixed.Abs(x.Y - y.Y) < _epsilon &&
                   Fixed.Abs(x.Z - y.Z) < _epsilon;
        }

        public int GetHashCode(Vector3 obj)
        {
            throw new NotImplementedException();
        }
    }

    public class Vector4Comparer : IEqualityComparer<Vector4>
    {
        static public Vector4Comparer Epsilon = new Vector4Comparer((Fixed)0.001M);

        private readonly Fixed _epsilon;

        private Vector4Comparer(Fixed epsilon)
        {
            _epsilon = epsilon;
        }

        public bool Equals(Vector4 x, Vector4 y)
        {
            return Fixed.Abs(x.X - y.X) < _epsilon &&
                   Fixed.Abs(x.Y - y.Y) < _epsilon &&
                   Fixed.Abs(x.Z - y.Z) < _epsilon &&
                   Fixed.Abs(x.W - y.W) < _epsilon;
        }

        public int GetHashCode(Vector4 obj)
        {
            throw new NotImplementedException();
        }
    }

    public class QuaternionComparer : IEqualityComparer<Quaternion>
    {
        static public QuaternionComparer Epsilon = new QuaternionComparer((Fixed)0.000001M);

        private readonly Fixed _epsilon;

        private QuaternionComparer(Fixed epsilon)
        {
            _epsilon = epsilon;
        }

        public bool Equals(Quaternion x, Quaternion y)
        {
            return Fixed.Abs(x.X - y.X) < _epsilon &&
                   Fixed.Abs(x.Y - y.Y) < _epsilon &&
                   Fixed.Abs(x.Z - y.Z) < _epsilon &&
                   Fixed.Abs(x.W - y.W) < _epsilon;
        }

        public int GetHashCode(Quaternion obj)
        {
            throw new NotImplementedException();
        }
    }
    public class PlaneComparer : IEqualityComparer<Plane>
    {
        static public PlaneComparer Epsilon = new PlaneComparer((Fixed)0.000001M);

        private readonly Fixed _epsilon;

        private PlaneComparer(Fixed epsilon)
        {
            _epsilon = epsilon;
        }

        public bool Equals(Plane x, Plane y)
        {
            return Fixed.Abs(x.Normal.X - y.Normal.X) < _epsilon &&
                   Fixed.Abs(x.Normal.Y - y.Normal.Y) < _epsilon &&
                   Fixed.Abs(x.Normal.Z - y.Normal.Z) < _epsilon &&
                   Fixed.Abs(x.D - y.D) < _epsilon;
        }

        public int GetHashCode(Plane obj)
        {
            throw new NotImplementedException();
        }
    }

    public static class ArrayUtil
    {
        public static T[] ConvertTo<T>(byte[] source) where T : struct
        {
            var sizeOfDest = Marshal.SizeOf(typeof(T));
            var count = source.Length / sizeOfDest;
            var dest = new T[count];

            var pinned = GCHandle.Alloc(source, GCHandleType.Pinned);
            var pointer = pinned.AddrOfPinnedObject();

            for (var i = 0; i < count; i++, pointer += sizeOfDest)
                dest[i] = (T)Marshal.PtrToStructure(pointer, typeof(T));

            pinned.Free();

            return dest;
        }

        public static byte[] ConvertFrom<T>(T[] source) where T : struct
        {
            var sizeOfSource = Marshal.SizeOf(typeof(T));
            var count = source.Length;
            var dest = new byte[sizeOfSource * count];

            var pinned = GCHandle.Alloc(dest, GCHandleType.Pinned);
            var pointer = pinned.AddrOfPinnedObject();

            for (var i = 0; i < count; i++, pointer += sizeOfSource)
                Marshal.StructureToPtr(source[i], pointer, true);

            pinned.Free();

            return dest;
        }
    }

    static class MathUtility
    {
        public static void MinMax(int a, int b, out int min, out int max)
        {
            if (a > b)
            {
                min = b;
                max = a;
            }
            else
            {
                min = a;
                max = b;
            }
        }
    }
}
