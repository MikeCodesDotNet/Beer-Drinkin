using System;
using BeerDrinkin.Service;
using Foundation;
using UIKit;
using Colour = BeerDrinkin.Helpers.Colours;
using Splat;

namespace BeerDrinkin.iOS
{
    partial class SettingsViewController : UITableViewController
    {
        public SettingsViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            btnClose.SetTitleTextAttributes(new UITextAttributes
            {
                Font = UIFont.FromName("Avenir-Book", 14f),
                TextColor = Colour.White.ToNative()
            }, UIControlState.Normal);

            TableView.WeakDelegate = this;
        }

        async partial void btnClose_Activated(UIBarButtonItem sender)
        {
            await DismissViewControllerAsync(true);
        }

        //TODO FIX THIS!
        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Section == 1 && indexPath.Row == 1)
            {
                //Sign out! 
                var userService = new UserService();
                userService.RemoveAuthToken(); //This means we wont automatically log in

                var signInView = UIApplication.SharedApplication.KeyWindow.RootViewController.ChildViewControllers[1];
                var tabView = signInView.ChildViewControllers[0];

                if (tabView != null)
                {
                    tabView.DismissViewController(true, null);
                }
            }
        }
    }
}