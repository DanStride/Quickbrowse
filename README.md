# Quickbrowse

Have you ever needed a quick way to flip through directories upon directories to find and copy all images of George to a single folder?

Well you probably haven't, but I have. This is why I decided to make Quickbrowse; an application which allows the user to flip through photos, save them, and navigate through directories using the keyboard. 

## Features:
- Keyboard navigation with WASD
- Target folder selection
- Stateful application, can remember previous session
- Full screen mode

## Main Window Keyboard Bindings:
| Key      | Action      |
|----------|-------------|
| `W`, `Up` | Previous image in current directory        |
| `S`, `Down` | Next image in current directory       |
| `A`, `Left` | Change laterally to previous sibling folder within the parent directory |
| `D`, `Right` | Change laterally to next sibling folder within the parent directory |
| `Q`, `Period` | Change directory to parent directory |
| `E`, `Forward Slash` | Change directory to the selected child directory |
| `R`, `Numpad 0` | Select a random image from the current directory |
| `Space`, `Enter` | Copy and save the currently selected image to the save directory |
| `X` | Copy and save the current directory and its contents to the save directory |

## Full Screen Window Keyboard Bindings:
| Key      | Action      |
|----------|-------------|
| `W`, `Up` | Previous image in current directory        |
| `S`, `Down` | Next image in current directory       |
| `A`, `Left` | Change laterally to previous sibling folder within the parent directory |
| `D`, `Right` | Change laterally to next sibling folder within the parent directory |
| `R`, `Numpad 0` | Select a random image from the current directory |
| `Space`, `Enter` | Copy and save the currently selected image to the save directory |
| `Escape` | Close the fullscreen window |

