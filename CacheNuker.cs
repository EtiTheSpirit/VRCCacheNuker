using MelonLoader;
using System;

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
			MelonPreferences.CreateEntry(KEY_CN_CAT, KEY_CLEAR_DL_CACHE, true, "Clear Content Cache");
		}

        /// <summary>
        /// Post a message to the console to let people know what's going on.
        /// </summary>
        public override void OnApplicationQuit() {
			if (!MelonPreferences.GetEntryValue<bool>(KEY_CN_CAT, KEY_CLEAR_DL_CACHE)) {
				MelonLogger.Msg("Not doing anything to Downloaded Content Cache. User has disabled the system.");
				return;
			}

			MelonLogger.Msg("Clearing Downloaded Content Cache...");
			try {
				AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0.field_Private_Cache_0.ClearCache();
				MelonLogger.Msg(ConsoleColor.Green, "[Success] Deleted Downloaded Content Cache.");
			} catch {
				MelonLogger.Error("[Fail] Could not use the default method to clear the cache. No action has been taken.");
			}
		}
	}
}
