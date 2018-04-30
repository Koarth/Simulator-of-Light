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
            //this.InitializeComponent();
            ///double result = Formulas.calculateActionDamage(250, 140, 2819, 1390, Constants.JobID.WHM, Constants.ActionType.MAGIC);
            //result = Formulas.calculateCriticalHitMultiplier(1970);
            //System.Diagnostics.Debug.WriteLine("CH multiplier: " + result.ToString());
            //double result = Formulas.calculateTotalMana(1115, Constants.JobID.WHM);
            //System.Diagnostics.Debug.WriteLine("Total Mana: " + result.ToString());

            /**
            var actions = new List<BaseAction>();

            actions.Add(new BaseAction("Stone IV", JobID.WHM, ActionType.MAGIC, ActionAspect.MAGIC, false,
            250, 600, 0, 2.5, 2.5, 25, 0, null));
            //actions.Add(new BaseAction("Stone IV", JobID.WHM, ActionType.MAGIC, ActionAspect.MAGIC, potency:250,mpCost:600,castTime:2.5,recastTime:2.5,range:25));
            actions.Add(new BaseAction("Stone III", JobID.WHM, ActionType.MAGIC, ActionAspect.MAGIC, false,
                210, 600, 0, 2.5, 2.5, 25, 0, null));
            actions.Add(new BaseAction("Aero III", JobID.WHM, ActionType.MAGIC, ActionAspect.MAGIC, false,
                50, 700, 0, 2.5, 2.5, 25, 5, null));

            string json = JsonConvert.SerializeObject(actions, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
            System.Diagnostics.Debug.WriteLine(json);

            string jsonAction = "[{\"Name\": \"Stone IV\",\"JobID\": \"WHM\",\"Type\": \"MAGIC\",\"Aspect\": \"MAGIC\",\"Ogcd\": false,\"Potency\": 250.0,\"MpCost\": 600.0,\"CastTime\": 2.5,\"RecastTime\": 2.5}]";

            var retrievedActions = new List<BaseAction>();

            retrievedActions = JsonConvert.DeserializeObject<List<BaseAction>>(jsonAction, new JsonSerializerSettings {
                DefaultValueHandling = DefaultValueHandling.Populate
            });

            System.Diagnostics.Debug.WriteLine(retrievedActions[0].Name);
            System.Diagnostics.Debug.WriteLine(retrievedActions[0].JobID);
            System.Diagnostics.Debug.WriteLine(retrievedActions[0].Range);
            **/

            var dict = BaseActionFactory.getBaseActionsByJobID(JobID.WHM);

            System.Diagnostics.Debug.WriteLine(dict["Aero II"]);


            Application.Current.Exit();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
