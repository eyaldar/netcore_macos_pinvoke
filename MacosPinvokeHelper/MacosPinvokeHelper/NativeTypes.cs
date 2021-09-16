﻿// !!! WARNING - GENERATED CODE - DO NOT EDIT !!!
//
// Generated by Drawing.tt, a T4 template.
//
// Drawing.cs: basic types with 32 or 64 bit member sizes:
//
//   - CGSize { nfloat, nfloat }
//   - CGPoint { nfloat, nfloat }
//   - CGRect { nfloat, nfloat, nfloat, nfloat }
//
// If ARCH_32 is defined, the underlying types for n* types will be
// 32 bit (int, uint, float). If not defined, the underlying types
// will be 64 bit (long, ulong, double).
//
// Authors:
//   Aaron Bockover <abock@xamarin.com>
//   Miguel de Icaza <miguel@xamarin.com>
//
// Copyright 2020 Microsoft Corp. All Rights Reserved.
// Copyright 2013 Xamarin, Inc. All rights reserved.
//

using System;
#if !NO_SYSTEM_DRAWING
using System.Drawing;
#endif
using System.Globalization;
using System.Runtime.InteropServices;

namespace CoreGraphics
{
	[Serializable]
	public struct CGPoint : IEquatable<CGPoint>
	{
		public static readonly CGPoint Empty;

		public static bool operator ==(CGPoint l, CGPoint r)
		{
			// the following version of Equals cannot be removed by the linker, while == can be
			return l.Equals(r);
		}

		public static bool operator !=(CGPoint l, CGPoint r)
		{
			return l.x != r.x || l.y != r.y;
		}

		public static CGPoint operator +(CGPoint l, CGSize r)
		{
			return new CGPoint(l.x + r.Width, l.y + r.Height);
		}

		public static CGPoint operator -(CGPoint l, CGSize r)
		{
			return new CGPoint(l.x - r.Width, l.y - r.Height);
		}

		public static CGPoint Add(CGPoint point, CGSize size)
		{
			return point + size;
		}

		public static CGPoint Subtract(CGPoint point, CGSize size)
		{
			return point - size;
		}

		nfloat x;
		nfloat y;

		public nfloat X
		{
			get { return x; }
			set { x = value; }
		}

		public nfloat Y
		{
			get { return y; }
			set { y = value; }
		}

		public bool IsEmpty
		{
			get { return x == 0.0 && y == 0.0; }
		}

		public CGPoint(nfloat x, nfloat y)
		{
			this.x = x;
			this.y = y;
		}

		public CGPoint(double x, double y)
		{
			this.x = (nfloat)x;
			this.y = (nfloat)y;
		}

		public CGPoint(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public CGPoint(CGPoint point)
		{
			this.x = point.x;
			this.y = point.y;
		}

		public override bool Equals(object obj)
		{
			return (obj is CGPoint t) && Equals(t);
		}

		public bool Equals(CGPoint point)
		{
			return point.x == x && point.y == y;
		}

		public override int GetHashCode()
		{
			var hash = 23;
			hash = hash * 31 + x.GetHashCode();
			hash = hash * 31 + y.GetHashCode();
			return hash;
		}

		public void Deconstruct(out nfloat x, out nfloat y)
		{
			x = X;
			y = Y;
		}

		public override string ToString()
		{
			return String.Format("{{X={0}, Y={1}}}",
				x.ToString(CultureInfo.CurrentCulture),
				y.ToString(CultureInfo.CurrentCulture)
			);
		}
	}

	[Serializable]
	public struct CGSize : IEquatable<CGSize>
	{
		public static readonly CGSize Empty;

		public static bool operator ==(CGSize l, CGSize r)
		{
			// the following version of Equals cannot be removed by the linker, while == can be
			return l.Equals(r);
		}

		public static bool operator !=(CGSize l, CGSize r)
		{
			return l.width != r.width || l.height != r.height;
		}

		public static CGSize operator +(CGSize l, CGSize r)
		{
			return new CGSize(l.width + r.Width, l.height + r.Height);
		}

		public static CGSize operator -(CGSize l, CGSize r)
		{
			return new CGSize(l.width - r.Width, l.height - r.Height);
		}

		public static explicit operator CGPoint(CGSize size)
		{
			return new CGPoint(size.Width, size.Height);
		}

		public static CGSize Add(CGSize size1, CGSize size2)
		{
			return size1 + size2;
		}

		public static CGSize Subtract(CGSize size1, CGSize size2)
		{
			return size1 - size2;
		}

		nfloat width;
		nfloat height;

		public nfloat Width
		{
			get { return width; }
			set { width = value; }
		}

		public nfloat Height
		{
			get { return height; }
			set { height = value; }
		}

		public bool IsEmpty
		{
			get { return width == 0.0 && height == 0.0; }
		}

		public CGSize(nfloat width, nfloat height)
		{
			this.width = width;
			this.height = height;
		}

		public CGSize(double width, double height)
		{
			this.width = (nfloat)width;
			this.height = (nfloat)height;
		}

		public CGSize(float width, float height)
		{
			this.width = width;
			this.height = height;
		}

		public CGSize(CGSize size)
		{
			this.width = size.width;
			this.height = size.height;
		}

		public CGSize(CGPoint point)
		{
			this.width = point.X;
			this.height = point.Y;
		}

		public override bool Equals(object obj)
		{
			return (obj is CGSize t) && Equals(t);
		}

		public bool Equals(CGSize size)
		{
			return size.width == width && size.height == height;
		}

		public override int GetHashCode()
		{
			var hash = 23;
			hash = hash * 31 + width.GetHashCode();
			hash = hash * 31 + height.GetHashCode();
			return hash;
		}

		public void Deconstruct(out nfloat width, out nfloat height)
		{
			width = Width;
			height = Height;
		}

		public CGSize ToRoundedCGSize()
		{
			return new CGSize((nfloat)Math.Round(width), (nfloat)Math.Round(height));
		}

		public CGPoint ToCGPoint()
		{
			return (CGPoint)this;
		}

		public override string ToString()
		{
			return String.Format("{{Width={0}, Height={1}}}",
				width.ToString(CultureInfo.CurrentCulture),
				height.ToString(CultureInfo.CurrentCulture)
			);
		}
	}


	[Serializable]
	public struct CGRect : IEquatable<CGRect>
	{
		public static bool operator ==(CGRect left, CGRect right)
		{
			// the following version of Equals cannot be removed by the linker, while == can be
			return left.Equals(right);
		}

		public static bool operator !=(CGRect left, CGRect right)
		{
			return
				left.X != right.X ||
				left.Y != right.Y ||
				left.Width != right.Width ||
				left.Height != right.Height;
		}

		public static CGRect Intersect(CGRect a, CGRect b)
		{
			// MS.NET returns a non-empty rectangle if the two rectangles
			// touch each other
			if (!a.IntersectsWithInclusive(b))
			{
				return new CGRect();
			}

			return FromLTRB(
				(nfloat)Math.Max(a.Left, b.Left),
				(nfloat)Math.Max(a.Top, b.Top),
				(nfloat)Math.Min(a.Right, b.Right),
				(nfloat)Math.Min(a.Bottom, b.Bottom)
			);
		}

		public void Intersect(CGRect rect)
		{
			this = CGRect.Intersect(this, rect);
		}

		public static CGRect Union(CGRect a, CGRect b)
		{
			return FromLTRB(
				(nfloat)Math.Min(a.Left, b.Left),
				(nfloat)Math.Min(a.Top, b.Top),
				(nfloat)Math.Max(a.Right, b.Right),
				(nfloat)Math.Max(a.Bottom, b.Bottom)
			);
		}

		public static CGRect FromLTRB(nfloat left, nfloat top, nfloat right, nfloat bottom)
		{
			return new CGRect(left, top, right - left, bottom - top);
		}

		public static CGRect Inflate(CGRect rect, nfloat x, nfloat y)
		{
			var inflated = new CGRect(rect.X, rect.Y, rect.Width, rect.Height);
			inflated.Inflate(x, y);
			return inflated;
		}

		nfloat x;
		nfloat y;
		nfloat width;
		nfloat height;

		public bool IsEmpty
		{
			get { return width == 0.0 || height == 0.0; }
		}

		public nfloat X
		{
			get { return x; }
			set { x = value; }
		}

		public nfloat Y
		{
			get { return y; }
			set { y = value; }
		}

		public nfloat Width
		{
			get { return width; }
			set { width = value; }
		}

		public nfloat Height
		{
			get { return height; }
			set { height = value; }
		}

		public nfloat Top
		{
			get { return Y; }
		}

		public nfloat Bottom
		{
			get { return Y + Height; }
		}

		public nfloat Left
		{
			get { return X; }
		}

		public nfloat Right
		{
			get { return X + Width; }
		}

		public CGPoint Location
		{
			get { return new CGPoint(x, y); }
			set
			{
				x = value.X;
				y = value.Y;
			}
		}

		public CGSize Size
		{
			get { return new CGSize(width, height); }
			set
			{
				width = value.Width;
				height = value.Height;
			}
		}

		public CGRect(CGPoint location, CGSize size)
		{
			x = location.X;
			y = location.Y;
			width = size.Width;
			height = size.Height;
		}

		public CGRect(nfloat x, nfloat y, nfloat width, nfloat height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public CGRect(double x, double y, double width, double height)
		{
			this.x = (nfloat)x;
			this.y = (nfloat)y;
			this.width = (nfloat)width;
			this.height = (nfloat)height;
		}


		public CGRect(float x, float y, float width, float height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public bool Contains(nfloat x, nfloat y)
		{
			return
				x >= Left &&
				x < Right &&
				y >= Top &&
				y < Bottom;
		}

		public bool Contains(float x, float y)
		{
			return Contains((nfloat)x, (nfloat)y);
		}

		public bool Contains(double x, double y)
		{
			return Contains((nfloat)x, (nfloat)y);
		}

		public bool Contains(CGPoint point)
		{
			return Contains(point.X, point.Y);
		}

		public bool Contains(CGRect rect)
		{
			return
				X <= rect.X &&
				Right >= rect.Right &&
				Y <= rect.Y &&
				Bottom >= rect.Bottom;
		}

		public void Inflate(nfloat x, nfloat y)
		{
			this.x -= x;
			this.y -= y;
			width += x * 2;
			height += y * 2;
		}

		public void Inflate(float x, float y)
		{
			Inflate((nfloat)x, (nfloat)y);
		}

		public void Inflate(double x, double y)
		{
			Inflate((nfloat)x, (nfloat)y);
		}

		public void Inflate(CGSize size)
		{
			Inflate(size.Width, size.Height);
		}

		public void Offset(nfloat x, nfloat y)
		{
			X += x;
			Y += y;
		}

		public void Offset(float x, float y)
		{
			Offset((nfloat)x, (nfloat)y);
		}

		public void Offset(double x, double y)
		{
			Offset((nfloat)x, (nfloat)y);
		}

		public void Offset(CGPoint pos)
		{
			Offset(pos.X, pos.Y);
		}

		public bool IntersectsWith(CGRect rect)
		{
			return !(
				Left >= rect.Right ||
				Right <= rect.Left ||
				Top >= rect.Bottom ||
				Bottom <= rect.Top
			);
		}

		private bool IntersectsWithInclusive(CGRect r)
		{
			return !(
				Left > r.Right ||
				Right < r.Left ||
				Top > r.Bottom ||
				Bottom < r.Top
			);
		}

		public override bool Equals(object obj)
		{
			return (obj is CGRect rect) && Equals(rect);
		}

		public bool Equals(CGRect rect)
		{
			return
				x == rect.x &&
				y == rect.y &&
				width == rect.width &&
				height == rect.height;
		}

		public override int GetHashCode()
		{
			var hash = 23;
			hash = hash * 31 + x.GetHashCode();
			hash = hash * 31 + y.GetHashCode();
			hash = hash * 31 + width.GetHashCode();
			hash = hash * 31 + height.GetHashCode();
			return hash;
		}

		public override string ToString()
		{
			return String.Format("{{X={0},Y={1},Width={2},Height={3}}}",
				x, y, width, height);
		}

		public void Deconstruct(out nfloat x, out nfloat y, out nfloat width, out nfloat height)
		{
			x = X;
			y = Y;
			width = Width;
			height = Height;
		}

		public void Deconstruct(out CGPoint location, out CGSize size)
		{
			location = Location;
			size = Size;
		}
	}

	internal static class NativeDrawingMethods
	{
		internal const string CG = "/System/Library/Frameworks/ApplicationServices.framework/Frameworks/CoreGraphics.framework/CoreGraphics";
		[DllImport(CG)]
		[return: MarshalAs(UnmanagedType.I1)]
		internal extern static bool CGRectMakeWithDictionaryRepresentation(IntPtr dict, out CGRect rect);
		[DllImport(CG)]
		[return: MarshalAs(UnmanagedType.I1)]
		internal extern static bool CGPointMakeWithDictionaryRepresentation(IntPtr dict, out CGPoint point);
		[DllImport(CG)]
		[return: MarshalAs(UnmanagedType.I1)]
		internal extern static bool CGSizeMakeWithDictionaryRepresentation(IntPtr dict, out CGSize point);

		[DllImport(CG)]
		internal extern static IntPtr CGRectCreateDictionaryRepresentation(CGRect rect);
		[DllImport(CG)]
		internal extern static IntPtr CGSizeCreateDictionaryRepresentation(CGSize size);
		[DllImport(CG)]
		internal extern static IntPtr CGPointCreateDictionaryRepresentation(CGPoint point);
	}
}
