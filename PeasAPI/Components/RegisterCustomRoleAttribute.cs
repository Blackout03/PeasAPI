﻿using System;
using System.Reflection;
using BepInEx.IL2CPP;
using HarmonyLib;
using PeasAPI.Roles;

namespace PeasAPI.Components
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterCustomRoleAttribute : Attribute
    {
        public static void Register(BasePlugin plugin)
        {
            Register(Assembly.GetCallingAssembly(), plugin);
        }

        public static void Register(Assembly assembly, BasePlugin plugin)
        {
            foreach (var type in assembly.GetTypes())
            {
                var attribute = type.GetCustomAttribute<RegisterCustomRoleAttribute>(); 

                if (attribute != null)
                {
                    if (!type.IsSubclassOf(typeof(BaseRole)))
                    {
                        throw new InvalidOperationException($"Type {type.FullDescription()} must extend {nameof(BaseRole)}.");
                    }
                    
                    Activator.CreateInstance(type, plugin);
                }
            }
        }
    }
}