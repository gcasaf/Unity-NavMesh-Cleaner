# NavMesh Cleaner

![Unity Version](https://img.shields.io/badge/Unity-2019.4%2B-blue)
![License](https://img.shields.io/badge/License-MIT-green)

A lightweight editor utility for Unity that removes all baked NavMesh data from your project with a single click.

## Overview

**NavMesh Cleaner** provides a simple yet powerful solution for Unity developers facing issues with corrupted or unwanted navigation mesh data. When NavMesh baking goes wrong or when you need to completely refresh your project's navigation setup, this tool delivers a comprehensive cleanup that manual methods simply can't match.

## Features

- **Complete NavMesh Purge**: Removes all traces of baked NavMesh data from your entire project
- **Selective Cleaning Options**: Choose to clean only scenes, prefabs, or NavMesh data assets
- **User-Friendly Interface**: Simple, intuitive editor window with clear options
- **Detailed Operation Logging**: Track exactly what's being cleaned with a real-time operation log
- **Safety Confirmation**: Includes confirmation dialog to prevent accidental data loss
- **Lightweight Implementation**: Zero dependencies, minimal overhead

## Perfect For

- Resolving corrupted NavMesh data issues
- Starting fresh with a new navigation setup
- Cleaning up before migrating to a different navigation system
- Reducing project size by removing unnecessary NavMesh data
- Fixing NavMesh-related errors that resist conventional troubleshooting

## Installation

### Option 1: Unity Asset Store
1. Purchase and download NavMesh Cleaner from the Unity Asset Store
2. Import the package into your Unity project
3. The tool will automatically be placed in the Editor folder

### Option 2: Manual Installation
1. Download or clone this repository
2. Copy the `NavMeshCleaner.cs` script into your project's `Editor` folder (create one if it doesn't exist)

## How to Use

### Accessing the Tool
1. In Unity, navigate to the top menu and select **Tools > NavMesh Cleaner**
2. The NavMesh Cleaner window will appear in the editor

### Using NavMesh Cleaner
1. **Back Up Your Project** - Always create a project backup before running the tool
2. **Select Cleaning Options**:
   - **Clean Scenes**: Removes NavMesh data from all scenes in your project
   - **Clean Prefabs**: Removes NavMesh components from prefabs
   - **Clean NavMesh Data Assets**: Deletes standalone NavMesh data assets
3. **Run the Cleaner** - Click "Clean NavMesh Data" and confirm the operation
4. **Review Results** - Check the operation log to see what was cleaned
5. **Refresh Your Project** - Unity will automatically refresh after cleaning is complete

### Best Practices
- Run the tool when no scenes are open to avoid conflicts
- For large projects, consider cleaning one category at a time
- After cleaning, consider closing and reopening Unity to ensure all caches are cleared
- Rebake your NavMesh after cleaning if you need to restore navigation functionality

## Technical Details

- **Unity Compatibility**: Works with Unity 2019.4 and higher
- **Implementation**: Pure C# editor extension with no runtime components
- **Dependencies**: No external dependencies required
- **Integration**: Simply drop the script into any Editor folder in your project
- **Performance**: Optimized to handle large projects efficiently with minimal memory usage
- **Code Structure**: Clean, well-commented, maintainable code following Unity best practices
- **Extensibility**: Easily modifiable to accommodate custom NavMesh components
- **Architecture**: Uses Unity's AssetDatabase and EditorUtility APIs for safe asset manipulation
- **Safety Features**: Built-in validation and confirmation to prevent accidental data loss

## Troubleshooting

- If some NavMesh data persists, try running the tool again with all options enabled
- For custom NavMesh components, you may need to modify the script to recognize specific component types
- If you encounter any errors, check the Unity Console for detailed information

### Common Issues

| Issue | Solution |
|-------|----------|
| Tool doesn't appear in menu | Ensure script is in an Editor folder; restart Unity |
| Some NavMesh data remains | Run tool again with all options enabled |
| Error during cleanup | Check Console for details; ensure no scenes are being edited |
| Unable to delete specific assets | Check file permissions; close scene containing those assets |

---

If you find this tool helpful, please consider leaving a review on the Asset Store. Your feedback helps improve the tool for everyone!
