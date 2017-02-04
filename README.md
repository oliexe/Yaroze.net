# Yaroze.NET
![Yaroze system](https://cloud.githubusercontent.com/assets/441290/22619563/325d6db0-eaf7-11e6-803e-6f9022187b8e.jpgg)

A modern open-source .NET implementation of deployment tools for PlayStation 1. 
In a nutshell - tool that can deploy any Playstation 1 Executable to Playstation RAM from modern computer and run it on OG hardware.
Also includes a Playstation side of things based on works of Jihad from Hitmen.

## Based on work by:

* [Matt](http://www.psxdev.net/forum/memberlist.php?mode=viewprofile&u=211)
* [Jihad from Hitmen](http://www.hitmen-console.org/)
* [Shendo](http://www.psxdev.net/forum/memberlist.php?mode=viewprofile&u=91)

Please use the "Issues" tab for **code related** issues only. If you need support please search on [psxdev.net](http://psxdev.net) before posting a question there.

## YAROZE.NET Builds

| Version | Download | SHA1 |
|---------|----------|------|
| Beta 1  | [Link]() | b2e0bdd31566f876d67cba036b5d29aef7ff257d  |

## Requirements

* Playstation 1 console with Serial Port, preferabbly modchipped to run burned iso from repository or [Matts PSXSERIAL ISO](http://www.psxdev.net/forum/viewtopic.php?f=69&t=378). 
* PSX Serial Cable -> USB. Multiple ways to make this one by yourself for cheap..
* PC with Windows and .NET 2.0 Framework to run Yaroze.NET software.

## Compiling:

1. `git clone` the repo.
2.  Open the repo in Visual Studio 2015 (The PC Side)
3.  Compile the project.
4.  Run psymake (PsyQ SDK) on a PSX side of the project, the makefile is suppiled.

