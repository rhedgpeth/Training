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
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        UIKit.UIButton Button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton largeSongsListButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton smallSongsListButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (largeSongsListButton != null) {
                largeSongsListButton.Dispose ();
                largeSongsListButton = null;
            }

            if (smallSongsListButton != null) {
                smallSongsListButton.Dispose ();
                smallSongsListButton = null;
            }
        }
    }
}