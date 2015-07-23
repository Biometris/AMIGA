﻿using System;
using System.Threading;

namespace Biometris.ProgressReporting {

    /// <summary>
    /// Interface for a progress state that can be used for progress reporting
    /// </summary>
    public interface IProgressState {

        /// <summary>
        /// Holds a string with a description of the current activity
        /// </summary>
        string CurrentActivity { get; set; }

        /// <summary>
        /// Hold a double (which should be between 0 and 100) of the progress percentage
        /// </summary>
        double Progress { get; }

        /// <summary>
        /// Fires when 'CurrentActivity' is changed
        /// </summary>
        event ProgressStateChangedEventHandler CurrentActivityChanged;

        /// <summary>
        /// Fires when 'Progress' is changed
        /// </summary>
        event ProgressStateChangedEventHandler ProgressStateChanged;

    }
}
