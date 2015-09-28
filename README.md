# MultipartFormData
Build HttpWebRequest stream as Multipart/form-data to manage File Upload.</br>
<a href="https://travis-ci.org/nicolau10010/MultipartFormData">
<img src="https://travis-ci.org/nicolau10010/MultipartFormData.svg?branch=master" />
</a>
# Installation
Simply add the MultipartFormData project to your solution and reference it in the projects you want to use it in.
# Example
```C#
// Create a request for the URL.
WebRequest request = WebRequest.Create ("http://www.contoso.com/default.html");
// If required by the server, set the credentials.
request.Credentials = CredentialCache.DefaultCredentials;
// POST variables.
Dictionary<string, string> values = new Dictionary<string, string>();
values.add("param1", "value1");
values.add("param2", "value2");
// Files to be send.
FileCustom file1 = new FileCustom("filename1", "path/to/file1","application/vnd.openxmlformats-officedocument.wordprocessingml.document");
FileCustom file2 = new FileCustom("filename2", "path/to/file2","image/jpeg");
List<CustomFile> filesList = new List<CustomFile>();
filesList.add(file1);
filesList.add(file2);
// Write request data.
HttpClient.BuildStreamRequest(request, values, filesList);
// Get the response.
WebResponse response = request.GetResponse ();
// Display the status.
Console.WriteLine (((HttpWebResponse)response).StatusDescription);
// Get the stream containing content returned by the server.
Stream dataStream = response.GetResponseStream ();
// Open the stream using a StreamReader for easy access.
StreamReader reader = new StreamReader (dataStream);
// Read the content.
string responseFromServer = reader.ReadToEnd ();
// Display the content.
Console.WriteLine (responseFromServer);
// Clean up the streams and the response.
reader.Close ();
response.Close ();
```
