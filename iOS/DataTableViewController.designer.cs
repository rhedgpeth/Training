// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace EpocratesTraining.iOS
{
    [Register ("DataTableViewController")]
    partial class DataTableViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        EpocratesTraining.iOS.DataTableView DataTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DataTable != null) {
                DataTable.Dispose ();
                DataTable = null;
            }
        }
    }
}