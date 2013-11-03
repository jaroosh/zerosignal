using UnityEngine;
using System.Collections;

namespace ZeroSignal.Utilities {

	public class FileReader  {

		public static string ReadFile(string fileName) {
			using(var reader = new System.IO.StreamReader(fileName)) {
				return reader.ReadToEnd();
			}
		}

	}
}