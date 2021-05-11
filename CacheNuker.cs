using MelonLoader;
using System;
using VRC.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
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

		/// <summary>
		/// The name of the downloaded content cache folder, which will be deleted after the play session concludes.
		/// </summary>
		private const string CACHE_FOLDER_NAME = "Cache-WindowsPlayer"; // TODO: Platform-agnostic design?

		/// <summary>
		/// The main cache folder, for instance, AppData/LocalLow/VRChat/VRChat
		/// </summary>
		private static DirectoryInfo CacheRoot = null;

		static CacheNuker() {
			MelonPreferences.CreateCategory(KEY_CN_CAT, "Cache Nuker");
			MelonPreferences.CreateEntry(KEY_CN_CAT, KEY_CLEAR_DL_CACHE, true, "Clear Content Cache");
		}

		/// <summary>
		/// Notifies the user that the cache is being deleted after this session.
		/// </summary>
		public override void OnApplicationStart() {
			LocalConfig cfg = new LocalConfig(Path.Combine(Application.persistentDataPath, "config.json"));
			if (cfg.HasKey("cache_directory")) {
				CacheRoot = new DirectoryInfo(cfg.GetString("cache_directory"));
			} else {
				CacheRoot = new DirectoryInfo(Application.persistentDataPath);
			}
			MelonLogger.Msg($"Cache directory has been located: {CacheRoot}");
		}

		/// <summary>
		/// Post a message to the console to let people know that it's gonna look like VRC crashed.
		/// </summary>
		public override void OnApplicationQuit() {
			if (!MelonPreferences.GetEntryValue<bool>(KEY_CN_CAT, KEY_CLEAR_DL_CACHE)) {
				MelonLogger.Msg("Not doing anything to Downloaded Content Cache. User has disabled the system.");
				return;
			}

			MelonLogger.Msg("Clearing Downloaded Content Cache...");
			DirectoryInfo cache = GetDirectory(CacheRoot, CACHE_FOLDER_NAME);

			if (cache.Exists) {
				ScheduleLateDeletion(cache);
				MelonLogger.Msg(ConsoleColor.Green, "[Success] Deleted Downloaded Content Cache.");
			} else {
				MelonLogger.Error("[Fail] Could not find Downloaded Content Cache folder. No action has been taken.");
			}
		}

		/// <summary>
		/// Schedules the deletion of the given directory after the VRChat application has exited.
		/// </summary>
		/// <param name="dir"></param>
		private static void ScheduleLateDeletion(DirectoryInfo dir) {
			// -WindowStyle Hidden			-- Used to hide the console window (it may flicker on screen for a split second but oh well)
			// Wait-Process -Name "VRChat"	-- Yields until VRChat closes
			// Remove-Item (dir) -Recurse	-- Deletes the given directory and all subdirectories / subfiles.

			// I don't particularly like this methodology and I would much rather prefer using VRC's native API to schedule the deletion of the cache.
			// TODO: Find out how VRC schedules deletion of the cache. Do not use a PS command to do it.

			Process.Start("powershell", $"-WindowStyle Hidden Wait-Process -Name \"VRChat\"; Remove-Item \"{dir.FullName}\" -Recurse");
		}

		/// <summary>
		/// Appends <paramref name="subDirName"/> onto the end of the path stored in <paramref name="dir"/> to create a new <see cref="DirectoryInfo"/> pointing at the given directory within <paramref name="dir"/>.<para/>
		/// Contrary to <see cref="Directory.CreateDirectory(string)"/>, this will not create a new directory on the filesystem if it doesn't exist.
		/// </summary>
		/// <param name="dir">The parent directory.</param>
		/// <param name="subDirName">The name of the desired subdirectory.</param>
		/// <returns></returns>
		private static DirectoryInfo GetDirectory(DirectoryInfo dir, string subDirName) {
			return new DirectoryInfo(Path.Combine(dir.FullName, subDirName));
		}

	}
}
