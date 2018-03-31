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

            var actions = new List<Simulator.Models.Action>();

            actions.Add(new Simulator.Models.Action("Stone IV", JobID.WHM, ActionType.MAGIC, ActionAspect.MAGIC,
            250, 600, 0, 2.5, 2.5));
            actions.Add(new Simulator.Models.Action("Stone III", JobID.WHM, ActionType.MAGIC, ActionAspect.MAGIC,
                210, 600, 0, 2.5, 2.5));

            //var stoneIV = new Simulator.Models.Action("Stone IV", JobID.WHM, ActionType.MAGIC, ActionAspect.MAGIC,
            //250, 600, 0, 2.5, 2.5);

            string serialized = actions.Serialize();

            byte[] byteArray = Encoding.UTF8.GetBytes(serialized);
            var stream = new MemoryStream(byteArray);

            var serializer = new XmlSerializer(typeof(List<Simulator.Models.Action>));
            List<Simulator.Models.Action> deserialized = (List<Simulator.Models.Action>)serializer.Deserialize(stream);

            System.Diagnostics.Debug.WriteLine(serialized);
            System.Diagnostics.Debug.WriteLine(((int)deserialized.ElementAt(1).Aspect).ToString());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
