//using Android.OS;
//using AndroidBluetooth.Platforms.Android;
//using AndroidX.Core.App;
//using AndroidX.Core.Content;
using AndroidBluetooth.Platforms.Android;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
//using System;
//using System.Runtime.Intrinsics.X86;

namespace AndroidBluetooth;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private async Task<bool> CheckForBluetoothPermission()
	{
#if ANDROID
		var status = await Permissions.CheckStatusAsync<MyBluetoothPermission>();

		if (status == PermissionStatus.Granted)
			return true;

		if (Permissions.ShouldShowRationale<MyBluetoothPermission>())
		{
			await Shell.Current.DisplayAlert("Needs permissions", "BECAUSE!!!", "OK");
		}

		status = await Permissions.RequestAsync<MyBluetoothPermission>();

		return status == PermissionStatus.Granted;
#endif
	}

	private async void BluetoothDetection_Clicked(object sender, EventArgs e)
	{
#if ANDROID
		if (!await CheckForBluetoothPermission())
			return;

		var ble = CrossBluetoothLE.Current;
		var adapter = CrossBluetoothLE.Current.Adapter;

		//var state = ble.State;

		List<IDevice> deviceList = new List<IDevice>();

		adapter.ScanTimeout = 100000;
		adapter.ScanMode = ScanMode.Passive;


		
		adapter.DeviceAdvertised += (s, a) => deviceList.Add(a.Device);
		adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);
		await adapter.StartScanningForDevicesAsync();

		List<IDevice> asd = adapter.DiscoveredDevices.ToList();
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

