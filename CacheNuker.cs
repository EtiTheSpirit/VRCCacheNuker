using MelonLoader;
using System;
using VRC.Core;
using System.IO;
using System.Diagnostics;
using UnityEngine;

namespace VRCCacheNuker {
	public class CacheNuker : MelonMod {

		/// <summary>
		/// The key to the category associated with CacheNuker in ML config.
		/// </summary>
		private const string KEY_CN_CAT = "vrcCacheNuker";

		/// <summary>
		/// The key associated with enabling or disabling CacheNuker's core functionality in ML config.
		/// </summary>
		private const string KEY_CLEAR_DL_CACHE = "cacheNukerClearDLC";

		static CacheNuker() {
			MelonPreferences.CreateCategory(KEY_CN_CAT, "Cache Nuker");
			MelonPreferences.CreateEntry(KEY_CN_CAT, KEY_CLEAR_DL_CACHE, true, "Clear Content Cache On Next Startup");
		}

		/// <summary>
		/// Grabs the cache folder on startup and stores it in <see cref="CacheRoot"/>
		/// </summary>
		public override void VRChat_OnUiManagerInit() {
			if (!MelonPreferences.GetEntryValue<bool>(KEY_CN_CAT, KEY_CLEAR_DL_CACHE)) {
				MelonLogger.Msg("Not doing anything to Downloaded Content Cache. User has disabled the system.");
				return;
			}
			DirectoryInfo cache = new DirectoryInfo(AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0.field_Private_Cache_0.path);
			MelonLogger.Msg($"Cache directory has been located at {cache} -- Attempting to delete now.");
			if (cache.Exists) {
				DeleteDirectory(cache);
				MelonLogger.Msg(ConsoleColor.Green, "[Success] Deleted Downloaded Content Cache.");
			} else {
				MelonLogger.Error("[Fail] Could not find Downloaded Content Cache folder. No action has been taken.");
			}
		}

		/// <summary>
		/// Recursively deletes the given directory.
		/// </summary>
		/// <param name="dir"></param>
		private static void DeleteDirectory(DirectoryInfo dir) {
			foreach (DirectoryInfo subDir in dir.GetDirectories()) {
				DeleteDirectory(subDir);
			}
			foreach (FileInfo file in dir.GetFiles()) {
				file.Delete();
			}
			dir.Delete();
		}

	}
}
