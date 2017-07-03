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

namespace Tizen.Maps
{
    /// <summary>
    /// Place Category information, used in Place Discovery and Search requests
    /// </summary>
    public class PlaceCategory : IDisposable
    {
        internal Interop.PlaceCategoryHandle handle;

        /// <summary>
        /// Constructs search category object.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when native operation failed to allocate memory.</exception>
        public PlaceCategory()
        {
            handle = new Interop.PlaceCategoryHandle();
        }

        internal PlaceCategory(Interop.PlaceCategoryHandle nativeHandle)
        {
            handle = nativeHandle;
        }


        /// <summary>
        /// Gets or sets an ID for this category.
        /// </summary>
        public string Id
        {
            get { return handle.Id; }
            set { handle.Id = value; }
        }

        /// <summary>
        /// Gets or sets a name for this category.
        /// </summary>
        public string Name
        {
            get { return handle.Name; }
            set { handle.Name = value; }
        }

        /// <summary>
        /// Gets or sets an URL for this category.
        /// </summary>
        public string Url
        {
            get { return handle.Url; }
            set { handle.Url = value; }
        }

        /// <summary>
        /// Returns a string that represents this object.
        /// </summary>
        /// <returns>Returns a string which presents this object.</returns>
        public override string ToString()
        {
            return $"{Name}";
        }

        #region IDisposable Support
        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                handle.Dispose();
                _disposedValue = true;
            }
        }

        /// <summary>
        /// Releases all resources used by this object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
