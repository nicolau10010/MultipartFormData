using System;
using System.IO;

namespace MultipartFormData
{
	/// <summary>
	/// Custom file.
	/// </summary>
	public class CustomFile
	{
		#region Global Variables
		/// <summary>
		/// The name of the file.
		/// </summary>
		private string fileName;

		/// <summary>
		/// The source.
		/// </summary>
		private string source;

		/// <summary>
		/// The type of the content.
		/// </summary>
		private string contentType;

		/// <summary>
		/// The file stream.
		/// </summary>
		private Stream fileStream;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="PhpdocxRestClient.CustomFile"/> class.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="source">Source.</param>
		/// <param name="contentType">Content type.</param>
		/// <param name="fileStream">File stream.</param>
		public CustomFile(string fileName, string source, string contentType)
		{
			this.fileName = fileName;
			this.source = source;
			this.contentType = contentType;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		/// <summary>
		/// Gets or sets the source.
		/// </summary>
		/// <value>The source.</value>
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		/// <summary>
		/// Gets or sets the type of the content.
		/// </summary>
		/// <value>The type of the content.</value>
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		/// <summary>
		/// Gets or sets the file stream.
		/// </summary>
		/// <value>The file stream.</value>
		public Stream FileStream
		{
			get
			{
				return this.fileStream;
			}
			set
			{
				this.fileStream = value;
			}
		}
		#endregion
	}
}

