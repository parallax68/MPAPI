# MPAPI
Message Passing API (MPAPI)

This repo is a fork from https://mpapi.codeplex.com/
is also contains https://remotinglite.codeplex.com/

These projects were in high activity in 2008, I forked the source code to be able to change it for my own parallel processing research with C# and mono in the objective to build a raspberry pi cluster.

After some modifications and little bug corrections I'm able now to run the code on my cluster running mono.

Roadmap
-------
- refactor the project to separate cluster specific from the problem resolution, in our example the prime numbers computation
- make it generic and simple to extend
- provide the code as a nuget package
