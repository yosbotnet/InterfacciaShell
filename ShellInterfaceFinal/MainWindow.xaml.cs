using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShellInterfaceFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnDisconnect.IsEnabled = false;
            btnInvia.IsEnabled = false;
        }
        #region variabili
        TcpClient connect;
        TcpListener server;
        NetworkStream stream;
        Process processoCmd;
        static StreamWriter outStream;
        static StreamReader inStream;
        static StreamWriter toCmd;
        #endregion
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //connect & get stream
                IPAddress ipDest = IPAddress.Parse(txtIP.Text);
                int port = int.Parse(txtPorta.Text);
                IPEndPoint ipEP = new IPEndPoint(ipDest, port);
                connect = new TcpClient();
                connect.Connect(ipEP);
                stream = connect.GetStream();
                byte[] resp = new byte[2048];
                //nel memorystream scriverò il risultato
                var memStream = new MemoryStream();
                int bytesread = stream.Read(resp, 0, resp.Length);
                while (bytesread > 0)
                {
                    memStream.Write(resp, 0, bytesread);
                    Thread.Sleep(100);
                    resp = new byte[2048];
                    //se non è arrivato il numero massimo di byte, esco
                    
                    if (!stream.DataAvailable)
                        break;
                    bytesread = stream.Read(resp, 0, resp.Length);
                }

                string response = Encoding.ASCII.GetString(memStream.ToArray());
                memStream.Close();
                lblPrompt.Content = response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore nella connessione:" + ex.Message);
            }
            btnDisconnect.IsEnabled = true;
            btnInvia.IsEnabled = true;
            btnAscolta.IsEnabled = false;

        }
        private void btnAscolta_Click(object sender, RoutedEventArgs e)
        {
            Thread t = null;
            try
            {
                t = new Thread(new ParameterizedThreadStart(ThreadAscolto));
                t.Start(txtPortaAscolto.Text);
                btnAscolta.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore!" + ex.Message);
                btnAscolta.IsEnabled = true;
            }
            finally
            {
                t.Join();
                if (server != null)
                    server.Stop();
            }

        }
        public void ThreadAscolto(object portaAscolto)
        {
            string _portaAscolto = (string)portaAscolto;
            #region inizializza processo
            //inizializzo il processo cmd
            processoCmd = new Process();
            processoCmd.StartInfo = new ProcessStartInfo("cmd");
            processoCmd.StartInfo.CreateNoWindow = true;
            processoCmd.StartInfo.UseShellExecute = true;
            processoCmd.StartInfo.RedirectStandardOutput = true;
            processoCmd.StartInfo.RedirectStandardInput = true;
            processoCmd.StartInfo.RedirectStandardError = true;
            processoCmd.StartInfo.UseShellExecute = false;
            processoCmd.OutputDataReceived += new DataReceivedEventHandler(SendOutput);
            server = new TcpListener(IPAddress.Any, int.Parse(_portaAscolto));
            server.Start();

            #endregion
            //fromCmd = processoCmd.StandardOutput;



            byte[] bufferC = new byte[1024];
            String comando = null;
            while (server.Pending() == false)
            {
                Thread.Sleep(50);
            }
            //finisco di impostare il processo cmd
            Socket ascolto = server.AcceptSocket();
            processoCmd.Start();
            toCmd = processoCmd.StandardInput;
            processoCmd.BeginOutputReadLine();
            bool aperto = true;
            while (aperto)
            {
                //leggo il comando
                comando = null;
                //imposto gli stream
                stream = new NetworkStream(ascolto);
                inStream = new StreamReader(stream);
                outStream = new StreamWriter(stream);
                toCmd.AutoFlush = true;
                outStream.AutoFlush = true;
                //leggo continuamente
                int i;
                while ((i = stream.Read(bufferC, 0, bufferC.Length)) != 0)
                {
                    comando = Encoding.ASCII.GetString(bufferC);
                    //esegui il comando
                    Esegui(comando);


                }
                //per performance
                Thread.Sleep(50);
            }
            ascolto.Close();

            return;
        }
        private static void SendOutput(object sendingProcess, DataReceivedEventArgs outLine)
        {
            StringBuilder strOutput = new StringBuilder();

            if (!String.IsNullOrEmpty(outLine.Data))
            {
                try
                {
                    strOutput.Append(outLine.Data);
                    outStream.WriteLine(strOutput);
                    //outStream.Flush();
                }
                catch (Exception ex)
                {
                    // silence is golden
                }
            }
        }
        public void Esegui(string comando)
        {
            processoCmd.StandardInput.WriteLine(comando);
        }
        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            connect.Close();
            btnInvia.IsEnabled = false;
            btnAscolta.IsEnabled = true;
        }
        private void btnInvia_Click(object sender, RoutedEventArgs e)
        {
            stream.ReadTimeout = 15000;
            //invio il comando in byte
            byte[] comando = Encoding.ASCII.GetBytes(txtComando.Text + "\r\n");
            stream.Write(comando, 0, comando.Length);
            byte[] resp = new byte[2048];
            //nel memorystream scriverò il risultato
            var memStream = new MemoryStream();
            int bytesread = stream.Read(resp, 0, resp.Length);
            while (bytesread > 0)
            {
                memStream.Write(resp, 0, bytesread);
                Thread.Sleep(75);
                resp = new byte[2048];
                //se non è arrivato il numero massimo di byte, esco
                if (!stream.DataAvailable)
                    break;
                bytesread = stream.Read(resp, 0, resp.Length);
            }

            string response = Encoding.ASCII.GetString(memStream.ToArray());
            memStream.Close();
            lblPrompt.Content = response;
        }
    }
}
