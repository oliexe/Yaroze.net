# Yaroze.NET
<img src="https://cloud.githubusercontent.com/assets/441290/22619563/325d6db0-eaf7-11e6-803e-6f9022187b8e.jpg" width="640" width="480">

A open-source implementation of deployment tools for PlayStation 1 in .NET and with GUI. 
In a nutshell - tool that can deploy any Playstation 1 Executable to Playstation RAM from modern computer and run it on OG hardware.
Also includes a Playstation side of things based on works of Jihad from Hitmen. Also compatible with PSXSERIAL ISO by Matt from PSXDEV.

Please use the "Issues" tab for **code related** issues only. If you need support please search on [psxdev.net](http://psxdev.net) before posting a question there.

## Created thanks to:
* [Matt](http://www.psxdev.net/forum/memberlist.php?mode=viewprofile&u=211)
* [Jihad from Hitmen](http://www.hitmen-console.org/)
* [Shendo](http://www.psxdev.net/forum/memberlist.php?mode=viewprofile&u=91)

## YAROZE.NET Builds

| Version | Download | SHA1 |
|---------|----------|------|
| Beta 1  | [Link]() | b2e0bdd31566f876d67cba036b5d29aef7ff257d  |


## Hardware requirements
* Playstation 1 console with Serial Port, preferabbly modchipped - able to run burned PSX-Side software from this repository or [Matts PSXSERIAL](http://www.psxdev.net/forum/viewtopic.php?f=69&t=378). 
* PSX Serial Cable -> USB. Multiple ways to make this one by yourself for pretty cheap.
* PC with Windows XP and above to run Yaroze.NET PC-Side software.

## Compiling:
1. `git clone` the repo.
2.  Open the repo in Visual Studio 2015 (The PC Side)
3.  Compile the project.
4.  Run psymake (PsyQ SDK) on a PSX side of the project, the makefile is supplied.

