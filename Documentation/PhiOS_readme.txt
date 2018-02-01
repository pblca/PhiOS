PhiOS (ASCII Rendering Engine for Unity)
--------------------------------------------
VERSION: v0.1
LAST UPDATED: 06/12/2016
AUTHOR: https://twitter.com/phi6
LICENSE: CC BY 4.0
https://creativecommons.org/licenses/by/4.0/
--------------------------------------------

This is the first (base) version of PhiOS, which provides cell rendering, cell stacking/layers, 
foreground & background colors, cell transition animations, bitmap font support & rudimentary mouse input.

The layered windowing system and scrollable UI widgets are not included in this release (yet).
All that stuff will be in future extended versions. SOON™

Note: PhiOS does NOT include any post-processing image effects (all that fancy stuff like scanlines,
bloom, CRT monitor distortion, glitches etc... that you may have seen on previous screenshots). 
These are all provided by third party plugins, please see the Third Party Effects section below 
if you want to replicate your display to look like this.

Disclaimer: There will probably be bugs and incomplete/missing features! This engine was always intended
to be part of a larger game project, and not as a tool released to the public. However, I'm putting it
out there anyway due to popular request. Code is provided as-is.

QUICKSTART
----------
1. Import the PhiOS_Base_0_1.unitypackage into your project.

2. The best place to start is to check out the ExampleScene under the PhiOS/Scenes folder.

3. ExampleScene contains 2 GameObject prefabs, PhiOS_Bootstrap and Scene_Example.
The PhiOS_Bootstrap object is required in all Unity scenes that make use of an ASCII display.
The Scene_Example object contains a MonoBehaviour script showing examples of basic functionality.

4. Study the code in the Example.cs script (the one attached to the Scene_Example object) and you'll be
rendering, clearing and animating cells in no time.

FONTS
-----
PhiOS uses the default REXPaint Codepage 437 font to help you get started quickly.
You can also use your own fonts in one of two ways :-

1. Use a REXPaint-compatible font tileset. This is the preferred method.
- Simply copy and paste the REXPaint font .png directly into your Unity project. 
The higher the resolution the better, so I upscaled the default 20x20 font to 60x60 using 
Nearest Neighbour scaling in order to preserve the hard pixels.
- Your .png texture should be imported into Unity with the Alpha From Grayscale and bilinear 
filtering enabled.
- Create a new material for your font texture, you can use my Phigames/Alpha Vertex Color shader 
but any transparent vertex color shader will work.
- Assign this material to the BitmapFont MonoBehaviour component on the Display object of the 
bootstrap in your scene hierarchy, under the REXPaint CP437 header.
- Make sure "Use REX Paint Font" IS ticked.
- Ensure texture size and grid size is correct for your .png font texture.
- Bilinear filtering will cause some bleeding artifacts, so set your bleed here to counteract
this. The bleed value of 3 works well with the default font.
- You can use the Quad Height Scale variable to vertically stretch your font. Square fonts 
(scale of 1) are preferable for topdown map tiles, but this is less readable as text. The default 1.4 
scale seems to be a nice compromise between readable text and aesthetically pleasing map tiles.

OR...

2. Convert a TTF to bitmap using BMFont. I used the following settings to generate the font used by 
PhiOS (Source Code Pro from the Font Squirrel website, license is included).
	> 	Font Settings: Size 72px, do not include kerning pairs, use TTF outline, hinting, smoothing,
		super sampling level 4
	> 	Export Options: 0 padding, 5 spacing, equalize cell heights, 2048x2048 texture size, white text 
		with alpha, XML file format, .png textures
- You will require both the .fnt (XML) and .png exported files, drop them both into your Unity project.
- Your .png texture should be imported into Unity with both alpha options and bilinear filtering enabled.
- Create a new material for your font texture, you can use my Phigames/Alpha Vertex Color shader 
but any transparent vertex color shader will work.
- Assign your .fnt and material to the BitmapFont MonoBehaviour component on the Display object 
of the bootstrap in your scene hierarchy, under the BMFont header.
- Make sure "Use REX Paint Font" is NOT ticked.
- Ensure your glyph height and glyph width (default is 72x34) matches the values in your .fnt (XML) file.
- You can use the Quad Height Scale variable to vertically stretch your font, but as you're using a 
TTF it's probably best to keep this at 1, unless you want to artificially make your font square.

DISPLAY GRID SIZE
-----------------
By default, PhiOS uses a fixed display width of 80 characters, and then automatically calculates 
the height based on the font used, the quad height scale and the screen aspect ratio. It will then 
adjust the camera's orthographic size in order to fit the display to the edges of the screen as tightly 
as possible.

If you wish, you can disable the automatic grid height, for cases where you might want a fixed grid 
size regardless of screen aspect ratio. Go to the Display MonoBehaviour component on the Display 
object of the bootstrap in your scene hierarchy, and untick Auto Display Height. You are now able 
to set your grid height manually.

API QUICKSTART
--------------
Check the Example.cs script for examples of basic functionality.
Things to note :-
- Make sure you wait until Display.IsInitialized() returns true before updating any cells.
- Grab the cell you want to update by calling Display.CellAt(layer, x, y)
- Update the cell content using Cell.SetContent()
- Clear the cell using Cell.Clear()
- Check out the SetContent() and Clear() function overloads. You can have separate foreground and 
background colors, as well as gradual fade transitions between cell updates.
- Only cells on the highest layers are drawn, everything below that cell on lower layers is ignored.
- x,y coordinates start at top left of the screen (0,0)
- Negative layers are reserved layers, and are actually drawn on top of everything else. An example 
of this is the mouse cursor, which draws at layer -1. Lower negative layers (such as -2) are prioritized 
over higher negative ones.
- You can set a cell to cycle through characters of a string as part of its update transition.
Handy for creating cyberpunky/hacking/glitching effects.
- You can implement one of the action interfaces (IClickAction, IDragAction, IHoverAction, IScrollAction) 
and set them to a specific cell in order to capture mouse input for that cell. Or just disable the Mouse
object entirely if you do not require mouse input.

THIRD PARTY EFFECTS
-------------------
My screenshots and videos show a variety of post-processing image effects to achieve a certain cyberpunk 
CRT aesthetic that I'm using for my games. You can replicate this look by using the following third party 
plugins :-
- Bloom from Unity's Standard Effects asset package.
- AnalogTV and Vintage from Thomas Hourdel's Colorful asset package.
- Dirty Lens from Sonic Ether's Natural Bloom asset package.

CONTACT
-------
Shout at me on Twitter @phi6 if I've forgotten to explain something important. I'm not officially providing
any significant amount of support for this, but off the record I'm generally happy to help if I can!

Good luck, and be sure to poke me if you've made something cool! :)