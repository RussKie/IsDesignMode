using Microsoft.VisualStudio.Threading;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace bla1
{
    internal class ThreadHelper
    {
        private const int RPC_E_WRONG_THREAD = unchecked((int)0x8001010E);
        private static JoinableTaskContext _joinableTaskContext = null!;
        private static JoinableTaskCollection _joinableTaskCollection = null!;
        private static JoinableTaskFactory _joinableTaskFactory = null!;

        public static JoinableTaskContext JoinableTaskContext
        {
            get
            {
                return _joinableTaskContext;
            }

            internal set
            {
                if (value == _joinableTaskContext)
                {
                    return;
                }

                if (value is null)
                {
                    _joinableTaskContext = null!;
                    _joinableTaskCollection = null!;
                    _joinableTaskFactory = null!;
                }
                else
                {
                    _joinableTaskContext = value;
                    _joinableTaskCollection = value.CreateCollection();
                    _joinableTaskFactory = value.CreateFactory(_joinableTaskCollection);
                }
            }
        }

        public static JoinableTaskFactory JoinableTaskFactory => _joinableTaskFactory;

        public static void ThrowIfNotOnUIThread([CallerMemberName] string callerMemberName = "")
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            if (!JoinableTaskContext.IsOnMainThread)
            {
                string message = string.Format(CultureInfo.CurrentCulture, "{0} must be called on the UI thread.", callerMemberName);
                throw new COMException(message, RPC_E_WRONG_THREAD);
            }
        }
    }
}
