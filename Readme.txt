1.  Twilio package version 5.13.4 does NOT compile to return TwiML(messagingResponse).
Therefore, working with 5.13.3.

2.  Each time I startd Ngrok, I had to update the forward URL such as https://f4cd06e9.ngrok.io  in Twilio SMS console

3.  Following need to be changed in Web.Config
a.  accountSid
b.  authToken
c.  ErrorLogPath
d.  Connection string should not require change but 

4.  Using Visual Studio .NET 2017 Community Edition and Entity Framework 6.x
5.  Since we have only one cell phone, it has been tested only with that number.
6.  I used Ngrok to tunnel through the proxy and my local fire wall.