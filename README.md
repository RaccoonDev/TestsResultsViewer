TestsResultsViewer
==================

This one is a small web application and can show parsed MSTests results in a nice convenient way.

Powershell command for upload file:

param(
[string]$path = "D:\file\"
)
Add-Type -Language CSharp @"
using System;
using System.Net;

public class ExtendedWebClient : WebClient
{
	private int timeout;
	
    public int Timeout { 
		get { return timeout;}
		set{timeout = value;} 
	}
    
    protected override WebRequest GetWebRequest(Uri address)
    {
        var request = base.GetWebRequest(address);
        if (request != null)
        {
            request.Timeout = timeout;
            var httpRequest = request as HttpWebRequest;
            if (httpRequest != null)
            {
                httpRequest.AllowWriteStreamBuffering = false;
            }
        }
        return request;
    }

    public ExtendedWebClient()
    {
        Timeout = int.MaxValue; // override standard HTTP Request Timeout
    }
}
"@

$client = New-Object ExtendedWebClient

foreach($item in ( dir $path "*.trx")) { $client.UploadFile("http:/HOSTNAME/api/testResultFiles", $item.FullName) }
