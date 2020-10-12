using System;
using System.IO;

namespace TestFileStreamCrossProcessLock
{
	class Program
	{
		static void Main (string[] args)
		{
			try {
				LockFile();
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
		}

		static void LockFile()
		{
			string fileName = Path.GetFullPath ("lock.txt");
			Console.WriteLine ("Locking file {0}", fileName);

			using (var fileStream = new FileStream (fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)) {

				// Mono requires FileStream.Lock to prevent two processes accessing the file.
				// .NET Core does not and will throw an exception if Lock is called on OSX.
				// fileStream.Lock (0, 0);

				Console.WriteLine ("File locked. Press a key to quit");
				Console.ReadKey ();
			}
		}
	}
}