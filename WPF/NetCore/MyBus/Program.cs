﻿using System;

namespace MyBus
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            App app = new();
            app.InitializeComponent();
            app.Run();
        }
    }
}
