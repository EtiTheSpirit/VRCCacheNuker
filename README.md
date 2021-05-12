# VRCCacheNuker
An incredibly simple utility mod for VRChat that schedules the cache for deletion after every play session, granted the mod is installed.

## Credits & Remarks
First off, this was created with the help of:
- [Eti (Xan)](https://github.com/EtiTheSpirit) for initial concept and design.
- [Davi](https://github.com/d-mageek) for tracking down how VRC does this on its own.

For the time being, a deletion must be performed by the mod code rather than using VRC's cache clearing system. Due to unknown reasons, an inconsistency has arisen that causes VRC's native method of scheduling cache deletion to completely fail. The issue is not stably reproducible (the method worked a few times at first for Davi then completely failed after a few tries despite no changes being made, and never worked for me at all).

## Usage Warning
Modding VRChat is a direct violation of VRC's Terms of Service. Usage of mods can lead to complete account termination. Use discretion when choosing mods, and understand the risks.

## Why use this? What are the drawbacks? Should I use it?
One of the common solutions to random lagspikes in VRChat is to clear your cache. While certain other mods aim to increase the cache size to reduce the need to download content, this mod aims to do the polar opposite.

**Generally speaking, you should only use VRCCacheNuker if:**
- You wish to reduce random lagspikes from cache indexing.
- You wish to partly reduce memory usage (this may not have a significant impact, I have yet to formally research it).
- You wish to regularly clean your cache without the need to remember to do so.<br/>
**...But...**<br/>
- You are able to deal with heightened network usage, as after every session you must redownload all content you encounter (for lack of a cache). This will not affect ongoing sessions, only new sessions.
- You are able to deal with increased disk activity after play sessions.

**Generally speaking, you will only notice benefits if:**
- Your computer has a low amount of RAM and/or a slow hard disk.
- Your computer has a slower CPU or older RAM.

Benefits to other groups (fast CPUs, M.2 cards/SSDs, etc.) may exist too, however the magnitude of improvement (or degredation) is unknown at this time. I use an i9-10900K and have VRC's cache on an SSD, and I've noticed benefits.
