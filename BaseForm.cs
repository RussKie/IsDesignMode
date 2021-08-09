using System;
using System.Windows.Forms;

namespace bla1
{
    public partial class BaseForm : Form
    {
        private readonly GitExtensionsControlInitialiser _initialiser;

        public BaseForm()
        {
            _initialiser = new GitExtensionsControlInitialiser(this);

            if (_initialiser.IsDesignMode)
            {
                return;
            }

            ShowInTaskbar = Application.OpenForms.Count <= 0;
        }

        public bool IsInDesignMode => DesignMode;

        protected void InitializeComplete()
        {
            _initialiser.InitializeComplete();

            if (_initialiser.IsDesignMode)
            {
                Text = "IsDesignMode=true";
                return;
            }

            Text = "IsDesignMode=false";
        }
    }
}
