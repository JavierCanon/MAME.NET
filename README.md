# MAME.NET

## Multiple Arcade Machine Emulator in .NET

MAME (originally an acronym of Multiple Arcade Machine Emulator) is a free and open-source emulator designed to recreate the hardware of arcade game systems in software on modern personal computers and other platforms.[2] The intention is to preserve gaming history by preventing vintage games from being lost or forgotten. The aim of MAME is to be a reference to the inner workings of the emulated arcade machines; the ability to actually play the games is considered "a nice side effect".[3] Joystiq has listed MAME as an application that every Windows and Mac gamer should have.[4]

The first public MAME release was by Nicola Salmoria on February 5, 1997. The emulator now supports over 7,000 unique games and 10,000 actual ROM image sets, though not all of the supported games are playable. MESS, an emulator for many video game consoles and computer systems, based on the MAME core, was integrated upstream into MAME in 2015.

## Introduction
MAME (Multiple Arcade Machine Emulator) is a free and open source emulator designed to recreate the hardware of arcade game system in software on modern personal computers and other platforms. MAME.NET is a C# based arcade emulator, and it maintains the same architecture of MAME. By using C# and the powerful integrated development environment -- Microsoft Visual Studio, there is no macro and you can debug the supported arcade game anywhere. There are some classic boards supported by now: M72, M92, CPS-1, CPS-1(Qsound), CPS2, Neo Geo, Namco System 1, IGS011, PGM(PolyGame Master).

MAME.NET runs at following steps: load the ROMs, initialize the machine, soft reset the machine, and loop "cpuexec_timeslice" operation. The "cpuexec_timeslice" operation means sequentially execute every CPU for a time slice, and execute timer callbacks. Timer callbacks contains: video update, soft reset, CPU interrupt, sound update, watchdog reset and other interrupts. By these steps, MAME.NET emulates the arcade board successfully. MAME.NET has more functions: save and load state, record and replay input, cheat, cheat search, IPS (patch main ROM), board debugger, CPU debugger.


## Contents

1	History and overview
2	Design
2.1	Game data
3	Philosophy and accuracy
4	Legal status
5	Original MAME-license
6	See also
7	References
8	External links

## History and overview
The project was started by the Italian programmer Nicola Salmoria. MAME traces its roots to an earlier emulator project called Multi-Pac, but the name was changed as more and more games started to be emulated within the MAME framework. The first version was released in 1996.[5] In April 1997, Salmoria stepped down for his national service commitments, handing stewardship of the project to fellow Italian Mirko Buffoni for a period of half a year. In May 2003, David Haywood took over the job of the coordinator. From April 2005 to April 2011, the project was coordinated by Aaron Giles.[6] Angelo Salese stepped in as the new coordinator.[7] In 2012, Miodrag Milanovic took over.[8] The project is supported by hundreds of developers around the world and thousands of outside contributors.

At first, MAME was developed exclusively for MS-DOS, but it was soon ported to Unix-like systems (X/MAME), Macintosh (MacMAME and later MAME OS X) and Windows (MAME32). Since 24 May 2001 with version 0.37b15[6], the main development occurs on the Windows platform, and most other platforms are supported through the SDLMAME project, which was integrated into the main development source tree in 2006.[9] In addition, different versions of MAME have been ported to other computers, game consoles, mobile phones and PDAs, and at one point even to digital cameras.[10] In 2012, Google ported MAME to Native Client, which allows MAME to run inside Chrome.[11]

Major releases of MAME occur approximately once a month. Windows executables in both 32-bit and 64-bit fashion are released on the official web site of the development team, along with the complete source code.[12] Smaller, incremental "u" (for update) releases were released weekly (until version 0.149u1) as source diffs against the most recent major version, to keep code in synchronization among developers.[13] The MAME source code is developed on a public GitHub repository.[14] This allows those with the required expertise and tools to build the most up-to-date version of the code and contribute enhancements in the form of pull requests. Historical version numbers 0.32, and 0.38 through 0.52 inclusively, do not exist; the former was skipped due of similar naming of the MAME32 variant (which itself has since been renamed MAMEUI due to the move to 64-bit builds), while the latter numbers were skipped due to the numerous releases in the 0.37 beta cycle (these version numbers have since been marked next to their equivalent 0.37 beta releases in the official MAMEdev website).[15]

The architecture of MAME has been extensively improved over the years. Support for both raster and vector displays, as well as multiple CPUs and sound chips, were added to MAME in the first six months of the project. A flexible timer system to coordinate the synchronization between multiple emulated CPU cores was implemented, and ROM images started to be loaded according to their CRC32 hash in the ZIP files they were stored in.[6] MAME has pioneered the reverse engineering of many undocumented system architectures, various CPUs (such as the M6809-derivative custom Konami CPU with new instructions) and sound chips (for example the Yamaha FM sound chips), and MAME developers have been instrumental in the reverse engineering of many proprietary encryption algorithms utilized in arcade games. Examples of these include the Neo Geo, CP System II, CP System III and many others.[citation needed]

The popularity of MAME has well since broken through to the mainstream, with enthusiasts building their own arcade game cabinets to relive the old games, and with companies producing illegal derivative works of MAME to be installed in arcades. Cabinets can be built either from scratch or by taking apart and modifying a genuine arcade game cabinet that was once used with the real hardware inside.[16][17] Cabinets inspired by classic arcade games can also be purchased and assembled (with optional and MAME preinstalled).[18]

Although MAME contains a rudimentary user interface, the use of MAME in arcade game cabinets and home theaters necessitates special launcher applications called front ends with more advanced user interfaces. Front ends provide varying degrees of customization – allowing one to see images of the cabinets, history of the games and tips on how to play, and even video of the game play or attract mode of the game.

The information contained within MAME is free for re-use, and companies have been known to utilize MAME when recreating their old classics on modern systems. Some have gone as far as to hire MAME developers to create emulators for their old properties. An example of this is the Taito Legends pack which contains ROMS readable on select versions of MAME.[19]

Since 2012, MAME is maintained by then MESS project leader Miodrag Milanović.[8]

On May 27, 2015 (0.162), the games console and computer system emulator MESS was integrated with MAME (so the MESS User Manual is still the most important usage instruction for the non-arcade parts of MAME).[20]

In May 2015, it was announced that MAME's developers were planning to re-license the software under a more common free and open-source license, away from the original MAME-license. MAME developer Miodrag Milanovic explained that the change is intended to draw more developer interest to the project, allow the manufacturers of games to distribute MAME to emulate their own games, and make the software a "learning tool for developers working on development boards". The transition of MAME's licensing to the BSD/GPL licenses was completed in March 2016.[21][22] With the license change, most of MAME's source code (90%+) is available under a three-clause BSD license and the complete project is under the GNU General Public License version 2 or later.[21][23]

On Feb 24, 2016 (0.171), MAME embedded MEWUI front-end (and developer joined the team), providing MAME with a flexible and more full-featured UI.[24]

## Design

This section possibly contains original research. Please improve it by verifying the claims made and adding inline citations. Statements consisting only of original research should be removed. (July 2019) (Learn how and when to remove this template message)
The MAME core coordinates the emulation of several elements at the same time. These elements replicate the behavior of the hardware present in the original arcade machines. MAME can emulate many different central processing units (CPUs) and associated hardware. These elements are virtualized so MAME acts as a software layer between the original program of the game, and the platform MAME runs on. MAME supports arbitrary screen resolutions, refresh rates and display configurations. Multiple emulated monitors, as required by for example Darius, are supported as well.

Individual arcade systems are specified by drivers which take the form of C preprocessor macros. These drivers specify the individual components to be emulated and how they communicate with each other. While MAME was originally written in C, the need for object oriented programming caused the development team to begin to compile all code as C++ for MAME 0.136, taking advantage of additional features of that language in the process.

Although a great majority of the CPU emulation cores are interpretive, MAME also supports dynamic recompilation through an intermediate language called the Universal Machine Language (UML) to increase the emulation speed. Back-end targets supported are x86 and x64. A C backend is also available to further aid verification of the correctness. CPUs emulated in this manner are SH-2, MIPS R3000 and PowerPC.

## Game data

The original program code, graphics and sound data need to be present so that the game can be emulated. In most arcade machines, the data is stored in read-only memory chips (ROMs), although other devices such as cassette tapes, floppy disks, hard disks, laserdiscs, and compact discs are also used. The contents of most of these devices can be copied to computer files, in a process called "dumping". The resulting files are often generically called ROM images or ROMs regardless of the kind of storage they came from. A game usually consists of multiple ROM and PAL images; these are collectively stored inside a single ZIP file, constituting a ROM set. In addition to the "parent" ROM set (usually chosen as the most recent "World" version of the game), games may have "clone" ROM sets with different program code, different language text intended for different markets etc. For example, Street Fighter II Turbo is considered a variant of Street Fighter II Champion Edition. System boards like the Neo Geo that have ROMs shared between multiple games require the ROMs to be stored in "BIOS" ROM sets and named appropriately.

Hard disks, compact discs and laserdiscs are stored in a MAME-specific format called CHD (Compressed Hunks of Data).[25] Some arcade machines use analog hardware, such as laserdiscs, to store and play back audio/video data such as soundtracks and cinematics. This data must be captured and encoded into digital files that can be read by MAME. MAME does not support the use of external analog devices, which (along with identical speaker and speaker enclosures) would be required for a 100% faithful reproduction of the arcade experience. A number of games use sound chips that have not yet been emulated successfully. These games require sound samples in WAV file format for sound emulation. MAME additionally supports artwork files in PNG format for bezel and overlay graphics.

## Philosophy and accuracy

The stated aim of the project is to document hardware, and so MAME takes a somewhat purist view of emulation, prohibiting programming hacks that might make a game run improperly or run faster at the expense of emulation accuracy. Components such as CPUs are emulated at a low level (meaning individual instructions are emulated) whenever possible, and high-level emulation (HLE) is only used when a chip is completely undocumented and cannot be reverse-engineered in detail. Signal level emulation is used to emulate audio circuitry that consists of analog components.

We want to document the hardware. Now a lot of people will say; "Where's your document? You just write a bunch of source code." And yes, that's true. One thing I've learned is that keeping documentation synced with source code is nearly impossible. The best proof that your documentation is right is "does this code work".

— Aaron Giles, California Extreme 2008[26]
MAME emulates well over a thousand different arcade system boards, a majority of which are completely undocumented and custom designed to run either a single game or a very small number of them. The approach MAME takes with regards to accuracy is an incremental one; systems are emulated as accurately as they reasonably can be. Bootleg copies of games are often the first to be emulated, with proper (and copy protected) versions emulated later. Besides encryption, arcade games were usually protected with custom microcontroller units (MCUs) that implemented a part of the game logic or some other important functions. Emulation of these chips is preferred even when they have little or no immediately visible effect on the game itself. For example, the monster behavior in Bubble Bobble was not perfected until the code and data contained with the custom MCU was dumped through the decapping of the chip.[27] This results in the ROM set requirements changing as the games are emulated to a more and more accurate degree, causing older versions of the ROM set becoming unusable in newer versions of MAME.

Portability and genericity are also important to MAME. Combined with the uncompromising stance on accuracy, this often results in high system requirements. Although a 2 GHz processor is enough to run almost all 2D games, more recent systems and particularly systems with 3D graphics can be unplayably slow, even on the fastest computers. MAME does not currently take advantage of hardware acceleration to speed up the rendering of 3D graphics, in part because of the lack of a stable cross-platform 3D API, and in part because software rendering can in theory be an exact reproduction of the various custom 3D rendering approaches that were used in the arcade games.

## Legal status
Owning and distributing MAME itself is legal in most countries, as it is merely an emulator. Companies such as Sony have attempted in court to prevent other software such as Virtual Game Station, a Sony Playstation emulator from being sold, but they have been ultimately unsuccessful.[28] MAME itself has thus far not been the subject of any court cases.

Most arcade games are still covered by copyright. Downloading or distributing copyrighted ROMs without permission from copyright holders is almost always a violation of copyright laws. However, some countries (including the US)[29] allow the owner of a board to transfer data contained in its ROM chips to a personal computer or other device they own. Some copyright holders have explored making arcade game ROMs available to the public through licensing. For example, in 2003 Atari made MAME-compatible ROMs for 27 of its arcade games available on the internet site Star ROMs. However, by 2006 the ROMs were no longer being sold there. At one point, various Capcom games were sold with the HotRod arcade joystick manufactured by Hanaho, but this arrangement was discontinued as well. Other copyright holders have released games which are no longer commercially viable free of charge to the public under licenses that prohibit commercial use of the games. Many of these games may be downloaded legally from the official MAME web site.[30] The Spanish arcade game developer Gaelco has also released World Rally for non-commercial use on their website.[31]

The MAME community has distanced itself from other groups redistributing ROMs via the internet or physical media, claiming they are blatantly infringing copyright and harm the project by potentially bringing it into disrepute.[32] Despite this, illegal distributions of ROMs are widespread on the internet, and many "Full Sets" also exist which contains a full collection of a specific version's roms.[33][34] In addition, many bootleg game systems, such as arcade multi carts, often use versions of MAME to run their games.

## Original MAME-license
MAME was formerly distributed under a custom own-written copyleft license, called "MAME license" or "MAME-like license", which was adopted also by other projects, e.g. Visual Pinball. This old "MAME license" ensures the source code availability, while the redistribution in commercial activities is prohibited. Due to this clause, the license is incompatible with the OSI's Open source definition and the FSF's Free software definition. The non-commercial clause was designed to prevent arcade operators from installing MAME cabinets and profiting from the works of the original manufacturers of the games.[22] The ambiguity of the definition "commercial" lead to legal problems with the license.[35][36]

Since March 2016 with version 0.172, MAME itself switched to common free and open source software licenses, the BSD and GPL licenses.[37]

## References
 "Releases - mamedev/mame". Retrieved December 25, 2019 – via GitHub.
 Herz, J.C. (March 5, 1998). "With Software Sleight of Hand, Video Ghosts Walk". The New York Times. Archived from the original on April 14, 2019. Retrieved July 3, 2013.
 "MAME | About MAME". Mamedev.org. Retrieved April 11, 2011.
 Quilty-Harper, Conrad (December 16, 2005). "PC and Mac Applications that Every Gamer Should Have". Joystiq. Archived from the original on January 6, 2006. Retrieved July 3, 2013.
 Maragos, Nich (July 25, 2015). "Afterlife: The World of Console Game Emulation". 1UP.com. Archived from the original on July 25, 2015. Retrieved August 21, 2018.
 "MAME Project History". Retrieved April 23, 2011.
 Giles, Aaron (April 5, 2011). "Regime Change". Mamedev.org. Retrieved July 3, 2013.
 Milanovic, Miodrag (April 26, 2012). "Passing the torch". Mamedev.org. Retrieved April 26, 2012.
 "The SDLMAME Homepage". Rbelmont.mameworld.info. October 13, 2006. Retrieved April 11, 2011.
 IGN Staff (November 3, 1999). "But Wait, That's a Camera..." IGN. Archived from the original on February 22, 2014. Retrieved July 3, 2013.
 Wawro, Alex (January 3, 2012). "MAME Runs In Google Chrome, Plays All Your Favorite Arcade Games". PC World. Archived from the original on September 7, 2013. Retrieved July 3, 2013.
 "MAME Latest MAME Release". Retrieved April 23, 2011.
 "MAME Source Updates". Archived from the original on April 20, 2011. Retrieved April 23, 2011.
 "GitHub - mamedev/mame: MAME - Multiple Arcade Machine Emulator". Retrieved May 19, 2016.
 "MAME Previous Releases".
 St. Clair, John (2004). Project Arcade: Build Your Own Arcade Machine. Indianapolis, IN: Wiley. ISBN 0764556169.
 Roush, George (April 16, 2008). "Build Your Own MAME Machine". IGN. Archived from the original on December 16, 2012. Retrieved July 3, 2013.
 Harris, Craig (November 30, 2005). "Dream Arcade Cocktail Kit". IGN. Archived from the original on February 22, 2014. Retrieved July 3, 2013.
 "Taito Legends manual" (PDF). Sega. Retrieved April 23, 2011.[permanent dead link]
 "MAME 0.162". MAMEDEV.org.
 Wawro, Alex (March 4, 2016). "10 months later, MAME finishes its transition to open source". Gamasutra. Archived from the original on April 22, 2016. Retrieved March 5, 2016.
 Wawro, Alex (May 15, 2015). "MAME is going open source to be a 'learning tool for developers'". Gamasutra. Archived from the original on May 16, 2015. Retrieved May 27, 2015.
 "MAME is now Free and Open Source Software". MAMEdev.org. Retrieved March 5, 2016.
 http://mamedev.org/releases/whatsnew_0171.txt
 "MAME | src/lib/util/chd.h". Mamedev.org. Archived from the original on July 4, 2013. Retrieved December 20, 2012.
 Giles, Aaron (July 17, 2009). "Aaron Giles at California Extreme 2008 – Part 2". YouTube. Retrieved December 20, 2012.
 Salmoria, Nicola. "Nicola's MAME Ramblings". Retrieved July 3, 2013.
 Glasner, Joanna (February 10, 2000). "Court Upholds PlayStation Rival". Wired. Archived from the original on June 18, 2001. Retrieved September 26, 2006.
 "17 U.S. Code § 117 (a)". U.S. Copyright Office. Retrieved February 8, 2014.
 "MAME ROMs for Free Download". Retrieved July 3, 2013.
 "Gaelco Games at Home!". Archived from the original on May 18, 2013. Retrieved July 3, 2013.
 "FAQ: Roms". MAME development site. Retrieved December 28, 2013.
 "Make The Most of It". PC Magazine. Vol. 26 no. 17. September 4, 2007. p. 61. Retrieved December 28, 2013.
 "Game On". Popular Science. Vol. 270 no. 4. April 2007. p. 78. Retrieved December 28, 2013.
 "David Haywood's Homepage » The 'Already Dead' Theory." October 31, 2013. Archived from the original on October 31, 2013.
 "So why did this annoy me so much?". mameworld.info. October 22, 2013. Retrieved October 29, 2017.
 "MAME is now Free and Open Source Software". MAMEDEV.org.

## External links
- https://github.com/mamedev/mame based simulator.
- https://www.mamedev.org/ 
- Official website http://www.mameworld.info/ MAMEworld MAME resource and news site
- http://adb.arcadeitalia.net/ Arcade Database Database containing details of any game supported by Mame, including past versions. There are images, videos, programs for downloading extra files, advanced searches, graphics and many other resources.

Detail: https://www.codeproject.com/Articles/1275365/MAME-NET

You should install Microsoft .NET Framework 3.5 or higher before running the program. You should download MAME.NET ROM files in roms directory.

### Screenshots

![sh01](screenshots/sh01.png) ![sh02](screenshots/sh02.png) ![sh04](screenshots/sh04.png)

### Hotkey: 

- F3 -- soft reset,
- F7 -- load state,
- Shift+F7 -- save state,
- F8 -- replay input,
- Shift+F8 -- record input (start and stop),
- 0-9 and A-Z after state related hotkey -- handle certain files,
- F10 -- toggle global throttle,
- P -- pause and continue,
- shift+P -- skip a frame.

### Control key:

- 1 -- P1 start,
- 2 -- P2 start,
- 5 -- P1 coin,
- 6 -- P2 coin,
- R -- Service 1,
- T -- Service,
- W -- P1 up,
- S -- P1 down,
- A -- P1 left,
- D -- P1 right,
- J -- P1 button1,
- K -- P1 button 2,
- L -- P1 button 3,
- U -- P1 button 4,
- I -- P1 button 5,
- O -- P1 button 6,
- Up -- P2 up,
- Down -- P2 down,
- Left -- P2 left,
- Right -- P2 right,
- NumPad1 -- P2 button 1,
- NumPad2 -- P2 button 2,
- NumPad3 -- P2 button 3,
- NumPad4 -- P2 button 4,
- NumPad5 -- P2 button 5,
- NumPad6 -- P2 button 6.

When the ROMs of a game are loaded, the emulator is auto paused. Press P to continue.

Occasionally GDI+ error occurs and a red cross is shown. You can click "File-Reset picturebox" to handle the error.

MAME.NET ROM files: https://pan.baidu.com/s/14bR2wEzU2Qqx5hM7hJXMZA


## Authors
- Original author: shunninghuang https://www.codeproject.com/Articles/1275365/MAME-NET 
