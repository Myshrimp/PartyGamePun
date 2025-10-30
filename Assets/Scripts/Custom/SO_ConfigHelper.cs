using System;
using System.Collections.Generic;
using GameFramework.Config;
using ScriptableObjects;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Party.Custom
{
    public class SO_ConfigHelper : DefaultConfigHelper
    {
        [Serializable]
        public struct ConfigData
        {
            public string key;
            public ConfigBase value;
        }
        [SerializeField] private List<ConfigData> configs;

        private Dictionary<string, ConfigBase> _config_map;

        public override bool ParseData(IConfigManager configManager, string configString, object userData)
        {
            if (_config_map == null)
            {
                _config_map = new Dictionary<string, ConfigBase>();
                foreach (var v in configs)
                {
                    _config_map[v.key] = v.value;
                }
            }
            if (!_config_map.ContainsKey(configString)) return false;
            ConfigBase cfg = _config_map[configString];
            foreach (var val in cfg.string_properties)
            {
                configManager.AddConfig(val.key, val.value);
            }

            return true;
        }
    }
}