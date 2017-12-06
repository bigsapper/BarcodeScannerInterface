using System;
using System.Reflection;
using System.Text;
using SymbolTechnologies;

namespace SymbolTechnologies
{
	/// <summary>
	/// Summary description for TestConsole.
	/// </summary>
	class TestConsole
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main(string[] args)
		{
			return process();
		}

		static int process()
		{
			int nRetStatus, AsciiMode, RTC;
			int	PacketLength, BarcodesRead;
			StringBuilder sbBuffer = new StringBuilder(256);
			StringBuilder sbTimestamp = new StringBuilder(32);
			byte[] cBuffer = new byte[256];
			byte[] cPacket = new byte[64];

			nRetStatus = CSP2.Init((int)CommPorts.COM1);
			if ( nRetStatus != CSP2.STATUS_OK ) // Connected to COM1
			{
				/* error condition, alert the user of the problem */
				Console.WriteLine("Comm port not initialized.\r\nError Status: {0}", nRetStatus);
				return 1;
			}

			/* get DLL version information */
			nRetStatus = CSP2.GetDllVersion(sbBuffer, 16);
			if ( nRetStatus < 0 || nRetStatus == CSP2.FILE_NOT_FOUND ) 
			{
				/* error condition, alert the user of the problem */
				Console.WriteLine("Error reading version info.\r\nError Status: {0}\n", nRetStatus);
			}
			else 
			{
				Console.Write("DLL Version {0}\n", sbBuffer.ToString());
			}

			// set DEBUG mode
			#if DEBUG
				CSP2.SetDebugMode(CSP2.PARAM_ON);
			#else
				CSP2.SetDebugMode(CSP2.PARAM_OFF);
			#endif			

			nRetStatus = CSP2.ReadData(); /* Read barcodes */
			if ( nRetStatus < 0 ) 
			{
				/* error condition, alert the user of the problem */
				Console.Write("Error reading barcode info.\r\nError Status: {0}", nRetStatus);
			}
			else 
			{
				/* save number of barcodes read into DLL */
				BarcodesRead = nRetStatus;
				Console.Write("Read {0} Buffers\n", BarcodesRead); 

				/* Get Device Id */
				nRetStatus = CSP2.GetDeviceId(cBuffer, 8);
				Console.Write("Device ID ");
				for ( int k = 0; k < nRetStatus; k++ ) 
					Console.Write(" {0:X2}", cBuffer[k]);

				/* Get Device Software Revision */
				nRetStatus = CSP2.GetSwVersion(sbBuffer, 9);
				Console.Write("\nDevice SW Version: {0}\n", sbBuffer.ToString());

				/* Get ansiBuffer type (ASCII/Binary) */
				AsciiMode = CSP2.GetASCIIMode();	
				Console.Write("ASCII Mode = {0}", AsciiMode);

				/* Get sbTimestamp setting (on/off) */
				RTC = CSP2.GetRTCMode();			
				Console.Write(", RTC Mode = {0}\n", RTC);
		
				for ( int i = 0; i < BarcodesRead; i++ ) 
				{
					//ansiBuffer = new StringBuilder(63);
					PacketLength = CSP2.GetPacket(cPacket, i, 63); /* Read Packets */
					string packet = ASCIIEncoding.ASCII.GetString(cPacket);
					Console.Write("\nBarcode {0}\n", i + 1);
					/* print out sbPacket (hex) */
					for ( int k = 0; k < PacketLength; k++ ) 
						Console.Write(" {0:X2}", cPacket[k]);
					Console.Write("\n");

					if ( (PacketLength > 0) && (AsciiMode == CSP2.PARAM_ON) ) /* normal ansiBuffer processing */
					{
						/* convert ascii mode code type to string */
						nRetStatus = CSP2.GetCodeType((int)cPacket[1], sbBuffer, 30);
						Console.Write("CodeConversion returns: {0}\n", nRetStatus);
						sbBuffer.Append(" ");

						if ( RTC == CSP2.PARAM_ON) /* convert sbTimestamp if necessary */
						{
							/* convert binary sbTimestamp to string */
							CSP2.TimeStamp2Str(packet.ToString().Substring(PacketLength - 4), sbTimestamp, 30);

							/* append barcode (less sbTimestamp) to codetype */
							/* offset: sbTimestamp=4, len=1, codetype = 1 */
							sbBuffer.Append(packet.ToString().Substring(2, PacketLength - 6)); 

							/* append sbTimestamp */
							sbBuffer.Append(sbTimestamp.ToString()); 
						}
						else /* if no RTC just append barcode to codetype (no sbTimestamp) */
							sbBuffer.Append(packet.ToString().Substring(2, PacketLength - 2));

						Console.Write("{0}\n", sbBuffer.ToString()); 
					}
					else
					{
						/* Process Binary ansiBuffer Mode */
					}
			
				} /* end of read ansiBuffers loop */
			}
			
			Console.Write("\nPress return to exit");

			Console.Read(); /* wait for keyboard to exit */

			return 0;
		}
	}
}
