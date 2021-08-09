using System;
using System.ComponentModel;

namespace bla1
{
    internal sealed class GitExtensionsControlInitialiser
    {
        private BaseForm _form;

        // Indicates whether the initialisation has been signalled as complete.
        private bool _initialiseCompleteCalled;

        public GitExtensionsControlInitialiser(BaseForm form)
        {
            _form = form;

            if (IsDesignMode)
            {
                return;
            }

            ThreadHelper.ThrowIfNotOnUIThread();
            form.Load += LoadHandler;
        }

        public bool IsDesignMode
        {
            get => _form.IsInDesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }

        public void InitializeComplete()
        {
            if (IsDesignMode)
            {
                return;
            }

            if (_initialiseCompleteCalled)
            {
                throw new InvalidOperationException($"{nameof(InitializeComplete)} already called.");
            }

            _initialiseCompleteCalled = true;
        }

        private void LoadHandler(object control, EventArgs e)
        {
            if (!_initialiseCompleteCalled)
            {
                throw new Exception($"{control.GetType().Name} must call {nameof(InitializeComplete)} in its constructor, ideally as the final statement.");
            }
        }
    }
}
