using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        private Stopwatch stopWatc;
        private bool isTimerRun;
        private Thread timerThread;
        public SimulatorWindow()
        {
            InitializeComponent();
            stopWatc = new Stopwatch();
        }

        private void startTimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isTimerRun)
            {
                stopWatc.Restart();
                isTimerRun = true;

                timerThread = new Thread(runTimer);
                timerThread.Start();
            }
        }
        private void stopTimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerRun)
            {
                stopWatc.Stop();
                isTimerRun = false;
            }
        }

        void setTexInvok(string text)
        {
            if (!CheckAccess())
            {
                Action<string> d = setTexInvok;
                Dispatcher.BeginInvoke(d, new object[] { text });
            }
            else
                this.timerTextBlock.Text = text;
        }
        private void runTimer()
        {
            while (isTimerRun)
            {
                string timerText = stopWatc.Elapsed.ToString();
                timerText = timerText.Substring(0, 8);

                setTexInvok(timerText);
                Thread.Sleep(1000);
            }
        }
    }
}
