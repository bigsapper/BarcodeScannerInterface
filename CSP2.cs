using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SymbolTechnologies
{
	public enum CommPorts
	{
		COM1 = 0,
		COM2 = 1,
		COM3 = 2,
		COM4 = 3,
		COM5 = 4,
		COM6 = 5,
		COM7 = 6,
		COM8 = 7,
		COM9 = 8,
		COM10 = 9,
		COM11 = 10,
		COM12 = 11,
		COM13 = 12,
		COM14 = 13,
		COM15 = 14,
		COM16 = 15
	};

	public class CSP2
	{
		// Returned status values...
		public const int STATUS_OK                  = 0;
		public const int COMMUNICATIONS_ERROR       = -1;
		public const int BAD_PARAM                  = -2;
		public const int SETUP_ERROR                = -3;
		public const int INVALID_COMMAND_NUMBER     = -4;
		public const int COMMAND_LRC_ERROR          = -7;
		public const int RECEIVED_CHARACTER_ERROR   = -8;
		public const int GENERAL_ERROR              = -9;
		public const int FILE_NOT_FOUND             = 2;
		public const int ACCESS_DENIED              = 5;

		// Parameter values...
		public const int PARAM_OFF                  = 0;
		public const int PARAM_ON                   = 1;

		public const int DETERMINE_SIZE             = 0;

		// Communications
		[DllImport("csp2.dll", EntryPoint="csp2Init")]
		public static extern int Init(int nComPort);
		[DllImport("csp2.dll", EntryPoint="csp2Restore")]
		public static extern int Restore();
		[DllImport("csp2.dll", EntryPoint="csp2WakeUp")]
		public static extern int WakeUp();
		[DllImport("csp2.dll", EntryPoint="csp2DataAvailable")]
		public static extern int DataAvailable();

		// Basic Functions
		[DllImport("csp2.dll", EntryPoint="csp2ReadData")]
		public static extern int ReadData();
		[DllImport("csp2.dll", EntryPoint="csp2ClearData")]
		public static extern int ClearData();
		[DllImport("csp2.dll", EntryPoint="csp2PowerDown")]
		public static extern int PowerDown();
		[DllImport("csp2.dll", EntryPoint="csp2GetTime")]
		public static extern int GetTime(byte[] aTimeBuf);
		[DllImport("csp2.dll", EntryPoint="csp2SetTime")]
		public static extern int SetTime(byte[] aTimeBuf);
		[DllImport("csp2.dll", EntryPoint="csp2SetDefaults")]
		public static extern int SetDefaults();

		// CSP Data Get
		[DllImport("csp2.dll", EntryPoint="csp2GetPacket")]
		public static extern int GetPacket(byte[] szBarData, int nBarcodeNumber, int nMaxLength);
		[DllImport("csp2.dll", EntryPoint="csp2GetDeviceId")]
		public static extern int GetDeviceId(byte[] szDeviceId, int nMaxLength);
		[DllImport("csp2.dll", EntryPoint="csp2GetProtocol")]
		public static extern int GetProtocol();
		[DllImport("csp2.dll", EntryPoint="csp2GetSystemStatus")]
		public static extern int GetSystemStatus();
		[DllImport("csp2.dll", EntryPoint="csp2GetSwVersion")]
		public static extern int GetSwVersion(StringBuilder szSwVersion, int nMaxLength);
		[DllImport("csp2.dll", EntryPoint="csp2GetASCIIMode")]
		public static extern int GetASCIIMode();
		[DllImport("csp2.dll", EntryPoint="csp2GetRTCMode")]
		public static extern int GetRTCMode();

		// DLL Configuration
		[DllImport("csp2.dll", EntryPoint="csp2SetRetryCount")]
		public static extern int SetRetryCount(int nRetryCount);
		[DllImport("csp2.dll", EntryPoint="csp2GetRetryCount")]
		public static extern int GetRetryCount();

		// Miscellaneous
		[DllImport("csp2.dll", EntryPoint="csp2GetDllVersion")]
		public static extern int GetDllVersion(StringBuilder szDllVersion, int nMaxLength);
		[DllImport("csp2.dll", EntryPoint="csp2TimeStamp2Str")]
		public static extern int TimeStamp2Str(string Stamp, StringBuilder Value, int nMaxLength);
		[DllImport("csp2.dll", EntryPoint="csp2GetCodeType")]
		public static extern int GetCodeType(int CodeID, StringBuilder CodeType, int nMaxLength);

		// Advanced functions
		[DllImport("csp2.dll", EntryPoint="csp2ReadRawData")]
		public static extern int ReadRawData(char[] aBuffer, int nMaxLength);
		[DllImport("csp2.dll", EntryPoint="csp2SetParam")]
		public static extern int SetParam(int nParam, string szString, int nMaxLength);
		[DllImport("csp2.dll", EntryPoint="csp2GetParam")]
		public static extern int GetParam(int nParam, StringBuilder szString, int nMaxLength);
		[DllImport("csp2.dll", EntryPoint="csp2Interrogate")]
		public static extern int Interrogate();
		[DllImport("csp2.dll", EntryPoint="csp2GetCTS")]
		public static extern int GetCTS();
		[DllImport("csp2.dll", EntryPoint="csp2SetDTR")]
		public static extern int SetDTR(int nOnOff);
		[DllImport("csp2.dll", EntryPoint="csp2SetDebugMode")]
		public static extern int SetDebugMode(int nOnOff);
		[DllImport("csp2.dll", EntryPoint="csp2StartPolling")]
		public static extern int StartPolling(int csp2CallBack);
		[DllImport("csp2.dll", EntryPoint="csp2StopPolling")]
		public static extern int StopPolling();
	}
}
