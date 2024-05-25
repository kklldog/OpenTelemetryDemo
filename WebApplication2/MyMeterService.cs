using System.Diagnostics.Metrics;

namespace WebApplication2
{
    public class MyMeterService
    {
        public static Meter MyMeter = new("MyMeter", "1.0");
        public static Counter<long> MyOrderCounter = MyMeter.CreateCounter<long>("MyOrderCounter");

        public static ObservableGauge<long> MyRandomNumber = MyMeter.CreateObservableGauge<long>("MyRandomNumber", () =>
        {
            Console.WriteLine($"Generating random number {DateTime.Now}");
            return new Random().Next(0, 100);
        });
    }
}
