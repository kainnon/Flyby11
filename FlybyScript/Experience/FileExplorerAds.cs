﻿using FlybyScript;
using Microsoft.Win32;
using System;
using System.Drawing;

namespace Settings.Ads
{
    internal class FileExplorerAds : FeatureBase
    {
        public FileExplorerAds(Logger logger) : base(logger)
        {
        }

        private const string keyName = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
        private const string valueName = "ShowSyncProviderNotifications";
        private const int desiredValue = 0;

        public override string GetRegistryKey()
        {
            return $"{keyName} | Value: {valueName} | Desired Value: {desiredValue}";
        }

        public override string ID()
        {
            return "Disable File Explorer Ads";
        }

        public override string Info()
        {
            return "This feature will disable ads in File Explorer.";
        }

        public override bool CheckFeature()
        {
            return (Utils.IntEquals(keyName, valueName, desiredValue));
        }

        public override bool DoFeature()
        {
            try
            {
                Registry.SetValue(keyName, valueName, 0, RegistryValueKind.DWord);
                return true;
            }
            catch (Exception ex)
            {
                logger.Log("Code red in " + ex.Message, Color.Red);
            }

            return false;
        }

        public override bool UndoFeature()
        {
            try
            {
                Registry.SetValue(keyName, valueName, 1, RegistryValueKind.DWord);
                return true;
            }
            catch (Exception ex)
            {
                logger.Log("Code red in " + ex.Message, Color.Red);
            }

            return false;
        }
    }
}