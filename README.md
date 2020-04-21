# NeoFPS_CompassNavPro
NeoFPS and Compass Navigator Pro integration assets

## Requirements
This repository was created using Unity 2018.1

It requires the assets [NeoFPS](https://assetstore.unity.com/packages/templates/systems/neofps-150179?aid=1011l58Ft) and [Compass Navigator Pro](https://assetstore.unity.com/packages/tools/gui/compass-navigator-pro-59519?aid=1011l58Ft).

## Installation
This integration example is intended to be dropped in to a fresh project along with NeoFPS and Compass Navigator Pro.

1. Import NeoFPS and apply the required Unity settings using the NeoFPS Settings Wizard. You can find more information about this process [here](https://docs.neofps.com/manual/neofps-installation.html).

2. Import the Compass Navigator Pro asset.

3. Clone this repository to a folder inside the project Assets folder such as "NeoFPS_CompassNavPro"
	
## Integration
The following are the important assets in this repo that enable NeoFPS and Compass Nav Pro to work side by side.

#### MonoBehaviours
The following scripts are available in the **Scripts** folder:
- **NeoFpsCompassNavPro_CharacterWatcher.cs** attaches the player character to the minimap and compass on spawn.
- **NeoFpsCompassNavPro_CompassFormatter.cs** allows the Compass Navigator Pro compass to be saved with the NeoFPS save system (for example, saving the fog of war coverage).
- **NeoFpsCompassNavPro_DiscoverArea.cs** is a simple trigger zone which deactivates a fog of war area once the player enters it.
- **NeoFpsCompassNavPro_POIFormatter.cs** enables the NeoFPS save system to save POI details such as their visited state.
- **NeoFpsCompassNavPro_WaypointsDemo.cs** is purely for demonstration purposes and spawns POIs in a sequence.
- **NeoFpsCompassNavPro_WaypointsDemoLookAt.cs** is purely for demonstration purposes and aligns POI markers with the player camera.

#### Demo Scene
The demo scene shows the use of POIs and fog of war zones, and also has a player UI HUD that includes the Compass Navigator Pro elements.

#### Prefabs
The following prefabs are available in the **Prefabs** folder:
- The **NeoFpsCompassNavPro_Canvas** prefab contains the Compass Navigato Pro HUD elements fitted and stlyed to the NeoFPS HUD
- The **NeoFpsCompassNavPro_InfoLaptop** prefab is a variant of the NeoFPS demo laptops that includes an info POI that is deactivated on use.
- The **NeoFpsCompassNavPro_Waypoint** prefab is a simple waypoint POI that is set up for the NeoFPS save system.

## Future Work
Keep an eye on this repo for updates and fixes.

Enjoy!