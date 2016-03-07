using System;
using System.IO;

namespace ConsoleApplication1
{
	class Program
	{
		static int offset = 32;
		static string separtor = ",";
		static string suffix = ".csv";



		static void Main(string[] args)
		{
			Console.Write("file path:");
			//Console.Write("Directory path:");
			string filePath = Console.ReadLine();
			if (filePath.StartsWith("\"") && filePath.EndsWith("\""))
			{
				filePath = filePath.Substring(1, filePath.Length - 2);
			}
			ReadDayFile(filePath);
			/*string[] files = Directory.GetFiles(filePath);
			for (int i = 0; i < files.Length; i++)
			{
				if (files[i].EndsWith(".day"))
				{
					ReadDayFile(files[i]);
				}
			}*/


			System.Console.Read();
		}

		static void ReadDayFile(string filePath)
		{
			//separtor = "\t";
			string outSteing = GetDataTitle() + "\n";

			using (FileStream fileStream = File.Open(filePath, FileMode.Open))
			{
				byte[] bytes = new byte[offset];
				Console.WriteLine(GetDataTitle());
				for (int i = 0; i < fileStream.Length; i += offset)
				{
					fileStream.Read(bytes, 0, offset);
					/*for (int j = 0; j < bytes.Length; j++)
					{
						Console.Write(bytes[j] + " ");
						if ((j + 1) % 4 == 0)
						{
							Console.Write("|");
						}
						if ((j + 1) % 32 == 0)
						{
							Console.WriteLine();
						}
					}*/
					string line = GetDataString(bytes, false);
					outSteing += line + "\n";
					Console.WriteLine(line);
				}
			}

			filePath = filePath.Replace(".day", suffix);
			Console.WriteLine("save to " + filePath);
			File.WriteAllText(filePath, outSteing);
		}

		static string GetDataTitle()
		{
			return "date" + separtor + "openPrice" + separtor + "highestPrice" + separtor + "lowestPrice" + separtor + "closePrice" + separtor + "money" + separtor + "volume";
		}

		static string GetDataString(byte[] bytes, bool toInt)
		{
			int date = 0;
			int openPrice = 0;
			int highestPrice = 0;
			int lowestPrice = 0;
			int closePrice = 0;
			int money = 0;
			int volume = 0;

			if (bytes.Length < offset)
			{
				return "bytes not matching.";
			}

			date = (bytes[0] & 0xFF) | (bytes[1] & 0xFF) << 8 | (bytes[2] & 0xFF) << 16 | (bytes[3] & 0xFF) << 24;
			openPrice = (bytes[4] & 0xFF) | ((bytes[5] & 0xFF) << 8) | ((bytes[6] & 0xFF) << 16) | ((bytes[7] & 0xFF) << 24);
			highestPrice = (bytes[8] & 0xFF) | ((bytes[9] & 0xFF) << 8) | ((bytes[10] & 0xFF) << 16) | ((bytes[11] & 0xFF) << 24);
			lowestPrice = (bytes[12] & 0xFF) | ((bytes[13] & 0xFF) << 8) | ((bytes[14] & 0xFF) << 16) | ((bytes[15] & 0xFF) << 24);
			closePrice = (bytes[16] & 0xFF) | ((bytes[17] & 0xFF) << 8) | ((bytes[18] & 0xFF) << 16) | ((bytes[19] & 0xFF) << 24);
			//money = (((bytes[20]) | ((bytes[21]) << 6) | ((bytes[22]) << 14) | ((bytes[23]) << 22)));
			money = (bytes[20] & 0xFF) | ((bytes[21] & 0xFF) << 8) | ((bytes[22] & 0xFF) << 16) | ((bytes[23] & 0xFF) << 24);
			volume = (bytes[24] & 0xFF) | ((bytes[25] & 0xFF) << 8) | ((bytes[26] & 0xFF) << 16) | ((bytes[27] & 0xFF) << 24);


			string outString = "";
			if (toInt)
			{
				outString = date + separtor + openPrice + separtor + highestPrice + separtor + lowestPrice + separtor + closePrice + separtor + money + separtor + volume;
			} else {
				outString = date + separtor + (openPrice / 100f) + separtor + (highestPrice / 100f) + separtor + (lowestPrice / 100f) + separtor + (closePrice / 100f) + separtor + money + separtor + volume;
			}
			return outString;
		}


	}
}
