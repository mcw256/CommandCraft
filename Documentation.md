# CommandCraft - Documentation
----------------
### Table of contents
1. Business/Domain logic
2. Naming conventions

-----------------

### 1. Business/Domain logic

**What's the purpose of project?**


Application that will take [https://www.grabcraft.com](www.grabcraft.com) structure URL as **input**   
*for example:* ```https://www.grabcraft.com/minecraft/pig-statue/farm-animals-1```  
and generate and save as **output** ```.mcfuntion``` file  that can be run in minecraft to instantly spawn that building.

**How it will be realised?**

1. Download html source of that given input URL
2. Look through html source to find and extract fragment with URL to .js file(*LayerMap*)  
```html
<script src="https://wwwgrabcraftcom-jzoul76nauwdp2hxnkfs.stackpathdns.com/js/LayerMap/LayerMap_35.js"></script>     
```
3. Download that .js file  
_example of how couple lines of LayerMap.js file may look like:_
```json
var layerMap = {"1":[{"x":5,"y":631,"s":21,"h":"Black Wool","y1":305,"x2":211},{"x":5,"y":611,"s":21,"h":"Pink Wool","x2":211},{"x":5,"y":591,"s":21,"h":"Pink Wool","x2":211},{"x":5,"y":571,"s":21,"h":"Black Wool","x2":211},
```
  *as you can see, just be cuting out* ```var layerMap = ``` *string you can obtain ```.json``` file that can be Deserialized*

4. Deserialize LayerMap json
5. Using acquired data, generate string containing minecraft setblock commands separated by new line. *Very short example:*  
```
setblock 3 3 18 minecraft:black_wool replace
setblock 3 3 16 minecraft:cobblestone replace
setblock 3 3 17 minecraft:hay_block[axis=y] replace
setblock 3 3 15 minecraft:oak_log[facing=east] replace
```

6. Save setblocks-containing string as ```exampleBuilding.mcfuntion``` file in user-specified minecraft appdata directory. *example:* ```C:\Users\Josh\AppData\Roaming\.minecraft\saves\UserSelectedSave\datapacks\CommandCraft\data\CommandCraft\functions```

**What grabcraft's LayerMap.js file represents?**

Data contained in layerMap is used to generate interactive blueprint map. Which is basically all html code contained between these ```map``` marks
```html
<map name="layerMap" id="layerMap">...</map>
```

example item:
```html
<area shape="rect" coords="5,305,26,326" title="Black Wool" class="material top_0" data-related="side_0,data_0" href="javascript:;">
```
and here is the result

![](https://i.ibb.co/9Hh0D3N/abc2.png)


Now let's look closely what each of ```area``` attributes mean:

```shape="rect"``` - this means we draw on our map, rectangle shaped figure. For more information, checkout [html map tag documentation](https://www.w3schools.com/tags/tag_map.asp)

```coords="5,305,26,326"``` - coordinates(x,y) of left-top(*```5,305```*) and right-bottom(*```26,326```*) corners of our little rectangle. The origin of coordinates is left-top corner of map image. These example coordinates are the ones of selected black wool on the image above. As you may calculated, side length of that little rectangle is ```21```. And it will be 21 for every single little rectangle in this pig project(oh, I didn't mention that's a part of blueprint of a pig ;D). But note:**Side length depends on how big one project is and it's not always the same!**

```title="Black Wool"``` - That's the name of minecraft block.

```class="material top_0" data-related="side_0,data_0" href="javascript:;"``` - Couple more attributes we don't need to care about. Well, actually ```href``` is important because it tells us that the source of data is in the javascript file.(LayerMap.js). Now let's look back into it:


To explain what each of attributes contained in json mean I need to refer to this image:![](https://i.ibb.co/s3rKwT4/abc.png)

```json
{"1":[{"x":5,"y":631,"s":21,"h":"Black Wool","y1":305,"x2":211}, {"x":5,"y":611,"s":21,"h":"Pink Wool","x2":211}
```
```"1"``` - refers to **1** on image. As the minecraft buildings are in 3dimension this is just z-coordinate of every block contained in the follwing ```[]```.  

```"x":5,"y":631``` - coords of left-top corner of area rectangle in html. Right-bottom corner in html is calculated using next atribute which is:

```"s":21``` - side length

```"h":"Black Wool"``` - name of the block(title in html)

```"y1":305,"x2":211``` - This attributes not always exist. They mean that the block is placed in one of side sections of blueprint **2**,**3** on image. **We are only interested in blocks that are not containing y1 or x2 attributes!**. Becuase we don't need data about cross sections. We only need data of blocks located on white background on image.

y of Black Wool is 631, and y of Pink Wool next to it is 611.  
631 - 611 = 20. So to know what are distances between adjacent blocks we need to calculate s-1 (that's because of area border)


-------------
So, to convert that layerMap data to normal 3d structure data we need to:
* Read ```s```, and set variable ```interval = s-1```.
* Discard blocks containing ```y1``` or ```x2``` attributes.
* Find the origin of white backgrounded area on layerMap
(```x``` is always 5, but ```y``` needs to be calculated: ```origin_y = max_z * interval) + 11```)
* To convert, apply following rules to every block
```csharp
int newX = (item.X - 5) / interval;
int newY = (item.Y - startingY) / interval;
```
* also ```int newZ = item.Z-1``` so it starts from 0

**Minecraft item names and Grabcraft item names**

TODO

-------------------------------

### 2. Naming conventions

Here is how will I refer to certain things in code.

* **Building** - single project on grabcraft, *link to example building:* ```https://www.grabcraft.com/minecraft/pig-statue/farm-animals-1``` _Despite grabcrafts categorising it's projects into categoires like buildings, statues, working mechanisms etc. I will refer to all of them as buildings._

* **Layermap** - file/string/json containing raw/slightly edited building's layerMap.js data

* **GBlock** - data structure that represents single minecraft object:
  * X
  * Y
  * Z
  * BlockGInfo

* **MBlock** - _(derives Block)_ data structure that represents Block with normalized coords. And depending on additional property ```bool IsMismatch``` translated if false or non-translated if true - Info.

* **GCoords** _(abstract)_ Blocks coordinates system used Layermap

* **MCoords** _(abstract)_ normalized coordinates that can be used to produce minecraft function

* **Coords Normalization** _(abstract)_ converting GCoords to MCoords

* **BlockGInfo** - grabcraft's way of identyfing minecraft items in layermap *example:* ```Spruce Fence Gate (Facing South, Closed)```

* **BlockGName** - part of BlockGInfo containing only name *example:* ```Spruce Fence Gate```

* **BlockGAttribute** - single attribute of BlockGInfo *example:* ```Facing South```

* **BlockMInfo** - minecraft way of identyfing objects *example:* ```minecraft:spruce_fence_gate[facing=south,open=false]```

* **BlockMName** - part of BlockMInfo containing only name *example:* ```minecraft:spruce_fence_gate```

* **BlockMAttribute** - single attribute of BlockMInfo *example:* ```facing=south```

* **MFunction** - string, output of a program, set of minecraft setblocks commands separated by new line
