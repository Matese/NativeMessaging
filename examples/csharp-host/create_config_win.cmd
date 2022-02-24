@echo off

REG ADD "HKLM\SOFTWARE\Mozilla\NativeMessagingHosts\ping_ponger" /ve /d "D:\02.github\Matese\NativeMessaging\examples\csharp-host\app\PingPonger\bin\Debug\net6.0\FirefoxAppManifest.json"
REG ADD "HKCU\SOFTWARE\Mozilla\NativeMessagingHosts\ping_ponger" /ve /d "D:\02.github\Matese\NativeMessaging\examples\csharp-host\app\PingPonger\bin\Debug\net6.0\FirefoxAppManifest.json"