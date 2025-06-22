# FixHorizon360

**FixHorizon360** is a Unity-based tool to **quickly level 360° equirectangular photos,** in VR or with its desktop UI.

It lets you visually align the horizon and exports the rotation offsets in the expected **Uptale format**.

---

## 🖥️ Desktop Controls

🔄 **Rotation Controls**

- `Q` / `E`  (azerty `A` / `E`) → Rotate 90° **Yaw**
- `1` → Activate **Pitch** control
- `2` → Activate **Yaw** control
- `3` → Activate **Roll** control
- 🖱️ **Scrollwheel** → Rotate image around the selected axis
- 🎚️ Use the **slider** to adjust scroll sensitivity

🖼️ **Image Workflow**

- Place your 360 image in:
    
    `fixHorizon360_Data/Assets/StreamingAssets/360/`
    
- Click the **LoadImage** button in the UI
- Click **SaveToDisk** to export a `.json` with Uptale-compatible rotation:
    - **Vertical Axis (Y)**
    - **Horizontal Axis (X)**
    - **Frontal Axis (Z)**

---

## 🥽 VR Support (OpenXR + PCVR)

**FixHorizon** also supports immersive use in VR with **OpenXR**.

🎮 **VR Controls**

- ✊ **Grip a cube (red/green/blue)** to rotate the image on the corresponding axis (pitch/yaw/roll)
- 🎯 Make sure the **center sphere** is visible to apply rotations as you handle the cubes
    
    → Toggle it with **Right Controller A button**
    
- Left controller `Y` / `X` → Rotate 90° **Yaw**
- 🎯 **Recenter the view** with **Right Stick Press**
    
    *(Avoid recentering while gripping a cube!)*

- ⌗ **Toggle On/Off the floor grid** with **Left Stick Press**