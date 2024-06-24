using System;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace FixedMath
{
    /// <summary>
    /// Describes a 2D-rectangle. 
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct FixedRectangle : IEquatable<FixedRectangle>
    {
        #region Private Fields

        private static FixedRectangle emptyRectangle = new FixedRectangle();

        #endregion

        #region Public Fields

        /// <summary>
        /// The x coordinate of the top-left corner of this <see cref="FixedRectangle"/>.
        /// </summary>
        [DataMember]
        public int X;

        /// <summary>
        /// The y coordinate of the top-left corner of this <see cref="FixedRectangle"/>.
        /// </summary>
        [DataMember]
        public int Y;

        /// <summary>
        /// The width of this <see cref="FixedRectangle"/>.
        /// </summary>
        [DataMember]
        public int Width;

        /// <summary>
        /// The height of this <see cref="FixedRectangle"/>.
        /// </summary>
        [DataMember]
        public int Height;

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns a <see cref="FixedRectangle"/> with X=0, Y=0, Width=0, Height=0.
        /// </summary>
        public static FixedRectangle Empty
        {
            get { return emptyRectangle; }
        }

        /// <summary>
        /// Returns the x coordinate of the left edge of this <see cref="FixedRectangle"/>.
        /// </summary>
        public int Left
        {
            get { return this.X; }
        }

        /// <summary>
        /// Returns the x coordinate of the right edge of this <see cref="FixedRectangle"/>.
        /// </summary>
        public int Right
        {
            get { return (this.X + this.Width); }
        }

        /// <summary>
        /// Returns the y coordinate of the top edge of this <see cref="FixedRectangle"/>.
        /// </summary>
        public int Top
        {
            get { return this.Y; }
        }

        /// <summary>
        /// Returns the y coordinate of the bottom edge of this <see cref="FixedRectangle"/>.
        /// </summary>
        public int Bottom
        {
            get { return (this.Y + this.Height); }
        }

        /// <summary>
        /// Whether or not this <see cref="FixedRectangle"/> has a <see cref="Width"/> and
        /// <see cref="Height"/> of 0, and a <see cref="Location"/> of (0, 0).
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return ((((this.Width == 0) && (this.Height == 0)) && (this.X == 0)) && (this.Y == 0));
            }
        }

        /// <summary>
        /// The top-left coordinates of this <see cref="FixedRectangle"/>.
        /// </summary>
        public FixedPoint Location
        {
            get
            {
                return new FixedPoint(this.X, this.Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// The width-height coordinates of this <see cref="FixedRectangle"/>.
        /// </summary>
        public FixedPoint Size
        {
            get
            {
                return new FixedPoint(this.Width,this.Height);
            }
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        /// <summary>
        /// A <see cref="FixedPoint"/> located in the center of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <remarks>
        /// If <see cref="Width"/> or <see cref="Height"/> is an odd number,
        /// the center point will be rounded down.
        /// </remarks>
        public FixedPoint Center
        {
            get
            {
                return new FixedPoint(this.X + (this.Width / 2), this.Y + (this.Height / 2));
            }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.X, "  ",
                    this.Y, "  ",
                    this.Width, "  ",
                    this.Height
                    );
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="FixedRectangle"/> struct, with the specified
        /// position, width, and height.
        /// </summary>
        /// <param name="x">The x coordinate of the top-left corner of the created <see cref="FixedRectangle"/>.</param>
        /// <param name="y">The y coordinate of the top-left corner of the created <see cref="FixedRectangle"/>.</param>
        /// <param name="width">The width of the created <see cref="FixedRectangle"/>.</param>
        /// <param name="height">The height of the created <see cref="FixedRectangle"/>.</param>
        public FixedRectangle(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Creates a new instance of <see cref="FixedRectangle"/> struct, with the specified
        /// location and size.
        /// </summary>
        /// <param name="location">The x and y coordinates of the top-left corner of the created <see cref="FixedRectangle"/>.</param>
        /// <param name="size">The width and height of the created <see cref="FixedRectangle"/>.</param>
        public FixedRectangle(FixedPoint location,FixedPoint size)
        {
            this.X = location.X;
            this.Y = location.Y;
            this.Width = size.X;
            this.Height = size.Y;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Compares whether two <see cref="FixedRectangle"/> instances are equal.
        /// </summary>
        /// <param name="a"><see cref="FixedRectangle"/> instance on the left of the equal sign.</param>
        /// <param name="b"><see cref="FixedRectangle"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(FixedRectangle a, FixedRectangle b)
        {
            return ((a.X == b.X) && (a.Y == b.Y) && (a.Width == b.Width) && (a.Height == b.Height));
        }

        /// <summary>
        /// Compares whether two <see cref="FixedRectangle"/> instances are not equal.
        /// </summary>
        /// <param name="a"><see cref="FixedRectangle"/> instance on the left of the not equal sign.</param>
        /// <param name="b"><see cref="FixedRectangle"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(FixedRectangle a, FixedRectangle b)
        {
            return !(a == b);
        }

        #endregion

        #region Public Methods
       
        /// <summary>
        /// Gets whether or not the provided coordinates lie within the bounds of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="x">The x coordinate of the point to check for containment.</param>
        /// <param name="y">The y coordinate of the point to check for containment.</param>
        /// <returns><c>true</c> if the provided coordinates lie inside this <see cref="FixedRectangle"/>; <c>false</c> otherwise.</returns>
		public bool Contains(int x, int y)
        {
            return ((((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y)) && (y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided coordinates lie within the bounds of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="x">The x coordinate of the point to check for containment.</param>
        /// <param name="y">The y coordinate of the point to check for containment.</param>
        /// <returns><c>true</c> if the provided coordinates lie inside this <see cref="FixedRectangle"/>; <c>false</c> otherwise.</returns>
        public bool Contains(Fixed x, Fixed y)
        {
            return (((((Fixed)this.X <= x) && (x < (Fixed)(this.X + this.Width))) && ((Fixed)this.Y <= y)) && (y < (Fixed)(this.Y + this.Height)));
        }
		
        /// <summary>
        /// Gets whether or not the provided <see cref="FixedPoint"/> lies within the bounds of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="FixedRectangle"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="FixedPoint"/> lies inside this <see cref="FixedRectangle"/>; <c>false</c> otherwise.</returns>
        public bool Contains(FixedPoint value)
        {
            return ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="FixedPoint"/> lies within the bounds of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="FixedRectangle"/>.</param>
        /// <param name="result"><c>true</c> if the provided <see cref="FixedPoint"/> lies inside this <see cref="FixedRectangle"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Contains(ref FixedPoint value, out bool result)
        {
            result = ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="FixedVector2"/> lies within the bounds of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="FixedRectangle"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="FixedVector2"/> lies inside this <see cref="FixedRectangle"/>; <c>false</c> otherwise.</returns>
        public bool Contains(FixedVector2 value)
        {
            return (((((Fixed)this.X <= value.X) && (value.X < (Fixed)(this.X + this.Width))) && ((Fixed)this.Y <= value.Y)) && (value.Y < (Fixed)(this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="FixedVector2"/> lies within the bounds of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="FixedRectangle"/>.</param>
        /// <param name="result"><c>true</c> if the provided <see cref="FixedVector2"/> lies inside this <see cref="FixedRectangle"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Contains(ref FixedVector2 value, out bool result)
        {
            result = (((((Fixed)this.X <= value.X) && (value.X < (Fixed)(this.X + this.Width))) && ((Fixed)this.Y <= value.Y)) && (value.Y < (Fixed)(this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="FixedRectangle"/> lies within the bounds of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="value">The <see cref="FixedRectangle"/> to check for inclusion in this <see cref="FixedRectangle"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="FixedRectangle"/>'s bounds lie entirely inside this <see cref="FixedRectangle"/>; <c>false</c> otherwise.</returns>
        public bool Contains(FixedRectangle value)
        {
            return ((((this.X <= value.X) && ((value.X + value.Width) <= (this.X + this.Width))) && (this.Y <= value.Y)) && ((value.Y + value.Height) <= (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="FixedRectangle"/> lies within the bounds of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="value">The <see cref="FixedRectangle"/> to check for inclusion in this <see cref="FixedRectangle"/>.</param>
        /// <param name="result"><c>true</c> if the provided <see cref="FixedRectangle"/>'s bounds lie entirely inside this <see cref="FixedRectangle"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Contains(ref FixedRectangle value,out bool result)
        {
            result = ((((this.X <= value.X) && ((value.X + value.Width) <= (this.X + this.Width))) && (this.Y <= value.Y)) && ((value.Y + value.Height) <= (this.Y + this.Height)));
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is FixedRectangle) && this == ((FixedRectangle)obj);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="other">The <see cref="FixedRectangle"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(FixedRectangle other)
        {
            return this == other;
        }

        /// <summary>
        /// Gets the hash code of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="FixedRectangle"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + Width.GetHashCode();
                hash = hash * 23 + Height.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Adjusts the edges of this <see cref="FixedRectangle"/> by specified horizontal and vertical amounts. 
        /// </summary>
        /// <param name="horizontalAmount">Value to adjust the left and right edges.</param>
        /// <param name="verticalAmount">Value to adjust the top and bottom edges.</param>
        public void Inflate(int horizontalAmount, int verticalAmount)
        {
            X -= horizontalAmount;
            Y -= verticalAmount;
            Width += horizontalAmount * 2;
            Height += verticalAmount * 2;
        }

        /// <summary>
        /// Adjusts the edges of this <see cref="FixedRectangle"/> by specified horizontal and vertical amounts. 
        /// </summary>
        /// <param name="horizontalAmount">Value to adjust the left and right edges.</param>
        /// <param name="verticalAmount">Value to adjust the top and bottom edges.</param>
        public void Inflate(Fixed horizontalAmount, Fixed verticalAmount)
        {
            X -= (int)horizontalAmount;
            Y -= (int)verticalAmount;
            Width += (int)horizontalAmount * 2;
            Height += (int)verticalAmount * 2;
        }

        /// <summary>
        /// Gets whether or not the other <see cref="FixedRectangle"/> intersects with this rectangle.
        /// </summary>
        /// <param name="value">The other rectangle for testing.</param>
        /// <returns><c>true</c> if other <see cref="FixedRectangle"/> intersects with this rectangle; <c>false</c> otherwise.</returns>
        public bool Intersects(FixedRectangle value)
        {
            return value.Left < Right &&
                   Left < value.Right &&
                   value.Top < Bottom &&
                   Top < value.Bottom;
        }


        /// <summary>
        /// Gets whether or not the other <see cref="FixedRectangle"/> intersects with this rectangle.
        /// </summary>
        /// <param name="value">The other rectangle for testing.</param>
        /// <param name="result"><c>true</c> if other <see cref="FixedRectangle"/> intersects with this rectangle; <c>false</c> otherwise. As an output parameter.</param>
        public void Intersects(ref FixedRectangle value, out bool result)
        {
            result = value.Left < Right &&
                     Left < value.Right &&
                     value.Top < Bottom &&
                     Top < value.Bottom;
        }

        /// <summary>
        /// Creates a new <see cref="FixedRectangle"/> that contains overlapping region of two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="FixedRectangle"/>.</param>
        /// <param name="value2">The second <see cref="FixedRectangle"/>.</param>
        /// <returns>Overlapping region of the two rectangles.</returns>
        public static FixedRectangle Intersect(FixedRectangle value1, FixedRectangle value2)
        {
            FixedRectangle rectangle;
            Intersect(ref value1, ref value2, out rectangle);
            return rectangle;
        }

        /// <summary>
        /// Creates a new <see cref="FixedRectangle"/> that contains overlapping region of two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="FixedRectangle"/>.</param>
        /// <param name="value2">The second <see cref="FixedRectangle"/>.</param>
        /// <param name="result">Overlapping region of the two rectangles as an output parameter.</param>
        public static void Intersect(ref FixedRectangle value1, ref FixedRectangle value2, out FixedRectangle result)
        {
            if (value1.Intersects(value2))
            {
                int right_side = Fixed.Min(value1.X + value1.Width, value2.X + value2.Width);
                int left_side = Fixed.Max(value1.X, value2.X);
                int top_side = Fixed.Max(value1.Y, value2.Y);
                int bottom_side = Fixed.Min(value1.Y + value1.Height, value2.Y + value2.Height);
                result = new FixedRectangle(left_side, top_side, right_side - left_side, bottom_side - top_side);
            }
            else
            {
                result = new FixedRectangle(0, 0, 0, 0);
            }
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="offsetX">The x coordinate to add to this <see cref="FixedRectangle"/>.</param>
        /// <param name="offsetY">The y coordinate to add to this <see cref="FixedRectangle"/>.</param>
        public void Offset(int offsetX, int offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="offsetX">The x coordinate to add to this <see cref="FixedRectangle"/>.</param>
        /// <param name="offsetY">The y coordinate to add to this <see cref="FixedRectangle"/>.</param>
        public void Offset(Fixed offsetX, Fixed offsetY)
        {
            X += (int)offsetX;
            Y += (int)offsetY;
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="amount">The x and y components to add to this <see cref="FixedRectangle"/>.</param>
        public void Offset(FixedPoint amount)
        {
            X += amount.X;
            Y += amount.Y;
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="amount">The x and y components to add to this <see cref="FixedRectangle"/>.</param>
        public void Offset(FixedVector2 amount)
        {
            X += (int)amount.X;
            Y += (int)amount.Y;
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="FixedRectangle"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>] Width:[<see cref="Width"/>] Height:[<see cref="Height"/>]}
        /// </summary>
        /// <returns><see cref="String"/> representation of this <see cref="FixedRectangle"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + " Width:" + Width + " Height:" + Height + "}";
        }

        /// <summary>
        /// Creates a new <see cref="FixedRectangle"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="FixedRectangle"/>.</param>
        /// <param name="value2">The second <see cref="FixedRectangle"/>.</param>
        /// <returns>The union of the two rectangles.</returns>
        public static FixedRectangle Union(FixedRectangle value1, FixedRectangle value2)
        {
            int x = Fixed.Min(value1.X, value2.X);
            int y = Fixed.Min(value1.Y, value2.Y);
            return new FixedRectangle(x, y,
                                 Fixed.Max(value1.Right, value2.Right) - x,
                                     Fixed.Max(value1.Bottom, value2.Bottom) - y);
        }

        /// <summary>
        /// Creates a new <see cref="FixedRectangle"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="FixedRectangle"/>.</param>
        /// <param name="value2">The second <see cref="FixedRectangle"/>.</param>
        /// <param name="result">The union of the two rectangles as an output parameter.</param>
        public static void Union(ref FixedRectangle value1, ref FixedRectangle value2, out FixedRectangle result)
        {
            result.X = Fixed.Min(value1.X, value2.X);
            result.Y = Fixed.Min(value1.Y, value2.Y);
            result.Width = Fixed.Max(value1.Right, value2.Right) - result.X;
            result.Height = Fixed.Max(value1.Bottom, value2.Bottom) - result.Y;
        }

        /// <summary>
        /// Deconstruction method for <see cref="FixedRectangle"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Deconstruct(out int x, out int y, out int width, out int height)
        {
            x = X;
            y = Y;
            width = Width;
            height = Height;
        }

        #endregion
    }
}
