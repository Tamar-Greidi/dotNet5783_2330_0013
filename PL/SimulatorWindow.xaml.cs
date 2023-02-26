using BlApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        IBl bl = Factory.Get();
        BackgroundWorker worker;
        BackgroundWorker timerWorker;
        private Stopwatch stopWatch;
        private bool isTimerRun;
        bool isRunning = true;
        public SimulatorWindow()
        {
            InitializeComponent();
            stopWatch = new Stopwatch();
            timerWorker = new BackgroundWorker();
            timerWorker.DoWork += TimerWorker_DoWork;
            timerWorker.ProgressChanged += TimerWorker_TimerProgressChanged;
            timerWorker.WorkerReportsProgress = true;
            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
            if (!isTimerRun)
            {
                stopWatch.Restart();
                isTimerRun = true;
                timerWorker.RunWorkerAsync();
            }
        }

        private void stopTimerButton_Click(object sender, RoutedEventArgs e)
        {
            Simulator.Simulator.Stop();
            isRunning = false;
        }

        private void worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.Start();
            Simulator.Simulator.statusChanged += StartWork;
            Simulator.Simulator.stopSimulator += FinishWork;
            if (!Dispatcher.Thread.IsAlive)
                e.Cancel = false;
        }

        private void TimerWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerWorker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }

        private void TimerWorker_TimerProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            string timer = stopWatch.Elapsed.ToString();
            timer = timer.Substring(0, 8);
            this.txtTimer.Text = timer;
        }

        public void StartWork(BO.Order order, string status, DateTime start, DateTime end, int time)
        {
            this.Dispatcher.Invoke(() =>
            {
                txtOrder.Text = Convert.ToString(order.ID);
                txtStatus.Text = Convert.ToString(order.Status);
                txtNewStatus.Text = status;
                txtTime.Text = Convert.ToString(time) + " sec";
                txtStart.Text = Convert.ToString(start);
                txtEnd.Text = Convert.ToString(end);
            });
        }
        public void FinishWork(DateTime end, string reason = "")
        {
            Dispatcher.Invoke(() =>
            {
                isTimerRun = false;
                if (reason != "")
                {
                    MessageBox.Show("stop process because: " + end.ToString() + " " + reason);
                    isRunning = false;
                }

            });
        }
        private void CloseWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = isRunning;
        }
    }
}
