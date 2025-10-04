# What
This is a C# library aimed at giving .NET based game developers the tools to read and integrate Valve BSP map files into their games.

# Why
This project exists to allow map makers and game developers who are in the Source ecosystem a way to decouple their work from the Source engine and use other, more current engines and tools. That is the primary motivation of writing this in C# and .NET. This gives developers
a much larger set of game engines to utilize, such as [Godot](https://godotengine.org/), [Stride](https://www.stride3d.net/), and [Unity](https://unity.com/).

# How
This library provides a very modular framework for reading and interpreting Source engine maps. It observes the Open/Close principle very closely. This means that even if you're using a dynamic version of the library (such as a NuGet package), you can update and modify your implementation
to better suit your needs. This matters a lot as Source engine games tend to have a *lot* of variation between games. With so many different file standards out there for Source engine maps, it stood to reason that a relevant solution for reading and
interpreting Source engine maps would require a broad and modular approach.
