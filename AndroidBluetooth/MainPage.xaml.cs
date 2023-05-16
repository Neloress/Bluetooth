using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System;

namespace AndroidBluetooth;

public partial class MainPage : ContentPage
{
	//private IDevice Tag = null;
	private List<Tuple<long, int>> _values;
	private bool _runSearch;

	public MainPage()
	{
		InitializeComponent();
	}

	private async void CollectData(int stepDuration,List<Tuple<long,int>> values)
	{
		var ble = CrossBluetoothLE.Current;
		var adapter = CrossBluetoothLE.Current.Adapter;


		List<IDevice> deviceList = new List<IDevice>();

		adapter.ScanTimeout = stepDuration;
		adapter.ScanMode = ScanMode.LowLatency;//ScanMode.Passive;
		adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);

		while (_runSearch) 
		{
			//deviceList = new List<IDevice>();
			//adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);
			await adapter.StartScanningForDevicesAsync();

			foreach (IDevice device in deviceList)
			{
				if (device.Name != null && device.Name.Contains("Smart Tag"))
				{
					int value = device.Rssi;
					long time = System.DateTime.Now.Ticks;
					_values.Add(new Tuple<long, int>(time,value));
				}
			}
		}
	}
	private async void OnCounterClicked(object sender, EventArgs e)
	{
		//_runSearch = false;

		//if (Tag != null)
		//{
			

		//	//while (!await Tag.UpdateRssiAsync()) ;

		//	bool rssiUpdateResult = await Tag.UpdateRssiAsync();
		//	int value = Tag.Rssi;

		//	CounterBtn.Text = $""+value;
		//}
		//else
		//{
		//	CounterBtn.Text = $"No Tag";
		//}
		

		//SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private async Task<bool> CheckForBluetoothPermission()
	{
		var status = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
		
		if (status == PermissionStatus.Granted)
			return true;

		if (Permissions.ShouldShowRationale<Permissions.LocationAlways>())
		{
			await Shell.Current.DisplayAlert("Needs permissions", "BECAUSE!!!", "OK");
		}

		status = await Permissions.RequestAsync<Permissions.LocationAlways>();

		return status == PermissionStatus.Granted;
	}

	private async Task<Tuple<bool,int>> Measure(int timeOut)
	{
		var ble = CrossBluetoothLE.Current;
		var adapter = CrossBluetoothLE.Current.Adapter;


		List<IDevice> deviceList = new List<IDevice>();

		adapter.ScanTimeout = timeOut;
		adapter.ScanMode = ScanMode.LowLatency;//ScanMode.Passive;



		adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);
		await adapter.StartScanningForDevicesAsync();




		foreach (IDevice device in deviceList)
		{
			if (device.Name != null && device.Name.Contains("Smart Tag"))
			{
				return new Tuple<bool, int>(true, device.Rssi);
			}
		}
		return new Tuple<bool, int>(false, -1);
	}

	private async void BluetoothDetection_Clicked(object sender, EventArgs e)
	{
		if (!await CheckForBluetoothPermission())
			throw new Exception("No Permision");

		//_runSearch = true;

		//_values = new List<Tuple<long, int>>();
		//Thread thread = new Thread(() => CollectData(1000, _values));
		//thread.Start();

		//Thread.Sleep(100000);
		//_runSearch = false;
		//thread.Join();

		//int stop = 56;

		//var ble = CrossBluetoothLE.Current;
		//var adapter = CrossBluetoothLE.Current.Adapter;


		//List<IDevice> deviceList2 = new List<IDevice>();

		//adapter.ScanTimeout = 1000;
		//adapter.ScanMode = ScanMode.LowLatency;//ScanMode.Passive;



		////adapter.DeviceAdvertised += (s, a) => deviceList1.Add(a.Device);
		//adapter.DeviceDiscovered += (s, a) => deviceList2.Add(a.Device);
		//await adapter.StartScanningForDevicesAsync();




		//foreach (IDevice device in deviceList2)
		//{
		//	if (device.Name != null && device.Name.Contains("Smart Tag"))
		//	{
		//		int stop3 = 23;// Tag = device;
		//	}
		//}

		List<Tuple<long, int>> values = new List<Tuple<long, int>>();
		for (int i = 0; i < 10; i++)
		{
			Tuple<bool, int> tuple = await Measure(10000);
			if (tuple.Item1)
			{
				long time = System.DateTime.Now.Ticks;
				values.Add(new Tuple<long, int>(time,tuple.Item2));
			}
		}

	}
}

