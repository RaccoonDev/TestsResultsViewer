TestsResultsViewer
==================

This one is a small web application and can show parsed MSTests results in a nice convenient way.

Powershell command for upload file:

gci $folderPath -include *.trx -recurse 
  |% { (new-object System.Net.WebClient).UploadFile("http://HOSTNAME/api/testResultFiles", $_.FullName) }
