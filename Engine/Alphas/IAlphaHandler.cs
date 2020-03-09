﻿/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/

using System.ComponentModel.Composition;
using QuantConnect.Interfaces;
using QuantConnect.Packets;
using QuantConnect.Securities;

namespace QuantConnect.Lean.Engine.Alpha
{
    /// <summary>
    /// Alpha handler defines how to process insights generated by an algorithm
    /// </summary>
    [InheritedExport(typeof(IAlphaHandler))]
    public interface IAlphaHandler
    {
        /// <summary>
        /// Gets a flag indicating if this handler's thread is still running and processing messages
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Gets the current alpha runtime statistics
        /// </summary>
        AlphaRuntimeStatistics RuntimeStatistics { get; }

        /// <summary>
        /// Initializes this alpha handler to accept insights from the specified algorithm
        /// </summary>
        /// <param name="job">The algorithm job</param>
        /// <param name="algorithm">The algorithm instance</param>
        /// <param name="messagingHandler">Handler used for sending insights</param>
        /// <param name="api">Api instance</param>
        /// <param name="orderEventProvider">Algorithms order event provider</param>
        void Initialize(AlgorithmNodePacket job, IAlgorithm algorithm, IMessagingHandler messagingHandler, IApi api, IOrderEventProvider orderEventProvider);

        /// <summary>
        /// Invoked after the algorithm's Initialize method was called allowing the alpha handler to check
        /// other things, such as sampling period for backtests
        /// </summary>
        /// <param name="algorithm">The algorithm instance</param>
        void OnAfterAlgorithmInitialized(IAlgorithm algorithm);

        /// <summary>
        /// Performs processing in sync with the algorithm's time loop to provide consisten reading of data
        /// </summary>
        void ProcessSynchronousEvents();

        /// <summary>
        /// Stops processing in the <see cref="Run"/> method
        /// </summary>
        void Exit();
    }
}
