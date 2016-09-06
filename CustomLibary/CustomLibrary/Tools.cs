using UnityEngine;
using System.Windows.Forms;

namespace CustomLibrary {

    public class Tools {

        private static readonly string currentVersion = "0.15";

        public static void ShowMessage(string message) {
            MessageBox.Show(message, "Custom Library v"+currentVersion, MessageBoxButtons.OK);
        }

        public static void ShowMessage(string message, string caption) {
            MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }
    }
}