using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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

        public IntPtr[] VisibleApplications
        {
            get
            {
                var list = new List<IntPtr>();

                var runningApplications = PinvokeMac.objc_msgSend_retIntPtr(
                    SharedWorkspace,
                    PinvokeMac.GetSelector("runningApplications"));
                int runningApplicationCount = PinvokeMac.objc_msgSend_retInt(
                    runningApplications,
                    PinvokeMac.GetSelector("count"));

                for (int i = 0; i < runningApplicationCount; i++)
                {
                    var appPtr = PinvokeMac.objc_msgSend_retIntPtr_IntPtr(
                        runningApplications,
                        PinvokeMac.GetSelector("objectAtIndex:"),
                        (IntPtr)i);
                    var activityPolicy = PinvokeMac.objc_msgSend_retInt(
                        appPtr,
                        PinvokeMac.GetSelector("activationPolicy"));

                    if (activityPolicy == (int)ApplicationPolicy.Regular)
                    {
                        list.Add(appPtr);
                    }
                }

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
