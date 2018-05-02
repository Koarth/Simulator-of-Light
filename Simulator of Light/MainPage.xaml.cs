﻿using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Simulator_of_Light.Simulator;
using Simulator_of_Light.Simulator.Resources;
using Simulator_of_Light.Simulator.Models;
using Simulator_of_Light.Simulator.Utilities;
using System.Text;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Simulator_of_Light
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            IGearsetDAO dao = new GearsetDAOJsonImpl();

            var set = dao.GetEquipmentSetByName("WHMGear");

            Actor actor = new Actor("Kirima Yaeger", CharacterClan.HYUR_MIDLANDER, JobID.WHM, set);

            Application.Current.Exit();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
