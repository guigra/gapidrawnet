GapiDraw.net

1	About
Gapidraw.net is created by:
Sean Cross
Intuitex Software
www.intuitex.com
gapi@intuitex.com

Gapidraw.net is a .net wrapper around Gapidraw. It requires Visual Studio 2003.  

Gapidraw is a freeware graphic library for the pocketpc and is used for many games.  
It can be found at www.gapidraw.com

THE GAPIDRAW LIBRARIES ARE REQUIRED FOR GAPIDRAW.NET.  WITHOUT THEM, GAPIDRAW.NET WILL NOT RUN!

2	Installing/Compiling
Unzip gapidraw.net to the folder of your choice.  
Download and install gapidraw 2.04.

Open the Gapidrawnet.sln file with visual studio and rebuild all.
Copy the appropriate dll files from gapidraw\dll to the bin\debug or bin\release folders for 
the samples.

Copy any image files from \common to the bin\debug or bin\release folders for 
the samples.

Samples should now run.

3	Documentation
This is it!  Gapidraw.net almost exactly a 1-1 wrapper around gapidraw, so all the gapidraw 
documentation is applicable.

4	Samples
Samples suffixed ppc are intended to be installed and run on the PocketPc.  They can also be run 
under windows with the windows gdxxx.dll.  the main reason for having separate windows apps is to
make debugging easier.

Samples suffixed 2003 are windows applications.

Compiled samples with the appropriate dlls are in the /demos/ directory.  They can be copied to the 
pocketpc (arm)  and run.

5	Tests
The file TestApp can be openned and run with NUnit.  It contains a suite of unit tests verifying
functionality.