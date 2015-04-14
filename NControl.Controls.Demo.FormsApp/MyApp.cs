﻿using System;
using NControl.Abstractions;
using NGraphics;
using Xamarin.Forms;
using System.Reflection;
using System.Linq;

namespace NControl.Controls.Demo.FormsApp
{
	public class MyApp : Application
	{
		public MyApp ()
		{
			var demoPageList = new ContentPage[] {
				new CustomFontPage(),
				new RoundCornerViewPage(),
				new FontAwesomeLabelPage(),
				new FloatingLabelPage(),
				new ActionButtonPage(),
				new CardPageDemo(),
			};

			var listView = new ListView {
				ItemsSource = demoPageList,
			};

			listView.ItemTemplate = new DataTemplate (typeof(TextCell));
			listView.ItemTemplate.SetBinding (TextCell.TextProperty, "Title");

			var startPage = new ContentPage {
				Title = "NControl.Controls",
                Content = listView                
			};

			listView.ItemSelected += async (sender, e) => {

				if(listView.SelectedItem == null)
					return;
				
				// Show page
				if(listView.SelectedItem is CardPageDemo)
					Device.OnPlatform(
						async () => await startPage.Navigation.PushModalAsync(listView.SelectedItem as ContentPage, false),
						null, null, async ()=> await startPage.Navigation.PushAsync(listView.SelectedItem as ContentPage));					
				else
					await startPage.Navigation.PushAsync(listView.SelectedItem as ContentPage);					
			};

			listView.ItemTapped += (sender, e) => listView.SelectedItem = null;
				
			// The root page of your application
			MainPage = new NavigationPage(startPage);
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

