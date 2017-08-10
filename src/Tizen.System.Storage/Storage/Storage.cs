﻿/*
* Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
*
* Licensed under the Apache License, Version 2.0 (the License);
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an AS IS BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;

namespace Tizen.System
{
    /// <summary>
    /// The class to access the storage device information.
    /// </summary>
    public class Storage
    {
        private const string LogTag = "Tizen.System";
        private Interop.Storage.StorageState _state;
        private ulong _totalSpace;

        internal Storage(int storageID, Interop.Storage.StorageArea storageType, Interop.Storage.StorageState storagestate, string rootDirectory)
        {
            Id = storageID;
            StorageType = (StorageArea)storageType;
            RootDirectory = rootDirectory;
            _state = storagestate;

            Interop.Storage.ErrorCode err = Interop.Storage.StorageGetTotalSpace(Id, out _totalSpace);
            if (err != Interop.Storage.ErrorCode.None)
            {
                Log.Warn(LogTag, string.Format("Failed to get total storage space for storage Id: {0}. err = {1}", Id, err));
            }

            s_stateChangedEventCallback = (id, state, userData) =>
            {
                if (id == Id)
                {
                    _state = state;
                    s_stateChangedEventHandler?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        private EventHandler s_stateChangedEventHandler;
        private Interop.Storage.StorageStateChangedCallback s_stateChangedEventCallback;

        private void RegisterStateChangedEvent()
        {
            Interop.Storage.ErrorCode err = Interop.Storage.StorageSetStateChanged(Id, s_stateChangedEventCallback, IntPtr.Zero);
            if (err != Interop.Storage.ErrorCode.None)
            {
                Log.Warn(LogTag, string.Format("Failed to Register state changed event callback for storage Id: {0}. err = {1}", Id, err));
            }
        }

        private void UnregisterStateChangedEvent()
        {
            Interop.Storage.ErrorCode err = Interop.Storage.StorageUnsetStateChanged(Id, s_stateChangedEventCallback);
            if (err != Interop.Storage.ErrorCode.None)
            {
                Log.Warn(LogTag, string.Format("Failed to Register state changed event callback for storage Id: {0}. err = {1}", Id, err));
            }
        }

        /// <summary>
        /// StorageStateChanged event. This event is occurred when a storage state changes.
        /// </summary>
        /// <remarks>
        /// The storage state will be updated before calling the event handler.
        /// </remarks>
        /// <since_tizen> 3 </since_tizen>
        /// <example>
        /// <code>
        /// myStorage.StorageStateChanged += (s, e) =>
        /// {
        ///     var storage = s as Storage;
        ///     Console.WriteLine(string.Format("State Changed to {0}", storage.State));
        /// }
        /// </code>
        /// </example>
        public event EventHandler StorageStateChanged
        {
            add
            {
                if (s_stateChangedEventHandler == null)
                {
                    _state = (Interop.Storage.StorageState)State;
                    RegisterStateChangedEvent();
                }
                s_stateChangedEventHandler += value;
            }
            remove
            {
                if (s_stateChangedEventHandler != null)
                {
                    s_stateChangedEventHandler -= value;
                    if (s_stateChangedEventHandler == null)
                    {
                        UnregisterStateChangedEvent();
                    }
                }
            }
        }

        /// <summary>
        /// The storage ID.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public int Id { get; }
        /// <summary>
        /// The type of storage.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public StorageArea StorageType { get; }
        /// <summary>
        /// The root directory for the storage.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public string RootDirectory { get; }
        /// <summary>
        /// The total storage size in bytes.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public ulong TotalSpace { get { return _totalSpace; } }

        /// <summary>
        /// The StorageState.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public StorageState State
        {
            get
            {
                if (s_stateChangedEventHandler == null)
                {
                    Interop.Storage.ErrorCode err = Interop.Storage.StorageGetState(Id, out _state);
                    if (err != Interop.Storage.ErrorCode.None)
                    {
                        Log.Warn(LogTag, string.Format("Failed to get storage state for storage Id: {0}. err = {1}", Id, err));
                    }
                }
                return (StorageState)_state;
            }
        }

        /// <summary>
        /// The available storage size in bytes.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public ulong AvaliableSpace
        {
            get
            {
                ulong available;
                Interop.Storage.ErrorCode err = Interop.Storage.StorageGetAvailableSpace(Id, out available);
                if (err != Interop.Storage.ErrorCode.None)
                {
                    Log.Warn(LogTag, string.Format("Failed to get available storage stace for storage Id: {0}. err = {1}", Id, err));
                }

                return available;
            }
        }

        /// <summary>
        /// Absolute path for a given directory type in the storage.
        /// </summary>
        /// <remarks>
        /// The returned directory path may not exist, so you must make sure that it exists before using it.
        /// For accessing internal storage except the ringtones directory, the application should have http://tizen.org/privilege/mediastorage privilege.
        /// For accessing ringtones directory, the application should have http://tizen.org/privilege/systemsettings privilege.
        /// For accessing external storage, the application should have http://tizen.org/privilege/externalstorage privilege.
        /// </remarks>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="dirType">Directory type.</param>
        /// <returns>Absolute path for a given directory type in the storage.</returns>
        /// <exception cref="ArgumentException">Thrown when failed because of an invalid argument.</exception>
        /// <exception cref="OutOfMemoryException">Thrown when failed due to out of memory exception.</exception>
        /// <exception cref="NotSupportedException">Thrown when failed if the storage is not supported or the application does not have the permission to access the directory path.</exception>
        /// <privilege>http://tizen.org/privilege/mediastorage</privilege>
        /// <privilege>http://tizen.org/privilege/systemsettings</privilege>
        /// <privilege>http://tizen.org/privilege/externalstorage</privilege>
        /// <example>
        /// <code>
        /// // To get the video directories for all the supported storage,
        /// var storageList = StorageManager.Storages as List&lt;Storage&gt;;
        /// foreach (var storage in storageList)
        /// {
        ///     string pathForVideoDir = storage.GetAbsolutePath(DirectoryType.Videos);
        /// }
        /// </code>
        /// </example>
        public string GetAbsolutePath(DirectoryType dirType)
        {
            string path;
            Interop.Storage.ErrorCode err = Interop.Storage.StorageGetAbsoluteDirectory(Id, (Interop.Storage.DirectoryType)dirType, out path);
            if (err != Interop.Storage.ErrorCode.None)
            {
                Log.Warn(LogTag, string.Format("Failed to get package Id. err = {0}", err));
                switch (err)
                {
                    case Interop.Storage.ErrorCode.InvalidParameter:
                        throw new ArgumentException("Invalid Arguments");
                    case Interop.Storage.ErrorCode.OutOfMemory:
                        throw new OutOfMemoryException("Out of Memory");
                    case Interop.Storage.ErrorCode.NotSupported:
                        throw new NotSupportedException("Operation Not Supported");
                    default:
                        throw new InvalidOperationException("Error = " + err);
                }
            }
            return path;
        }
    }
}
