# Not another platformer, please

![Gameplay Demo](./Repo/gameplay.gif)

## Overview

This repository contains a basic 2D run and gun platformer project developed in **Unity**. The game features exciting gameplay mechanics and a *strong code architecture* that can serve as a foundation for any single-player game development. If you're looking to start a new single-player game project, this base provides a solid starting point with all the essential components in place.

## Game Features

- Smooth and responsive 2D player movement and shooting mechanics.
- Intuitive on-screen controls for an enjoyable gaming experience.
- Keyboard and gamepad dynamic input support.
- Dynamic and interactive 2D level design for challenging gameplay (some day).
- Enemies with advanced AI patterns for engaging combat scenarios (hopefully).
- Modular code architecture designed for easy expansion and customization.

## How to Use this Base

You can take this repository and expand it to create your own single-player game, or you can use the asset files in another project. The following sections explain how to do both.

### Expand this repo

1. Clone or download the repository to your local machine.
2. Open the project in Unity (I am using Unity version 2022.3.1f1).
3. Explore the existing code, assets, and scenes to understand the structure.
4. Customize the gameplay elements, art, and sound to fit your game concept.
5. Use this base as a starting point to build your own single-player game.

### Use asset files in another project

1. Go to the [Releases](https://github.com/CesarJZO/Not-Another-Platformer-Please/releases) page and download the latest `unitypackage`.
2. Import the package into your project. It can either be a new project or an existing one, but a new project is recommended since this base is designed to be used as a starting point.

## Project Structure

```bash
|-- Assets
|   |-- Game # Where game assets are stored
|   |   |-- Physics Materials
|   |   |-- Prefabs
|   |   |-- Settings # Settings scriptable objects
|   |   |-- ...
|   |-- Plugins # Where third-party plugins are stored
|   |   |-- DOTween
|   |   |-- ...
|   |-- Resources # Where general-purpose assets are stored
|   |   |-- Fonts
|   |   |-- Sprites
|   |   |   |-- pixel.png # 1×1 white pixel
|   |   |-- ...
|   |-- Scenes # Where game scenes are stored
|   |-- Scripts # Where all game scripts are stored
|   |   |-- Core
|   |   |-- Entities
|   |   |-- Input
|   |   |-- Management
|   |   |-- UI
|   |   |-- Units
|   |   |-- Utilities
|   |   |-- ...
|   |-- Settings # Where project settings are stored
|   |-- Volumes # Post-processing volumes
|-- Docs
|   |-- Design_Document.md
|   |-- User_Manual.md
|-- readme.md
```

## Documentation

Detailed documentation for the base game could be found in the [Docs](./Docs) directory *when exists*. It *will* include:

- **Design Document:** *Will* provide an overview of the game design and mechanics.
- **User Manual:** *Will* explain how to use and customize the base game for your projects.

## Contributing

Contributions to the project are *not* welcome (yet)! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## License

This project is not licensed under the [MIT License](./LICENSE) (yet).
