# CommandCraft
Tool for generating minecraft commands using *grabcraft.com* buildings URLs.
- - -
Note that this is not an official minecraft program. It's not approved by or associated with mojang. 

Also note that, I do not claim any rights to GrabCraft. 
GrabCraft and all related brands, names and content are the property of GrabCraft LLC.
This is just a friendly application to help people generating  in-game structures
- - -
## Screenshots

<img src="screenshots/screenshot1.png" alt="1" width="400" height="300"/>.<img src="screenshots/screenshot2.png" alt="2" width="400" height="300"/><img src="screenshots/screenshot3.png" alt="3" width="400" height="300"/>





## FAQ
**1. Minecraft doesn't detect .mcfunction, even though it's in the right location.**
This is very common. It probably means, that mcfunction contains a block that isn't in your minecraft version, or it was badly translated by CommandCraft. To fix this error, go to ```C:\Users\_thatsyou_\AppData\Roaming\.minecraft\logs```
, then open latest log file, and look for block that caused error.
After that, you can add your custom translation for this block to the ```programdata\user_block_dictionary.json``` file, which is loaded at the start by CommandCraft.

- - -


