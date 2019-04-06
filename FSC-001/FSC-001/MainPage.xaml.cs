
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FSC_001
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int red_state = 1;
        private bool green_state = false;
        private const int RED = 5;
        private const int GREEN = 18;
        private GpioPin redPin;
        private GpioPin greenPin;


        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();
            redPin = gpio.OpenPin(RED);
            greenPin = gpio.OpenPin(GREEN);
            redPin.Write(GpioPinValue.Low);
            greenPin.Write(GpioPinValue.Low);
            redPin.SetDriveMode(GpioPinDriveMode.Output);
            greenPin.SetDriveMode(GpioPinDriveMode.Output);
        }

        private void MainPage_Unloaded(object sender, object args)
        {
            redPin.Dispose();
            greenPin.Dispose();
        }

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (red_state == 0)
            {
                redPin.Write(GpioPinValue.Low);
                red_state = 1;
            }
            else if (red_state == 1)
            {
                redPin.Write(GpioPinValue.High);
                red_state = 0;
            }
        }

        private async void Button2_Click(object sender, RoutedEventArgs e)
        {
            green_state = !green_state;
            while (green_state == true)
            {
                greenPin.Write(GpioPinValue.High);
                await Task.Delay(500);
                greenPin.Write(GpioPinValue.Low); ;
                await Task.Delay(500);
            }
        }
    }
}
