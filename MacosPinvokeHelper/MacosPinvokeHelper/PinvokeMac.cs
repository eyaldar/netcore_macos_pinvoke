using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MacosPinvokeHelper
{
    public enum CGWindowListOption : uint {
        All = 0,
        OnScreenOnly = 1,
        OnScreenAboveWindow = 2,
        OnScreenBelowWindow = 4,
        IncludingWindow = 8,
        ExcludeDesktopElements = 16
    }

    public enum CFStringEncoding : uint
    {
        UTF16 = 0x0100,
        UTF16BE = 0x10000100,
        UTF16LE = 0x14000100,
        ASCII = 0x0600
    }

    public enum ApplicationPolicy : uint {
        Regular = 0,
        Accessory = 1,
        Prohibited = 2
    }

    public static class PinvokeMac
    {
        public static IntPtr GetSelector(string name)
        {
            IntPtr cfstrSelector = CreateCFString(name);
            IntPtr selector = NSSelectorFromString(cfstrSelector);
            CFRelease(cfstrSelector);
            return selector;
        }

        private const string FoundationFramework = "/System/Library/Frameworks/Foundation.framework/Foundation";
        private const string AppKitFramework = "/System/Library/Frameworks/AppKit.framework/AppKit";

        public unsafe static IntPtr CreateCFString(string aString)
        {
            var bytes = Encoding.Unicode.GetBytes(aString);
            fixed (byte* b = bytes)
            {
                var cfStr = CFStringCreateWithBytes(IntPtr.Zero, (IntPtr)b, bytes.Length, CFStringEncoding.UTF16, false);
                return cfStr;
            }
        }

        // warning: this doesn't call retain/release on the elements in the array
        public unsafe static IntPtr CreateCFArray(IntPtr[] objectes)
        {
            fixed (IntPtr* vals = objectes)
            {
                return CFArrayCreate(IntPtr.Zero, (IntPtr)vals, objectes.Length, IntPtr.Zero);
            }
        }

        public static string NSStringToUTF8String(IntPtr nsString)
        {
            var strPtr = PinvokeMac.objc_msgSend_retIntPtr(
                nsString,
                PinvokeMac.GetSelector("UTF8String"));

            return Marshal.PtrToStringUTF8(strPtr);
        }

        [DllImport(FoundationFramework)]
        public static extern IntPtr CFStringCreateWithBytes(IntPtr allocator, IntPtr buffer, long bufferLength, CFStringEncoding encoding, bool isExternalRepresentation);

        [DllImport(FoundationFramework)]
        public static extern IntPtr CFArrayCreate(IntPtr allocator, IntPtr values, long numValues, IntPtr callbackStruct);

        [DllImport(FoundationFramework)]
        public static extern void CFRetain(IntPtr handle);

        [DllImport(FoundationFramework)]
        public static extern void CFRelease(IntPtr handle);

        [DllImport(AppKitFramework, CharSet = CharSet.Ansi)]
        public static extern IntPtr objc_getClass(string name);

        [DllImport(AppKitFramework)]
        public static extern IntPtr NSSelectorFromString(IntPtr cfstr);

        [DllImport(FoundationFramework, EntryPoint = "objc_msgSend")]
        public static extern int objc_msgSend_retInt(IntPtr target, IntPtr selector);

        [DllImport(FoundationFramework, EntryPoint = "objc_msgSend")]
        public static extern IntPtr objc_msgSend_retIntPtr(IntPtr target, IntPtr selector);

        [DllImport(FoundationFramework, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend_retVoid_IntPtr_IntPtr(IntPtr target, IntPtr selector, IntPtr param1, IntPtr param2);

        [DllImport(FoundationFramework, EntryPoint = "objc_msgSend")]
        public static extern IntPtr objc_msgSend_retIntPtr_IntPtr(IntPtr target, IntPtr selector, IntPtr param);

        [DllImport(FoundationFramework, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend_retVoid(IntPtr target, IntPtr selector);

        [DllImport(@"/System/Library/Frameworks/QuartzCore.framework/QuartzCore")]
        public static extern IntPtr CGWindowListCopyWindowInfo(CGWindowListOption option, uint relativeToWindow);
    }
}
