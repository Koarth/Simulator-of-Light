using System;
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
using Simulator_of_Light.Simulator.Resources;
using static Simulator_of_Light.Simulator.Resources.Constants;
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

            var actions = new List<BaseAction>();

            actions.Add(new BaseAction("Stone IV", JobID.WHM, ActionType.MAGIC, ActionAspect.MAGIC, false,
            250, 600, 0, 2.5, 2.5, 25, 0));
            actions.Add(new BaseAction("Stone III", JobID.WHM, ActionType.MAGIC, ActionAspect.MAGIC, false,
                210, 600, 0, 2.5, 2.5, 25, 0));
            actions.Add(new BaseAction("Aero III", JobID.WHM, ActionType.MAGIC, ActionAspect.MAGIC, false,
                50, 700, 0, 2.5, 2.5, 25, 5));

            string json = JsonConvert.SerializeObject(actions, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
            System.Diagnostics.Debug.WriteLine(json);

            var retrievedActions = new List<BaseAction>();

            retrievedActions = JsonConvert.DeserializeObject<List<BaseAction>>(json);

            System.Diagnostics.Debug.WriteLine(retrievedActions[0].Name);
            System.Diagnostics.Debug.WriteLine(retrievedActions[0].JobID);

            var jsonDAO = new Simulator.Resources.ActionDAOJsonImpl();

            jsonDAO.getActionsByJobID(JobID.WHM);


            Application.Current.Exit();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
