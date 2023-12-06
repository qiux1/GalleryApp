﻿        using System;
using GalleryApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalleryApp
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new GalleryPage());
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

