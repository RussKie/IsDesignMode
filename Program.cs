using bla1;
using Microsoft.VisualStudio.Threading;
using System.Windows.Forms;

Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);

// This form created to obtain UI synchronization context only
using (new Form())
{
    // Store the shared JoinableTaskContext
    ThreadHelper.JoinableTaskContext = new JoinableTaskContext();
}

Application.Run(new Form1());
