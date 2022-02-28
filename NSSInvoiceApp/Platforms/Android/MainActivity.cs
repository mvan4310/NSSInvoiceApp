﻿using Android.App;
using Android.Content.PM;
using Android.OS;

namespace NSSInvoiceApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
public class MainActivity : MauiAppCompatActivity
{
	protected override void OnCreate(Bundle savedInstanceState)
	{
		base.OnCreate(savedInstanceState);
		Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 39, 39, 47));

	}
}
