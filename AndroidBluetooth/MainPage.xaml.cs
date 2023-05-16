//using Android.OS;
//using AndroidBluetooth.Platforms.Android;
//using AndroidX.Core.App;
//using AndroidX.Core.Content;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
//using System;
//using System.Runtime.Intrinsics.X86;

namespace AndroidBluetooth;

public partial class MainPage : ContentPage
{
	private IDevice Tag = null;

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		if (Tag != null)
		{
			var rssiUpdateResult = await Tag.UpdateRssiAsync();
			int value = Tag.Rssi;

			CounterBtn.Text = $""+value;
		}
		else
		{
			CounterBtn.Text = $"No Tag";
		}
		

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private async Task<bool> CheckForBluetoothPermission()
	{
#if ANDROID
		var status = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();//MyBluetoothPermission

		if (status == PermissionStatus.Granted)
			return true;

		if (Permissions.ShouldShowRationale<Permissions.LocationAlways>())
		{
			await Shell.Current.DisplayAlert("Needs permissions", "BECAUSE!!!", "OK");
		}

		status = await Permissions.RequestAsync<Permissions.LocationAlways>();

		return status == PermissionStatus.Granted;
#endif
	}
	public int Rssi { [Android.Runtime.Register("getRssi", "()I", "")] get; }

	private async void BluetoothDetection_Clicked(object sender, EventArgs e)
	{
#if ANDROID
		if (!await CheckForBluetoothPermission())
			return;

		var ble = CrossBluetoothLE.Current;
		var adapter = CrossBluetoothLE.Current.Adapter;

		//var state = ble.State;

		//List<IDevice> deviceList1 = new List<IDevice>();
		List<IDevice> deviceList2 = new List<IDevice>();

		adapter.ScanTimeout = 10000;
		adapter.ScanMode = ScanMode.Passive;

		
		
		//adapter.DeviceAdvertised += (s, a) => deviceList1.Add(a.Device);
		adapter.DeviceDiscovered += (s, a) => deviceList2.Add(a.Device);
		await adapter.StartScanningForDevicesAsync();

		//List<IDevice> asd = adapter.DiscoveredDevices.ToList();

		

		foreach (IDevice device in deviceList2)
		{
			if (device.Name.Contains("Smart Tag"))
			{
				Tag = device;

				var rssiUpdateResult = await device.UpdateRssiAsync();
				int value = device.Rssi;

				int sdrf = 0;
			}
		}

#endif
		//#if ANDROID

		//		if (!await CheckForBluetoothPermission())
		//			return;

		//		var adapter = CrossBluetoothLE.Current.Adapter;

		//		adapter.DeviceAdvertised += Adapter_DeviceAdvertised;
		//		adapter.DeviceConnected += Adapter_DeviceConnected;
		//		adapter.DeviceConnectionLost += Adapter_DeviceConnectionLost;
		//		adapter.DeviceDisconnected += Adapter_DeviceDisconnected;
		//		adapter.DeviceDiscovered += Adapter_DeviceDiscovered;
		//		adapter.ScanTimeoutElapsed += Adapter_ScanTimeoutElapsed;
		//		await adapter.StartScanningForDevicesAsync();
		//#endif
	}

	//private void Adapter_ScanTimeoutElapsed(object sender, EventArgs e)
	//{
	//	Console.WriteLine("ScanTimeoutElapsed");
	//}

	//private void Adapter_DeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
	//{
	//	Console.WriteLine("DeviceDiscovered");
	//}

	//private void Adapter_DeviceDisconnected(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
	//{
	//	Console.WriteLine("DeviceDisconnected");
	//}

	//private void Adapter_DeviceConnectionLost(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceErrorEventArgs e)
	//{
	//	Console.WriteLine("DeviceConnectionLost");
	//}

	//private void Adapter_DeviceConnected(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
	//{
	//	Console.WriteLine("DeviceConnected");
	//}

	//private void Adapter_DeviceAdvertised(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
	//{
	//	Console.WriteLine("DeviceAdvertised");
	//}
}

