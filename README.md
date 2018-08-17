# About Pac#

## Origin

I created Pac# as a personal challenge. It was born out of a class project to create a small application using Windows Forms or WPF. However, the recommended ideas were, to be frank, rather boring and/or simplistic.

I've used MonoGame previously to create Windows desktop games in C#. This class was targeted for the official Microsoft GUI frameworks, so instead of relying on an external library, I replicated what I knew from MonoGame when creating the core game loop and rendering to the screen.

## Implementation

Pac# is a faithful recreation of the original arcade game. Nearly everything is implemented besides the animations (or "cutscenes") and some of the side effects of the original hardware limitations, like level 256.

I researched and attempted to fully replicate the bugs present in the original AI, such as ghosts targeting the wrong position.

I recreated the graphics (sprites and palettes) by hand.

Since Windows Forms disallows simultaneous playback of multiple sounds, I incorporated [CSCore](https://github.com/filoe/cscore) to be able to fully support sound effects.

Bitmaps in Windows Forms are extremely costly when it comes to performance. I took a lot of care to optimize the amount of copying I had to do. Since the game uses palette swapping frequently, this was quite difficult! Since this isn't an emulator and I don't know the original game's timings, I had to guess as to how long to delay certain things, such as fruit disappearing, the player's death animation, and the start of each level.

Hopefully you won't notice any FPS drops or pauses while playing.

Some things I'm proud of in the source code:
- Finite state machines for AI and the player
- Abstracting most of the game graphics implementation from Windows Forms itself
- Abstracting most of the graphics implementation from the game update logic
- Reusing sprites when rotating the player
- Loading level maps from files that I can create in [Tiled](https://www.mapeditor.org/)
- Gracefully converting between gamespace and screenspace
- Animated sprites with arbritary timings and multiple animations
- Palette swapping
- Cruise Elroy mode

References and Cool Stuff:
- [The Pacman Dossier](https://www.gamasutra.com/view/feature/132330/the_pacman_dossier.php)
- [Emulation](https://www.lomont.org/Software/Games/PacMan/PacmanEmulation.pdf)
- [Pinky's Behavior](http://donhodges.com/pacman_pinky_explanation.htm)
- [Ghost Behavior](https://www.webpacman.com/ghosts.html)
- [Emulator](https://github.com/frisnit/pacman-emulator)
- [Points](http://pacman.wikia.com/wiki/Point_Configurations)
- [Start Up Sequence](https://www.youtube.com/watch?v=0ODSLvP5utI)
