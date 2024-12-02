using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRWpfClient;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private HubConnection _connection;

    public MainWindow()
    {
        InitializeComponent();

        // Configure the SignalR connection
        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5097/chatHub")
            .Build();

        // Handle messages from the server
        _connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Dispatcher.Invoke(() =>
            {
                MessagesList.Items.Add($"{user}: {message}");
            });
        });

        // Start the connection
        StartConnection();
    }

    private async void StartConnection()
    {
        try
        {
            await _connection.StartAsync();
            MessagesList.Items.Add("Connection started.");
        }
        catch (Exception ex)
        {
            MessagesList.Items.Add($"Connection failed: {ex.Message}");
        }
    }

    private async void SendButton_Click(object sender, RoutedEventArgs e)
    {
        if (_connection.State == HubConnectionState.Connected)
        {
            await _connection.InvokeAsync("SendMessage", UserTextBox.Text, MessageTextBox.Text);
            MessageTextBox.Clear();
        }
    }
}