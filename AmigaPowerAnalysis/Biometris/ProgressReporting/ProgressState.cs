﻿using System;

namespace Biometris.ProgressReporting {

    public delegate void ProgressStateChangedEventHandler(object sender, EventArgs e);

    public sealed class ProgressState : IProgressState {

        private double _progress = 0;
        private string _currentActivity = string.Empty;

        /// <summary>
        /// Fires when the progress state value has changed
        /// </summary>
        public event ProgressStateChangedEventHandler ProgressStateChanged;

        /// <summary>
        /// Fires when the current activity message has changed
        /// </summary>
        public event ProgressStateChangedEventHandler CurrentActivityChanged;

        /// <summary>
        /// Initializes the composite progress state without a cancellation token.
        /// </summary>
        public ProgressState() {
        }

        /// <summary>
        /// Current activity message
        /// </summary>
        public string CurrentActivity {
            get { return _currentActivity; }
            set { 
                _currentActivity = value;
                onCurrentActivityChanged();
            }
        }

        /// <summary>
        /// The current progress
        /// </summary>
        public double Progress {
            get { return _progress; }
            set { 
                _progress = value <= 100 ? value : 100;
                onProgressStateChanged();
            }
        }

        /// <summary>
        /// Updates the progress
        /// </summary>
        /// <param name="progress"></param>
        public void Update(double progress) {
            Progress = progress;
        }

        /// <summary>
        /// Updates the progress and activity message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="progress"></param>
        public void Update(string message, double progress) {
            CurrentActivity = message;
            Progress = progress;
        }

        /// <summary>
        /// Updates the activity message
        /// </summary>
        /// <param name="message"></param>
        public void Update(string message) {
            CurrentActivity = message;
        }

        /// <summary>
        /// Increment progress with the specified increment amount
        /// </summary>
        /// <param name="incrementAmount"></param>
        public void Increment(double incrementAmount) {
            Progress += incrementAmount;
        }

        /// <summary>
        /// Increment progress with the specified increment amount and update
        /// the activity message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="incrementAmount"></param>
        public void Increment(string message, double incrementAmount) {
            CurrentActivity = message;
            Progress += incrementAmount;
        }

        /// <summary>
        /// Action that is to be performed when the progress state has changed.
        /// Fires a progress state changed event.
        /// </summary>
        private void onProgressStateChanged() {
            if (ProgressStateChanged != null) {
                ProgressStateChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Action that is to be performed when the current activity has changed.
        /// Fires a progress state changed event.
        /// </summary>
        private void onCurrentActivityChanged() {
            if (CurrentActivityChanged != null) {
                CurrentActivityChanged(this, new EventArgs());
            }
        }
    }
}
