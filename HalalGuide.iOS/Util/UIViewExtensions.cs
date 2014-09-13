using System;
using System.Runtime.CompilerServices;
using MonoTouch.UIKit;
using HalalGuide.Util;

namespace HalalGuide.iOS.Util
{
	public static class UIViewExtensions
	{
		public static void TranslateLabelsAndPlaceholders (this UIView searchView)
		{



			foreach (UIView view in searchView.Subviews) {

				if (view is UILabel) {
					UILabel label = (UILabel)view;
					string text = label.Text;
					if (text != null) {
						string translation = Localization.GetLocalizedValue (text) ?? text;
						label.Text = translation;
					}

				} else if (view is UITextField) {
					UITextField textfield = (UITextField)view;
					string text = textfield.Placeholder;
					if (text != null) {
						string translation = Localization.GetLocalizedValue (text) ?? text;
						textfield.Placeholder = translation;
					}
				} else if (view is UIButton) {
					UIButton button = (UIButton)view;
					string text = button.Title (UIControlState.Normal);
					if (text != null) {
						string translation = Localization.GetLocalizedValue (text) ?? text;
						button.SetTitle (translation, UIControlState.Normal);
					}

				} else if (view is UINavigationBar) {
					UINavigationBar item = (UINavigationBar)view;
					if (item.TopItem != null) {
						string text = item.TopItem.Title;
						if (text != null) {
							string translation = Localization.GetLocalizedValue (text) ?? text;
							item.TopItem.Title = translation;
						}

						if (item.TopItem.LeftBarButtonItem != null) {
							text = item.TopItem.LeftBarButtonItem.Title;
							if (text != null) {
								string translation = Localization.GetLocalizedValue (text) ?? text;
								item.TopItem.LeftBarButtonItem.Title = translation;
							}
						}
						if (item.TopItem.RightBarButtonItem != null) {
							text = item.TopItem.RightBarButtonItem.Title;
							if (text != null) {
								string translation = Localization.GetLocalizedValue (text) ?? text;
								item.TopItem.RightBarButtonItem.Title = translation;
							}
						}
					}

				} else {
					TranslateLabelsAndPlaceholders (view);
				}

			}
		}
	}
}

