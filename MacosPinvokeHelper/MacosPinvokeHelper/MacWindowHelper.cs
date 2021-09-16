using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CoreGraphics;

namespace MacosPinvokeHelper
{
    public class MacWindowHelper : IDisposable
    {
        public static readonly IntPtr NSWorkspaceClass;

        static MacWindowHelper()
        {
            NSWorkspaceClass = PinvokeMac.objc_getClass("NSWorkspace");
        }

        private IntPtr sharedWorkspace;
        private IntPtr SharedWorkspace
        {
            get
            {
                if(sharedWorkspace == IntPtr.Zero)
                {
                    sharedWorkspace = PinvokeMac.objc_msgSend_retIntPtr(
                        NSWorkspaceClass,
                        PinvokeMac.GetSelector("sharedWorkspace"));
                }

                return sharedWorkspace;
            }
        }

        public IntPtr[] VisibleWindows(HashSet<int> relevantPids) 
        {
            var windowInfoPtr = PinvokeMac.CGWindowListCopyWindowInfo(CGWindowListOption.OnScreenOnly | CGWindowListOption.ExcludeDesktopElements, 0);

            PinvokeMac.NSArrayForeach(windowInfoPtr, (windowDataPtr) =>
            {
                try
                { 
                    var kCGWindowOwnerPID = PinvokeMac.CreateCFString("kCGWindowOwnerPID");
                    var ownerPidCFNumber = PinvokeMac.objc_msgSend_retIntPtr_IntPtr(
                        windowDataPtr,
                        PinvokeMac.GetSelector("valueForKey:"),
                        kCGWindowOwnerPID);
                    PinvokeMac.CFNumberGetValue(ownerPidCFNumber, (IntPtr)9, out int ownerPid);

                    if (!relevantPids.Contains(ownerPid))
                    {
                        return;
                    }

                    var kCGWindowBoundsKey = PinvokeMac.CreateCFString("kCGWindowBounds");
                    var rect = PinvokeMac.objc_msgSend_retIntPtr_IntPtr(
                        windowDataPtr,
                        PinvokeMac.GetSelector("valueForKey:"),
                        kCGWindowBoundsKey);

                    var kCGWindowNumber = PinvokeMac.CreateCFString("kCGWindowNumber");
                    var windowNumberCfNumber = PinvokeMac.objc_msgSend_retIntPtr_IntPtr(
                        windowDataPtr,
                        PinvokeMac.GetSelector("valueForKey:"),
                        kCGWindowNumber);
                    PinvokeMac.CFNumberGetValue(windowNumberCfNumber, (IntPtr)9, out int windowNumber);

                    NativeDrawingMethods.CGRectMakeWithDictionaryRepresentation(rect, out CGRect cgRect);

                    Console.WriteLine($"pid: {ownerPid} windowNumber: {windowNumber} Top: {cgRect.Top} Left: {cgRect.Left} Bottom: {cgRect.Bottom} Right: {cgRect.Right}");

                    PinvokeMac.CFRelease(kCGWindowNumber);
                    PinvokeMac.CFRelease(kCGWindowOwnerPID);
                    PinvokeMac.CFRelease(kCGWindowBoundsKey);
                    PinvokeMac.CFRelease(rect);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }); 

            return new IntPtr[0];
        }

        public IntPtr[] VisibleApplications
        {
            get
            {
                var list = new List<IntPtr>();

                var runningApplications = PinvokeMac.objc_msgSend_retIntPtr(
                    SharedWorkspace,
                    PinvokeMac.GetSelector("runningApplications"));

                PinvokeMac.NSArrayForeach(runningApplications, (appPtr) =>
                {
                    var activityPolicy = PinvokeMac.objc_msgSend_retInt(
                        appPtr,
                        PinvokeMac.GetSelector("activationPolicy"));

                    if (activityPolicy == (int)ApplicationPolicy.Regular)
                    {
                        list.Add(appPtr);
                    }
                });

                PinvokeMac.CFRelease(runningApplications);

                return list.ToArray();
            }
        }

        public IntPtr FrontmostApplication
        {
            get
            {
                var frontmostApplication = PinvokeMac.objc_msgSend_retIntPtr(
                    SharedWorkspace,
                    PinvokeMac.GetSelector("frontmostApplication"));

                return frontmostApplication;
            }
        }

        public int GetApplicationPid(IntPtr applicationPtr)
        {
            var pid = PinvokeMac.objc_msgSend_retInt(
                applicationPtr,
                PinvokeMac.GetSelector("processIdentifier"));

            return pid;
        }

        public string GetApplicationName(IntPtr applicationPtr)
        {
            var localizedNameNSString = PinvokeMac.objc_msgSend_retIntPtr(
                applicationPtr,
                PinvokeMac.GetSelector("localizedName"));

            return PinvokeMac.NSStringToUTF8String(localizedNameNSString);
        }

        public bool IsApplicationActive(IntPtr applicationPtr)
        {
            var isActive = PinvokeMac.objc_msgSend_retInt(
                applicationPtr,
                PinvokeMac.GetSelector("isActive"));

            return isActive == 1;
        }

        #region Disposable Pattern

        ~MacWindowHelper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            PinvokeMac.CFRelease(SharedWorkspace);
        }

        protected virtual void Dispose(bool disposing)
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        } 

        #endregion
    }
}
