# CosmicLearn
[![Compile CosmicLearn (Linux)](https://github.com/Brainstorm4266/CosmicLearn/actions/workflows/compile-linux.yml/badge.svg)](https://github.com/Brainstorm4266/CosmicLearn/actions/workflows/compile-linux.yml)
[![Compile CosmicLearn (Windows)](https://github.com/Brainstorm4266/CosmicLearn/actions/workflows/compile-windows.yml/badge.svg)](https://github.com/Brainstorm4266/CosmicLearn/actions/workflows/compile-windows.yml)

A quick learning program I made in ~3 days, as a kind of Quizlet replacement.

## Setup
I recommend downloading it from the [releases tab](https://github.com/Brainstorm4266/CosmicLearn/releases/), but if you want to, you can also build CosmicLearn by yourself.
To build it yourself, you must have Visual Studio (I used version 2022) with .NET Core (version 6) support, or just use msbuild.

**AUTOBUILD IS CURRENTLY DISABLED DUE TO INCOMPATABILITIES WITH WINDOWS FORMS**

For unstable builds, you can go [here](https://github.com/Brainstorm4266/CosmicLearn/actions/workflows/compile-windows.yml) for Windows builds and [here](https://github.com/Brainstorm4266/CosmicLearn/actions/workflows/compile-linux.yml) for Linux builds. Select the latest, non-failed build. Then download the artifact.

## Configuration
You may configure CosmicLearn by using the `config.json` file in the location where you unpacked CosmicLearn.
This file is generated automatically when you start the program.
Currently, LightDB and MongoDB are supported.
