using System;
using System.Runtime.InteropServices;
public class VolumeMixer
{
    public static int GetMasterVolume()
    {
        var volume = GetAudioEndpointVolume();
        float level;
        volume.GetMasterVolumeLevelScalar(out level);

        return (int)(level * 100.0f);
    }

    public static void SetMasterVolume(int newVolume)
    {
        var volume = GetAudioEndpointVolume();
        volume.SetMasterVolumeLevelScalar(newVolume / 100.0f, Guid.NewGuid());
    }

    public static IAudioEndpointVolume GetAudioEndpointVolume()
    {
        IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
        IMMDevice defaultDevice;
        deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out defaultDevice);

        Guid endpointVolumeGuid = typeof(IAudioEndpointVolume).GUID;
        object endpointVolume;
        defaultDevice.Activate(ref endpointVolumeGuid, 0, IntPtr.Zero, out endpointVolume);
        IAudioEndpointVolume volume = (IAudioEndpointVolume)endpointVolume;

        return volume;
    }

    [ComImport]
    [Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
    internal class MMDeviceEnumerator { }

    internal enum EDataFlow
    {
        eRender,
        eCapture,
        eAll,
        EDataFlow_enum_count
    }

    internal enum ERole
    {
        eConsole,
        eMultimedia,
        eCommunications,
        ERole_enum_count
    }

    public partial interface IAudioEndpointVolumeCallback
    {
        [PreserveSig]
        int OnNotify(
            [In] IntPtr notificationData);
    }

    [Guid("5CDF2C82-841E-4546-9722-0CF74078229A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IAudioEndpointVolume
    {
        [PreserveSig]
        int RegisterControlChangeNotify(
            [In] [MarshalAs(UnmanagedType.Interface)] IAudioEndpointVolumeCallback client);

        [PreserveSig]
        int UnregisterControlChangeNotify(
            [In] [MarshalAs(UnmanagedType.Interface)] IAudioEndpointVolumeCallback client);

        [PreserveSig]
        int GetChannelCount(
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 channelCount);

        [PreserveSig]
        int SetMasterVolumeLevel(
            [In] [MarshalAs(UnmanagedType.R4)] float level,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

        [PreserveSig]
        int SetMasterVolumeLevelScalar(
            [In] [MarshalAs(UnmanagedType.R4)] float level,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

        [PreserveSig]
        int GetMasterVolumeLevel(
            [Out] [MarshalAs(UnmanagedType.R4)] out float level);

        [PreserveSig]
        int GetMasterVolumeLevelScalar(
            [Out] [MarshalAs(UnmanagedType.R4)] out float level);

        [PreserveSig]
        int SetChannelVolumeLevel(
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 channelNumber,
            [In] [MarshalAs(UnmanagedType.R4)] float level,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

        [PreserveSig]
        int SetChannelVolumeLevelScalar(
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 channelNumber,
            [In] [MarshalAs(UnmanagedType.R4)] float level,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

        [PreserveSig]
        int GetChannelVolumeLevel(
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 channelNumber,
            [Out] [MarshalAs(UnmanagedType.R4)] out float level);

        [PreserveSig]
        int GetChannelVolumeLevelScalar(
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 channelNumber,
            [Out] [MarshalAs(UnmanagedType.R4)] out float level);

        [PreserveSig]
        int SetMute(
            [In] [MarshalAs(UnmanagedType.Bool)] Boolean isMuted,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

        [PreserveSig]
        int GetMute(
            [Out] [MarshalAs(UnmanagedType.Bool)] out Boolean isMuted);

        [PreserveSig]
        int GetVolumeStepInfo(
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 step,
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 stepCount);

        [PreserveSig]
        int VolumeStepUp(
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

        [PreserveSig]
        int VolumeStepDown(
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

        [PreserveSig]
        int QueryHardwareSupport(
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 hardwareSupportMask);

        [PreserveSig]
        int GetVolumeRange(
            [Out] [MarshalAs(UnmanagedType.R4)] out float volumeMin,
            [Out] [MarshalAs(UnmanagedType.R4)] out float volumeMax,
            [Out] [MarshalAs(UnmanagedType.R4)] out float volumeStep);
    }

    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDeviceEnumerator
    {
        int NotImpl1();

        [PreserveSig]
        int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppDevice);
    }

    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDevice
    {
        [PreserveSig]
        int Activate(ref Guid iid, int dwClsCtx, IntPtr pActivationParams, [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
    }

    [Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ISimpleAudioVolume
    {
        [PreserveSig]
        int SetMasterVolume(float fLevel, ref Guid EventContext);

        [PreserveSig]
        int GetMasterVolume(out float pfLevel);
    }

    public enum AudioSessionState
    {
        AudioSessionStateInactive = 0,
        AudioSessionStateActive = 1,
        AudioSessionStateExpired = 2
    }

    public enum AudioSessionDisconnectReason
    {
        DisconnectReasonDeviceRemoval = 0,
        DisconnectReasonServerShutdown = (DisconnectReasonDeviceRemoval + 1),
        DisconnectReasonFormatChanged = (DisconnectReasonServerShutdown + 1),
        DisconnectReasonSessionLogoff = (DisconnectReasonFormatChanged + 1),
        DisconnectReasonSessionDisconnected = (DisconnectReasonSessionLogoff + 1),
        DisconnectReasonExclusiveModeOverride = (DisconnectReasonSessionDisconnected + 1)
    }

    [Guid("24918ACC-64B3-37C1-8CA9-74A66E9957A8"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioSessionEvents
    {
        [PreserveSig]
        int OnDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string NewDisplayName, Guid EventContext);
        [PreserveSig]
        int OnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] string NewIconPath, Guid EventContext);
        [PreserveSig]
        int OnSimpleVolumeChanged(float NewVolume, bool newMute, Guid EventContext);
        [PreserveSig]
        int OnChannelVolumeChanged(UInt32 ChannelCount, IntPtr NewChannelVolumeArray, UInt32 ChangedChannel, Guid EventContext);
        [PreserveSig]
        int OnGroupingParamChanged(Guid NewGroupingParam, Guid EventContext);
        [PreserveSig]
        int OnStateChanged(AudioSessionState NewState);
        [PreserveSig]
        int OnSessionDisconnected(AudioSessionDisconnectReason DisconnectReason);
    }
}

class Program
{
    static void Main(string[] args)
    {
        VolumeMixer.SetMasterVolume(0);
        var volume = VolumeMixer.GetMasterVolume();
    }
}

