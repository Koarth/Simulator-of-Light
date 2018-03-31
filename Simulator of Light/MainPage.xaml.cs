using System;
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
using Simulator.Resources;

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
            this.InitializeComponent();
            double result = Formulas.calculateActionDamage(250, 140, 2819, 1390, Constants.JobID.WHM, Constants.ActionType.MAGIC);
            result = Formulas.calculateCriticalHitMultiplier(1970);
            System.Diagnostics.Debug.WriteLine("CH multiplier: " + result.ToString());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
