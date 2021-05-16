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
        }
        Socket corrente;
        TcpClient connect;
        TcpListener server;
        NetworkStream stream;
        Process processoCmd;
        static StreamWriter outStream;
        static StreamReader inStream;
        static StreamWriter toCmd;
        static StreamReader fromCmd;
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IPAddress ipDest = IPAddress.Parse(txtIP.Text);
                int port = int.Parse(txtPorta.Text);
                IPEndPoint ipEP = new IPEndPoint(ipDest, port);
                connect = new TcpClient();
                connect.Connect(ipEP);

                stream = connect.GetStream();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore nella connessione:" + ex.Message);
            }
            btnDisconnect.IsEnabled = true;
            btnInvia.IsEnabled = true;
            btnAscolta.IsEnabled = false;
        }
    }
}
