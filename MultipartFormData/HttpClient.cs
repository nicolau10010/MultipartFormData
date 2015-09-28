using System;
using System.IO;
using System.Net;
using System.Collections.Generic;

namespace MultipartFormData
{
	public class HttpClient
	{
		/// <summary>
		/// Builds the stream request before execute GetResponse() method.
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="values">Values.</param>
		/// <param name="files">Files.</param>
		public static void BuildStreamRequest(HttpWebRequest request, Dictionary<string, string> values, List<CustomFile> files)
		{
			foreach (CustomFile file in files) {
				file.FileStream = HttpClient.BuildFileStream (file.Source);
			}
			HttpClient.BuildQuery (request, values, files);
		}

		/// <summary>
		/// Builds the file stream.
		/// </summary>
		/// <returns>The file stream.</returns>
		/// <param name="file">Source file</param>
		private static Stream BuildFileStream(string file)
		{
			Stream memStream = new System.IO.MemoryStream();

			FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
			byte[] buffer = new byte[1024];

			int bytesRead = 0;
			while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
			{
				memStream.Write(buffer, 0, bytesRead);
			}
			fileStream.Close();

			return memStream;
		}

		/// <summary>
		/// Builds the query.
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="values">Every dictionary entry is a name, value post variable.</param>
		/// <param name="files">List of files sources</param>
		private static void BuildQuery(HttpWebRequest request ,Dictionary<string, string> values, List<CustomFile> files)
		{	
			var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", System.Globalization.NumberFormatInfo.InvariantInfo);
			request.ContentType = "multipart/form-data; boundary=" + boundary;
			boundary = "--" + boundary;

			using (var requestStream = request.GetRequestStream())
			{
				// Write POST variables.
				foreach (string name in values.Keys)
				{
					var postBuffer = System.Text.Encoding.ASCII.GetBytes(boundary + "\r\n");
					requestStream.Write(postBuffer, 0, postBuffer.Length);
					postBuffer = System.Text.Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, "\r\n"));
					requestStream.Write(postBuffer, 0, postBuffer.Length);
					postBuffer = System.Text.Encoding.UTF8.GetBytes(values[name] + "\r\n");
					requestStream.Write(postBuffer, 0, postBuffer.Length);
				}
				// Write POST files.
				foreach (var file in files)
				{
					var fileBuffer = System.Text.Encoding.ASCII.GetBytes(boundary + "\r\n");
					requestStream.Write(fileBuffer, 0, fileBuffer.Length);
					fileBuffer = System.Text.Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", file.FileName, file.Source, "\r\n"));
					requestStream.Write(fileBuffer, 0, fileBuffer.Length);
					fileBuffer = System.Text.Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, "\r\n"));
					requestStream.Write(fileBuffer, 0, fileBuffer.Length);

					file.FileStream.Position = 0;
					byte[] tempBuffer = new byte[file.FileStream.Length];
					file.FileStream.Read(tempBuffer, 0, tempBuffer.Length);
					requestStream.Write(tempBuffer, 0, tempBuffer.Length);
					fileBuffer = System.Text.Encoding.ASCII.GetBytes("\r\n");
					requestStream.Write(fileBuffer, 0, fileBuffer.Length);
				}
				var boundaryBuffer = System.Text.Encoding.ASCII.GetBytes(boundary + "--");
				requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
				// Close and release the resource.
				requestStream.Close ();
			}
		}
	}
}

