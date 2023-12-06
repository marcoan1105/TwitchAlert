# TwitchAlert

Alerta desktop para lives da Twitch

- A aplicação consiste em logar em um e-mail utilizando o IMAP, verificar os alertas enviados pela Twitch e emiti-los via TrayIcon.

Para executar:

- dotnet publish -c Release
- cd bin/Release/net6.0-windows/publish/
- configurar o appsettings.json de acordo com sua conta de e-mail
- dotnet TwitchAlert.TrayIcon.exe