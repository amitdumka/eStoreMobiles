﻿using Syncfusion.DataSource;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace eStoreMobile.Behaviors
{
    /// <summary>
    /// This class extends the behavior of SfListView control to group the contact names list in alphabetical order.
    /// </summary>
    [Preserve (AllMembers = true)]
    public class ContactsListViewBehavior : Behavior<Syncfusion.ListView.XForms.SfListView>
    {
        #region Fields

        private Syncfusion.ListView.XForms.SfListView listView;

        #endregion

        #region Overrides

        /// <summary>
        /// Invoked when adding the SfListView to view.
        /// </summary>
        /// <param name="bindable">The SfListView</param>
        protected override void OnAttachedTo(Syncfusion.ListView.XForms.SfListView bindable)
        {
            if ( bindable != null )
            {
                this.listView = bindable;
                this.listView.DataSource.GroupDescriptors.Add (new GroupDescriptor ()
                {
                    PropertyName = "Name",
                    KeySelector = (object obj1) =>
                    {
                        var item = obj1 as Models.Contact;
                        return item.Name [0].ToString (CultureInfo.CurrentCulture);
                    },
                });
                base.OnAttachedTo (bindable);
            }
        }

        /// <summary>
        /// Invoked when the list view is detached. 
        /// </summary>
        /// <param name="bindable">The SfListView</param>
        protected override void OnDetachingFrom(Syncfusion.ListView.XForms.SfListView bindable)
        {
            this.listView = null;
            base.OnDetachingFrom (bindable);
        }

        #endregion
    }
}